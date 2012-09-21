using System;
using System.Collections.Generic;
using System.Linq;
using HansKindberg.Web.Mvp.IoC.StructureMap.Binder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StructureMap;
using StructureMap.Pipeline;
using StructureMap.Query;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.IoC.StructureMap.Tests.Binder
{
	[TestClass]
	public class PresenterFactoryTest
	{
		#region Methods

		[TestMethod]
		public void Assumption_Test()
		{
			ClearStructureMap();

			ValidateThatStructureMapIsCleared();

			ObjectFactory.Initialize(initializer => initializer.For<ILifecycle>().Use<ThreadLocalStorageLifecycle>());

			IContainer container = ObjectFactory.Container;

			ILifecycle lifecycle = container.GetInstance<ILifecycle>();

			Type viewType = typeof(IView);
			IView viewInstance = Mock.Of<IView>();

			container.Configure(configuration => configuration.For(viewType).LifecycleIs(lifecycle).Use(viewInstance));

			IEnumerable<InstanceRef> allInstances = ObjectFactory.Model.AllInstances.ToList();
			Assert.AreEqual(4, allInstances.Count());

			IView viewFromStructureMap = container.GetInstance<IView>();
			Assert.AreEqual(viewInstance, viewFromStructureMap);

			IView anotherViewInstance = Mock.Of<IView>();

			container.Configure(configuration => configuration.For(viewType).LifecycleIs(lifecycle).Use(anotherViewInstance));

			allInstances = ObjectFactory.Model.AllInstances.ToList();
			Assert.AreEqual(5, allInstances.Count());

			viewFromStructureMap = container.GetInstance<IView>();
			Assert.AreEqual(anotherViewInstance, viewFromStructureMap);

			IView yetAnotherViewInstance = Mock.Of<IView>();

			container.Configure(configuration => configuration.For(viewType).LifecycleIs(lifecycle).Use(yetAnotherViewInstance));

			allInstances = container.Model.AllInstances.ToList();
			Assert.AreEqual(6, allInstances.Count());

			viewFromStructureMap = container.GetInstance<IView>();
			Assert.AreEqual(yetAnotherViewInstance, viewFromStructureMap);

			Assert.AreEqual(3, lifecycle.FindCache().Count);
			lifecycle.FindCache().DisposeAndClear();
			Assert.AreEqual(0, lifecycle.FindCache().Count);
			lifecycle.EjectAll();

			viewFromStructureMap = container.GetInstance<IView>();
			Assert.AreEqual(yetAnotherViewInstance, viewFromStructureMap);

			container.EjectAllInstancesOf<IView>();
			allInstances = container.Model.AllInstances.ToList();
			Assert.AreEqual(3, allInstances.Count());

			viewFromStructureMap = container.TryGetInstance<IView>();
			Assert.IsNull(viewFromStructureMap);

			//Assert.Fail(container.WhatDoIHave());
		}

		private static void ClearStructureMap()
		{
			ObjectFactory.Initialize(initialization => { });
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_IfTheContainerParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			using(new PresenterFactory(null)) {}
		}

		[TestMethod]
		[ExpectedException(typeof(StructureMapException))]
		public void Create_IfNotILifecycleIsRegistered_ShouldThrowAStructureMapException()
		{
			ClearStructureMap();

			using(PresenterFactory presenterFactory = new PresenterFactory(ObjectFactory.Container))
			{
				presenterFactory.Create(typeof(IPresenter), typeof(IView), Mock.Of<IView>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(StructureMapException))]
		public void Create_IfStructureMapCannotGetAnInstanceOfThePresenterType_ShouldThrowAStructureMapException()
		{
			using(PresenterFactory presenterFactory = new PresenterFactory(Mock.Of<IContainer>()))
			{
				presenterFactory.Create(typeof(IPresenter), typeof(IView), Mock.Of<IView>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Create_IfThePresenterTypeParameterDoesNotImplementWebFormsMvpIPresenter_ShouldThrowAnArgumentException()
		{
			using(PresenterFactory presenterFactory = new PresenterFactory(Mock.Of<IContainer>()))
			{
				presenterFactory.Create(typeof(object), typeof(IView), Mock.Of<IView>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_IfThePresenterTypeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			using(PresenterFactory presenterFactory = new PresenterFactory(Mock.Of<IContainer>()))
			{
				presenterFactory.Create(null, Mock.Of<Type>(), Mock.Of<IView>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_IfTheViewInstanceParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			using(PresenterFactory presenterFactory = new PresenterFactory(Mock.Of<IContainer>()))
			{
				presenterFactory.Create(typeof(IPresenter), typeof(IView), null);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Create_IfTheViewTypeParameterDoesNotImplementWebFormsMvpIView_ShouldThrowAnArgumentException()
		{
			using(PresenterFactory presenterFactory = new PresenterFactory(Mock.Of<IContainer>()))
			{
				presenterFactory.Create(typeof(IPresenter), typeof(object), Mock.Of<IView>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_IfTheViewTypeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			using(PresenterFactory presenterFactory = new PresenterFactory(Mock.Of<IContainer>()))
			{
				presenterFactory.Create(typeof(IPresenter), null, Mock.Of<IView>());
			}
		}

		[TestMethod]
		public void Create_Test()
		{
			ClearStructureMap();

			IComparable comparable = Mock.Of<IComparable>();
			IDisposable disposable = Mock.Of<IDisposable>();

			ObjectFactory.Initialize(initializer =>
			{
				initializer.For<ILifecycle>().Use<ThreadLocalStorageLifecycle>();
				initializer.For<IComparable>().Use(comparable);
				initializer.For<IDisposable>().Use(disposable);
			});

			using(PresenterFactory presenterFactory = new PresenterFactory(ObjectFactory.Container))
			{
				IView view = Mock.Of<IView>();
				IPresenter presenter = presenterFactory.Create(typeof(PresenterFactoryTestPresenter), typeof(IView), Mock.Of<IView>());
				Assert.IsNotNull(presenter);
				presenter = presenterFactory.Create(typeof(PresenterFactoryTestPresenter), typeof(IView), view);
				Assert.IsNotNull(presenter);
				PresenterFactoryTestPresenter presenterFactoryTestPresenter = (PresenterFactoryTestPresenter) presenter;
				Assert.IsNotNull(presenterFactoryTestPresenter);
				Assert.AreEqual(view, presenterFactoryTestPresenter.View);
				Assert.AreEqual(view, presenterFactoryTestPresenter.SecondView);
				Assert.AreEqual(comparable, presenterFactoryTestPresenter.Comparable);
				Assert.AreEqual(disposable, presenterFactoryTestPresenter.Disposable);
			}
		}

		[TestMethod]
		public void Dispose_ShouldDisposeTheStructureMapContainer()
		{
			bool containerIsDisposed = false;
			Mock<IContainer> containerMock = new Mock<IContainer>();
			containerMock.As<IDisposable>().Setup(disposable => disposable.Dispose()).Callback(() => containerIsDisposed = true);
			// ReSharper disable CSharpWarnings::CS0183
			Assert.IsTrue(containerMock.Object is IDisposable);
			// ReSharper restore CSharpWarnings::CS0183
			Assert.IsFalse(containerIsDisposed);
			PresenterFactory presenterFactory = new PresenterFactory(containerMock.Object);
			presenterFactory.Dispose();
			Assert.IsTrue(containerIsDisposed);
		}

		[TestMethod]
		public void Release_IfThePresenterParameterImplementsIDisposable_ShouldDisposeThePresenter()
		{
			bool presenterIsDisposed = false;
			Mock<IPresenter> presenterMock = new Mock<IPresenter>();
			presenterMock.As<IDisposable>().Setup(disposable => disposable.Dispose()).Callback(() => presenterIsDisposed = true);
			Assert.IsTrue(presenterMock.Object is IDisposable);
			Assert.IsFalse(presenterIsDisposed);
			using(PresenterFactory presenterFactory = new PresenterFactory(Mock.Of<IContainer>()))
			{
				presenterFactory.Release(presenterMock.Object);
			}
			Assert.IsTrue(presenterIsDisposed);
		}

		private static void ValidateThatStructureMapIsCleared()
		{
			IEnumerable<InstanceRef> allInstances = ObjectFactory.Model.AllInstances.ToList();
			Assert.AreEqual(2, allInstances.Count());
			Assert.AreEqual(typeof(Func<>), allInstances.ElementAt(0).PluginType);
			Assert.AreEqual(typeof(IContainer), allInstances.ElementAt(1).PluginType);
		}

		#endregion
	}

	internal class PresenterFactoryTestPresenter : Presenter<IView>
	{
		#region Fields

		private readonly IComparable _comparable;
		private readonly IDisposable _disposable;
		private readonly IView _secondView;

		#endregion

		#region Constructors

		public PresenterFactoryTestPresenter(IView view) : this(Mock.Of<IComparable>(), view, Mock.Of<IDisposable>(), view) {}

		public PresenterFactoryTestPresenter(IComparable comparable, IView firstView, IDisposable disposable, IView secondView) : base(firstView)
		{
			if(comparable == null)
				throw new ArgumentNullException("comparable");

			if(disposable == null)
				throw new ArgumentNullException("disposable");

			if(secondView == null)
				throw new ArgumentNullException("secondView");

			this._comparable = comparable;
			this._disposable = disposable;

			this._secondView = secondView;
		}

		#endregion

		#region Properties

		public virtual IComparable Comparable
		{
			get { return this._comparable; }
		}

		public virtual IDisposable Disposable
		{
			get { return this._disposable; }
		}

		public virtual IView SecondView
		{
			get { return this._secondView; }
		}

		#endregion
	}
}