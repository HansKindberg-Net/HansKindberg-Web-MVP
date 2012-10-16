using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HansKindberg.Web.Mvp.Extensions;
using WebFormsMvp;
using WebFormsMvp.Binder;

namespace HansKindberg.Web.Mvp.Binder
{
	public class ConventionBasedPresenterDiscoveryStrategy : WebFormsMvp.Binder.ConventionBasedPresenterDiscoveryStrategy
	{
		#region Fields

		private readonly IBuildManager _buildManager;

		private static readonly Func<WebFormsMvp.IView, IBuildManager, IEnumerable<string>, IEnumerable<string>, PresenterDiscoveryResult> _getBindingMethodDelegate =
			(Func<WebFormsMvp.IView, IBuildManager, IEnumerable<string>, IEnumerable<string>, PresenterDiscoveryResult>) Delegate.CreateDelegate(typeof(Func<WebFormsMvp.IView, IBuildManager, IEnumerable<string>, IEnumerable<string>, PresenterDiscoveryResult>), typeof(WebFormsMvp.Binder.ConventionBasedPresenterDiscoveryStrategy).GetMethod("GetBinding", BindingFlags.NonPublic | BindingFlags.Static));

		private static readonly IEnumerable<string> _viewNamespaceSuffixes = new[] {".Views"};

		#endregion

		#region Constructors

		public ConventionBasedPresenterDiscoveryStrategy(IBuildManager buildManager) : base(buildManager)
		{
			this._buildManager = buildManager;
		}

		#endregion

		#region Properties

		public virtual IEnumerable<string> ViewNamespaceSuffixes
		{
			get { return _viewNamespaceSuffixes; }
		}

		#endregion

		#region Methods

		protected internal static PresenterDiscoveryResult GetBinding(WebFormsMvp.IView viewInstance, IBuildManager buildManager, IEnumerable<string> viewInstanceSuffixes, IEnumerable<string> presenterTypeFullNameFormats)
		{
			return _getBindingMethodDelegate(viewInstance, buildManager, viewInstanceSuffixes, presenterTypeFullNameFormats);
		}

		public override IEnumerable<PresenterDiscoveryResult> GetBindings(IEnumerable<object> hosts, IEnumerable<WebFormsMvp.IView> viewInstances)
		{
			if(hosts == null)
				throw new ArgumentNullException("hosts");

			if(viewInstances == null)
				throw new ArgumentNullException("viewInstances");

			return viewInstances
				.Select(viewInstance => GetBinding(viewInstance, this._buildManager, this.ViewInstanceSuffixes, this.GetCandidatePresenterTypeFullNameFormats(new TypeWrapper(viewInstance.GetType()))))
				.ToArray();
		}

		protected internal virtual string GetCandidatePresenterTypeFullNameFormatFromViewTypeNamespace(IType viewType)
		{
			if(viewType == null)
				throw new ArgumentNullException("viewType");

			string presenterTypeNamespacePrefix = (from viewNamespaceSuffix in this.ViewNamespaceSuffixes
			                                       where viewType.Namespace.EndsWith(viewNamespaceSuffix, StringComparison.OrdinalIgnoreCase)
			                                       select viewType.Namespace.TrimFromEnd(viewNamespaceSuffix)).FirstOrDefault();

			return (string.IsNullOrEmpty(presenterTypeNamespacePrefix) ? viewType.Namespace : presenterTypeNamespacePrefix) + ".Presenters.{presenter}";
		}

		public virtual IEnumerable<string> GetCandidatePresenterTypeFullNameFormats(IType viewType)
		{
			if(viewType == null)
				throw new ArgumentNullException("viewType");

			return new[] {this.GetCandidatePresenterTypeFullNameFormatFromViewTypeNamespace(viewType)}.Concat(this.CandidatePresenterTypeFullNameFormats);
		}

		#endregion
	}
}