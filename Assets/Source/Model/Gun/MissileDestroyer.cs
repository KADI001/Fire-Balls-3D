using System;
using System.Linq;
using System.Collections.Generic;

namespace FireBalls3D.Model
{
    public class MissileDestroyer : IUpdatable, IDisposable
    {
        private List<ContextTimer<Missile>> _timers;
        private float _lifeTimeInSeconds;
        private Gun _gun;

        public MissileDestroyer(Gun gun, float lifeTimeInSeconds)
        {
            _timers = new List<ContextTimer<Missile>>();
            _lifeTimeInSeconds = lifeTimeInSeconds;
            _gun = gun;

            OnEnable();
        }

        public void Update(float deltaTime)
        {
            if (_timers.Count == 0)
                return;

            _timers.For(timer => timer.Tick(deltaTime));
        }

        public void Register(Missile missile)
        {
            _timers.Add(new ContextTimer<Missile>(_lifeTimeInSeconds, missile, OnMissileLifeTimeIsEnded));
        }

        public void UnRegister(Missile missile)
        {
            _timers.Remove(new ContextTimer<Missile>(_lifeTimeInSeconds, missile, OnMissileLifeTimeIsEnded));
        }

        private void OnMissileLifeTimeIsEnded(ContextTimer<Missile> timer)
        {
            timer.Context.Dispose();
            _timers.Remove(timer);
        }

        private void OnEnable()
        {
            _gun.Shot += OnGunShot;
        }

        private void OnDisable()
        {
            _gun.Shot -= OnGunShot;
        }

        private void OnGunShot(Missile missile)
        {
            Register(missile);
        }

        public void Dispose()
        {
            OnDisable();
        }
    }
}
