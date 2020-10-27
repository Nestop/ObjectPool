using System.Collections.Generic;
using Game.FX;
using UnityEngine;
using Utils;
using Utils.Pool;

namespace Game.Managers
{
    public class PoolManager : MBSingleton<PoolManager>
    {
        //W.I.P.
        public List<IPool<SimpleParticle>> ExplosionEffectPools { get; private set; }
        public IPool<DamageScreen> DamageScreenPool { get; private set; }
        
        [Header("Prefabs")]
        [SerializeField] private SimpleParticle[] explosionEffects;
        [SerializeField] private DamageScreen damageScreen;
        [Header("Settings")]
        [SerializeField] private int poolSizeOnEffect = 10;
        [SerializeField] private bool fillAtStart = true;
        [SerializeField] private bool expandable = true;
        [SerializeField] private bool badPool = false;

        private IPoolFactory _poolFactory;

        protected override void OnSingletonAwake()
        {
            DontDestroyOnLoad(this);

            _poolFactory = badPool ? (IPoolFactory) new BadPoolFactory() : new ObjectPoolFactory();
            
            ExplosionEffectPools = new List<IPool<SimpleParticle>>();

            foreach (var effect in explosionEffects)
                ExplosionEffectPools.Add(_poolFactory.CreatePool(transform, effect, poolSizeOnEffect, fillAtStart, expandable));

            DamageScreenPool = _poolFactory.CreatePool(transform, damageScreen, poolSizeOnEffect, fillAtStart, expandable);
        }
    }
}