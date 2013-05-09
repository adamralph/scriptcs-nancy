// <copyright file="PathProvider.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.IO;
    using global::Nancy;

    public class PathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Path.Combine(@"..\..\", Environment.CurrentDirectory);
        }
    }
}
