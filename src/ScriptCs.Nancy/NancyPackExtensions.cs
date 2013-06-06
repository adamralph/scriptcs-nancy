﻿// <copyright file="NancyPackExtensions.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Globalization;
    using System.Linq;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Hosting.Self;

    public static class NancyPackExtensions
    {
        [CLSCompliant(false)]
        public static NancyPack Use(this NancyPack pack, INancyBootstrapper bootstrapper)
        {
            pack.Boot = bootstrapper;
            return pack;
        }

        [CLSCompliant(false)]
        public static NancyPack Use(this NancyPack pack, HostConfiguration configuration)
        {
            pack.Config = configuration;
            return pack;
        }

        public static NancyPack At(this NancyPack pack, params Uri[] uris)
        {
            pack.Uris = uris;
            return pack;
        }

        public static NancyPack At(this NancyPack pack, params string[] uriStrings)
        {
            pack.Uris = uriStrings.Select(uriString => new Uri(uriString));
            return pack;
        }

        public static NancyPack At(this NancyPack pack, params int[] ports)
        {
            pack.Uris = ports.Select(port =>
                new Uri(string.Format(CultureInfo.InvariantCulture, "http://localhost:{0}/", port.ToString(CultureInfo.InvariantCulture))));

            return pack;
        }

        public static NancyPack Reset(this NancyPack pack)
        {
            return pack.ResetUris().ResetConfig().ResetBoot();
        }

        public static NancyPack ResetBoot(this NancyPack pack)
        {
            pack.Boot = new DefaultNancyPackBootstrapper();
            return pack;
        }

        public static NancyPack ResetConfig(this NancyPack pack)
        {
            pack.Config = new HostConfiguration();
            return pack;
        }

        public static NancyPack ResetUris(this NancyPack pack)
        {
            pack.Uris = NancyPack.DefaultUris;
            return pack;
        }

        public static void Host(this NancyPack pack)
        {
            pack.Go();
            Console.WriteLine("Press any key to stop hosting Nancy");
            Console.ReadKey();
            pack.Stop();
        }
    }
}
