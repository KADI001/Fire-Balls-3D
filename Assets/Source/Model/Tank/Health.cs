using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FireBalls3D.Model
{
    public class Health
    {
        private int _value;
        private List<IObstacle> _obstacles;

        public event Action Died;
        public event Action Damaged;

        public Health(int value, IEnumerable<IObstacle> obstacles)
        {
            _value = value;
            _obstacles = obstacles.ToList();

            OnEnable();
        }

        public int Value => _value;
        public bool IsDead => _value <= 0;

        private void OnEnable()
        {
            _obstacles.ForEach(o => o.Collided += OnObstacleCollided);
        }

        private void OnDisable()
        {
            _obstacles.ForEach(o => o.Collided -= OnObstacleCollided);
        }

        private void OnObstacleCollided()
        {
            ApplyDamage(Config.Damage);
        }

        public void ApplyDamage(int damage) 
        {
            if (damage < 0)
                throw new ArgumentException("Damage can't be less than zero");

            _value -= damage;
            _value = _value < 0 ? 0 : _value;

            Damaged?.Invoke();

            if(_value == 0)
            {
                Dispose();
                Died?.Invoke();
            }
        }

        public void Dispose()
        {
            OnDisable();
        }
    }
}
