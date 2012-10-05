using System.Diagnostics.CodeAnalysis;
using HansKindberg.Web.Mvp.UI.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansKindberg.Web.Mvp.Tests.UI.Views
{
	[TestClass]
	public class TemplatedControlViewTest
	{
		#region Methods

		[TestMethod]
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "TestIs")]
		[SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "We")]
		public void RemoveThisWhenYouWriteRealTests_ItIsJustHereSoWeKeepAReferenceToTheTypeWeNeedToTest_IfTheTypeThatThisTestIsForWillBeRemovedWeWantForgetToRemoveThisTestClassEitherBecauseItWillNotCompile()
		{
			Assert.Inconclusive(typeof(TemplatedControlView).FullName + " need more tests.");
		}

		#endregion
	}
}