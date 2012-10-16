using System;
using System.ComponentModel;
using System.Web.UI;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public interface IControlView : IView
	{
		#region Events

		event EventHandler CreatingChildControls;
		event EventHandler<CancelEventArgs> DataBindingChildren;
		event EventHandler<CancelEventArgs> EnsuringChildControls;

		#endregion

		#region Properties

		ControlCollection Controls { get; }
		bool DesignMode { get; }
		Control NamingContainer { get; }
		bool Visible { get; set; }

		#endregion

		#region Methods

		void DataBind();
		Control FindControl(string id);

		#endregion
	}

	public interface IControlView<TModel> : IControlView, WebFormsMvp.IView<TModel> where TModel : class, new() {}
}