namespace HansKindberg.Web.Mvp
{
	public interface IPageableView
	{
		#region Properties

		int ItemsPerPagingItem { get; set; }
		bool PagingEnabled { get; set; }
		int PagingItems { get; }
		int PagingPosition { get; }

		#endregion
	}
}