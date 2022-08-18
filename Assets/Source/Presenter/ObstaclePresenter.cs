using UnityEngine;
using FireBalls3D.Model;

namespace FireBalls3D.Presenter
{
    public class ObstaclePresenter : Presenter
    {
        [SerializeField] private ParticleFactory _factory;

        public new Obstacle Model => base.Model as Obstacle;

        private void OnTriggerEnter(Collider other)
        {
            _factory.CreateBurst(other.transform.position, Vector3.zero);

            Model.OnCollided();
        }
    }
}