using System;
using UnityEngine;

namespace FireBalls3D.Model
{
    public class Obstacle : Transformable, IObstacle
    {
        public event Action Collided;
        private float _degreePerUnit;

        public Obstacle(Vector3 position, Vector3 rotation, float degreePerUnit) : base(position, rotation)
        {
            _degreePerUnit = degreePerUnit;
        }

        public void MoveArroundCircle(Vector3 circleCenter, float deltaTime)
        {
            Vector3 position = Position - circleCenter;

            position = Quaternion.Euler(0, _degreePerUnit * deltaTime, 0) * position;

            Vector3 newPosition = position + circleCenter;

            SetPosition(newPosition);
            LookAt(circleCenter);
        }

        public void OnCollided()
        {
            Collided?.Invoke();
        } 
    }
}