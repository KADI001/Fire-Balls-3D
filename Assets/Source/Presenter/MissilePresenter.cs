using UnityEngine;

namespace FireBalls3D.Presenter
{
    public class MissilePresenter : Presenter
    {
        private void OnTriggerEnter(Collider other)
        {
            Model.Dispose();
        }
    }
}