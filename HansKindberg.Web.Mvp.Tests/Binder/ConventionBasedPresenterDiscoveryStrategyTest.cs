using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HansKindberg.Web.Mvp.Binder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.Tests.Binder
{
	[TestClass]
	public class ConventionBasedPresenterDiscoveryStrategyTest
	{
		#region Fields

		private readonly IEnumerable<string> _originalCandidatePresenterTypeFullNameFormats;

		#endregion

		#region Constructors

		public ConventionBasedPresenterDiscoveryStrategyTest()
		{
			this._originalCandidatePresenterTypeFullNameFormats = new WebFormsMvp.Binder.ConventionBasedPresenterDiscoveryStrategy(Mock.Of<IBuildManager>()).CandidatePresenterTypeFullNameFormats;
		}

		#endregion

		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Web.Mvp.Binder.ConventionBasedPresenterDiscoveryStrategy")]
		public void Constructor_IfTheBuildManagerParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new ConventionBasedPresenterDiscoveryStrategy(null);
		}

		private static void GetCandidatePresenterTypeFullNameFormatFromViewTypeNamespaceTest(ConventionBasedPresenterDiscoveryStrategy conventionBasedPresenterDiscoveryStrategy, Mock<IType> typeMock, string viewTypeNamespace, string expectedCandidatePresenterTypeFullNameFormat)
		{
			typeMock.Setup(type => type.Namespace).Returns(viewTypeNamespace);
			Assert.AreEqual(expectedCandidatePresenterTypeFullNameFormat, conventionBasedPresenterDiscoveryStrategy.GetCandidatePresenterTypeFullNameFormatFromViewTypeNamespace(typeMock.Object));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetCandidatePresenterTypeFullNameFormatFromViewTypeNamespace_IfTheViewTypeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new ConventionBasedPresenterDiscoveryStrategy(Mock.Of<IBuildManager>()).GetCandidatePresenterTypeFullNameFormatFromViewTypeNamespace(null);
		}

		[TestMethod]
		public void GetCandidatePresenterTypeFullNameFormatFromViewTypeNamespace_Test()
		{
			Mock<ConventionBasedPresenterDiscoveryStrategy> conventionBasedPresenterDiscoveryStrategyMock = new Mock<ConventionBasedPresenterDiscoveryStrategy>(new object[] {Mock.Of<IBuildManager>()}) {CallBase = true};
			conventionBasedPresenterDiscoveryStrategyMock.Setup(presenterDiscoveryStrategy => presenterDiscoveryStrategy.ViewNamespaceSuffixes).Returns(new[] {".Tests", ".Test"});
			ConventionBasedPresenterDiscoveryStrategy conventionBasedPresenterDiscoveryStrategy = conventionBasedPresenterDiscoveryStrategyMock.Object;
			Mock<IType> typeMock = new Mock<IType>();

			GetCandidatePresenterTypeFullNameFormatFromViewTypeNamespaceTest(conventionBasedPresenterDiscoveryStrategy, typeMock, "First.Second.Third.Tests", "First.Second.Third.Presenters.{presenter}");
			GetCandidatePresenterTypeFullNameFormatFromViewTypeNamespaceTest(conventionBasedPresenterDiscoveryStrategy, typeMock, "First.Second.Third.Test", "First.Second.Third.Presenters.{presenter}");
			GetCandidatePresenterTypeFullNameFormatFromViewTypeNamespaceTest(conventionBasedPresenterDiscoveryStrategy, typeMock, "First.Second.Third.Fourth", "First.Second.Third.Fourth.Presenters.{presenter}");
		}

		private void GetCandidatePresenterTypeFullNameFormatsTest(ConventionBasedPresenterDiscoveryStrategy conventionBasedPresenterDiscoveryStrategy, Mock<IType> typeMock, string viewTypeNamespace, string expectedFirstCandidatePresenterTypeFullNameFormat)
		{
			typeMock.Setup(type => type.Namespace).Returns(viewTypeNamespace);
			IEnumerable<string> actualCandidatePresenterTypeFullNameFormats = conventionBasedPresenterDiscoveryStrategy.GetCandidatePresenterTypeFullNameFormats(typeMock.Object);
			// ReSharper disable PossibleMultipleEnumeration			
			Assert.IsTrue(actualCandidatePresenterTypeFullNameFormats.Count() == this._originalCandidatePresenterTypeFullNameFormats.Count() + 1);
			Assert.AreEqual(expectedFirstCandidatePresenterTypeFullNameFormat, actualCandidatePresenterTypeFullNameFormats.ElementAt(0));
			for(int i = 0; i < this._originalCandidatePresenterTypeFullNameFormats.Count(); i++)
			{
				Assert.AreEqual(this._originalCandidatePresenterTypeFullNameFormats.ElementAt(i), actualCandidatePresenterTypeFullNameFormats.ElementAt(i + 1));
			}
			// ReSharper restore PossibleMultipleEnumeration
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetCandidatePresenterTypeFullNameFormats_IfTheViewTypeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new ConventionBasedPresenterDiscoveryStrategy(Mock.Of<IBuildManager>()).GetCandidatePresenterTypeFullNameFormats(null);
		}

		[TestMethod]
		public void GetCandidatePresenterTypeFullNameFormats_Test()
		{
			Mock<ConventionBasedPresenterDiscoveryStrategy> conventionBasedPresenterDiscoveryStrategyMock = new Mock<ConventionBasedPresenterDiscoveryStrategy>(new object[] {Mock.Of<IBuildManager>()}) {CallBase = true};
			conventionBasedPresenterDiscoveryStrategyMock.Setup(presenterDiscoveryStrategy => presenterDiscoveryStrategy.ViewNamespaceSuffixes).Returns(new[] {".Tests", ".Test"});
			ConventionBasedPresenterDiscoveryStrategy conventionBasedPresenterDiscoveryStrategy = conventionBasedPresenterDiscoveryStrategyMock.Object;
			Mock<IType> typeMock = new Mock<IType>();

			this.GetCandidatePresenterTypeFullNameFormatsTest(conventionBasedPresenterDiscoveryStrategy, typeMock, "First.Second.Third.Tests", "First.Second.Third.Presenters.{presenter}");
			this.GetCandidatePresenterTypeFullNameFormatsTest(conventionBasedPresenterDiscoveryStrategy, typeMock, "First.Second.Third.Test", "First.Second.Third.Presenters.{presenter}");
			this.GetCandidatePresenterTypeFullNameFormatsTest(conventionBasedPresenterDiscoveryStrategy, typeMock, "First.Second.Third.Fourth", "First.Second.Third.Fourth.Presenters.{presenter}");
		}

		[TestMethod]
		public void PrerequisiteTest()
		{
			Assert.AreEqual("HansKindberg.Web.Mvp.Tests.Binder", this.GetType().Namespace);
		}

		#endregion
	}
}