// <copyright file="DefaultModule.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Collections.Concurrent;
    using global::Nancy;

    [CLSCompliant(false)]
    public class DefaultModule : NancyModule
    {
        private static ConcurrentQueue<Action<DefaultModule>> constructionActions =
            new ConcurrentQueue<Action<DefaultModule>>();

        public DefaultModule()
        {
            foreach (var action in constructionActions)
            {
                action(this);
            }
        }

        public static bool HasConstructorActions
        {
            get { return constructionActions.Count != 0; }
        }

        public static void AddToConstructor(Action<DefaultModule> action)
        {
            constructionActions.Enqueue(action);
        }
    }
}
