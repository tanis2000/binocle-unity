﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Binocle.Processors
{
    public class ComponentTypeManager
    {
        private static Dictionary<Type, int> _componentTypesMask = new Dictionary<Type, int>();


        public static void Initialize()
        {
            var currentdomain = typeof(string).Assembly.GetType("System.AppDomain").GetProperty("CurrentDomain").GetGetMethod().Invoke(null, new object[] { });
            var getassemblies = currentdomain.GetType().GetMethod("GetAssemblies", new Type[] { });
            var assemblies = getassemblies.Invoke(currentdomain, new object[] { }) as Assembly[];

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (typeof(MonoBehaviour).IsAssignableFrom(type))
                    {
                        Add(type);
                    }
                }
            }
        }


        public static void Add(Type type)
        {
            int v;
            if (!_componentTypesMask.TryGetValue(type, out v))
                _componentTypesMask[type] = _componentTypesMask.Count;
        }


        public static int GetIndexFor(Type type)
        {
            var v = -1;
            _componentTypesMask.TryGetValue(type, out v);
            return v;
        }


        public static IEnumerable<Type> GetTypesFromBits(BitSet bits)
        {
            foreach (var keyValuePair in _componentTypesMask)
            {
                if (bits.get(keyValuePair.Value))
                    yield return keyValuePair.Key;
            }
        }
    }
}

