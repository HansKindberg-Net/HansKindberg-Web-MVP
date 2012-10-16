namespace HansKindberg.Web.Mvp
{
	public interface IPageableView
	{
		#region Properties

		int ItemsPerPagingItem { get; }
		int? MaximumDisplayedPagingItems { get; }
		bool PagingEnabled { get; }
		int PagingItems { get; set; }
		int PagingPosition { get; }

		#endregion
	}
}