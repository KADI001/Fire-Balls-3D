using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBalls3D.Model
{
    public class Animator
    {
        private Animation _lastAnimation;
        private IAnimatableTransform _animatableTransform;

        public Animator(IAnimatableTransform animatableTransform)
        {
            _animatableTransform = animatableTransform;
        }

        public void StartAnimation(Animation animation)
        {
            if (_lastAnimation != null)
            {
                if (_lastAnimation.Priority > animation.Priority)
                    return;
            }

            _lastAnimation?.Stop();

            _lastAnimation = animation.Animate(_animatableTransform);

            _lastAnimation.OnStopped(OnAnimationStoped);
        }

        private void OnAnimationStoped()
        {
            _lastAnimation.Dispose();
            _lastAnimation = null;
        }
    }
}
