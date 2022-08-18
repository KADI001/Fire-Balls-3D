using UnityEngine;

namespace FireBalls3D.Model
{
    public class Missile : Transformable, IUpdatable, IPauseable
    {
        private bool _isPaused;
        public readonly Vector3 FlyDirection;

        public Missile(Vector3 position, Vector3 rotation, Vector3 flyDirection) : base(position, rotation)
        {
            FlyDirection = flyDirection;
        }

        public bool IsPaused => _isPaused;

        public void Update(float deltaTime)
        {
            if (IsPaused == true)
                return;

            MoveTo(FlyDirection * Config.MissileFlySpeed * deltaTime);
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }
    }
}
