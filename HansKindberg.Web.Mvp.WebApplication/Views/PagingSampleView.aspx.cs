using System.Collections.Generic;
using HansKindberg.Web.Mvp.WebApplication.Models;
using HansKindberg.Web.Mvp.WebApplication.Presenters;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.WebApplication.Views
{
	[PresenterBinding(typeof(PagingSamplePresenter))]
	public partial class PagingSampleView : View<PagingSampleModel>, IPagingSampleView
	{
		#region Fields

		private List<int> _dataSource;
		private const int _numberOfItems = 11312;

		#endregion

		#region Properties

		protected virtual IEnumerable<int> DataSource
		{
			get
			{
				if(this._dataSource == null)
				{
					this._dataSource = new List<int>();

					for(int i = 1; i <= _numberOfItems; i++)
					{
						this._dataSource.Add(i);
					}
				}

				return this._dataSource.ToArray();
			}
		}

		protected virtual int ItemsPerPagingItem
		{
			get { return 10; }
		}

		#endregion
	}
}