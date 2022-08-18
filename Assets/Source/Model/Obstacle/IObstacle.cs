using System;

namespace FireBalls3D.Model
{
    public interface IObstacle
    {
        public event Action Collided;
    }
}
