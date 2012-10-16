using System;
using System.Reflection;
using System.Web;
using System.Web.UI;
using HansKindberg.Web.Mvp.Binder;
using HansKindberg.Web.Mvp.UI;
using WebFormsMvp.Binder;
using WebFormsMvp.Web;

namespace HansKindberg.Web.Mvp
{
	public class PageViewHostWrapper : IPageViewHost
	{
		#region Fields

		private readonly PageViewHost _pageViewHost;
		private static readonly FieldInfo _presenterBinderFieldInfo = typeof(PageViewHost).GetField("presenterBinder", BindingFlags.Instance | BindingFlags.NonPublic);

		#endregion

		#region Constructors

		public PageViewHostWrapper(PageViewHost pageViewHost)
		{
			if(pageViewHost == null)
				throw new ArgumentNullException("pageViewHost");

			this._pageViewHost = pageViewHost;
		}

		#endregion

		#region Properties

		public virtual IPresenterBinder PresenterBinder
		{
			get { return (PresenterBinderWrapper) ((PresenterBinder) _presenterBinderFieldInfo.GetValue(this._pageViewHost)); }
		}

		#endregion

		#region Methods

		public static void Register<T>(T control, HttpContext httpContext, bool enableAutomaticDataBinding, IPage page)
			where T : Control, WebFormsMvp.IView
		{
			// We can not call PageViewHost.Register before OnInit.
			// In PageViewHost the private event handler Page_InitComplete performs the binding of the presenter.
			// The problem is that Page.InitComplete already has occured when we register some controls (typically childcontrols in templated controls)
			// and no presenter is bound.
			// Thats why this workaround.

			if(page == null)
				throw new ArgumentNullException("page");

			IPageViewHost cachedPageViewHost = page.CachedPageViewHost;

			PageViewHost.Register(control, httpContext, enableAutomaticDataBinding);

			if(cachedPageViewHost == null)
				page.CachedPageViewHost.PresenterBinder.PerformBinding();
		}

		#endregion
	}
}