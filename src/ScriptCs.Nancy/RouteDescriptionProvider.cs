// <copyright file="RouteDescriptionProvider.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using global::Nancy;
    using global::Nancy.Routing;

    public class RouteDescriptionProvider : IRouteDescriptionProvider
    {
        [CLSCompliant(false)]
        public string GetDescription(INancyModule module, string path)
        {
            return string.Empty;
        }
    }
}
