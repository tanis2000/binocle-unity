using System.Collections.Generic;

namespace Binocle.Processors
{
    /// <summary>
    /// Basic entity processing system. Use this as the base for processing many entities with specific components
    /// </summary>
    public abstract class BaseEntityProcessor : EntityProcessor
    {
        public BaseEntityProcessor(Matcher matcher) : base(matcher)
        { }


        /// <summary>
        /// Processes a specific entity. It's called for all the entities in the list.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public abstract void Process(Entity entity);


        /// <summary>
        /// Goes through all the entities of this system and processes them one by one
        /// </summary>
        /// <param name="entities">Entities.</param>
        public override void Process(List<Entity> entities)
        {
            for (var i = 0; i < entities.Count; i++)
                Process(entities[i]);
        }
    }
}

