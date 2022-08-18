using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FireBalls3D
{
    public class ParticleFactory : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _destroyingSegmentEffect;
        [SerializeField] private ParticleSystem _burstEffect;

        public void CreateBurst(Vector3 position, Vector3 rotation)
        {
            CreateParticle(_burstEffect, position, Quaternion.Euler(rotation));
        }

        public void CreateDestroyingSegment(Vector3 position, Vector3 rotation, Color color)
        {
            ParticleSystem particle = CreateParticle(_destroyingSegmentEffect, position, Quaternion.Euler(rotation));
            ParticleSystemRenderer renderer = particle.GetComponent<ParticleSystemRenderer>();
            renderer.material.color = color;
        }

        private ParticleSystem CreateParticle(ParticleSystem effect, Vector3 position, Quaternion rotation)
        {
            ParticleSystem particle = Instantiate(effect, position, rotation);
            Destroy(particle.gameObject, particle.main.duration);

            return particle;
        }
    }
}
