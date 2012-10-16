using WebFormsMvp.Web;

namespace HansKindberg.Web.Mvp.WebApplication.Views
{
	public abstract class View<TModel> : MvpPage<TModel> where TModel : class, new()
	{
		#region Constructors

		protected View()
		{
			this.AutoDataBind = false;
		}

		#endregion
	}
}