using System;

namespace FireBalls3D.Model
{
    public interface ITimer : ITickable
    {
        public event Action<float> Updated;
    }

    public interface ITickable
    {
        public void Tick(float deltaTime);
    }

    public abstract class Timer : ITimer, IPauseable
    {
        private float _targetTime;
        private float _accumulatedTime;
        private bool _ended;
        private bool _isPaused;

        public event Action<float> Updated;

        public Timer(float targetTime)
        {
            _accumulatedTime = 0;
            _targetTime = targetTime;
            _ended = false;
            _isPaused = false;
        }

        public float TargetTime => _targetTime;
        public float AccumulatedTime => _accumulatedTime;
        public bool Ended => _ended;
        public bool IsPaused => _isPaused;

        public void Tick(float deltaTime)
        {
            if (_ended == true || _isPaused == true)
                return;

            _accumulatedTime += deltaTime;

            if (_accumulatedTime > _targetTime)
            {
                Stop();
                InvokeEndedEvent();
            }

            Updated?.Invoke(_accumulatedTime);
        }

        public abstract void InvokeEndedEvent();

        public void Reset()
        {
            _accumulatedTime = 0;
            _ended = false;
        }

        public void Stop()
        {
            _ended = true;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }
    }

    public class DefaultTimer : Timer
    {
        public readonly Action OnEnd;

        public DefaultTimer(float targetTime) : base(targetTime)
        {
        }

        public DefaultTimer(float targetTime, Action onEnd) : this(targetTime)
        {
            OnEnd = onEnd;
        }

        public override void InvokeEndedEvent()
        {
            OnEnd?.Invoke();
        }
    }

    public class ContextTimer<T> : Timer
    {
        public readonly T Context;
        public readonly Action<ContextTimer<T>> OnEnd;

        public ContextTimer(float targetTime, T context, Action<ContextTimer<T>> onEnd) : base(targetTime)
        {
            Context = context;
            OnEnd = onEnd;
        }

        public override void InvokeEndedEvent()
        {
            OnEnd?.Invoke(this);
        }
    }

}