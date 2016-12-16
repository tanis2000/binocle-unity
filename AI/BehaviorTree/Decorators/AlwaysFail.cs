using System;
using UnityEngine.Assertions;

namespace Binocle.AI.BehaviorTrees
{
    /// <summary>
    /// will always return failure except when the child task is running
    /// </summary>
    public class AlwaysFail<T> : Decorator<T>
    {
        public override TaskStatus update(T context)
        {
            Assert.IsNotNull(child, "child must not be null");

            var status = child.update(context);

            if (status == TaskStatus.Running)
                return TaskStatus.Running;

            return TaskStatus.Failure;
        }
    }
}

