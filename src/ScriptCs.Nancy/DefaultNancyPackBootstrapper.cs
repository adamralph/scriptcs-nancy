// <copyright file="DefaultNancyPackBootstrapper.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Nancy;
    using global::Nancy.Bootstrapper;
    using global::Nancy.TinyIoc;

    [CLSCompliant(false)]
    public class DefaultNancyPackBootstrapper : DefaultNancyBootstrapper
    {
        private static readonly string[] IgnoredAssemblyPrefixes = new[]
            {
                "Autofac,",
                "Autofac.",
                "Common.Logging",
                "log4net,",
                "Nancy,",
                "Nancy.",
                "NuGet.",
                "PowerArgs,",
                "Roslyn.",
                "scriptcs,",
                "ScriptCs.",
                "ServiceStack.",
            };

        protected override IEnumerable<Func<System.Reflection.Assembly, bool>> AutoRegisterIgnoredAssemblies
        {
            get
            {
                return base.AutoRegisterIgnoredAssemblies
                    .Concat(new Func<Assembly, bool>[]
                        {
                            assembly => assembly == typeof(DefaultNancyBootstrapper).Assembly,
                            assembly => IgnoredAssemblyPrefixes.Any(prefix => assembly.FullName.StartsWith(prefix, StringComparison.Ordinal)),
                        });
            }
        }

        protected override IEnumerable<ModuleRegistration> Modules
        {
            get
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => !this.AutoRegisterIgnoredAssemblies.Any(ignore => ignore(assembly))).ToArray();

                foreach (var assembly in assemblies.OrderBy(asm => asm.FullName))
                {
                    Console.WriteLine("Searching assembly: {0}", assembly.FullName);
                }

                var types = assemblies.SelectMany(assembly =>
                        assembly.GetTypes().Where(type => !type.IsAbstract && !type.IsGenericTypeDefinition && typeof(INancyModule).IsAssignableFrom(type)))
                    .ToArray();

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

                return types.Select(type => new ModuleRegistration(type));
            }
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(x => x.RouteDescriptionProvider = typeof(RouteDescriptionProvider)); }
        }

        protected override IRootPathProvider RootPathProvider
        {
            get { return new PathProvider(); }
        }
    }
}
