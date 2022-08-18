using FireBalls3D.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FireBalls3D.Presenter
{
    public class ObstaclePatternPresenter : Presenter
    {
        [SerializeField] private PresentersFactory _factory;
        private int _index;

        public new ObstaclePattern Model => base.Model as ObstaclePattern;

        protected override void OnInitializing()
        {
            Model.ObstaclesCreated += OnObstaclesCreated;
        }

        protected override void OnDisposing()
        {
            Model.ObstaclesCreated -= OnObstaclesCreated;
        }

        private void OnObstaclesCreated(IEnumerable<Obstacle> obstacles)
        {
            Obstacle[] obstaclesArray = obstacles.ToArray();

            for (int i = 0; i < Model.NumberObstacles; i++)
            {
                _factory.CreateObstacle(obstaclesArray[i]);
                MoveIndex();
            }
        }


        private void MoveIndex()
        {
            _index++;

            if (_index >= _factory.NumberSegmentPresenters)
                _index = 0;
        }
    }
}
