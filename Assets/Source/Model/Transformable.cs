using System;
using UnityEngine;

namespace FireBalls3D.Model
{
    public class Transformable
    {
        public Vector3 Position { get; private set; }
        public Vector3 Rotation { get; private set; }

        public event Action Moved;
        public event Action Rotated;
        public event Action Destroyed;

        public Transformable()
        {
            Position = Vector3.zero;
            Rotation = Vector3.zero;
        }

        public Transformable(Vector3 position = default) : this()
        {
            Position = position;
        }

        public Transformable(Vector3 position = default, Vector3 rotation = default) : this(position)
        {
            Rotation = rotation;
        }

        public Vector3 Forward => Quaternion.Euler(Rotation.x, Rotation.y, Rotation.z) * Vector3.forward;
        public Vector3 Up => Quaternion.Euler(Rotation.x, Rotation.y, Rotation.z) * Vector3.up;
        public Vector3 Right => Quaternion.Euler(Rotation.x, Rotation.y, Rotation.z) * Vector3.right;

        public void LookAt(Vector3 target)
        {
            Vector3 direction = (target - Position).normalized;

            if (Position == target || Forward == direction)
                return;

            float projectionX = Vector3.Dot(direction, Vector3.right);
            float projectionY = Vector3.Dot(direction, Vector3.up);
            float projectionZ = Vector3.Dot(direction, Vector3.forward);

            float sinA = projectionX / new Vector2(projectionX, projectionZ).magnitude;
            float cosA = projectionZ / new Vector2(projectionX, projectionZ).magnitude;

            float sinB = projectionY;
            float cosB = new Vector2(projectionX, projectionZ).magnitude;

            float angelY = Mathf.Atan2(sinA, cosA) * Mathf.Rad2Deg;
            float angelX = -1 * Mathf.Atan2(sinB, cosB) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(angelX, angelY, Rotation.z);
            SetRotation(rotation.eulerAngles);
        }

        public void MoveTo(Vector3 delta)
        {
            Vector3 newPosition = Position + delta;

            SetPosition(newPosition);
        }

        public void Rotate(Vector3 delta)
        {
            Vector3 newRotationn = Rotation + delta;

            SetRotation(newRotationn);
        }

        public void SetRotation(Vector3 rotation)
        {
            Vector3 newRotation = rotation;

            newRotation.x = Mathf.Repeat(newRotation.x, 360);
            newRotation.y = Mathf.Repeat(newRotation.y, 360);
            newRotation.z = Mathf.Repeat(newRotation.z, 360);

            Rotation = newRotation;

            Rotated?.Invoke();
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
            Moved?.Invoke();
        }

        public void Dispose()
        {
            OnDestroying();

            Destroyed?.Invoke();
        }

        protected virtual void OnDestroying() { }
    }
}
