using System;
using System.Web.UI;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public interface ITemplatedControlView : IAutoDataBindableView, INamingContainer
	{
		#region Events

		event EventHandler InvalidatingInternalState;

		#endregion
	}

	public interface ITemplatedControlView<TModel> : ITemplatedControlView, IAutoDataBindControlView<TModel> where TModel : class, new() {}
}