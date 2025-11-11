using System;
using System.Collections.Generic;
using Game399.Shared;
using Game399.Shared.DependencyInjection;
using Game399.Shared.DependencyInjection.Implementation;
using Game399.Shared.Diagnostics;
using Game399.Shared.Services;

namespace Game.Runtime
{
    public static class ServiceResolver
    {
        public static T Resolve<T>() => Container.Value.Resolve<T>();

        private static readonly Lazy<IMiniContainer> Container = new (() =>
        {
            var container = new MiniContainer();

            var logger = new UnityGameLogger();
            container.RegisterSingletonInstance<IGameLog>(logger);

            var stations = new List<IDrinkStation>
            {
                new IceStation(),
                // add more as we go
            };

            var drinkServices = new DrinkServices(stations);
            container.RegisterSingletonInstance(drinkServices);

            return container;
        });
    }
}