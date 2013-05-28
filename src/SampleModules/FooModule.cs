// <copyright file="FooModule.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace SampleModules
{
    using Nancy;

    public class FooModule : NancyModule
    {
        public FooModule()
        {
            Get["/foo"] = _ => "bar";
        }
    }
}
