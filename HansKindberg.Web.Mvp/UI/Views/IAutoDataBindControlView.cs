namespace HansKindberg.Web.Mvp.UI.Views
{
	public interface IAutoDataBindControlView : IControlView
	{
		#region Properties

		bool AutoDataBind { get; set; }

		#endregion

		#region Methods

		void DataBind(bool raiseOnDataBinding, bool ensureChildControls, bool dataBindChildren);

		#endregion
	}

	public interface IAutoDataBindControlView<TModel> : IAutoDataBindControlView, IControlView<TModel> where TModel : class, new() {}
}