using System;
using UnityEngine;

namespace FireBalls3D.Model
{
    public class Tank : Transformable, IAnimatableTransform
    {
        public Vector3 ShootPoint => Position + Forward;
        public Transformable Transformable => this;

        public Tank(Vector3 position, Vector3 rotation) : base(position, rotation) 
        {
        }

        public object Clone()
        {
            return new Tank(Position, Rotation);
        }
    }
}
