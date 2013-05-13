// <copyright file="NancyPack.String.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using ScriptCs.Contracts;

    public partial class NancyPack : IScriptPackContext
    {
        public void Host(params string[] baseUriStrings)
        {
            this.Host(baseUriStrings.Select(baseUriString => new Uri(baseUriString)).ToArray());
        }

        public void Host(string baseUriString, IEnumerable<Assembly> assemblies)
        {
            this.Host(assemblies, new Uri(baseUriString));
        }
    }
}
