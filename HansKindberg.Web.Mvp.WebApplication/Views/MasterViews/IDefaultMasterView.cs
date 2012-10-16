using System.Web.UI;
using HansKindberg.Web.Mvp.WebApplication.Models.MasterModels;

namespace HansKindberg.Web.Mvp.WebApplication.Views.MasterViews
{
	public interface IDefaultMasterView : IView<DefaultMasterModel>
	{
		#region Properties

		Page Page { get; }
		Control SubNavigationAreaControl { get; }

		#endregion
	}
}