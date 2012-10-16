using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
		public void CandidatePresenterTypeFullNameFormats_ShouldReturnTheTransformedValuesFromTheThePresenterNamespacesConstructorParameterAsTheFirstItems()
		{
			int numberOfPresenterNamespaces = DateTime.Now.Second;
			List<string> expectedCandidatePresenterTypeFullNameFormats = new List<string>();
			List<string> presenterNamespaces = new List<string>();

			for(int i = 1; i <= numberOfPresenterNamespaces; i++)
			{
				expectedCandidatePresenterTypeFullNameFormats.Add("Test" + i.ToString(CultureInfo.InvariantCulture) + ".{presenter}");
				presenterNamespaces.Add("Test" + i.ToString(CultureInfo.InvariantCulture));
			}

			expectedCandidatePresenterTypeFullNameFormats.AddRange(this._originalCandidatePresenterTypeFullNameFormats);

			IEnumerable<string> actualCandidatePresenterTypeFullNameFormats = new ConventionBasedPresenterDiscoveryStrategy(presenterNamespaces, Mock.Of<IBuildManager>()).CandidatePresenterTypeFullNameFormats;

			// ReSharper disable PossibleMultipleEnumeration
			Assert.AreEqual(expectedCandidatePresenterTypeFullNameFormats.Count, actualCandidatePresenterTypeFullNameFormats.Count());

			for(int i = 0; i < presenterNamespaces.Count; i++)
			{
				Assert.AreEqual(expectedCandidatePresenterTypeFullNameFormats[i], actualCandidatePresenterTypeFullNameFormats.ElementAt(i));
				Assert.AreEqual("Test" + (i + 1).ToString(CultureInfo.InvariantCulture) + ".{presenter}", actualCandidatePresenterTypeFullNameFormats.ElementAt(i));
			}
			// ReSharper restore PossibleMultipleEnumeration
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		[SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "HansKindberg.Web.Mvp.Binder.ConventionBasedPresenterDiscoveryStrategy")]
		public void Constructor_IfThePresenterNamespacesParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new ConventionBasedPresenterDiscoveryStrategy(null, Mock.Of<IBuildManager>());
		}

		[TestMethod]
		public void Constructor_ShouldTransformThePresenterNamespacesToCandidatePresenterTypeFullNameFormats()
		{
			int numberOfPresenterNamespaces = DateTime.Now.Second;
			List<string> expectedCandidatePresenterTypeFullNameFormats = new List<string>();
			List<string> presenterNamespaces = new List<string>();

			for(int i = 1; i <= numberOfPresenterNamespaces; i++)
			{
				expectedCandidatePresenterTypeFullNameFormats.Add("Test" + i.ToString(CultureInfo.InvariantCulture) + ".{presenter}");
				presenterNamespaces.Add("Test" + i.ToString(CultureInfo.InvariantCulture));
			}

			ConventionBasedPresenterDiscoveryStrategy conventionBasedPresenterDiscoveryStrategy = new ConventionBasedPresenterDiscoveryStrategy(presenterNamespaces, Mock.Of<IBuildManager>());

			// ReSharper disable PossibleNullReferenceException
			IEnumerable<string> actualCandidatePresenterTypeFullNameFormats = (IEnumerable<string>) typeof(ConventionBasedPresenterDiscoveryStrategy).GetField("_candidatePresenterTypeFullNameFormats", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(conventionBasedPresenterDiscoveryStrategy);
			// ReSharper restore PossibleNullReferenceException

			Assert.AreEqual(expectedCandidatePresenterTypeFullNameFormats.Count, actualCandidatePresenterTypeFullNameFormats.Count());

			for(int i = 0; i < expectedCandidatePresenterTypeFullNameFormats.Count; i++)
			{
				Assert.AreEqual(expectedCandidatePresenterTypeFullNameFormats[i], actualCandidatePresenterTypeFullNameFormats.ElementAt(i));
			}
		}

		#endregion
	}
}