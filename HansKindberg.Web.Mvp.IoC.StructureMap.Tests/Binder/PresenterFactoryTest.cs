using System;
using HansKindberg.Web.Mvp.IoC.StructureMap.Binder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StructureMap;
using WebFormsMvp;

namespace HansKindberg.Web.Mvp.IoC.StructureMap.Tests.Binder
{
	[TestClass]
	public class PresenterFactoryTest
	{
		#region Methods

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

		private static PresenterFactory CreatePresenterFactory()
		{
			return new PresenterFactory(Mock.Of<IContainer>());
		}

		[TestMethod]
		[ExpectedException(typeof(StructureMapException))]
		public void Create_IfStructureMapCannotGetAnInstanceOfThePresenterType_ShouldThrowAStructureMapException()
		{
			ClearStructureMap();

			using(PresenterFactory presenterFactory = CreatePresenterFactory())
			{
				presenterFactory.Create(Mock.Of<IPresenter<IView>>().GetType(), typeof(IView), Mock.Of<IView>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Create_IfThePresenterTypeParameterDoesNotImplementWebFormsMvpIPresenterWithOneGenericArgument_ShouldThrowAnArgumentException()
		{
			using(PresenterFactory presenterFactory = CreatePresenterFactory())
			{
				presenterFactory.Create(typeof(IPresenter), typeof(IView), Mock.Of<IView>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Create_IfThePresenterTypeParameterDoesNotImplementWebFormsMvpIPresenter_ShouldThrowAnArgumentException()
		{
			using(PresenterFactory presenterFactory = CreatePresenterFactory())
			{
				presenterFactory.Create(typeof(object), typeof(IView), Mock.Of<IView>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_IfThePresenterTypeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			using(PresenterFactory presenterFactory = CreatePresenterFactory())
			{
				presenterFactory.Create(null, Mock.Of<Type>(), Mock.Of<IView>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_IfTheViewInstanceParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			using(PresenterFactory presenterFactory = CreatePresenterFactory())
			{
				presenterFactory.Create(Mock.Of<IPresenter<IView>>().GetType(), typeof(IView), null);
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void Create_IfTheViewTypeParameterDoesNotImplementWebFormsMvpIView_ShouldThrowAnArgumentException()
		{
			using(PresenterFactory presenterFactory = CreatePresenterFactory())
			{
				presenterFactory.Create(Mock.Of<IPresenter<IView>>().GetType(), typeof(object), Mock.Of<IView>());
			}
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Create_IfTheViewTypeParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			using(PresenterFactory presenterFactory = CreatePresenterFactory())
			{
				presenterFactory.Create(Mock.Of<IPresenter<IView>>().GetType(), null, Mock.Of<IView>());
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
			using(PresenterFactory presenterFactory = CreatePresenterFactory())
			{
				presenterFactory.Release(presenterMock.Object);
			}
			Assert.IsTrue(presenterIsDisposed);
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