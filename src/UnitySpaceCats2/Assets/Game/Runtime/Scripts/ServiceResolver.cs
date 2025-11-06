using System;
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

            var drinkService = new DrinkService(logger);
            container.RegisterSingletonInstance<IDrinkService>(drinkService);

            var drinkState = new DrinkState();
            container.RegisterSingletonInstance(drinkState);
            
            return container;
        });
    }
}