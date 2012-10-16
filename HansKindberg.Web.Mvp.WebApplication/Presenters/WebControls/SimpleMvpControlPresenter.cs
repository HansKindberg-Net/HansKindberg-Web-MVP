using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;
using HansKindberg.Web.Mvp.UI.Presenters;
using HansKindberg.Web.Mvp.WebApplication.Views.WebControls;

namespace HansKindberg.Web.Mvp.WebApplication.Presenters.WebControls
{
	public class SimpleMvpControlPresenter : ControlPresenter<ISimpleMvpControlView>
	{
		#region Constructors

		public SimpleMvpControlPresenter(ISimpleMvpControlView view) : base(view)
		{
			this.View.CreatingChildControls += this.OnViewCreatingChildControls;
		}

		#endregion

		#region Eventhandlers

		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Web.UI.WebControls.Literal.set_Text(System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EventArgs")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OnViewCreatingChildControls")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		protected internal virtual void OnViewCreatingChildControls(object sender, EventArgs e)
		{
			this.View.Controls.Add(new Literal
				{
					Text =
						"<p><strong>Control</strong>: " + this.View.GetType().FullName + "</p>" +
						"<p><strong>Presenter</strong>: " + this.GetType().FullName + "</p>" +
						"<p>This text comes from " + this.GetType().FullName + ".OnViewCreatingChildControls(object sender, EventArgs e).</p>"
				});
		}

		#endregion
	}
}