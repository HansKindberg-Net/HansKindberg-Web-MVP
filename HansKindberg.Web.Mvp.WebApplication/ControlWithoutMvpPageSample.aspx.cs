namespace HansKindberg.Web.Mvp.WebApplication
{
	public partial class ControlWithoutMvpPageSample : System.Web.UI.Page
	{
		#region Methods

		protected override void OnLoad(System.EventArgs e)
		{
			base.OnLoad(e);

			if(!this.IsPostBack)
				this.repeater.DataBind();
		}

		#endregion
	}
}