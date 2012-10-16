namespace HansKindberg.Web.Mvp.UI
{
	public interface IPage
	{
		#region Properties

		IPageViewHost CachedPageViewHost { get; }

		#endregion
	}
}