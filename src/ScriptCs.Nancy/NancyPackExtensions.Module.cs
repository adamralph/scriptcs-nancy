// <copyright file="NancyPackExtensions.Module.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace ScriptCs.Nancy
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::Nancy;

    public static partial class NancyPackExtensions
    {
        public static NancyPack Module(this NancyPack pack, Action<DefaultModule> action)
        {
            DefaultModule.AddToConstructor(action);
            pack.RestartIfStarted();
            return pack;
        }

        public static NancyPack Delete(
            this NancyPack pack, Action<NancyModule.RouteBuilder> action)
        {
            return pack.Module(m => action(m.Delete));
        }

        public static NancyPack Get(
            this NancyPack pack, Action<NancyModule.RouteBuilder> action)
        {
            return pack.Module(m => action(m.Get));
        }

        public static NancyPack Options(
            this NancyPack pack, Action<NancyModule.RouteBuilder> action)
        {
            return pack.Module(m => action(m.Options));
        }

        public static NancyPack Patch(
            this NancyPack pack, Action<NancyModule.RouteBuilder> action)
        {
            return pack.Module(m => action(m.Patch));
        }

        public static NancyPack Post(
            this NancyPack pack, Action<NancyModule.RouteBuilder> action)
        {
            return pack.Module(m => action(m.Post));
        }

        public static NancyPack Put(
            this NancyPack pack, Action<NancyModule.RouteBuilder> action)
        {
            return pack.Module(m => action(m.Put));
        }

        public static NancyPack Delete(
            this NancyPack pack, string path, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Delete(b => b[path] = routeBuilder);
        }

        public static NancyPack Delete(
            this NancyPack pack, string path, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Delete(b => b[path, runAsync] = routeBuilder);
        }

        public static NancyPack Delete(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Delete(b => b[path, condition] = routeBuilder);
        }

        public static NancyPack Delete(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Delete(b => b[path, condition, runAsync] = routeBuilder);
        }

        public static NancyPack Get(
        this NancyPack pack, string path, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Get(b => b[path] = routeBuilder);
        }

        public static NancyPack Get(
            this NancyPack pack, string path, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Get(b => b[path, runAsync] = routeBuilder);
        }

        public static NancyPack Get(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Get(b => b[path, condition] = routeBuilder);
        }

        public static NancyPack Get(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Get(b => b[path, condition, runAsync] = routeBuilder);
        }

        public static NancyPack Options(
        this NancyPack pack, string path, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Options(b => b[path] = routeBuilder);
        }

        public static NancyPack Options(
            this NancyPack pack, string path, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Options(b => b[path, runAsync] = routeBuilder);
        }

        public static NancyPack Options(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Options(b => b[path, condition] = routeBuilder);
        }

        public static NancyPack Options(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Options(b => b[path, condition, runAsync] = routeBuilder);
        }

        public static NancyPack Patch(
        this NancyPack pack, string path, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Patch(b => b[path] = routeBuilder);
        }

        public static NancyPack Patch(
            this NancyPack pack, string path, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Patch(b => b[path, runAsync] = routeBuilder);
        }

        public static NancyPack Patch(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Patch(b => b[path, condition] = routeBuilder);
        }

        public static NancyPack Patch(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Patch(b => b[path, condition, runAsync] = routeBuilder);
        }

        public static NancyPack Post(
        this NancyPack pack, string path, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Post(b => b[path] = routeBuilder);
        }

        public static NancyPack Post(
            this NancyPack pack, string path, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Post(b => b[path, runAsync] = routeBuilder);
        }

        public static NancyPack Post(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Post(b => b[path, condition] = routeBuilder);
        }

        public static NancyPack Post(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Post(b => b[path, condition, runAsync] = routeBuilder);
        }

        public static NancyPack Put(
        this NancyPack pack, string path, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Put(b => b[path] = routeBuilder);
        }

        public static NancyPack Put(
            this NancyPack pack, string path, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Put(b => b[path, runAsync] = routeBuilder);
        }

        public static NancyPack Put(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, Func<dynamic, dynamic> routeBuilder)
        {
            return pack.Put(b => b[path, condition] = routeBuilder);
        }

        public static NancyPack Put(
            this NancyPack pack, string path, Func<NancyContext, bool> condition, bool runAsync, Func<dynamic, CancellationToken, Task<dynamic>> routeBuilder)
        {
            return pack.Put(b => b[path, condition, runAsync] = routeBuilder);
        }
    }
}
