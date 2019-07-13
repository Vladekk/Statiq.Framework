﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Cli;
using Statiq.Common;

namespace Statiq.App
{
    public static class BootstrapperConfiguratorExtensions
    {
        public static IBootstrapper AddCommand<TCommand>(this IBootstrapper bootstrapper, string name)
            where TCommand : class, ICommand
        {
            bootstrapper.Configurators.Add(new AddCommandConfigurator<TCommand>(name));
            return bootstrapper;
        }

        public static IBootstrapper AddServices(this IBootstrapper bootstrapper, Action<IServiceCollection> action) =>
            bootstrapper.Configure<ConfigurableServices>(x => action(x.Services));

        public static IBootstrapper Configure<TConfigurable>(this IBootstrapper bootstrapper, Action<TConfigurable> action)
            where TConfigurable : IConfigurable
        {
            bootstrapper.Configurators.Add(action);
            return bootstrapper;
        }

        public static IBootstrapper AddConfigurator<TConfigurable, TConfigurator>(this IBootstrapper bootstrapper)
            where TConfigurable : IConfigurable
            where TConfigurator : Common.IConfigurator<TConfigurable>
        {
            bootstrapper.Configurators.Add<TConfigurable, TConfigurator>();
            return bootstrapper;
        }

        public static IBootstrapper AddConfigurator<TConfigurable>(
            this IBootstrapper bootstrapper,
            Common.IConfigurator<TConfigurable> configurator)
            where TConfigurable : IConfigurable
        {
            bootstrapper.Configurators.Add(configurator);
            return bootstrapper;
        }
    }
}
