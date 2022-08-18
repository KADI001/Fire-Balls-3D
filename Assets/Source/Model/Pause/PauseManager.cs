using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBalls3D.Model
{
    public class PauseManager : IPauseManager, IPauseable
    {
        private List<IPauseable> _pauseables;

        private bool _isPaused;
        public bool IsPaused => _isPaused;

        public PauseManager()
        {
            _pauseables = new List<IPauseable>();
        }

        public PauseManager(IEnumerable<IPauseable> pauseables)
        {
            _pauseables = pauseables.ToList();
        }

        public void Register(IPauseable pauseable)
        {
            _pauseables.Add(pauseable);
        }

        public void UnRegister(IPauseable pauseable)
        {
            _pauseables.Remove(pauseable);
        }

        public void Pause()
        {
            _pauseables.ForEach(pauseable => pauseable.Pause());
            _isPaused = true;
        }

        public void Resume()
        {
            _pauseables.ForEach(pauseable => pauseable.Resume());
            _isPaused = false;
        }
    }
}