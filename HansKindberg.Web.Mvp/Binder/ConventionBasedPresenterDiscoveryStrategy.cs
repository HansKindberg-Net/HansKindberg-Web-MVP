using System;
using System.Collections.Generic;
using System.Linq;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.Binder
{
	public class ConventionBasedPresenterDiscoveryStrategy : WebFormsMvp.Binder.ConventionBasedPresenterDiscoveryStrategy
	{
		#region Fields

		private readonly IEnumerable<string> _candidatePresenterTypeFullNameFormats;

		#endregion

		#region Constructors

		public ConventionBasedPresenterDiscoveryStrategy(IEnumerable<string> presenterNamespaces, IBuildManager buildManager) : base(buildManager)
		{
			if(presenterNamespaces == null)
				throw new ArgumentNullException("presenterNamespaces");

			this._candidatePresenterTypeFullNameFormats = presenterNamespaces.Select(presenterNamespace => presenterNamespace + ".{presenter}");
		}

		#endregion

		#region Properties

		public override IEnumerable<string> CandidatePresenterTypeFullNameFormats
		{
			get { return this._candidatePresenterTypeFullNameFormats.Concat(base.CandidatePresenterTypeFullNameFormats); }
		}

		#endregion
	}
}