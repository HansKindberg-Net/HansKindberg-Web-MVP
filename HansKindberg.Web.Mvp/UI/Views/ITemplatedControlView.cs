using System.Web.UI;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public interface ITemplatedControlView : IAutoDataBindControlView, INamingContainer {}

	public interface ITemplatedControlView<TModel> : ITemplatedControlView, IAutoDataBindControlView<TModel> where TModel : class, new() {}
}