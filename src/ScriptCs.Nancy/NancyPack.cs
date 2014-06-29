// <copyright file="NancyPack.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Hosting.Self;
    using ScriptCs.Contracts;

    public class NancyPack : IScriptPackContext, IDisposable
    {
        private static readonly ReadOnlyCollection<Uri> DefaultUrisField = new ReadOnlyCollection<Uri>(new[] { new Uri("http://localhost:8888/") }.ToList());

        private ReadOnlyCollection<Uri> uris;
        private NancyHost host;

        public NancyPack()
        {
            this.Reset();
        }

        ~NancyPack()
        {
            this.Dispose(false);
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

                if (value.Any(uri => !uri.ToString().EndsWith("/", StringComparison.Ordinal)))
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

        public NancyPack Go()
        {
            this.Stop();

            this.host = this.Config != null
                ? new NancyHost(this.Boot, this.Config, this.uris.ToArray())
                : new NancyHost(this.Boot, this.uris.ToArray());

            try
            {
                this.host.Start();
            }
            catch (Exception)
            {
                this.host = null;
                throw;
            }

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

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Stop();
            }
        }
    }
}
