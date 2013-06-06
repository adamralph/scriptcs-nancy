// <copyright file="NancyPack.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Hosting.Self;
    using ScriptCs.Contracts;

    public class NancyPack : IScriptPackContext
    {
        private static readonly ReadOnlyCollection<Uri> DefaultUrisField = new ReadOnlyCollection<Uri>(new[] { new Uri("http://localhost:8888/") }.ToList());

        private ReadOnlyCollection<Uri> uris;

        private NancyHost host;

        public NancyPack()
        {
            this.Reset();
        }

        public static IEnumerable<Uri> DefaultUris
        {
            get { return DefaultUrisField; }
        }

        [CLSCompliant(false)]
        public INancyBootstrapper Boot { get; set; }

        public IEnumerable<Uri> Uris
        {
            get
            {
                return this.uris;
            }

            set
            {
                Guard.AgainstNullArgument("value", value);

                if (value.Any(uri => uri == null))
                {
                    throw new ArgumentException("At least one of the URIs is null.");
                }

                if (value.Any(uri => !uri.ToString().EndsWith("/")))
                {
                    throw new ArgumentException("Only Uri prefixes ending in '/' are allowed.", "value");
                }

                this.uris = new ReadOnlyCollection<Uri>(value.ToList());
            }
        }

        // TODO (Adam): make public when https://github.com/scriptcs/scriptcs/issues/288 is released
        // i.e. when https://github.com/scriptcs/scriptcs/blob/master/src/ScriptCs/packages.config points to ServiceStack.Text 3.9.47
        // and latest master has been pushed to Chocolatey
        ////[CLSCompliant(false)]
        internal HostConfiguration Config { get; set; }

        [CLSCompliant(false)]
        public NancyPack Use(INancyBootstrapper bootstrapper)
        {
            this.Boot = bootstrapper;
            return this;
        }

        [CLSCompliant(false)]
        public NancyPack Use(HostConfiguration configuration)
        {
            this.Config = configuration;
            return this;
        }

        public NancyPack At(params Uri[] uris)
        {
            this.Uris = uris;
            return this;
        }

        public NancyPack At(params string[] uriStrings)
        {
            this.Uris = uriStrings.Select(uriString => new Uri(uriString));
            return this;
        }

        public NancyPack At(params int[] ports)
        {
            this.Uris = ports.Select(port =>
                new Uri(string.Format(CultureInfo.InvariantCulture, "http://localhost:{0}/", port.ToString(CultureInfo.InvariantCulture))));

            return this;
        }

        public NancyPack Reset()
        {
            return this.ResetUris().ResetConfig().ResetBoot();
        }

        public NancyPack ResetBoot()
        {
            this.Boot = new DefaultNancyPackBootstrapper();
            return this;
        }

        public NancyPack ResetConfig()
        {
            this.Config = new HostConfiguration();
            return this;
        }

        public NancyPack ResetUris()
        {
            this.Uris = DefaultUris;
            return this;
        }

        public void Host()
        {
            this.Go();
            Console.WriteLine("Press any key to stop hosting Nancy");
            Console.ReadKey();
            this.Stop();
        }

        public NancyPack Go()
        {
            this.Stop();

            this.host = this.Config != null
                ? new NancyHost(this.Boot, this.Config, this.uris.ToArray())
                : new NancyHost(this.Boot, this.uris.ToArray());

            this.host.Start();

            if (!this.uris.Any())
            {
                Console.WriteLine("NOT hosting Nancy at any URL");
            }
            else
            {
                foreach (var uri in this.uris)
                {
                    Console.WriteLine("Hosting Nancy at: " + uri.ToString());
                }
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
