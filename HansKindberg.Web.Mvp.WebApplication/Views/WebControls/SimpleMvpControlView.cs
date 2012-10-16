using HansKindberg.Web.Mvp.UI.Views;
using HansKindberg.Web.Mvp.WebApplication.Presenters.WebControls;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.WebApplication.Views.WebControls
{
	[PresenterBinding(typeof(SimpleMvpControlPresenter))]
	public class SimpleMvpControlView : ControlView, ISimpleMvpControlView {}
}