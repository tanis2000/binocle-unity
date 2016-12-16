using System;
using UnityEngine;

namespace Binocle.AI.BehaviorTrees
{
    /// <summary>
    /// simple task which will output the specified text and return success. It can be used for debugging.
    /// </summary>
    public class LogAction<T> : Behavior<T>
    {
        /// <summary>
        /// text to log
        /// </summary>
        public string text;

        /// <summary>
        /// is this text an error
        /// </summary>
        public bool isError;


        public LogAction(string text)
        {
            this.text = text;
        }


        public override TaskStatus update(T context)
        {
            if (isError)
                Debug.LogError(text);
            else
                Debug.Log(text);

            return TaskStatus.Success;
        }
    }
}

