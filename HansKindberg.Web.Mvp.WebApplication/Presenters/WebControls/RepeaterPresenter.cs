using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.UI.WebControls;
using HansKindberg.Web.Mvp.UI.Presenters;
using HansKindberg.Web.Mvp.WebApplication.Views.WebControls;

namespace HansKindberg.Web.Mvp.WebApplication.Presenters.WebControls
{
	public class RepeaterPresenter : TemplatedControlPresenter<IRepeaterView>
	{
		#region Fields

		private readonly IPageableViewResolver _pageableViewResolver;
		private int? _pagingPosition;
		private readonly string _pagingPositionParameterName;

		#endregion

		#region Constructors

		public RepeaterPresenter(IRepeaterView view, string pagingPositionParameterName, IPageableViewResolver pageableViewResolver) : base(view)
		{
			if(string.IsNullOrEmpty(pagingPositionParameterName))
				throw new ArgumentException("The paging position parameter name can not be null or empty.");

			if(pageableViewResolver == null)
				throw new ArgumentNullException("pageableViewResolver");

			this._pageableViewResolver = pageableViewResolver;
			this._pagingPositionParameterName = pagingPositionParameterName;
		}

		#endregion

		#region Properties

		protected internal virtual int PagingPosition
		{
			get
			{
				if(this._pagingPosition == null)
				{
					this._pagingPosition = 0;

					string pagingPositionString = this.Request.QueryString[this._pagingPositionParameterName];

					if(!string.IsNullOrEmpty(pagingPositionString))
					{
						int pagingPosition;
						if(int.TryParse(pagingPositionString, out pagingPosition))
						{
							if(pagingPosition > 0)
								this._pagingPosition = pagingPosition;
						}
					}
				}

				return this._pagingPosition.Value;
			}
		}

		#endregion

		#region Eventhandlers

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		protected override void OnViewCreatingChildControls(object sender, EventArgs e)
		{
			base.OnViewCreatingChildControls(sender, e);

			this.View.PagingPosition = this.PagingPosition;

			List<object> items = new List<object>();

			if(this.View.DataSource != null)
				items = new List<object>(this.View.DataSource.OfType<object>());

			this._pageableViewResolver.ResolveItems(this.View, items);

			if(!items.Any())
				return;

			if(this.View.HeaderTemplate != null)
				this.AddTemplate(new RepeaterItem(0, ListItemType.Header), this.View.HeaderTemplate);

			if(this.View.ItemTemplate != null)
			{
				for(int i = 0; i < items.Count; i++)
				{
					this.AddTemplate(new RepeaterItem(i, ListItemType.Item) {DataItem = items[i]}, this.View.ItemTemplate);
				}
			}

			if(this.View.FooterTemplate != null)
				this.AddTemplate(new RepeaterItem(0, ListItemType.Footer), this.View.FooterTemplate);
		}

		protected override void OnViewInvalidatingInternalState(object sender, EventArgs e) {}

		#endregion
	}
}