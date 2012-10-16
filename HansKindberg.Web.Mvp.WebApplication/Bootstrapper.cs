using System;
using HansKindberg.Web.Mvp.IoC.StructureMap.Binder;
using StructureMap;
using WebFormsMvp.Binder;
using WebFormsMvp.Web;
using ConventionBasedPresenterDiscoveryStrategy = HansKindberg.Web.Mvp.Binder.ConventionBasedPresenterDiscoveryStrategy;

namespace HansKindberg.Web.Mvp.WebApplication
{
	public class Bootstrapper : IBootstrapper
	{
		#region Fields

		private static Boolean _hasStarted;

		#endregion

		#region Methods

		public static void Bootstrap()
		{
			new Bootstrapper().BootstrapStructureMap();
			PresenterBinder.Factory = new PresenterFactory(ObjectFactory.Container);
			PresenterBinder.DiscoveryStrategy = new CompositePresenterDiscoveryStrategy(new AttributeBasedPresenterDiscoveryStrategy(), new ConventionBasedPresenterDiscoveryStrategy(new[] {"HansKindberg.Web.Mvp.WebApplication.Presenters.WebControls"}, new BuildManagerWrapper()));
		}

		public void BootstrapStructureMap()
		{
			ObjectFactory.Initialize(initializer => { initializer.PullConfigurationFromAppConfig = true; });
		}

		public static void Restart()
		{
			if(_hasStarted)
			{
				ObjectFactory.ResetDefaults();
			}
			else
			{
				Bootstrap();
				_hasStarted = true;
			}
		}

		#endregion
	}
}