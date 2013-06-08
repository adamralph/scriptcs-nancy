// <copyright file="FooModule.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace SampleModules
{
    using System;
    using Nancy;

    [CLSCompliant(false)]
    public class FooModule : NancyModule
    {
        public FooModule()
        {
            Get["/foo"] = _ => "bar";
        }
    }
}
