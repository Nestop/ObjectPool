using UnityEngine;

namespace Utils.Pool
{
    public class ObjectPoolFactory : IPoolFactory
    {
        public IPool<T> CreatePool<T>(Transform container, T prefab, int poolSize = 0, bool fillAtStart = false, bool expandable = false) where T : MonoBehaviour, IPoolable
        {
            return new ObjectPool<T>(container, prefab, poolSize, fillAtStart, expandable);
        }
    }
}