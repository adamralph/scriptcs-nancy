// <copyright file="DefaultNancyPackBootstrapper.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Nancy;
    using global::Nancy.Bootstrapper;
    using global::Nancy.TinyIoc;

    [CLSCompliant(false)]
    public class DefaultNancyPackBootstrapper : DefaultNancyBootstrapper
    {
        private readonly Type[] moduleTypes;

        public DefaultNancyPackBootstrapper(params Type[] moduleTypes)
        {
            this.moduleTypes = moduleTypes.ToArray();
        }

        protected override IEnumerable<ModuleRegistration> Modules
        {
            get { return this.moduleTypes.Select(type => new ModuleRegistration(type)); }
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
