using System.ComponentModel;
using System.Web.UI;
using HansKindberg.Web.Mvp.UI.Presenters;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.UI.Views
{
	[PresenterBinding(typeof(PagingPresenter))]
	public class PagingView : TemplatedControlView, IPagingView
	{
		#region Fields

		private const string _pageableViewIdViewStateName = "PageableViewID";
		private const string _pageableViewViewStateName = "PageableView";

		#endregion

		#region Properties

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PagingItemContainer))]
		public virtual ITemplate FirstItemTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PagingSummaryContainer))]
		public virtual ITemplate FooterTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PagingSummaryContainer))]
		public virtual ITemplate HeaderTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PagingItemContainer))]
		public virtual ITemplate ItemTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PagingItemContainer))]
		public virtual ITemplate LastItemTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PagingItemContainer))]
		public virtual ITemplate NextItemGroupTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PagingItemContainer))]
		public virtual ITemplate NextItemTemplate { get; set; }

		[Category("Information"), DefaultValue(null)]
		public virtual IPageableView PageableView
		{
			get { return this.ViewState[_pageableViewViewStateName] as IPageableView; }
			set { this.ViewState[_pageableViewViewStateName] = value; }
		}

		[Category("Information"), DefaultValue("")]
		public virtual string PageableViewId
		{
			get { return this.ViewState[_pageableViewIdViewStateName] as string ?? string.Empty; }
			set { this.ViewState[_pageableViewIdViewStateName] = value; }
		}

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PagingItemContainer))]
		public virtual ITemplate PreviousItemGroupTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PagingItemContainer))]
		public virtual ITemplate PreviousItemTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PagingItemContainer))]
		public virtual ITemplate SelectedItemTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(PagingItemContainer))]
		public virtual ITemplate SeparatorTemplate { get; set; }

		#endregion
	}
}