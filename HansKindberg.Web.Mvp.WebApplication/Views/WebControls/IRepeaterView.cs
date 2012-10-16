using System.Collections;
using System.Web.UI;
using HansKindberg.Web.Mvp.UI.Views;

namespace HansKindberg.Web.Mvp.WebApplication.Views.WebControls
{
	public interface IRepeaterView : ITemplatedControlView, IPageableView
	{
		#region Properties

		IEnumerable DataSource { get; }
		ITemplate FooterTemplate { get; set; }
		ITemplate HeaderTemplate { get; set; }
		ITemplate ItemTemplate { get; set; }
		new int PagingItems { get; set; }
		new int PagingPosition { get; set; }

		#endregion
	}
}