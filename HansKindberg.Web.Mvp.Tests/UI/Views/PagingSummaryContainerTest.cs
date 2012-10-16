using System;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Web.Mvp.UI.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.Mvp.Tests.UI.Views
{
	[TestClass]
	public class PagingSummaryContainerTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Web.Mvp.UI.Views.PagingSummaryContainer")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void Constructor_IfTheNumberOfPagingItemsParameterIsLessThanZero_ShouldThrowAnArgumentOutOfRangeException()
		{
			new PagingSummaryContainer(-DateTime.Now.Second);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void NumberOfPagingItems_Set_IfTheValueParameterIsLessThanZero_ShouldThrowAnArgumentOutOfRangeException()
		{
			using(PagingSummaryContainer pagingSummaryContainer = new PagingSummaryContainer(0))
			{
				pagingSummaryContainer.NumberOfPagingItems = -DateTime.Now.Second;
			}
		}

		#endregion
	}
}