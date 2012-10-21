using System;

namespace HansKindberg.Web.Mvp
{
	public class ResolvedUrls : IResolvedUrls
	{
		#region Fields

		private readonly IView _view;

		#endregion

		#region Constructors

		public ResolvedUrls(IView view)
		{
			if(view == null)
				throw new ArgumentNullException("view");

			this._view = view;
		}

		#endregion

		#region Properties

		public virtual string this[string relativeUrl]
		{
			get
			{
				if(relativeUrl == null)
					throw new ArgumentNullException("relativeUrl");

				return this._view.ResolveUrl(relativeUrl);
			}
		}

		#endregion
	}
}