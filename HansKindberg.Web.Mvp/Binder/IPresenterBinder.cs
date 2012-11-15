using WebFormsMvp;

namespace HansKindberg.Web.Mvp.Binder
{
	public interface IPresenterBinder
	{
		#region Properties

		IMessageCoordinator MessageCoordinator { get; }

		#endregion

		#region Methods

		void PerformBinding();

		#endregion
	}
}