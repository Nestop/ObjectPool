using UnityEngine;

namespace Utils.Pool
{
    public interface IPoolFactory 
    {
        IPool<T> CreatePool<T>(Transform container, T prefab, int poolSize = 0, bool fillAtStart = false,
            bool expandable = false) where T : MonoBehaviour, IPoolable;
    }
}