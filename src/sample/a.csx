    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using global::Nancy.Bootstrapper;
    using global::Nancy.Hosting.Self;
    using ScriptCs.Contracts;

    var n = new NancyPack();
    Console.WriteLine("Ready at {0}", DateTime.Now);

    public partial class NancyPack
    {
        public Action Foo { get { return () => { }; } }
    }