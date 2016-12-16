using System;
using UnityEngine.Assertions;

namespace Binocle.AI.BehaviorTrees
{
    /// <summary>
    /// will keep executing its child task until the child task returns success
    /// </summary>
    public class UntilSuccess<T> : Decorator<T>
    {
        public override TaskStatus update(T context)
        {
            Assert.IsNotNull(child, "child must not be null");

            var status = child.tick(context);

            if (status != TaskStatus.Success)
                return TaskStatus.Running;

            return TaskStatus.Success;
        }
    }
}

