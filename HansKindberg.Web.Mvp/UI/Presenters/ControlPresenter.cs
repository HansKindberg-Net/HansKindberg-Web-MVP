using System.Web.UI;
using HansKindberg.Web.Mvp.UI.Views;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.UI.Presenters
{
	public abstract class ControlPresenter<TView> : Presenter<TView>
		where TView : class, IControlView
	{
		#region Constructors

		protected ControlPresenter(TView view) : base(view) {}

		#endregion

		#region Methods

		protected internal virtual Control FindControlHierarchic(string controlId)
		{
			return this.View.FindControl(controlId) ?? this.FindControlHierarchic(this.View.NamingContainer, controlId);
		}

		protected internal virtual Control FindControlHierarchic(Control namingContainer, string controlId)
		{
			if(namingContainer == null)
				return null;

			return namingContainer.FindControl(controlId) ?? this.FindControlHierarchic(namingContainer.NamingContainer, controlId);
		}

		#endregion
	}
}