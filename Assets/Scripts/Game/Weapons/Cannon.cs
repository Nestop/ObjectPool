using Game.Managers;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Game.Weapons
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] private Animator cannonAnimator;
        [SerializeField] private Transform cannonAim;
        [SerializeField] private float shotLoopTimeInSeconds = 1f;
        [SerializeField] private int shotsPerLoop = 1;
        [SerializeField] private float spreadAngle = 10f;
        
        private int _explosionEffectsCount;
        private Timer _shootingTimer;

        private void Awake()
        {
            _shootingTimer = new Timer(shotLoopTimeInSeconds, 
                int.MaxValue, 
                MakeShotsAtTime, 
                true, 
                true,
                InfinityShooting);
            
            cannonAnimator.speed /= shotLoopTimeInSeconds / cannonAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }

        private void Start()
        {
            _explosionEffectsCount = PoolManager.Instance.ExplosionEffectPools.Count;
            
            InfinityShooting();
        }

        private void InfinityShooting()
        {
            TimerManager.Instance.StartTimer(_shootingTimer);
        }

        private void MakeShotsAtTime()
        {
            for (int i = 0; i < shotsPerLoop; i++)
            {
                Shoot();
            }
        }
    
        private void Shoot()
        {
            var fireDirection =
                Quaternion.Euler(
                    Random.Range(-spreadAngle, spreadAngle), 
                    Random.Range(-spreadAngle, spreadAngle), 
                    0f) * cannonAim.forward;
        
            if (!Physics.Raycast(cannonAim.position, fireDirection, out var hit, float.MaxValue)) return;
        
            var effectIndex = Random.Range(0, _explosionEffectsCount);
            PoolManager.Instance.ExplosionEffectPools[effectIndex].GetObject().Play(hit.point + hit.normal);
        
            PoolManager.Instance.DamageScreenPool.GetObject().ShowDamage(
                Random.Range(0,100),
                2f, 
                hit.point + 2*hit.normal,
                GameManager.Instance.MainCamera.transform.position);
        }
    }
}
