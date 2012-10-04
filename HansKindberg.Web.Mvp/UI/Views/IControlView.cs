using System;
using System.ComponentModel;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public interface IControlView : IView
	{
		#region Events

		event EventHandler<CancelEventArgs> DataBindingChildren;
		event EventHandler<CancelEventArgs> EnsuringChildControls;

		#endregion

		#region Properties

		bool EnsureChildControlsEnabled { get; set; }
		bool Visible { get; set; }

		#endregion

		#region Methods

		void DataBind();
		void EnsureChildControls(bool force);

		#endregion
	}

	public interface IControlView<TModel> : IControlView, WebFormsMvp.IView<TModel> where TModel : class, new() {}
}