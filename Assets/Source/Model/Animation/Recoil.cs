using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace FireBalls3D.Model
{
    public class Recoil : TransformAnimation
    {
        public Recoil() : base()
        {
        }

        public Recoil(AnimationPriority priority) : base(priority)
        {
        }

        protected async override Task TransformateAsync(IAnimatableTransform animatable, CancellationToken token)
        {
            Transformable transform = animatable.Transformable;
            Vector3 startPosition = transform.Position;
            Vector3 offset = animatable.Transformable.Forward * Config.RecoilDistance;
            Vector3 endPosition = transform.Position - offset;
            float deltaTimeInSeconds = 0.02f;
            float stepDuration = Config.RecoilDuration * 0.25f;

            Action<float>[] steps = new Action<float>[2];
            steps[0] = progress => transform.SetPosition(Vector3.Lerp(startPosition, endPosition, progress));
            steps[1] = progress => transform.SetPosition(Vector3.Lerp(endPosition, startPosition, progress));

            await AnimatedAction.StartAsync(stepDuration, deltaTimeInSeconds, steps[0], token);
            await AnimatedAction.StartAsync(stepDuration, deltaTimeInSeconds, steps[1], token);
        }
    }
}
