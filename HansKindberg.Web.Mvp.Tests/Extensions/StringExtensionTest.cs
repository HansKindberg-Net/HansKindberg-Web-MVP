using HansKindberg.Web.Mvp.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.Mvp.Tests.Extensions
{
	[TestClass]
	public class StringExtensionTest
	{
		#region Methods

		[TestMethod]
		public void TrimFromEnd_IfTheTrimValueParameterIsEmpty_ShouldReturnTheValue()
		{
			Assert.AreEqual("Test", "Test".TrimFromEnd(string.Empty));
		}

		[TestMethod]
		public void TrimFromEnd_IfTheTrimValueParameterIsNull_ShouldReturnTheValue()
		{
			Assert.AreEqual("Test", "Test".TrimFromEnd(null));
		}

		[TestMethod]
		public void TrimFromEnd_IfTheValueParameterIsEmpty_ShouldReturnAnEmptyString()
		{
			Assert.AreEqual(string.Empty, string.Empty.TrimFromEnd("Test"));
		}

		[TestMethod]
		public void TrimFromEnd_IfTheValueParameterIsNull_ShouldReturnNull()
		{
			Assert.IsNull(((string) null).TrimFromEnd("Test"));
		}

		[TestMethod]
		public void TrimFromEnd_ShouldBeCaseInsensitive()
		{
			const string testValue = "Test.Test.Test";
			Assert.AreEqual("Test", testValue.TrimFromEnd(".test.test"));
			Assert.AreEqual("Test", testValue.TrimFromEnd(".TEST.TEST"));
			Assert.AreEqual("Test", testValue.TrimFromEnd(".tEsT.tEsT"));
		}

		#endregion
	}
}