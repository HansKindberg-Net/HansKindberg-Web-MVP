using System;
using System.ComponentModel;
using System.Web.UI;
using WebFormsMvp.Web;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public abstract class UserControlView : UserControl, WebFormsMvp.IView
	{
		#region Properties

		[Browsable(false)]
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

	public abstract class UserControlView<TModel> : UserControlView, WebFormsMvp.IView<TModel> where TModel : class, new()
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

		#endregion
	}
}