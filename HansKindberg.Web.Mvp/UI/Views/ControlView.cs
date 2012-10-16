using System;
using System.ComponentModel;
using System.Web.UI;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public abstract class ControlView : Control, IControlView
	{
		#region Events

		public event EventHandler CreatingChildControls;
		public event EventHandler<CancelEventArgs> DataBindingChildren;
		public event EventHandler<CancelEventArgs> EnsuringChildControls;

		#endregion

		#region Properties

		[Browsable(false)]
		public new virtual bool DesignMode
		{
			get { return base.DesignMode; }
		}

		// ReSharper disable InconsistentNaming
		protected internal virtual IPage IPage // ReSharper restore InconsistentNaming
		{
			get { return (PageWrapper) this.Page; }
		}

		[Browsable(false)]
		public virtual bool ThrowExceptionIfNoPresenterBound
		{
			get { return true; }
		}

		#endregion

		#region Eventhandlers

		protected override void OnInit(EventArgs e)
		{
			PageViewHostWrapper.Register(this, this.Context, false, this.IPage);
			base.OnInit(e);
		}

		#endregion

		#region Methods

		protected override void CreateChildControls()
		{
			if(this.CreatingChildControls != null)
				this.CreatingChildControls(this, EventArgs.Empty);
		}

		protected override void DataBindChildren()
		{
			if(this.DataBindingChildren != null)
			{
				CancelEventArgs cancelEventArgs = new CancelEventArgs();

				this.DataBindingChildren(this, cancelEventArgs);

				if(cancelEventArgs.Cancel)
					return;
			}

			base.DataBindChildren();
		}

		protected override void EnsureChildControls()
		{
			if(this.EnsuringChildControls != null)
			{
				CancelEventArgs cancelEventArgs = new CancelEventArgs();

				this.EnsuringChildControls(this, cancelEventArgs);

				if(cancelEventArgs.Cancel)
					return;
			}

			base.EnsureChildControls();
		}

		#endregion
	}

	public abstract class ControlView<TModel> : ControlView, IControlView<TModel> where TModel : class, new()
	{
		#region Fields

		private TModel _model;

		#endregion

		#region Properties

		public virtual TModel Model
		{
			get
			{
				ViewHelper.ThrowExceptionIfModelIsNull(this._model);

				return this._model;
			}
			set { this._model = value; }
		}

		#endregion
	}
}