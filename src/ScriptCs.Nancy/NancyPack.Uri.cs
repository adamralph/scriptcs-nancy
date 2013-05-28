// <copyright file="NancyPack.Uri.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Hosting.Self;
    using ScriptCs.Contracts;

    public partial class NancyPack : IScriptPackContext
    {
        [CLSCompliant(false)]
        public void Host(Uri baseUri, HostConfiguration configuration)
        {
            this.Host(configuration, baseUri);
        }

        [CLSCompliant(false)]
        public void Host(Uri baseUri, INancyBootstrapper bootstrapper)
        {
            this.Host(bootstrapper, baseUri);
        }

        [CLSCompliant(false)]
        public void Host(Uri baseUri, INancyBootstrapper bootstrapper, HostConfiguration configuration)
        {
            this.Host(bootstrapper, configuration, baseUri);
        }
    }
}
