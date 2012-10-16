using System;
using HansKindberg.Web.Mvp.IoC.StructureMap.Binder;
using StructureMap;

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
			WebFormsMvp.Binder.PresenterBinder.Factory = new PresenterFactory(ObjectFactory.Container);
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