using UnityEngine;
using Binocle.Processors;
using Binocle.Components;

namespace Binocle
{
    public class Entity : MonoBehaviour
    {
        public GameObject GameObject
        {
            get { return _gameObject; }
            set { _gameObject = value; }
        }

        private GameObject _gameObject;

        public Scene Scene
        {
            get { return _scene; }
            set { _scene = value; }
        }

        private Scene _scene;

        public BitSet ComponentBits
        {
            get { return _componentBits; }
        }

        private BitSet _componentBits = new BitSet();

        public Entity Parent
        {
            get { return _parent; }
        }

        private Entity _parent;

        public T AddComponent<T>()
            where T : UnityEngine.Component
        {
            var c = _gameObject.AddComponent<T>();
            if (c is BaseMonoBehaviour)
            {
                object b = (object)c;
                ((BaseMonoBehaviour)b).Entity = this;
            }
            this.ComponentBits.set(ComponentTypeManager.GetIndexFor(typeof(T)));
            _scene.EntityProcessors.onComponentAdded(this);
            return c;
        }

        public void RemoveComponent<T>()
            where T : UnityEngine.Component
        {
            var c = _gameObject.GetComponent<T>();
            GameObject.Destroy(c);
            this.ComponentBits.set(ComponentTypeManager.GetIndexFor(typeof(T)), false);
            _scene.EntityProcessors.onComponentRemoved(this);
        }

        public void SetParent(Entity parent)
        {
            _parent = parent;
            if (parent != null)
            {
                GameObject.transform.SetParent(parent.GameObject.transform);
            }
            else
            {
                GameObject.transform.SetParent(null);
            }
        }

        public static void Destroy(Entity entity)
        {
            foreach (var c in entity.GameObject.GetComponents(typeof(MonoBehaviour)))
            {
                //Debug.Log("Removed [" + entity.name + "] " + c.GetType().ToString());
                if (c is Entity) continue;
                entity.ComponentBits.set(ComponentTypeManager.GetIndexFor(c.GetType()), false);
                entity.Scene.EntityProcessors.onComponentRemoved(entity);
                GameObject.Destroy(c);
            }
            GameObject.Destroy(entity.GameObject);
        }
    }
}

