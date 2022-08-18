using System;
using System.Threading.Tasks;
using UnityEngine;
using System.Threading;

namespace FireBalls3D.Model
{
    public static class TransformableExtension
    {
        public async static void YoyoMoveToAsync(this Transformable transformable, Vector3 offset, float deltaTimeInSeconds, float duration, CancellationToken token)
        {
            Vector3 startPosition = transformable.Position;
            Vector3 endPosition = transformable.Position + offset;
            float stepDuration = duration * 0.5f;

            Action<float>[] steps = new Action<float>[2]; 
            steps[0] = progress => transformable.SetPosition(Vector3.Lerp(startPosition, endPosition, progress));
            steps[1] = progress => transformable.SetPosition(Vector3.Lerp(endPosition, startPosition, progress));

            await AnimatedAction.StartAsync(stepDuration, deltaTimeInSeconds, steps[0], token);
            await AnimatedAction.StartAsync(stepDuration, deltaTimeInSeconds, steps[1], token);
        }
    }
}
