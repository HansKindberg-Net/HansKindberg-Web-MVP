using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.Mvp.Tests
{
	[TestClass]
	public class TypeWrapperTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Web.Mvp.TypeWrapper")]
		public void Constructor_IfTheTypeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new TypeWrapper(null);
		}

		[TestMethod]
		public void Namespace_ShouldReturnTheNamespaceOfTheWrappedType()
		{
			Assert.AreEqual(this.GetType().Namespace, new TypeWrapper(this.GetType()).Namespace);
		}

		#endregion
	}
}