// <copyright file="AssemblyExtensions.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Nancy;

    internal static class AssemblyExtensions
    {
        public static Type[] FindNancyModuleTypes(this IEnumerable<Assembly> assemblies)
        {
            var types = assemblies.SelectMany(assembly => assembly.GetTypes().Where(type => typeof(INancyModule).IsAssignableFrom(type))).ToArray();
            if (types.Length == 0)
            {
                Console.WriteLine("Didn't find any Nancy modules.");
            }
            else
            {
                foreach (var type in types)
                {
                    Console.WriteLine("Found Nancy module: {0}", type.FullName);
                }
            }

            return types;
        }
    }
}
