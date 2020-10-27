using UnityEngine;

namespace Utils
{
    public abstract class MBSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                              "' already destroyed on application quit." +
                              " Won't create again - returning null.");
                    return null;
                }

                if (_instance != null) return _instance;
                
                var instance = new GameObject($"{typeof(T).Name} [singleton ({typeof(T)})]");
                _instance = instance.AddComponent<T>();
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                OnSingletonAwake();
            }
            else if (_instance != this)
            {
                Debug.LogError("[Singleton] Instance '" + typeof(T) +
                                 "': more than one in the '" + gameObject.scene.name +
                                 "' scene. Remove the extra copies and save the scene.");
                Destroy(gameObject);
            }
        }
        
        private static bool applicationIsQuitting = false;
        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed,
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        public void OnDestroy()
        {
            if (_instance != this) return;
            OnSingletonDestroy();
            applicationIsQuitting = true;
        }
        
        protected virtual void OnSingletonAwake(){}
        protected virtual void OnSingletonDestroy(){}
    }
}