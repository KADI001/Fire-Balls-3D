using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBalls3D.Model
{
    public class Score
    {
        private int _value;
        private Pipe _pipe;
        private IScoringPolicy _scoringPolicy;

        public event Action Changed;

        public Score(Pipe pipe, IScoringPolicy scoringPolicy)
        {
            _pipe = pipe;
            _scoringPolicy = scoringPolicy;

            OnEnable();
        }

        public int Value => _value;

        private void OnEnable()
        {
            _pipe.SegmentDestroyed += OnSegmentDestroyed;
        }

        private void OnDisable()
        {
            _pipe.SegmentDestroyed -= OnSegmentDestroyed;
        }

        private void OnSegmentDestroyed()
        {
            _value += _scoringPolicy.GetScore();

            Changed?.Invoke();
        }

        public void Dispose()
        {
            OnDisable();
        }
    }

    public class ScoringPolicy : IScoringPolicy
    {
        public int GetScore()
        {
            return Config.Reward;
        }
    }

    public class BoostedScoringPolicy : IScoringPolicy, IUpdatable
    {
        private IScoringPolicy _scoringPolicy;
        private Timer _timer;
        private int _scale;

        public BoostedScoringPolicy(float boostPeriod)
        {
            _scoringPolicy = new ScoringPolicy();
            _timer = new DefaultTimer(boostPeriod, OnTimerEnded);
            _scale = 1;
        }

        public void Update(float deltaTime)
        {
            _timer.Tick(deltaTime);
        }

        private void OnTimerEnded()
        {
            _scale = 1;
        }

        public int GetScore()
        {
            if (_timer.Ended == true)
                _timer.Reset();

            return _scoringPolicy.GetScore() * _scale++;
        }
    }


    public interface IScoringPolicy
    {
        public int GetScore();
    }
}
