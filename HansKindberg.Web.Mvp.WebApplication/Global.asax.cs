using System;

namespace HansKindberg.Web.Mvp.WebApplication
{
	public class Global : System.Web.HttpApplication
	{
		#region Methods

		protected void Application_Start(object sender, EventArgs e)
		{
			Bootstrapper.Bootstrap();
		}

		#endregion
	}
}