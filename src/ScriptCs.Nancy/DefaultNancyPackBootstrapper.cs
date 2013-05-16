// <copyright file="DefaultNancyPackBootstrapper.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using global::Nancy;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Hosting.Self;
    using global::Nancy.TinyIoc;

    [CLSCompliant(false)]
    public class DefaultNancyPackBootstrapper : DefaultNancyBootstrapper
    {
        private Assembly[] assemblies;

        public DefaultNancyPackBootstrapper(params Assembly[] assemblies)
        {
            this.assemblies = assemblies.Where(assembly => assembly != null).ToArray();
        }

        public IEnumerable<Assembly> Assemblies
        {
            get { return this.assemblies.Select(x => x); }

            set { this.assemblies = value == null ? new Assembly[0] : value.Where(assembly => assembly != null).ToArray(); }
        }

        protected override IEnumerable<ModuleRegistration> Modules
        {
            get
            {
                var assembliesToSearch = this.assemblies
                    .Concat(new StackTrace().GetFrames()
                        .Select(frame => frame.GetMethod().DeclaringType.Assembly))
                    .Concat(AppDomain.CurrentDomain.GetAssemblies())
                    .Distinct()
                    .Except(new[] { this.GetType().Assembly, typeof(DefaultNancyBootstrapper).Assembly, typeof(NancyHost).Assembly }).ToArray();

                foreach (var assembly in assembliesToSearch)
                {
                    Console.WriteLine("Searching assembly: {0}", assembly.FullName);
                }

                var types = assembliesToSearch.SelectMany(assembly =>
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

        // NOTE (adamralph): switch off auto-registration by not calling base
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
        }
    }
}
