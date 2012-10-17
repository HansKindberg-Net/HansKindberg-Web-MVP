using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using HansKindberg.Web.Mvp.UI.Views;

namespace HansKindberg.Web.Mvp.UI.Presenters
{
	public abstract class TemplatedControlPresenter<TView> : AutoDataBindControlPresenter<TView>
		where TView : class, ITemplatedControlView
	{
		#region Constructors

		protected TemplatedControlPresenter(TView view) : base(view)
		{
			this.View.CreatingChildControls += this.OnViewCreatingChildControls;
			this.View.InvalidatingInternalState += this.OnViewInvalidatingInternalState;
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "template")]
		protected internal virtual void AddTemplate(Control container, ITemplate template)
		{
			if(container == null)
				throw new ArgumentNullException("container");

			if(template == null)
				throw new ArgumentNullException("template");

			template.InstantiateIn(container);
			this.View.Controls.Add(container);
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewCreatingChildControls(object sender, EventArgs e)
		{
			if(!this.OnDataBindingRaised)
				this.View.DataBind(true, false, false);
		}

		protected internal abstract void OnViewInvalidatingInternalState(object sender, EventArgs e);

		#endregion
	}
}