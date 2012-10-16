using System;
using WebFormsMvp.Web;

namespace HansKindberg.Web.Mvp.WebApplication.Views.MasterViews
{
	public class MasterView<TModel> : System.Web.UI.MasterPage, IView<TModel> where TModel : class, new()
	{
		#region Fields

		private TModel _model;

		#endregion

		#region Properties

		public virtual TModel Model
		{
			get
			{
				ViewHelper.ThrowExceptionIfModelIsNull(this._model);

				return this._model;
			}
			set { this._model = value; }
		}

		public virtual bool ThrowExceptionIfNoPresenterBound
		{
			get { return true; }
		}

		#endregion

		#region Eventhandlers

		protected override void OnInit(EventArgs e)
		{
			PageViewHost.Register(this, this.Context, false);
			base.OnInit(e);
		}

		#endregion
	}
}