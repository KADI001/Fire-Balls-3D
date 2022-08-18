using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FireBalls3D.Model
{
    public class ObstaclePattern : Transformable, IUpdatable, IPauseable
    {
        private float _offsetFromCenter;
        private float _distanceBetweenObstacles;
        private bool _isPaused;
        private List<Obstacle> _obstacles;
        private int _numberObstacles;

        public event Action<IEnumerable<Obstacle>> ObstaclesCreated;

        public ObstaclePattern(Vector3 position, int numberObstacles, float offsetFromCenter, float distanceBetweenObstacles) : base(position, Vector3.zero)
        {
            _offsetFromCenter = offsetFromCenter;
            _distanceBetweenObstacles = distanceBetweenObstacles;
            _obstacles = new List<Obstacle>();
            _numberObstacles = numberObstacles;
        }

        public IEnumerable<IObstacle> Obstacles => _obstacles;
        public bool IsPaused => _isPaused;
        public int NumberObstacles => _numberObstacles;

        public void Update(float deltaTime)
        {
            if (IsPaused == true)
                return;

            for (int i = 0; i < _obstacles.Count; i++)
            {
                _obstacles[i].MoveArroundCircle(Position, deltaTime);
            }
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }

        public void CreateObstacles()
        {
            for (int i = 0; i < _numberObstacles; i++)
            {
                Vector3 position = Position + Vector3.forward * (_offsetFromCenter + _distanceBetweenObstacles * i);
                float degreesPreSecond = UnityEngine.Random.Range(Config.MinObstacleDegreePerUnit, Config.MaxObstacleDegreePerUnit);
                Obstacle obstacle = Create(position, Vector3.zero, degreesPreSecond);
                _obstacles.Add(obstacle);
            }

            ObstaclesCreated?.Invoke(_obstacles);
        }

        private Obstacle Create(Vector3 position, Vector3 rotation, float degreesPerSecond)
        {
            return new Obstacle(position, rotation, degreesPerSecond);
        }
    }
}
