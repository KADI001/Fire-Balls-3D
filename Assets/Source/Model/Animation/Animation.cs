using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FireBalls3D.Model
{
    public abstract class Animation
    {
        private AnimationPriority _priority;
        protected bool IsCompleted;
        private Action _onCompleted;
        private Action _onInterrupted;
        private Action _onStopped;
        protected IAnimatableTransform Animatable { get; private set; }
        protected IAnimatableTransform StartContext { get; private set; }

        public event Action Completed;
        public event Action Interrupted;
        public event Action Stopped;

        public Animation()
        {
            _priority = AnimationPriority.Normal;
        }

        public Animation(AnimationPriority priority) 
        {
            _priority = priority;
        }

        public AnimationPriority Priority => _priority;

        public Animation Animate(IAnimatableTransform animatable)
        {
            Animatable = animatable;
            StartContext = (IAnimatableTransform)animatable.Clone();

            Start(animatable);

            return this;
        }

        protected abstract void Start(IAnimatableTransform animatable);

        protected abstract void Break();

        public void Stop()
        {
            Break();

            if(IsCompleted == true)
                Completed?.Invoke();
            else
                Interrupted?.Invoke();

            Animatable = null;
            StartContext = null;

            Stopped?.Invoke();
        }

        private void OnDisable()
        {
            Completed -= _onCompleted;
            Interrupted -= _onInterrupted;
            Stopped -= _onStopped;
        }

        public Animation OnCompleted(Action action)
        {
            Completed -= _onCompleted;
            _onCompleted = action;
            Completed += _onCompleted;
            return this;
        }

        public Animation OnInterrupted(Action action)
        {
            Interrupted -= _onInterrupted;
            _onInterrupted = action;
            Interrupted += _onInterrupted;
            return this;
        }

        public Animation OnStopped(Action action)
        {
            Stopped -= _onStopped;
            _onStopped = action;
            Stopped += _onStopped;
            return this;
        }

        public void Dispose()
        {
            OnDisable();
        }
    }
}
