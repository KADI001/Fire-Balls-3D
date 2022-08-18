using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FireBalls3D.Model;


namespace FireBalls3D.Plate
{
    public class UICompositeRoot : MonoBehaviour
    {
        [SerializeField] private Root _root;
        [SerializeField] private LoseMenu _loseMenu;
        [SerializeField] private NumberSegmentsText _numberSegmentsText;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private ScoreText _scoreText;
        [SerializeField] private ReloadBar _reloadBar;

        private void Start()
        {
            Compose();
        }

        private void Compose()
        {
            _loseMenu.Init(_root.Health, _root.Pipe);
            _numberSegmentsText.Init(_root.Pipe);
            _healthBar.Init(_root.Health);
            _scoreText.Init(_root.Score);
            _reloadBar.Init(_root.GunTimer);
        }
    }
}