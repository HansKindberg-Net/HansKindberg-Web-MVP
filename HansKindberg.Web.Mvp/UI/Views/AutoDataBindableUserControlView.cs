using System;
using System.ComponentModel;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public abstract class AutoDataBindableUserControlView : UserControlView, IAutoDataBindableView
	{
		#region Fields

		private const string _autoDataBindViewStateName = "AutoDataBind";

		#endregion

		#region Events

		public event EventHandler CreatingChildControls;
		public event EventHandler<CancelEventArgs> DataBindingChildren;
		public event EventHandler<CancelEventArgs> EnsuringChildControls;

		#endregion

		#region Properties

		[Category("Information"), DefaultValue(false)]
		public virtual bool AutoDataBind
		{
			get { return (this.ViewState[_autoDataBindViewStateName] as bool? ?? (bool?) false).Value; }
			set { this.ViewState[_autoDataBindViewStateName] = value; }
		}

		[Browsable(false)]
		public new virtual bool DesignMode
		{
			get { return base.DesignMode; }
		}

		#endregion

		#region Methods

		protected override void CreateChildControls()
		{
			if(this.CreatingChildControls != null)
				this.CreatingChildControls(this, EventArgs.Empty);
		}

		public override void DataBind()
		{
			this.DataBind(true, true, true);
		}

		public virtual void DataBind(bool raiseOnDataBinding, bool ensureChildControls, bool dataBindChildren)
		{
			if(raiseOnDataBinding)
				this.OnDataBinding(EventArgs.Empty);

			if(ensureChildControls)
				this.EnsureChildControls();

			if(dataBindChildren)
				this.DataBindChildren();
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

	public abstract class AutoDataBindableUserControlView<TModel> : AutoDataBindableUserControlView, IAutoDataBindableView<TModel> where TModel : class, new()
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