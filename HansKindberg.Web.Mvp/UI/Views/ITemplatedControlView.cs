using System;
using System.Web.UI;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public interface ITemplatedControlView : IControlView, INamingContainer
	{
		#region Events

		event EventHandler CreatingChildControls;

		#endregion

		#region Properties

		ControlCollection Controls { get; }

		#endregion
	}

	public interface ITemplatedControlView<TModel> : ITemplatedControlView, WebFormsMvp.IView<TModel> where TModel : class, new() {}
}