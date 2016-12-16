﻿using System;


namespace Binocle.AI.BehaviorTrees
{
    /// <summary>
    /// the parallel task will run each child task until a child task returns failure. The difference is that the parallel task will run all of
    /// its children tasks simultaneously versus running each task one at a time. Like the sequence class, the parallel task will return
    /// success once all of its children tasks have returned success. If one tasks returns failure the parallel task will end all of the child
    /// tasks and return failure.
    /// </summary>
    public class Parallel<T> : Composite<T>
    {
        public override TaskStatus update(T context)
        {
            var didAllSucceed = true;
            for (var i = 0; i < _children.Count; i++)
            {
                var child = _children[i];
                child.tick(context);

                // if any child fails the whole branch fails
                if (child.status == TaskStatus.Failure)
                    return TaskStatus.Failure;
                // if all children didn't succeed, we're not done yet
                else if (child.status != TaskStatus.Success)
                    didAllSucceed = false;
            }

            if (didAllSucceed)
                return TaskStatus.Success;

            return TaskStatus.Running;
        }

        protected override void handleConditionalAborts(T context)
        {
        }

    }
}

