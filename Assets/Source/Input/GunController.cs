using FireBalls3D.Model;
using System;
using UnityEngine.InputSystem;

namespace FireBalls3D.Input
{
    public class GunController : IDisposable
    {
        private Gun _gun;
        private GunInput _input;

        public GunController()
        {
            _input = new GunInput();

            OnEnable();
        }

        public GunController(Gun gun) : this()
        {
            _gun = gun;
        }

        public void BindGun(Gun gun)
        {
            _gun = gun;
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Gun.Shoot.performed += OnGunShot;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Gun.Shoot.performed -= OnGunShot;
        }

        private void OnGunShot(InputAction.CallbackContext obj)
        {
            _gun.TryShoot();
        }

        public void Dispose()
        {
            OnDisable();
        }
    }
}
