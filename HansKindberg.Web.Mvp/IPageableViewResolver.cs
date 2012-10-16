using System.Collections;

namespace HansKindberg.Web.Mvp
{
	public interface IPageableViewResolver
	{
		#region Methods

		void ResolveItems(IPageableView pageableView, IList items);

		#endregion
	}
}