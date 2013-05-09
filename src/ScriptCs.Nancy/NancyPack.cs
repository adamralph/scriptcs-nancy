// <copyright file="NancyPack.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Globalization;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Hosting.Self;
    using ScriptCs.Contracts;

    public class NancyPack : IScriptPackContext
    {
        public void Host(params Type[] moduleTypes)
        {
            this.Host(8888, moduleTypes);
        }

        public void Host(int port, params Type[] moduleTypes)
        {
            this.Host(string.Concat("http://localhost:", port.ToString(CultureInfo.InvariantCulture), "/"), moduleTypes);
        }

        public void Host(string baseUriString, params Type[] moduleTypes)
        {
            this.Host(new Bootstrapper(moduleTypes), baseUriString, moduleTypes);
        }

        [CLSCompliant(false)]
        public void Host(INancyBootstrapper bootstrapper, string baseUriString, params Type[] moduleTypes)
        {
            using (var host = new NancyHost(bootstrapper, new Uri(baseUriString)))
            {
                Run(baseUriString, host);
            }
        }

        [CLSCompliant(false)]
        public void Host(HostConfiguration hostConfiguration, string baseUriString, params Type[] moduleTypes)
        {
            using (var host = new NancyHost(new Bootstrapper(moduleTypes), hostConfiguration, new Uri(baseUriString)))
            {
                Run(baseUriString, host);
            }
        }

        [CLSCompliant(false)]
        public void Host(HostConfiguration hostConfiguration, INancyBootstrapper bootstrapper, string baseUriString, params Type[] moduleTypes)
        {
            using (var host = new NancyHost(bootstrapper, hostConfiguration, new Uri(baseUriString)))
            {
                Run(baseUriString, host);
            }
        }

        private static void Run(string baseUriString, NancyHost host)
        {
            host.Start();

            Console.WriteLine("Nancy is running at " + baseUriString);
            Console.WriteLine("Press any key to end");
            Console.ReadKey();

            host.Stop();
        }
    }
}