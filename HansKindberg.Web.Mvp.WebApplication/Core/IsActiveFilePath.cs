using System;
using System.Web;

namespace HansKindberg.Web.Mvp.WebApplication.Core
{
	public class IsActiveFilePath : IIsActiveFilePath
	{
		#region Fields

		private readonly HttpRequestBase _httpRequest;

		#endregion

		#region Constructors

		public IsActiveFilePath(HttpRequestBase httpRequest)
		{
			if(httpRequest == null)
				throw new ArgumentNullException("httpRequest");

			this._httpRequest = httpRequest;
		}

		#endregion

		#region Properties

		public virtual bool this[string filePath]
		{
			get
			{
				if(filePath == null)
					throw new ArgumentNullException("filePath");

				return filePath.Equals(this._httpRequest.FilePath, StringComparison.OrdinalIgnoreCase);
			}
		}

		#endregion
	}
}