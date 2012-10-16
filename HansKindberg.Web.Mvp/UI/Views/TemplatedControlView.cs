using System.Web.UI;

namespace HansKindberg.Web.Mvp.UI.Views
{
	[ParseChildren(ChildrenAsProperties = true), PersistChildren(false)]
	public abstract class TemplatedControlView : AutoDataBindControlView, ITemplatedControlView {}

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