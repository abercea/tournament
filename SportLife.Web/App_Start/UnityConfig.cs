using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using SportLife.Bll.Contracts;
using SportLife.Bll.Bus;
using System.Web.Http;
using SportLife.Dal.Contracts;
using SportLife.Dal.Dao;
using SportLife.Dal;

namespace SportLife
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IObjectContext, SportLifeDataContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IDbContext, CodeFirstDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserBus, UserBus>();
            container.RegisterType<IUserDao, UserDao>();
            container.RegisterType<IEventBus, EventBus>();
            container.RegisterType<IEventDao, EventDao>();
            
            
            
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
        }
    }
}