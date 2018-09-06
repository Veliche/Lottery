using Autofac;
using Autofac.Integration.WebApi;
using Lottery.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Reflection;
using Lottery.Service.IoC.Autofac;

namespace Lottery.Web.App_Start
{
    public class IocConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterDependencies(new ContainerBuilder()));
        }

        private static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        private static IContainer RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<LotteryService>().As<ILotteryService>().InstancePerRequest();
            builder.RegisterModule(new ServiceModule());
            return builder.Build();
        }
    }
    
}