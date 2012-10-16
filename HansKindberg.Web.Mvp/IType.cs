using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Web.Mvp
{
	public interface IType
	{
		#region Properties

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Namespace")]
		string Namespace { get; }

		#endregion
	}
}