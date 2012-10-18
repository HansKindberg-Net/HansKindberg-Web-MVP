using System;
using System.ComponentModel;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public abstract class AutoDataBindControlView : ControlView, IAutoDataBindableView
	{
		#region Fields

		private const string _autoDataBindViewStateName = "AutoDataBind";

		#endregion

		#region Properties

		[Category("Information"), DefaultValue(false)]
		public virtual bool AutoDataBind
		{
			get { return (this.ViewState[_autoDataBindViewStateName] as bool? ?? (bool?) false).Value; }
			set { this.ViewState[_autoDataBindViewStateName] = value; }
		}

		#endregion

		#region Methods

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

		#endregion
	}

	public abstract class AutoDataBindControlView<TModel> : AutoDataBindControlView, IAutoDataBindableView<TModel> where TModel : class, new()
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