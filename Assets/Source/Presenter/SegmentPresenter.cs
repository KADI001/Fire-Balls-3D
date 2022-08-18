using UnityEngine;
using FireBalls3D.Model;

namespace FireBalls3D.Presenter
{
    [RequireComponent(typeof(MeshRenderer))]
    public class SegmentPresenter : Presenter
    {
        [SerializeField] private ParticleFactory _factory;
        private MeshRenderer _meshRenderer;

        protected new Segment Model => base.Model as Segment;

        protected override void OnInitializing()
        {
            transform.localScale = Model.Scale;
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _factory.CreateDestroyingSegment(Model.Position, Vector3.right * -90, _meshRenderer.material.color);

            Model.Dispose();
        }
    }
}