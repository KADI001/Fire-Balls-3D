using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBalls3D.Model
{
    public class FPSCounter 
    {
        private int _bufferSize;

        private int[] _frameBuffer;
        private int _index;

        public FPSCounter(int bufferSize)
        {
            _bufferSize = bufferSize;
            _frameBuffer = new int[_bufferSize];
            _index = 0;
        }

        public int FPS { get; private set; }

        public void Update(float deltaTime)
        {
            UpdateBuffer(deltaTime);
            CalculateFPS();
        }

        private void UpdateBuffer(float deltaTime)
        {
            _frameBuffer[_index++] = (int)(1 / deltaTime);

            if (_index >= _bufferSize)
                _index = 0;
        }

        private void CalculateFPS()
        {
            int sum = 0;

            foreach (var frame in _frameBuffer)
            {
                sum += frame;
            }

            FPS = sum / _bufferSize;
        }
    }
}
