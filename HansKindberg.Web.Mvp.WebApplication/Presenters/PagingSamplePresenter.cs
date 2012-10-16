using System;
using System.Collections.Specialized;
using System.Web;
using HansKindberg.Web.Mvp.WebApplication.Models;
using HansKindberg.Web.Mvp.WebApplication.Views;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.WebApplication.Presenters
{
	public class PagingSamplePresenter : Presenter<IPagingSampleView>
	{
		#region Fields

		private string _pagingPositionHref;

		#endregion

		#region Constructors

		public PagingSamplePresenter(IPagingSampleView view) : base(view)
		{
			this.View.Load += this.OnViewLoad;
		}

		#endregion

		#region Properties

		protected internal virtual string PagingPositionHref
		{
			get
			{
				if(this._pagingPositionHref == null)
				{
					UriBuilder uriBuilder = new UriBuilder(new Uri("http://localhost" + this.Request.RawUrl));
					NameValueCollection queryString = HttpUtility.ParseQueryString(uriBuilder.Query);
					queryString.Set(PagingSampleModel.PagingPositionParameterName, "{0}");

					string query = string.Empty;
					foreach(string key in queryString.Keys)
					{
						if(!string.IsNullOrEmpty(query))
							query += "&";

						query += key + "=" + (!key.Equals(PagingSampleModel.PagingPositionParameterName, StringComparison.OrdinalIgnoreCase) ? HttpUtility.UrlEncode(queryString[key]) : queryString[key]);
					}

					uriBuilder.Query = query;

					this._pagingPositionHref = uriBuilder.Path + uriBuilder.Query;
				}

				return this._pagingPositionHref;
			}
		}

		#endregion

		#region Methods

		protected internal virtual void PopulateModel()
		{
			this.View.Model.PagingPositionHref = this.PagingPositionHref;
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewLoad(object sender, EventArgs e)
		{
			this.PopulateModel();
		}

		#endregion
	}
}