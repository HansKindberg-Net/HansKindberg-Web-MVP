using System;
using System.Collections;

namespace HansKindberg.Web.Mvp
{
	public class PageableViewResolver : IPageableViewResolver
	{
		#region Methods

		public virtual void ResolveItems(IPageableView pageableView, IList items)
		{
			if(pageableView == null)
				throw new ArgumentNullException("pageableView");

			if(!pageableView.PagingEnabled)
				return;

			if(items == null || items.Count == 0)
			{
				pageableView.PagingItems = 0;
				return;
			}

			if(pageableView.ItemsPerPagingItem < 1 || (pageableView.MaximumDisplayedPagingItems.HasValue && pageableView.MaximumDisplayedPagingItems.Value < 1))
			{
				pageableView.PagingItems = 0;
				items.Clear();
				return;
			}

			int remainder;

			pageableView.PagingItems = Math.DivRem(items.Count, pageableView.ItemsPerPagingItem, out remainder);

			if(remainder > 0)
				pageableView.PagingItems++;

			int firstItemIndex = pageableView.PagingPosition*pageableView.ItemsPerPagingItem;
			int lastItemIndex = firstItemIndex + pageableView.ItemsPerPagingItem - 1;

			for(int i = items.Count - 1; i >= 0; i--)
			{
				if(i < firstItemIndex || i > lastItemIndex)
					items.RemoveAt(i);
			}
		}

		#endregion
	}
}