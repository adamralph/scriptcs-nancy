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