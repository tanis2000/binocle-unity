using System.Collections.Generic;


namespace Binocle.Processors
{
    public class EntityProcessorList
    {
        protected List<EntityProcessor> _processors = new List<EntityProcessor>();


        public void add(EntityProcessor processor)
        {
            _processors.Add(processor);
        }


        public void remove(EntityProcessor processor)
        {
            _processors.Remove(processor);
        }


        public virtual void onComponentAdded(Entity entity)
        {
            notifyEntityChanged(entity);
        }


        public virtual void onComponentRemoved(Entity entity)
        {
            notifyEntityChanged(entity);
        }


        public virtual void onEntityAdded(Entity entity)
        {
            notifyEntityChanged(entity);
        }


        public virtual void onEntityRemoved(Entity entity)
        {
            removeFromProcessors(entity);
        }


        protected virtual void notifyEntityChanged(Entity entity)
        {
            for (var i = 0; i < _processors.Count; i++)
                _processors[i].OnChange(entity);
        }

        protected virtual void removeFromProcessors(Entity entity)
        {
            for (var i = 0; i < _processors.Count; i++)
                _processors[i].Remove(entity);
        }


        public void begin()
        { }


        public void update()
        {
            for (var i = 0; i < _processors.Count; i++)
                _processors[i].Update();
        }


        public void end()
        { }


        public T getProcessor<T>() where T : EntityProcessor
        {
            for (var i = 0; i < _processors.Count; i++)
            {
                var processor = _processors[i];
                if (processor is T)
                    return processor as T;
            }

            return null;
        }

    }
}

