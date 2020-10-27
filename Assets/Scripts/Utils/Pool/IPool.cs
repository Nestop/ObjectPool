using UnityEngine;

namespace Utils.Pool
{
    public interface IPool<out T> where T : MonoBehaviour, IPoolable
    {
        T GetObject();
        void DeactivateAllObjects();
    }
}