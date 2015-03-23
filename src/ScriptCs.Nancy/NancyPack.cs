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
    using Microsoft.Owin.Hosting;
    using Owin;

    public class NancyPack : IScriptPackContext, IDisposable
    {
        private static readonly ReadOnlyCollection<Uri> DefaultUrisField = new ReadOnlyCollection<Uri>(new[] { new Uri("http://localhost:8888/") }.ToList());

        private ReadOnlyCollection<Uri> uris;
        private IDisposable host;

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

        public StartOptions StartOptions
        { 
            get;
            set; 
        }

        public Action<IAppBuilder> Startup
        {
            get;
            set;
        }

        public NancyPack Go()
        {
            this.Stop();

            try
            {
                if (this.StartOptions == null)
                {
                    var opt = new StartOptions();
                    foreach (var item in this.uris)
                    {
                        opt.Urls.Add(item.OriginalString);
                    }
                    this.StartOptions = opt;
                }

                if (this.Startup == null)
                {
                    this.host = WebApp.Start(this.StartOptions, app => app.UseNancy(options => options.Bootstrapper = this.Boot));
                }
                else
                {
                    this.host = WebApp.Start(this.StartOptions, app => this.Startup(app));
                }
            }
            catch (Exception)
            {
                this.host = null;
                throw;
            }

            if (!this.StartOptions.Urls.Any())
            {
                Console.WriteLine("NOT hosting Nancy at any URL");
            }
            else
            {
                foreach (var uri in this.StartOptions.Urls)
                {
                    Console.WriteLine("Hosting Nancy with OWIN at: " + uri.ToString());
                }
            }

            return this;
        }

        public NancyPack Stop()
        {
            if (this.host != null)
            {
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
