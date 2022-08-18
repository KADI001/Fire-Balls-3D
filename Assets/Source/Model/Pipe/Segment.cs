using System;
using UnityEngine;

namespace FireBalls3D.Model
{
    public class Segment : Transformable
    {
        public Vector3 Scale { get; private set; }

        public event Action<Segment> Destroying;

        public Segment(Vector3 position, Vector3 rotation, Vector3 scale) : base(position, rotation)
        {
            Scale = scale;
        }

        protected override void OnDestroying()
        {
            Destroying?.Invoke(this);
        }
    }
}
