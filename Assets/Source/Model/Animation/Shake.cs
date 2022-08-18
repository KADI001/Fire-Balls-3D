using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace FireBalls3D.Model
{
    public class Shake : TransformAnimation
    {

        public Shake() : base()
        {
        }

        public Shake(AnimationPriority priority) : base(priority)
        {
        }

        protected async override Task TransformateAsync(IAnimatableTransform animatable, CancellationToken token)
        {
            Transformable transform = animatable.Transformable;
            Vector3 startPosition = transform.Position;
            Vector3 offsetX = startPosition + transform.Right * Config.ShakeDistance;
            Vector3 offsetNegX = startPosition - transform.Right * Config.ShakeDistance;
            float stepDuration = Config.ShakeDuration * 0.25f;
            float deltaTime = 0.02f;

            Action<float>[] steps = new Action<float>[4];
            steps[0] = progress => transform.SetPosition(Vector3.Lerp(startPosition, offsetX, progress));
            steps[1] = progress => transform.SetPosition(Vector3.Lerp(offsetX, startPosition, progress));
            steps[2] = progress => transform.SetPosition(Vector3.Lerp(startPosition, offsetNegX, progress));
            steps[3] = progress => transform.SetPosition(Vector3.Lerp(offsetNegX, startPosition, progress));

            await AnimatedAction.StartAsync(stepDuration, deltaTime, steps[0], token);
            await AnimatedAction.StartAsync(stepDuration, deltaTime, steps[1], token);
            await AnimatedAction.StartAsync(stepDuration, deltaTime, steps[2], token);
            await AnimatedAction.StartAsync(stepDuration, deltaTime, steps[3], token);
        }
    }
}
