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
        private readonly Assembly[] assemblies;

        public DefaultNancyPackBootstrapper(params Assembly[] assemblies)
        {
            this.assemblies = assemblies.Concat(new[] { Assembly.GetCallingAssembly() }).Distinct().ToArray();
        }

        protected override IEnumerable<ModuleRegistration> Modules
        {
            get
            {
                foreach (var assembly in this.assemblies)
                {
                    Console.WriteLine("Searching assembly: {0}", assembly.FullName);
                }

                var types = this.assemblies
                    .SelectMany(assembly =>
                        assembly
                            .GetTypes()
                            .Where(type => typeof(INancyModule).IsAssignableFrom(type)))
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

        // NOTE (adamralph): switch off auto-registration by not calling base
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
        }
    }
}
