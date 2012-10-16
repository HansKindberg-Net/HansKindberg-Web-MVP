using System;
using HansKindberg.Web.Mvp.WebApplication.Core;
using HansKindberg.Web.Mvp.WebApplication.Views.MasterViews;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.WebApplication.Presenters.MasterPresenters
{
	public class DefaultMasterPresenter : Presenter<IDefaultMasterView>
	{
		#region Fields

		private bool? _homeIsActive;
		private IIsActiveFilePath _isActiveFilePath;

		#endregion

		#region Constructors

		public DefaultMasterPresenter(IDefaultMasterView view) : base(view)
		{
			this.View.PreRender += this.OnViewPreRender;
		}

		#endregion

		#region Properties

		protected internal virtual bool HomeIsActive
		{
			get
			{
				if(this._homeIsActive == null)
					this._homeIsActive = this.IsActiveFilePath["/Views/DefaultView.aspx"];

				return this._homeIsActive.Value;
			}
		}

		protected internal virtual bool IncludeSubNavigation
		{
			get { return !this.HomeIsActive; }
		}

		protected internal virtual IIsActiveFilePath IsActiveFilePath
		{
			get { return this._isActiveFilePath ?? (this._isActiveFilePath = new IsActiveFilePath(this.Request)); }
		}

		protected internal virtual bool SamplesIsActive
		{
			get { return !this.HomeIsActive; }
		}

		#endregion

		#region Methods

		protected internal virtual void PopulateModel()
		{
			this.View.Model.Heading = this.View.Model.Title = this.View.Page.Title;
			this.View.Model.HomeIsActive = this.HomeIsActive;
			this.View.Model.IncludeSubNavigation = this.IncludeSubNavigation;
			this.View.Model.IsActiveFilePath = this.IsActiveFilePath;
			this.View.Model.SamplesIsActive = this.SamplesIsActive;
		}

		protected internal virtual void ResolveControls()
		{
			this.View.SubNavigationAreaControl.Visible = this.IncludeSubNavigation;

			if(this.IncludeSubNavigation)
				this.View.SubNavigationAreaControl.DataBind();
		}

		#endregion

		#region Eventhandlers

		protected internal virtual void OnViewPreRender(object sender, EventArgs e)
		{
			this.PopulateModel();
			this.ResolveControls();
		}

		#endregion
	}
}