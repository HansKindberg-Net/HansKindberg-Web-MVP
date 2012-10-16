using System;
using System.Reflection;
using System.Web.UI;
using WebFormsMvp.Web;

namespace HansKindberg.Web.Mvp.UI
{
	public class PageWrapper : IPage
	{
		#region Fields

		private static readonly string _iPageViewHostCacheKey = typeof(IPageViewHost).FullName + ".PageContextKey";
		private readonly Page _page;
		private static readonly string _viewHostCacheKey = (string) typeof(PageViewHost).GetField("viewHostCacheKey", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

		#endregion

		#region Constructors

		public PageWrapper(Page page)
		{
			if(page == null)
				throw new ArgumentNullException("page");

			this._page = page;
		}

		#endregion

		#region Properties

		public virtual IPageViewHost CachedPageViewHost
		{
			get
			{
				IPageViewHost iPageViewHost = (IPageViewHost) this._page.Items[_iPageViewHostCacheKey];

				if(iPageViewHost == null)
				{
					PageViewHost pageViewHost = (PageViewHost) this._page.Items[_viewHostCacheKey];

					if(pageViewHost != null)
					{
						iPageViewHost = new PageViewHostWrapper(pageViewHost);
						this._page.Items[_iPageViewHostCacheKey] = iPageViewHost;
					}
				}

				return iPageViewHost;
			}
		}

		#endregion

		#region Methods

		public static PageWrapper FromPage(Page page)
		{
			return page;
		}

		#endregion

		#region Implicit operator

		public static implicit operator PageWrapper(Page page)
		{
			return page == null ? null : new PageWrapper(page);
		}

		#endregion
	}
}