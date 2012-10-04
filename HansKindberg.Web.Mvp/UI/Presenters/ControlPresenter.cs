using System;
using System.ComponentModel;
using HansKindberg.Web.Mvp.UI.Views;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.UI.Presenters
{
	public abstract class ControlPresenter<TView> : Presenter<TView>
		where TView : class, IControlView
	{
		#region Constructors

		protected ControlPresenter(TView view) : base(view)
		{
			this.View.DataBindingChildren += this.OnViewDataBindingChildren;
			this.View.EnsuringChildControls += this.OnViewEnsuringChildControls;
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewDataBindingChildren(object sender, CancelEventArgs e)
		{
			if(e == null)
				throw new ArgumentNullException("e");

			if(e.Cancel)
				return;

			this.View.EnsureChildControls(true);
		}

		protected internal virtual void OnViewEnsuringChildControls(object sender, CancelEventArgs e)
		{
			if(e == null)
				throw new ArgumentNullException("e");

			e.Cancel = !this.View.EnsureChildControlsEnabled;
		}

		#endregion
	}
}