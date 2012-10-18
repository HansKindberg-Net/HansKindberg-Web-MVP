using System;
using System.Web.UI;

namespace HansKindberg.Web.Mvp.UI.Views
{
	[ParseChildren(ChildrenAsProperties = true), PersistChildren(false)]
	public abstract class TemplatedControlView : AutoDataBindableControlView, ITemplatedControlView
	{
		#region Events

		public event EventHandler InvalidatingInternalState;

		#endregion

		#region Methods

		protected internal virtual void InvalidateInternalState()
		{
			if(this.InvalidatingInternalState != null)
				this.InvalidatingInternalState(this, EventArgs.Empty);
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