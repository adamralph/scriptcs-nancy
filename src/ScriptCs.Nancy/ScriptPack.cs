// <copyright file="ScriptPack.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using ScriptCs.Contracts;

    public class ScriptPack : IScriptPack
    {
        [CLSCompliant(false)]
        public void Initialize(IScriptPackSession session)
        {
            session.ImportNamespace("Nancy");
            session.ImportNamespace("Nancy.Bootstrapper");
            session.ImportNamespace("Nancy.Conventions");
            session.ImportNamespace("Nancy.Cryptography");
            session.ImportNamespace("Nancy.ErrorHandling");
            session.ImportNamespace("Nancy.Hosting.Self");
            session.ImportNamespace("Nancy.ModelBinding");
            session.ImportNamespace("Nancy.Security");
            session.ImportNamespace("Nancy.Validation");
        }

        [CLSCompliant(false)]
        public IScriptPackContext GetContext()
        {
            return new NancyPack();
        }

        public void Terminate()
        {
        }
    }
}
