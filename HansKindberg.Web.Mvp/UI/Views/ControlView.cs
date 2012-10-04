using System;
using System.ComponentModel;
using System.Web.UI;
using WebFormsMvp.Web;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public abstract class ControlView : Control, IControlView
	{
		#region Fields

		private const string _ensureChildControlsEnabledViewStateName = "EnsureChildControlsEnabled";

		#endregion

		#region Events

		public event EventHandler<CancelEventArgs> DataBindingChildren;
		public event EventHandler<CancelEventArgs> EnsuringChildControls;

		#endregion

		#region Properties

		[Bindable(true), Category("Information"), DefaultValue(false)]
		public virtual bool EnsureChildControlsEnabled
		{
			get
			{
				bool? nullable = this.ViewState[_ensureChildControlsEnabledViewStateName] as bool?;
				return nullable.HasValue && nullable.Value;
			}
			set { this.ViewState[_ensureChildControlsEnabledViewStateName] = value; }
		}

		public virtual bool ThrowExceptionIfNoPresenterBound
		{
			get { return true; }
		}

		#endregion

		#region Methods

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
			this.EnsureChildControls(false);
		}

		public virtual void EnsureChildControls(bool force)
		{
			if(!force && this.EnsuringChildControls != null)
			{
				CancelEventArgs cancelEventArgs = new CancelEventArgs();

				this.EnsuringChildControls(this, cancelEventArgs);

				if(cancelEventArgs.Cancel)
					return;
			}

			base.EnsureChildControls();
		}

		#endregion

		#region Eventhandlers

		protected override void OnInit(EventArgs e)
		{
			PageViewHost.Register(this, this.Context, false);
			base.OnInit(e);
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