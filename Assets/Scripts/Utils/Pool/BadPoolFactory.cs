using UnityEngine;

namespace Utils.Pool
{
    public class BadPoolFactory : IPoolFactory
    {
        public IPool<T> CreatePool<T>(Transform container, T prefab, int poolSize = 0, bool fillAtStart = false, bool expandable = false) where T : MonoBehaviour, IPoolable
        {
            return new BadPool<T>(container, prefab, poolSize, fillAtStart, expandable);
        }
    }
}