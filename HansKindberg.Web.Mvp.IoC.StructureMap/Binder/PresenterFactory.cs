using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using StructureMap;
using StructureMap.Pipeline;
using WebFormsMvp;
using WebFormsMvp.Binder;

namespace HansKindberg.Web.Mvp.IoC.StructureMap.Binder
{
	[CLSCompliant(false)]
	public class PresenterFactory : IPresenterFactory, IDisposable
	{
		#region Fields

		private readonly IContainer _container;
		private readonly object _registerLock = new object();

		#endregion

		#region Constructors

		public PresenterFactory(IContainer container)
		{
			if(container == null)
				throw new ArgumentNullException("container");

			this._container = container;
		}

		#endregion

		#region Methods

		public virtual IPresenter Create(Type presenterType, Type viewType, IView viewInstance)
		{
			if(presenterType == null)
				throw new ArgumentNullException("presenterType");

			if(!typeof(IPresenter).IsAssignableFrom(presenterType))
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The presenter-type \"{0}\" does not implement \"{1}\".", presenterType.FullName, typeof(IPresenter).FullName), "presenterType");

			if(viewType == null)
				throw new ArgumentNullException("viewType");

			if(!typeof(IView).IsAssignableFrom(viewType))
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The view-type \"{0}\" does not implement \"{1}\".", viewType.FullName, typeof(IView).FullName), "viewType");

			if(viewInstance == null)
				throw new ArgumentNullException("viewInstance");

			ILifecycle lifecycle = this._container.GetInstance<ILifecycle>();

			lock(this._registerLock)
			{
				this._container.Configure(
					configuration =>
					configuration.For(this.GetAbstractViewType(viewType)).LifecycleIs(lifecycle).Use(viewInstance));
			}

			IPresenter presenter = (IPresenter) this._container.GetInstance(presenterType);

			if(presenter == null)
				throw new StructureMapException(202, new object[] {presenterType});

			return presenter;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if(disposing && this._container != null)
				this._container.Dispose();
		}

		protected virtual Type GetAbstractViewType(Type viewType)
		{
			if(viewType == null)
				throw new ArgumentNullException("viewType");

			IEnumerable<Type> allInterfaces = viewType.GetInterfaces();
			IEnumerable<Type> topInterfaces = allInterfaces.Except(allInterfaces.SelectMany(type => type.GetInterfaces()));
			return topInterfaces.SingleOrDefault(type => typeof(IView).IsAssignableFrom(type)) ?? viewType;
		}

		public virtual void Release(IPresenter presenter)
		{
			this._container.EjectAllInstancesOf<IPresenter>();

			IDisposable disposablePresenter = presenter as IDisposable;

			if(disposablePresenter != null)
				disposablePresenter.Dispose();
		}

		#endregion
	}
}