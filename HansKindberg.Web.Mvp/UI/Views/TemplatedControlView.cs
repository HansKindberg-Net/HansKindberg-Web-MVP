using System;
using System.Web.UI;

namespace HansKindberg.Web.Mvp.UI.Views
{
	[ParseChildren(ChildrenAsProperties = true), PersistChildren(false)]
	public abstract class TemplatedControlView : ControlView, ITemplatedControlView
	{
		#region Events

		public event EventHandler CreatingChildControls;

		#endregion

		#region Methods

		protected override void CreateChildControls()
		{
			if(this.CreatingChildControls != null)
				this.CreatingChildControls(this, EventArgs.Empty);
		}

		#endregion
	}

	public abstract class TemplatedControlView<TModel> : TemplatedControlView, ITemplatedControlView<TModel> where TModel : class, new()
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