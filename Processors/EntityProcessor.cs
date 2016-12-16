using System.Collections.Generic;
using UnityEngine;

namespace Binocle.Processors
{
    public class EntityProcessor
    {
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        private bool _enabled = true;

        protected GameObject[] _gameObjects;

        protected Matcher _matcher;

        public Matcher matcher
        {
            get { return _matcher; }
        }

        protected List<Entity> _entities = new List<Entity>();

        public Game Game
        {
            get { return _game; }
            set { _game = value; }
        }

        private Game _game;

        public Scene Scene
        {
            get { return _scene; }
            set { _scene = value; }
        }

        private Scene _scene;

        public EntityProcessor()
        {
            _matcher = Matcher.Empty();
        }

        public EntityProcessor(Matcher matcher)
        {
            _matcher = matcher;
        }

        public virtual void OnChange(Entity entity)
        {
            var contains = _entities.Contains(entity);
            var interest = _matcher.IsInterested(entity);

            if (interest && !contains)
                Add(entity);
            else if (!interest && contains)
                Remove(entity);
        }


        public virtual void Add(Entity entity)
        {
            _entities.Add(entity);
            OnAdded(entity);
        }


        public virtual void Remove(Entity entity)
        {
            _entities.Remove(entity);
            OnRemoved(entity);
        }


        public virtual void OnAdded(Entity entity)
        {
        }


        public virtual void OnRemoved(Entity entity)
        {
        }

        protected virtual void Begin()
        {
        }

        protected virtual void End()
        {
        }

        public virtual void Update()
        {
            if (!_enabled)
                return;
            Begin();
            Process(_entities);
            End();
        }

        public virtual void Process(List<Entity> entities)
        {
        }

    }
}

