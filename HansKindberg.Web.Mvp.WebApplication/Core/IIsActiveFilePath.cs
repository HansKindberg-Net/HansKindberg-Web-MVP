namespace HansKindberg.Web.Mvp.WebApplication.Core
{
	public interface IIsActiveFilePath
	{
		#region Properties

		bool this[string filePath] { get; }

		#endregion
	}
}