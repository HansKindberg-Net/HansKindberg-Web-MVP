using System;
using System.Web.UI;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public class PagingSummaryContainer : Control, INamingContainer
	{
		#region Fields

		private int _numberOfPagingItems;

		#endregion

		#region Constructors

		public PagingSummaryContainer(int numberOfPagingItems)
		{
			ThrowExceptionIfNumberOfPagingItemsIsLessThanZero("numberOfPagingItems", numberOfPagingItems);
			this._numberOfPagingItems = numberOfPagingItems;
		}

		#endregion

		#region Properties

		public virtual int NumberOfPagingItems
		{
			get { return this._numberOfPagingItems; }
			set
			{
				ThrowExceptionIfNumberOfPagingItemsIsLessThanZero("value", value);
				this._numberOfPagingItems = value;
			}
		}

		#endregion

		#region Methods

		private static void ThrowExceptionIfNumberOfPagingItemsIsLessThanZero(string parameterName, int numberOfPagingItems)
		{
			if(numberOfPagingItems < 0)
				throw new ArgumentOutOfRangeException(parameterName, "Number of paging items can not be less than 0.");
		}

		#endregion
	}
}