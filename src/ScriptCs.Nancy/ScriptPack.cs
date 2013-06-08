// <copyright file="ScriptPack.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using ScriptCs.Contracts;

    public class ScriptPack : IScriptPack
    {
        [CLSCompliant(false)]
        public void Initialize(IScriptPackSession session)
        {
            Guard.AgainstNullArgument("session", session);

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

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Factory method.")]
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
