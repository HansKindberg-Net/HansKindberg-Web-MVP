using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Web.Mvp.Tests
{
	[TestClass]
	public class ResolvedUrlsTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Web.Mvp.ResolvedUrls")]
		public void Constructor_IfTheViewParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				new ResolvedUrls(null);
			}
			catch(ArgumentNullException exception)
			{
				if(exception.ParamName.Equals("view"))
					throw;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "resolvedUrl")]
		public void This_IfTheRelativeUrlParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			try
			{
				string resolvedUrl = new ResolvedUrls(Mock.Of<IView>())[null];
			}
			catch(ArgumentNullException exception)
			{
				if(exception.ParamName.Equals("relativeUrl"))
					throw;
			}
		}

		#endregion
	}
}