[assembly: WebActivator.PostApplicationStartMethod(typeof(SototiSite.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace SototiSite.App_Start
{
    using System;
    using System.Reflection;
    using System.Web.Mvc;

    using SototiCore.DataProviders;
    using SototiData;
    using SototiData.DataProviders;

    using SototiCore.Data;
    using SototiCore.Data.Common;
    using Sototi.Web;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
	
    using SototiData.Data;

	/// <summary>
	/// The simple injector initializer.
	/// </summary>
	public static class SimpleInjectorInitializer
    {
	    /// <summary>
	    /// Gets the container.
	    /// </summary>
	    public static Container Container { get; private set; }

        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            // Did you know the container can diagnose your configuration? Go to: http://bit.ly/YE8OJj.
			Container = new Container();

			InitializeContainer(Container);

			Container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

			Container.RegisterMvcAttributeFilterProvider();

			Container.Verify();

			DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(Container));
        }

	    /// <summary>
	    /// Иницилизация сонтейнера зависимостей.
	    /// </summary>
	    /// <param name="container">
	    /// The container.
	    /// </param>
	    private static void InitializeContainer(Container container)
        {
            container.Register<ICurrentUser, RequestUser>(new WebRequestLifestyle());
            container.Register<IDataContext, DataContext>(new WebRequestLifestyle());

            container.Register<ILocalizer, Localizer>(new WebRequestLifestyle());
            container.Register<ILogger, Logger>(new WebRequestLifestyle());
            container.Register<IServiceProvider>(() => container, new WebRequestLifestyle());
            container.Register<IDataProviderFactory, DataProviderFactory>(new WebRequestLifestyle());

            container.Register<ISecurityDataProvider, SecurityDataProvider>(new WebRequestLifestyle());
        }
    }
}