// <copyright file="ScriptPack.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ScriptCs.Contracts;

    [CLSCompliant(false)]
    public class ScriptPack : ScriptPack<NancyPack>
    {
        private readonly ManualResetEventSlim isRunningBackgroundTask = new ManualResetEventSlim(true);

        public bool IsRunningBackgroundTask
        {
            get
            {
                return !this.isRunningBackgroundTask.IsSet;
            }

            set
            {
                if (value)
                {
                    this.isRunningBackgroundTask.Reset();
                }
                else
                {
                    this.isRunningBackgroundTask.Set();
                }
            }
        }

        public override Task BackgroundTask
        {
            get
            {
                return !this.isRunningBackgroundTask.IsSet
                    ? Task.Factory.StartNew(() => this.isRunningBackgroundTask.Wait())
                    : null;
            }
        }

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

            this.Context = new NancyPack(this);
        }
    }
}
