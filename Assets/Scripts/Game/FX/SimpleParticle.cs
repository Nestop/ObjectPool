using System;
using UnityEngine;
using Utils.Pool;

namespace Game.FX
{
    public class SimpleParticle : MonoBehaviour, IPoolable
    {
        public event Action<IPoolable> ObjectDeactivation;

        [SerializeField] private ParticleSystem particle;

        public void Play(Vector3 position)
        {
            particle.Clear();
            particle.transform.position = position;
            particle.Play();
        }

        private void OnParticleSystemStopped()
        {
            ObjectDeactivation?.Invoke(this);
        }
    }
}