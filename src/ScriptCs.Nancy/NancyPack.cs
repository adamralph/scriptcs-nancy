// <copyright file="NancyPack.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Hosting.Self;
    using ScriptCs.Contracts;

    public partial class NancyPack : IScriptPackContext
    {
        public void Host(params Uri[] baseUris)
        {
            this.Host(new DefaultNancyPackBootstrapper(Assembly.GetCallingAssembly()), baseUris);
        }

        public void Host(IEnumerable<Assembly> assemblies, params Uri[] baseUris)
        {
            this.Host(new DefaultNancyPackBootstrapper(assemblies.Concat(new[] { Assembly.GetCallingAssembly() }).ToArray()), baseUris);
        }

        [CLSCompliant(false)]
        public void Host(HostConfiguration configuration, params Uri[] baseUris)
        {
            this.Host(new DefaultNancyPackBootstrapper(Assembly.GetCallingAssembly()), configuration, baseUris);
        }

        [CLSCompliant(false)]
        public void Host(HostConfiguration configuration, IEnumerable<Assembly> assemblies, params Uri[] baseUris)
        {
            this.Host(new DefaultNancyPackBootstrapper(assemblies.Concat(new[] { Assembly.GetCallingAssembly() }).ToArray()), configuration, baseUris);
        }

        [CLSCompliant(false)]
        public void Host(INancyBootstrapper bootstrapper, params Uri[] baseUris)
        {
            Run(bootstrapper, null, baseUris);
        }

        [CLSCompliant(false)]
        public void Host(INancyBootstrapper bootstrapper, HostConfiguration configuration, params Uri[] baseUris)
        {
            Run(bootstrapper, configuration, baseUris);
        }

        private static void Run(INancyBootstrapper bootstrapper, HostConfiguration configuration, params Uri[] baseUris)
        {
            using (var host = configuration == null
                ? new NancyHost(bootstrapper, baseUris)
                : new NancyHost(bootstrapper, configuration, baseUris))
            {
                host.Start();

                foreach (var baseUri in baseUris)
                {
                    Console.WriteLine("Hosting Nancy at: " + baseUri.ToString());
                }

                if (baseUris.Length == 0)
                {
                    Console.WriteLine("NOT hosting Nancy at any URL");
                }

                Console.WriteLine("Press any key to end");
                Console.ReadKey();

                host.Stop();
            }
        }
    }
}
