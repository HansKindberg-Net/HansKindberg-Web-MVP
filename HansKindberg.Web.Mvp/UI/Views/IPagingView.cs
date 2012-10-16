using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public interface IPagingView : ITemplatedControlView
	{
		#region Properties

		ITemplate FirstItemTemplate { get; set; }
		ITemplate FooterTemplate { get; set; }
		ITemplate HeaderTemplate { get; set; }
		// ReSharper disable InconsistentNaming
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
		string ID { get; }

		// ReSharper restore InconsistentNaming
		ITemplate ItemTemplate { get; set; }
		ITemplate LastItemTemplate { get; set; }
		ITemplate NextItemGroupTemplate { get; set; }
		ITemplate NextItemTemplate { get; set; }
		IPageableView PageableView { get; set; }
		string PageableViewId { get; set; }
		ITemplate PreviousItemGroupTemplate { get; set; }
		ITemplate PreviousItemTemplate { get; set; }
		ITemplate SelectedItemTemplate { get; set; }
		ITemplate SeparatorTemplate { get; set; }

		#endregion
	}
}