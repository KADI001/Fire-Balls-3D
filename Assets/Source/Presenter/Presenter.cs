using FireBalls3D.Model;
using System;
using UnityEngine;

namespace FireBalls3D.Presenter
{
    public class Presenter : MonoBehaviour, IDisposable
    {
        protected IUpdatable Updatable { get; private set; }
        protected Transformable Model { get; private set; }

        private void Update()
        {
            Updatable?.Update(Time.deltaTime);
        }

        private void OnEnable()
        {
            OnEnabling();

            Model.Moved += OnModelMoved;
            Model.Rotated += OnModelRotated;
            Model.Destroyed += OnModelDestory;
        }

        private void OnDisable()
        {
            OnDisabling();

            Model.Moved -= OnModelMoved;
            Model.Rotated -= OnModelRotated;
            Model.Destroyed -= OnModelDestory;
        }

        protected virtual void OnEnabling() { }
        protected virtual void OnDisabling() { }

        public void Init(Transformable model)
        {
            Model = model;

            if (Model is IUpdatable)
                Updatable = (IUpdatable)Model;

            OnInitializing();

            OnModelMoved();
            OnModelRotated();

            enabled = true;
        }

        protected virtual void OnInitializing() { }

        private void OnModelMoved()
        {
            transform.position = Model.Position;
        }

        private void OnModelRotated()
        {
            transform.rotation = Quaternion.Euler(Model.Rotation);
        }

        private void OnModelDestory()
        {
            Dispose();
        }

        public void Dispose()
        {
            OnDisposing();
            OnDisable();
            Destroy(this.gameObject);
        }

        protected virtual void OnDisposing() { }
    }
}