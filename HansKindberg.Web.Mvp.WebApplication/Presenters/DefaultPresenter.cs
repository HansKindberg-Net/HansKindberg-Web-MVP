using HansKindberg.Web.Mvp.WebApplication.Views;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.WebApplication.Presenters
{
	public class DefaultPresenter : Presenter<IDefaultView>
	{
		#region Constructors

		public DefaultPresenter(IDefaultView view) : base(view) {}

		#endregion
	}
}