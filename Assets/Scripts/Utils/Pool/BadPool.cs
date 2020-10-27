using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.Pool
{
    public class BadPool<T> : IPool<T> where T : MonoBehaviour, IPoolable
    {
        private readonly List<T> _objects;
        private readonly Transform _container;
        private readonly T _prefab;

        public BadPool(Transform container, T prefab, int poolSize = 0, bool fillAtStart = false, bool expandable = false)
        {
            var poolContainer = new GameObject(prefab.gameObject.name).transform;
            poolContainer.parent = container;
            _container = poolContainer;
            _prefab = prefab;
            _objects = new List<T>(poolSize);
        }

        public T GetObject()
        {
            return InitializeObject();
        }

        private T InitializeObject()
        {
            var obj = Object.Instantiate(_prefab, _container);
            obj.ObjectDeactivation += DestroyObject;
            _objects.Add(obj);
            
            return obj;
        }

        public void DeactivateAllObjects()
        {
            foreach (var obj in _objects)
                DestroyObject(obj);
        }
        
        private void DestroyObject(IPoolable obj)
        {
            DestroyObject(obj as T);
        }
        
        private void DestroyObject(T obj)
        {
            Object.Destroy(obj.gameObject);
        }
    }
}