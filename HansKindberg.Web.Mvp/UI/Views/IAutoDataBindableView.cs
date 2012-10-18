namespace HansKindberg.Web.Mvp.UI.Views
{
	public interface IAutoDataBindableView : IControlView
	{
		#region Properties

		bool AutoDataBind { get; set; }

		#endregion

		#region Methods

		void DataBind(bool raiseOnDataBinding, bool ensureChildControls, bool dataBindChildren);

		#endregion
	}

	public interface IAutoDataBindableView<TModel> : IAutoDataBindableView, IControlView<TModel> where TModel : class, new() {}
}