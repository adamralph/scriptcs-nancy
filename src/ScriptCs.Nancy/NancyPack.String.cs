// <copyright file="NancyPack.String.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Linq;
    using ScriptCs.Contracts;

    public partial class NancyPack : IScriptPackContext
    {
        public void Host(params string[] baseUriStrings)
        {
            this.Host(baseUriStrings.Select(baseUriString => new Uri(baseUriString)).ToArray());
        }
    }
}
