using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI.WebControls;
using HansKindberg.Web.Mvp.UI.Presenters;
using HansKindberg.Web.Mvp.WebApplication.WebControls.Views;

namespace HansKindberg.Web.Mvp.WebApplication.WebControls.Presenters
{
	public class PresenterDiscoveryStrategySampleControlPresenter : ControlPresenter<IPresenterDiscoveryStrategySampleControlView>
	{
		#region Constructors

		public PresenterDiscoveryStrategySampleControlPresenter(IPresenterDiscoveryStrategySampleControlView view) : base(view)
		{
			this.View.CreatingChildControls += this.OnViewCreatingChildControls;
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Web.UI.WebControls.Literal.set_Text(System.String)")]
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EventArgs")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OnViewCreatingChildControls")]
		protected internal virtual void OnViewCreatingChildControls(object sender, EventArgs e)
		{
			this.View.Controls.Add(new Literal {Text = string.Format(CultureInfo.InvariantCulture, "<p>This text is added in {0}.OnViewCreatingChildControls(object sender, EventArgs e).</p>", this.GetType().FullName)});
		}

		#endregion
	}
}