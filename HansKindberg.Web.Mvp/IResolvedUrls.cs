using System.Diagnostics.CodeAnalysis;

namespace HansKindberg.Web.Mvp
{
	public interface IResolvedUrls
	{
		#region Properties

		[SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
		string this[string relativeUrl] { get; }

		#endregion
	}
}