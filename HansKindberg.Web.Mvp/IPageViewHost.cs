using HansKindberg.Web.Mvp.Binder;

namespace HansKindberg.Web.Mvp
{
	public interface IPageViewHost
	{
		#region Properties

		IPresenterBinder PresenterBinder { get; }

		#endregion
	}
}