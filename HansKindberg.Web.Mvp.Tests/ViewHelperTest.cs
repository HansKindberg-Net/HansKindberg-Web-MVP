using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.Mvp.Tests
{
	[TestClass]
	public class ViewHelperTest
	{
		#region Methods

		[TestMethod]
		public void ThrowExceptionIfModelIsNull_IfTheModelParameterIsNotNull_ShouldNotThrowAnException()
		{
			ViewHelper.ThrowExceptionIfModelIsNull(new object());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ThrowExceptionIfModelIsNull_IfTheModelParameterIsNull_ShouldThrowAnInvalidOperationException()
		{
			ViewHelper.ThrowExceptionIfModelIsNull(null);
		}

		#endregion
	}
}