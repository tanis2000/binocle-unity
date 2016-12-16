using System;
using UnityEngine.Assertions;

namespace Binocle.AI.BehaviorTrees
{
    /// <summary>
    /// inverts the result of the child node
    /// </summary>
    public class Inverter<T> : Decorator<T>
    {
        public override TaskStatus update(T context)
        {
            Assert.IsNotNull(child, "child must not be null");

            var status = child.tick(context);

            if (status == TaskStatus.Success)
                return TaskStatus.Failure;

            if (status == TaskStatus.Failure)
                return TaskStatus.Success;

            return TaskStatus.Running;
        }
    }
}

