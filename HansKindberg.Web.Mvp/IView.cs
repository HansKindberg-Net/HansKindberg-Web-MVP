using System;
using System.Diagnostics.CodeAnalysis;

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

		#region Methods

		[SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
		[SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
		string ResolveUrl(string relativeUrl);

		#endregion
	}

	public interface IView<TModel> : IView, WebFormsMvp.IView<TModel> where TModel : class, new() {}
}