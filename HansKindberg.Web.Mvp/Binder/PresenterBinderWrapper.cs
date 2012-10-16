using System;
using WebFormsMvp.Binder;

namespace HansKindberg.Web.Mvp.Binder
{
	public class PresenterBinderWrapper : IPresenterBinder
	{
		#region Fields

		private readonly PresenterBinder _presenterBinder;

		#endregion

		#region Constructors

		public PresenterBinderWrapper(PresenterBinder presenterBinder)
		{
			if(presenterBinder == null)
				throw new ArgumentNullException("presenterBinder");

			this._presenterBinder = presenterBinder;
		}

		#endregion

		#region Methods

		public static PresenterBinderWrapper FromPresenterBinder(PresenterBinder presenterBinder)
		{
			return presenterBinder;
		}

		public virtual void PerformBinding()
		{
			this._presenterBinder.PerformBinding();
		}

		#endregion

		#region Implicit operator

		public static implicit operator PresenterBinderWrapper(PresenterBinder presenterBinder)
		{
			return presenterBinder == null ? null : new PresenterBinderWrapper(presenterBinder);
		}

		#endregion
	}
}