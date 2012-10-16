using HansKindberg.Web.Mvp.WebApplication.Models;
using HansKindberg.Web.Mvp.WebApplication.Presenters;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.WebApplication.Views
{
	[PresenterBinding(typeof(PresenterDiscoveryStrategySamplePresenter))]
	public partial class PresenterDiscoveryStrategySampleView : View<PresenterDiscoveryStrategySampleModel>, IPresenterDiscoveryStrategySampleView {}
}