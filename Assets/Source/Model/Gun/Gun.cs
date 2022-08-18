using System;
using UnityEngine;

namespace FireBalls3D.Model
{
    public class Gun : IUpdatable, IPauseable
    {
        private Transformable _tank;
        private float _reload;
        private DefaultTimer _timer;
        private bool _isPaused;

        public event Action<Missile> Shot;

        public Gun(Transformable tank, Vector3 shootDirection, float reload)
        {
            _tank = tank;
            _reload = reload;
            _timer = new DefaultTimer(_reload);
        }

        public bool IsPaused => _isPaused;
        public float Reload => _reload;
        public Vector3 ShootDirection => _tank.Forward;
        public ITimer Timer => _timer;

        public void Update(float deltaTime)
        {
            if (IsPaused == true)
                return;

            _timer.Tick(deltaTime);
        }

        public bool TryShoot()
        {
            if (_timer.Ended == false || IsPaused == true)
                return false;

            Missile bullet = CreateMissile();
            bullet.LookAt(bullet.Position + _tank.Forward);
            _timer.Reset();
            Shot?.Invoke(bullet);

            return true;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }

        private Missile CreateMissile()
        {
            return new Missile(_tank.Position, Vector3.zero, ShootDirection);
        }
    }
}
