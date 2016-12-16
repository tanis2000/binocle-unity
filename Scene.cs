using Binocle.Processors;
using UnityEngine;

namespace Binocle
{
    public class Scene : MonoBehaviour
    {
        private Game _game;

        public Game Game
        {
            get { return _game; }
            set { _game = value; }
        }

        public EntityProcessorList EntityProcessors
        {
            get { return _entityProcessors; }
        }

        private EntityProcessorList _entityProcessors = new EntityProcessorList();

        public virtual void Awake()
        {
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
            if (_entityProcessors != null)
                _entityProcessors.update();
        }

        public void AddEntityProcessor(EntityProcessor processor)
        {
            processor.Game = _game;
            processor.Scene = this;
            _entityProcessors.add(processor);
        }

        /// <summary>
        /// Gets an EntitySystem processor
        /// </summary>
        /// <returns>The processor.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T GetEntityProcessor<T>() where T : EntityProcessor
        {
            return _entityProcessors.getProcessor<T>();
        }

        public Entity CreateEntity(string name)
        {
            var go = new GameObject(name);
            var e = go.AddComponent<Entity>();
            e.GameObject = go;
            e.Scene = this;
            e.GameObject.transform.SetParent(this.transform);
            return e;
        }

        public virtual void Remove()
        {
        }

    }
}

