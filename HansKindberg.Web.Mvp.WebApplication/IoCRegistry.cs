using System;
using System.Web;
using HansKindberg.Web.Mvp.WebApplication.Models;
using HansKindberg.Web.Mvp.WebApplication.Presenters.WebControls;
using StructureMap.Configuration.DSL;

namespace HansKindberg.Web.Mvp.WebApplication
{
	[CLSCompliant(false)]
	public abstract class IoCRegistry : Registry
	{
		#region Constructors

		protected IoCRegistry()
		{
			this.For<HttpRequest>().Use(HttpContext.Current.Request);
			this.For<HttpRequestBase>().Use<HttpRequestWrapper>();
			this.For<IPageableViewResolver>().Singleton().Use<PageableViewResolver>();
			this.For<RepeaterPresenter>().Use<RepeaterPresenter>().Ctor<string>("pagingPositionParameterName").Is(PagingSampleModel.PagingPositionParameterName);
		}

		#endregion
	}

	[CLSCompliant(false)]
	public class DebugRegistry : IoCRegistry
	{
		#region Constructors

		public DebugRegistry() {}

		#endregion
	}

	[CLSCompliant(false)]
	public class ReleaseRegistry : IoCRegistry
	{
		#region Constructors

		public ReleaseRegistry() {}

		#endregion
	}
}