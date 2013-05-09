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
        // quickstart
        public void Host(params Type[] moduleTypes)
        {
            this.Host(8888, moduleTypes);
        }

        public void Host(int port, params Type[] moduleTypes)
        {
            this.Host(string.Concat("http://localhost:", port.ToString(CultureInfo.InvariantCulture), "/"), moduleTypes);
        }

        // URI strings
        public void Host(string baseUriString, params Type[] moduleTypes)
        {
            this.Host(new Uri(baseUriString), moduleTypes);
        }

        [CLSCompliant(false)]
        public void Host(HostConfiguration hostConfiguration, string baseUriString, params Type[] moduleTypes)
        {
            this.Host(hostConfiguration, new Uri(baseUriString), moduleTypes);
        }

        [CLSCompliant(false)]
        public void Host(INancyBootstrapper bootstrapper, string baseUriString)
        {
            this.Host(bootstrapper, new Uri(baseUriString));
        }

        [CLSCompliant(false)]
        public void Host(INancyBootstrapper bootstrapper, HostConfiguration hostConfiguration, string baseUriString)
        {
            this.Host(bootstrapper, hostConfiguration, new Uri(baseUriString));
        }

        // main
        public void Host(Uri baseUri, params Type[] moduleTypes)
        {
            this.Host(new Bootstrapper(moduleTypes), baseUri);
        }

        [CLSCompliant(false)]
        public void Host(HostConfiguration hostConfiguration, Uri baseUri, params Type[] moduleTypes)
        {
            this.Host(new Bootstrapper(moduleTypes), hostConfiguration, baseUri);
        }

        [CLSCompliant(false)]
        public void Host(INancyBootstrapper bootstrapper, Uri baseUri)
        {
            using (var host = new NancyHost(bootstrapper, baseUri))
            {
                Run(baseUri, host);
            }
        }

        [CLSCompliant(false)]
        public void Host(INancyBootstrapper bootstrapper, HostConfiguration hostConfiguration, Uri baseUri)
        {
            using (var host = new NancyHost(bootstrapper, hostConfiguration, baseUri))
            {
                Run(baseUri, host);
            }
        }

        private static void Run(Uri baseUri, NancyHost host)
        {
            host.Start();

            Console.WriteLine("Nancy is running at " + baseUri.ToString());
            Console.WriteLine("Press any key to end");
            Console.ReadKey();

            host.Stop();
        }
    }
}