using System;
using System.ComponentModel;
using System.Reflection;
using HansKindberg.Web.Mvp.UI.Presenters;
using HansKindberg.Web.Mvp.UI.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Web.Mvp.Tests.UI.Presenters
{
	[TestClass]
	public class ControlPresenterTest
	{
		#region Methods

		private static void ConstructorShouldAddAnEventHandler(string eventName)
		{
			using(ControlPresenterTestControlView view = new ControlPresenterTestControlView())
			{
				FieldInfo eventField = typeof(ControlView).GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic);
				Assert.IsNotNull(eventField);
				Assert.IsNull(eventField.GetValue(view));

				ControlPresenterTestControlPresenter<ControlPresenterTestControlView> presenter = new ControlPresenterTestControlPresenter<ControlPresenterTestControlView>(view);
				Assert.IsNotNull(presenter);

				Delegate eventDelegate = (Delegate) eventField.GetValue(view);
				Assert.AreEqual(1, eventDelegate.GetInvocationList().Length);
				Assert.AreEqual("OnView" + eventName, eventDelegate.GetInvocationList()[0].Method.Name);
			}
		}

		[TestMethod]
		public void Constructor_ShouldAddADataBindingChildrenEventHandler()
		{
			ConstructorShouldAddAnEventHandler("DataBindingChildren");
		}

		[TestMethod]
		public void Constructor_ShouldAddAnEnsuringChildControlsEventHandler()
		{
			ConstructorShouldAddAnEventHandler("EnsuringChildControls");
		}

		[TestMethod]
		public void OnViewDataBindingChildren_IfTheCancelEventArgsParameterHasCancelSetToFalse_ShouldCallEnsureChildControlsWithTheForceParameterSetToTrue()
		{
			bool ensureChildControlsCalled = false;
			bool forceEnsureChildControls = false;

			Mock<IControlView> viewMock = new Mock<IControlView>();
			viewMock.Setup(view => view.EnsureChildControls(It.IsAny<bool>())).Callback<bool>(delegate(bool force)
			{
				ensureChildControlsCalled = true;
				forceEnsureChildControls = force;
			});

			Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] {viewMock.Object}) {CallBase = true};
			presenterMock.Object.OnViewDataBindingChildren(DateTime.Now.Second%2 == 0 ? new object() : null, new CancelEventArgs(false));

			Assert.IsTrue(ensureChildControlsCalled);
			Assert.IsTrue(forceEnsureChildControls);
		}

		[TestMethod]
		public void OnViewDataBindingChildren_IfTheCancelEventArgsParameterHasCancelSetToTrue_ShouldNotCallEnsureChildControls()
		{
			bool ensureChildControlsCalled = false;

			Mock<IControlView> viewMock = new Mock<IControlView>();
			viewMock.Setup(view => view.EnsureChildControls(It.IsAny<bool>())).Callback(() => ensureChildControlsCalled = true);

			Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] {viewMock.Object}) {CallBase = true};
			presenterMock.Object.OnViewDataBindingChildren(DateTime.Now.Second%2 == 0 ? new object() : null, new CancelEventArgs(true));

			Assert.IsFalse(ensureChildControlsCalled);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void OnViewDataBindingChildren_IfTheCancelEventArgsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] {Mock.Of<IControlView>()}) {CallBase = true};
			presenterMock.Object.OnViewDataBindingChildren(DateTime.Now.Second%2 == 0 ? new object() : null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void OnViewEnsuringChildControls_IfTheCancelEventArgsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] {Mock.Of<IControlView>()}) {CallBase = true};
			presenterMock.Object.OnViewDataBindingChildren(DateTime.Now.Second%2 == 0 ? new object() : null, null);
		}

		[TestMethod]
		public void OnViewEnsuringChildControls_IfTheEnsureChildControlsEnabledPropertyIsSetToFalse_ShouldSetTheCancelEventArgsCancelParameterToTrue()
		{
			Mock<IControlView> viewMock = new Mock<IControlView>();
			viewMock.Setup(view => view.EnsureChildControlsEnabled).Returns(false);

			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			Assert.IsFalse(cancelEventArgs.Cancel);
			Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] {viewMock.Object}) {CallBase = true};
			presenterMock.Object.OnViewEnsuringChildControls(DateTime.Now.Second%2 == 0 ? new object() : null, cancelEventArgs);

			Assert.IsTrue(cancelEventArgs.Cancel);
		}

		[TestMethod]
		public void OnViewEnsuringChildControls_IfTheEnsureChildControlsEnabledPropertyIsSetToTrue_ShouldSetTheCancelEventArgsCancelParameterToFalse()
		{
			Mock<IControlView> viewMock = new Mock<IControlView>();
			viewMock.Setup(view => view.EnsureChildControlsEnabled).Returns(true);

			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			Assert.IsFalse(cancelEventArgs.Cancel);
			Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] {viewMock.Object}) {CallBase = true};
			presenterMock.Object.OnViewEnsuringChildControls(DateTime.Now.Second%2 == 0 ? new object() : null, cancelEventArgs);

			Assert.IsFalse(cancelEventArgs.Cancel);
		}

		#endregion
	}

	internal class ControlPresenterTestControlView : ControlView {}

	internal class ControlPresenterTestControlPresenter<TView> : ControlPresenter<TView>
		where TView : class, IControlView
	{
		#region Constructors

		public ControlPresenterTestControlPresenter(TView view) : base(view) {}

		#endregion
	}
}