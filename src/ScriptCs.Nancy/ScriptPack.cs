// <copyright file="ScriptPack.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics.CodeAnalysis;
    using Common.Logging;
    using global::Nancy.TinyIoc;
    using ScriptCs.Contracts;

    [CLSCompliant(false)]
    public class ScriptPack : ScriptPack<NancyPack>
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Following script pack prescribed pattern")]
        [ImportingConstructor]
        public ScriptPack(ILog log, IConsole console)
        {
            TinyIoCContainer.Current.Register<ILog>(log);
            TinyIoCContainer.Current.Register<IConsole>(console);
            this.Context = new NancyPack();
        }

        [CLSCompliant(false)]
        public override void Initialize(IScriptPackSession session)
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
    }
}
