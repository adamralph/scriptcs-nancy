// <copyright file="DefaultNancyPackBootstrapper.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Common.Logging;
    using global::Nancy;
    using global::Nancy.Bootstrapper;
    using global::Nancy.TinyIoc;

    [CLSCompliant(false)]
    public class DefaultNancyPackBootstrapper : DefaultNancyBootstrapper
    {
        private static readonly ILog Log = TinyIoCContainer.Current.Resolve<ILog>();
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

                Log.InfoFormat(CultureInfo.InvariantCulture, "Searching {0} assemblies for Nancy modules", assemblies.Length.ToString(CultureInfo.InvariantCulture));
                foreach (var assembly in assemblies.OrderBy(asm => asm.FullName))
                {
                    Log.DebugFormat(CultureInfo.InvariantCulture, "Searching assembly for Nancy modules: {0}", assembly.FullName);
                }

                var types = assemblies.SelectMany(assembly =>
                        assembly.GetTypes().Where(type => !type.IsAbstract && !type.IsGenericTypeDefinition && typeof(INancyModule).IsAssignableFrom(type)))
                    .ToArray();

                Log.InfoFormat(CultureInfo.InvariantCulture, "Found {0} Nancy module(s)", types.Length.ToString(CultureInfo.InvariantCulture));
                foreach (var type in types)
                {
                    Log.DebugFormat(CultureInfo.InvariantCulture, "Found Nancy module: {0}", type.FullName);
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
