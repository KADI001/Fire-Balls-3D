using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireBalls3D.Model
{
    public interface IPauseable
    {
        public bool IsPaused { get; }

        public void Pause();
        public void Resume();
    }
}
