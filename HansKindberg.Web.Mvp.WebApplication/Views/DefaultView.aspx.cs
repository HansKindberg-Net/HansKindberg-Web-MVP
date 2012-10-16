using HansKindberg.Web.Mvp.WebApplication.Models;
using HansKindberg.Web.Mvp.WebApplication.Presenters;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.WebApplication.Views
{
	[PresenterBinding(typeof(DefaultPresenter))]
	public partial class DefaultView : View<DefaultModel>, IDefaultView {}
}