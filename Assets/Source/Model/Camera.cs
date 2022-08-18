using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FireBalls3D.Model
{
    public class Camera : Transformable, IAnimatableTransform
    {
        public Camera(Vector3 position, Vector3 rotation) : base(position, rotation)
        {

        }

        public Transformable Transformable => this;

        public object Clone()
        {
            return new Camera(Position, Rotation);
        }
    }
}
