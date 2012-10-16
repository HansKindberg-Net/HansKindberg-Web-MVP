using System.Web.UI;
using HansKindberg.Web.Mvp.WebApplication.Models.MasterModels;
using HansKindberg.Web.Mvp.WebApplication.Presenters.MasterPresenters;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.WebApplication.Views.MasterViews
{
	[PresenterBinding(typeof(DefaultMasterPresenter))]
	public partial class DefaultMasterView : MasterView<DefaultMasterModel>, IDefaultMasterView
	{
		#region Constructors

		protected DefaultMasterView() {}

		#endregion

		#region Properties

		public virtual Control SubNavigationAreaControl
		{
			get { return this.subNavigationPlaceHolder; }
		}

		#endregion
	}
}