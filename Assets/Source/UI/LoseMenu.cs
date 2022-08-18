using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using FireBalls3D.Model;

namespace FireBalls3D.Plate
{
    public class LoseMenu : MonoBehaviour
    {
        [SerializeField] private MenuContent _content;

        private Health _tankHealth;
        private Pipe _pipe;

        public void Init(Health tankHealth, Pipe pipe)
        {
            _tankHealth = tankHealth;
            _pipe = pipe;

            enabled = true;
        }

        private void OnEnable()
        {
            _tankHealth.Died += OnTankDie;
            _pipe.Destroyed += OnPipeDestroyed;
        }


        private void OnDisable()
        {
            _tankHealth.Died -= OnTankDie;
            _pipe.Destroyed -= OnPipeDestroyed;
        }


        private void OnPipeDestroyed()
        {
            ShowAsync();
        }

        private void OnTankDie()
        {
            ShowAsync();
        }

        private async void ShowAsync()
        {
            _content.gameObject.SetActive(true);

            await AnimatedAction.StartAsync(0.3f, Time.fixedDeltaTime, (progress) =>
            {
                float alpha = Mathf.Lerp(0, _content.Alpha, progress);
                _content.SetColorAlpha(alpha);
            });
        }
    }
}