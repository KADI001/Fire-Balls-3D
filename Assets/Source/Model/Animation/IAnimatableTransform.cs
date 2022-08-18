using System;

namespace FireBalls3D.Model
{
    public interface IAnimatableTransform : ICloneable
    {
        public Transformable Transformable { get; }
    }
}
