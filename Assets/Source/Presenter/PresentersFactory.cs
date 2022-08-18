using System.Collections;
using UnityEngine;
using FireBalls3D.Model;

namespace FireBalls3D.Presenter
{
    public class PresentersFactory : MonoBehaviour
    {
        [SerializeField] private SegmentPresenter[] _segmentPresenters;
        [SerializeField] private MissilePresenter _missilePresenter;
        [SerializeField] private ObstaclePresenter _obstaclePresenter;

        public int NumberSegmentPresenters => _segmentPresenters.Length;

        public void CreateMissile(Missile missile)
        {
            Create(_missilePresenter, missile);
        }

        public void CreateSegment(Segment segment, int index)
        {
            Create(_segmentPresenters[index], segment);
        }

        public void CreateObstacle(Obstacle obstacle)
        {
            Create(_obstaclePresenter, obstacle);
        }

        private Presenter Create(Presenter prefab, Transformable model)
        {
            Presenter presenter = Instantiate(prefab);
            presenter.Init(model);

            return presenter;
        }
    }
}