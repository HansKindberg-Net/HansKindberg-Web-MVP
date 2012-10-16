using HansKindberg.Web.Mvp.WebApplication.Views;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.WebApplication.Presenters
{
	public class PresenterDiscoveryStrategySamplePresenter : Presenter<IPresenterDiscoveryStrategySampleView>
	{
		#region Constructors

		public PresenterDiscoveryStrategySamplePresenter(IPresenterDiscoveryStrategySampleView view) : base(view) {}

		#endregion
	}
}