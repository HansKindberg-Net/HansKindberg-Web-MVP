using System;
using System.Diagnostics.CodeAnalysis;
using HansKindberg.Web.Mvp.UI.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.Mvp.Tests.UI.Views
{
	[TestClass]
	public class PagingItemContainerTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Web.Mvp.UI.Views.PagingItemContainer")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public void Constructor_IfThePositionParameterIsLessThanZero_ShouldThrowAnArgumentOutOfRangeException()
		{
			new PagingItemContainer(- DateTime.Now.Second);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void Position_Set_IfTheValueParameterIsLessThanZero_ShouldThrowAnArgumentOutOfRangeException()
		{
			using(PagingItemContainer pagingItemContainer = new PagingItemContainer(0))
			{
				pagingItemContainer.Position = -DateTime.Now.Second;
			}
		}

		#endregion
	}
}