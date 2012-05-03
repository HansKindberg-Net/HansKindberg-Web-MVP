using System;

namespace HansKindberg.Web.Mvp
{
	public interface IView : WebFormsMvp.IView
	{
		#region Events

		event EventHandler DataBinding;
		event EventHandler Disposed;
		event EventHandler PreRender;
		event EventHandler Unload;

		#endregion
	}

	public interface IView<TModel> : IView, WebFormsMvp.IView<TModel> where TModel : class, new() {}
}