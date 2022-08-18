using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FireBalls3D.Model
{
    public abstract class TransformAnimation : Animation
    {
        protected CancellationTokenSource Source;

        public TransformAnimation() : base()
        {
        }

        public TransformAnimation(AnimationPriority priority) : base(priority)
        {
        }

        protected override void Break()
        {
            Source.Cancel();
            Animatable.Transformable.SetPosition(StartContext.Transformable.Position);
            Animatable.Transformable.SetRotation(StartContext.Transformable.Rotation);
        }

        protected async override void Start(IAnimatableTransform animatable)
        {
            Source = new CancellationTokenSource();
            CancellationToken token = Source.Token;

            await TransformateAsync(animatable, token);

            if (token.IsCancellationRequested == true)
                return;

            IsCompleted = true;

            Stop();
        }

        protected abstract Task TransformateAsync(IAnimatableTransform animatable, CancellationToken token);
    }
}
