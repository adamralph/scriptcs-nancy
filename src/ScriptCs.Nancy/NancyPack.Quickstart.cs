// <copyright file="NancyPack.Quickstart.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Reflection;
    using ScriptCs.Contracts;

    public partial class NancyPack : IScriptPackContext
    {
        private static readonly Uri DefaultUri = new Uri("http://localhost:8888/");

        public void Host()
        {
            this.Host(DefaultUri);
        }

        public void Host(params Assembly[] moduleAssemblies)
        {
            this.Host(DefaultUri, moduleAssemblies);
        }

        public void Host(params Type[] moduleTypes)
        {
            this.Host(DefaultUri, moduleTypes);
        }
   }
}
