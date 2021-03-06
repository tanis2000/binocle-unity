using System;
using System.Collections.Generic;
using UnityEngine;

namespace Binocle
{
    public class EntityPool<T> where T : Entity
    {
        private static Queue<T> _objectQueue = new Queue<T>(10);
        private Scene scene;
        private Entity PoolEntity;

        public EntityPool(Scene scene)
        {
            this.scene = scene;
            PoolEntity = scene.CreateEntity<Entity>("Pool");
        }

        /// <summary>
        /// warms up the cache filling it with a max of cacheCount objects
        /// </summary>
        /// <param name="cacheCount">new cache count</param>
        public void WarmCache(int cacheCount)
        {
            cacheCount -= _objectQueue.Count;
            if (cacheCount > 0)
            {
                for (var i = 0; i < cacheCount; i++) {
                    var e = scene.CreateEntity<T>("_pooledobject");
                    e.SetParent(PoolEntity);
                    e.gameObject.SetActive(false);
                    _objectQueue.Enqueue(e);
                }
            }
        }

        /// <summary>
        /// trims the cache down to cacheCount items
        /// </summary>
        /// <param name="cacheCount">Cache count.</param>
        public static void TrimCache(int cacheCount)
        {
            while (cacheCount > _objectQueue.Count)
            {
                _objectQueue.Dequeue();
            }
        }


        /// <summary>
        /// clears out the cache
        /// </summary>
        public static void ClearCache()
        {
            _objectQueue.Clear();
        }


        /// <summary>
        /// pops an item off the stack if available creating a new item as necessary
        /// </summary>
        public T Obtain(string name)
        {
            if (_objectQueue.Count > 0)
            {
                var e = _objectQueue.Dequeue();
                e.name = name;
                e.gameObject.SetActive(true);
                return e;
            }

            return scene.CreateEntity<T>(name);
        }


        /// <summary>
        /// pushes an item back on the stack
        /// </summary>
        /// <param name="obj">Object.</param>
        public void Free(T obj)
        {
            obj.name = "_pooledobject";
            obj.gameObject.SetActive(false);
            obj.SetParent(PoolEntity);

            /*
            // Remove all children
            foreach(Transform child in obj.transform) {
                GameObject.Destroy(child.gameObject);
            }
            
            // Remove all components
            foreach (var comp in obj.GetComponents<Component>())
            {
                if (!(comp is Transform) && !(comp is T))
                {
                    GameObject.Destroy(comp);
                }
            }
            */

            _objectQueue.Enqueue(obj);

            if (obj is IPoolableEntity)
                ((IPoolableEntity)obj).Reset();
        }
    }


    /// <summary>
    /// Objects implementing this interface will have {@link #reset()} called when passed to {@link #push(Object)}
    /// </summary>
    public interface IPoolableEntity
    {
        /// <summary>
        /// Resets the object for reuse. Object references should be nulled and fields may be set to default values
        /// </summary>
        void Reset();
    }
}