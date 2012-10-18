using System;
using System.ComponentModel;
using HansKindberg.Web.Mvp.UI.Views;

namespace HansKindberg.Web.Mvp.UI.Presenters
{
	public abstract class AutoDataBindablePresenter<TView> : ControlPresenter<TView>
		where TView : class, IAutoDataBindableView
	{
		#region Constructors

		protected AutoDataBindablePresenter(TView view) : base(view)
		{
			this.View.DataBinding += this.OnViewDataBinding;
			this.View.DataBindingChildren += this.OnViewDataBindingChildren;
			this.View.EnsuringChildControls += this.OnViewEnsuringChildControls;
			this.View.PreRender += this.OnViewPreRender;
		}

		#endregion

		#region Properties

		protected internal virtual bool ChildControlsAreEnsured { get; set; }
		protected internal virtual bool ChildrenAreDataBound { get; set; }
		protected internal virtual bool OnDataBindingRaised { get; set; }

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewDataBinding(object sender, EventArgs e)
		{
			this.OnDataBindingRaised = true;
		}

		protected internal virtual void OnViewDataBindingChildren(object sender, CancelEventArgs e)
		{
			if(e == null)
				throw new ArgumentNullException("e");

			if(!e.Cancel)
				this.ChildrenAreDataBound = true;
		}

		protected internal virtual void OnViewEnsuringChildControls(object sender, CancelEventArgs e)
		{
			if(e == null)
				throw new ArgumentNullException("e");

			if(!e.Cancel)
				this.ChildControlsAreEnsured = true;
		}

		protected internal virtual void OnViewPreRender(object sender, EventArgs e)
		{
			if(!this.View.AutoDataBind)
				return;

			if(!this.OnDataBindingRaised || !this.ChildrenAreDataBound)
				this.View.DataBind(!this.OnDataBindingRaised, !this.ChildControlsAreEnsured, !this.ChildrenAreDataBound);
		}

		#endregion
	}
}