using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using HansKindberg.Web.Mvp.UI.Views;
using HansKindberg.Web.Mvp.WebApplication.Presenters.WebControls;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.WebApplication.Views.WebControls
{
	[PresenterBinding(typeof(RepeaterPresenter))]
	public class RepeaterView : TemplatedControlView, IRepeaterView
	{
		#region Fields

		private const string _dataSourceViewStateName = "DataSource";
		private const string _itemsPerPagingItemViewStateName = "ItemsPerPagingItem";
		private const string _maximumDisplayedPagingItemsViewStateName = "MaximumDisplayedPagingItems";
		private const string _pagingEnabledViewStateName = "PagingEnabled";

		#endregion

		#region Properties

		[Category("Information"), DefaultValue(null)]
		public virtual IEnumerable DataSource
		{
			get { return this.ViewState[_dataSourceViewStateName] as IEnumerable; }
			set { this.ViewState[_dataSourceViewStateName] = value; }
		}

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(RepeaterItem))]
		public virtual ITemplate FooterTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(RepeaterItem))]
		public virtual ITemplate HeaderTemplate { get; set; }

		[Browsable(false), PersistenceMode(PersistenceMode.InnerProperty), TemplateContainer(typeof(RepeaterItem))]
		public virtual ITemplate ItemTemplate { get; set; }

		[Category("Information"), DefaultValue(10)]
		public virtual int ItemsPerPagingItem
		{
			get { return (this.ViewState[_itemsPerPagingItemViewStateName] as int? ?? (int?) 10).Value; }
			set { this.ViewState[_itemsPerPagingItemViewStateName] = value; }
		}

		[Category("Information"), DefaultValue(null)]
		public virtual int? MaximumDisplayedPagingItems
		{
			get { return this.ViewState[_maximumDisplayedPagingItemsViewStateName] as int?; }
			set { this.ViewState[_maximumDisplayedPagingItemsViewStateName] = value; }
		}

		[Category("Information"), DefaultValue(false)]
		public virtual bool PagingEnabled
		{
			get { return (this.ViewState[_pagingEnabledViewStateName] as bool? ?? (bool?) false).Value; }
			set { this.ViewState[_pagingEnabledViewStateName] = value; }
		}

		[Browsable(false)]
		public virtual int PagingItems { get; set; }

		[Browsable(false)]
		public virtual int PagingPosition { get; set; }

		#endregion
	}
}