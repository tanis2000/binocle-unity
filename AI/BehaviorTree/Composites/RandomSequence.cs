﻿using System;
using Binocle;

namespace Binocle.AI.BehaviorTrees
{
    /// <summary>
    /// Same as Sequence except it shuffles the children when started
    /// </summary>
    public class RandomSequence<T> : Sequence<T>
    {
        public override void onStart()
        {
            _children.shuffle();
        }
    }
}

