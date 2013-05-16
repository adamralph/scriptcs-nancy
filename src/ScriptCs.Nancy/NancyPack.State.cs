// <copyright file="NancyPack.State.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Globalization;
    using System.Linq;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Hosting.Self;
    using ScriptCs.Contracts;

    // TODO: consider removing all but basic Host overloads after state control is in place
    // TODO: auto-regeneration when state is changed
    // TODO: auto-regeneration when files on disk are changed (when we have module loading in place)
    // TODO: auto-regeneration when a module is added??? either via #load or declaration
    // TODO: Nancy.Wait() - tells Nancy to wait to regenerate until Go() is called
    public partial class NancyPack : IScriptPackContext
    {
        private static readonly Uri[] DefaultUris = new[] { new Uri("http://localhost:8888/") };
        private readonly HostConfiguration config = new HostConfiguration();

        private INancyBootstrapper boot = new DefaultNancyPackBootstrapper();
        private Uri[] uris = DefaultUris.ToArray();
        private NancyHost host;

        // TODO (Adam): make public when https://github.com/scriptcs/scriptcs/issues/288 is resolved
        ////[CLSCompliant(false)]
        ////public HostConfiguration Configuration { get { return this.config; } }

        [CLSCompliant(false)]
        public INancyBootstrapper Boot
        {
            get { return this.boot; }
            set { this.boot = value ?? new DefaultNancyPackBootstrapper(); }
        }

        public Uri[] Uris
        {
            get { return this.uris.Select(uri => uri).ToArray(); }

            set
            {
                if (value != null && value.Any(uri => !uri.ToString().EndsWith("/")))
                {
                    throw new ArgumentException("Only Uri prefixes ending in '/' are allowed.", "value");
                }

                this.uris = value == null || !value.Any()
                    ? DefaultUris.ToArray()
                    : value.ToArray();
            }
        }

        public NancyPack Use(params Uri[] uris)
        {
            this.Uris = uris;
            return this;
        }

        public NancyPack Use(params string[] uriStrings)
        {
            this.Uris = uriStrings.Select(uriString => new Uri(uriString)).ToArray();
            return this;
        }

        public NancyPack Use(params int[] ports)
        {
            this.Uris = ports.Select(port =>
                new Uri(string.Format(CultureInfo.InvariantCulture, "http://localhost:{0}/", port.ToString(CultureInfo.InvariantCulture)))).ToArray();

            return this;
        }

        public NancyPack Reset()
        {
            this.Boot = null;
            this.Uris = null;
            return this;
        }

        public NancyPack Go()
        {
            this.Stop();

            this.host = new NancyHost(this.boot, this.config, this.uris);
            this.host.Start();

            foreach (var baseUri in this.uris)
            {
                Console.WriteLine("Hosting Nancy at: " + baseUri.ToString());
            }

            if (this.uris.Length == 0)
            {
                Console.WriteLine("NOT hosting Nancy at any URL");
            }

            return this;
        }

        public NancyPack Stop()
        {
            if (this.host != null)
            {
                this.host.Stop();
                this.host.Dispose();
                this.host = null;
                Console.WriteLine("Stopped hosting Nancy");
            }

            return this;
        }
    }
}
