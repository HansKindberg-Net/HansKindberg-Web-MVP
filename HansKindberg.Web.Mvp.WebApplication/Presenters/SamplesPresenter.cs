using HansKindberg.Web.Mvp.WebApplication.Views;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.WebApplication.Presenters
{
	public class SamplesPresenter : Presenter<ISamplesView>
	{
		#region Constructors

		public SamplesPresenter(ISamplesView view) : base(view) {}

		#endregion
	}
}