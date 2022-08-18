using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace FireBalls3D.Model
{
    public static class AnimatedAction
    {
        public async static Task StartAsync(float duration, float deltaTimeInSeconds, Action<float> action, CancellationToken token = default)
        {
            float accumulatedTime = 0;
            float progress = 0;

            while (progress < 1)
            {
                if (token.IsCancellationRequested == true)
                    break;

                accumulatedTime += deltaTimeInSeconds;
                progress = accumulatedTime / duration;
                action?.Invoke(progress);
                await Task.Delay((int)(deltaTimeInSeconds * 1000));
            }
        }
    }
}
