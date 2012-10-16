using System;
using System.Web.UI;
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

		private static ControlPresenter<IControlView> CreateFindControlHierarchicPresenter(string controlId, Control foundControl)
		{
			if(controlId == null)
				throw new ArgumentNullException("controlId");

			if(foundControl == null)
				throw new ArgumentNullException("foundControl");

			Mock<Control> rootNamingContaierMock = new Mock<Control>();
			rootNamingContaierMock.Setup(control => control.FindControl(controlId)).Returns(foundControl);

			Mock<Control> childNamingContaierMock = new Mock<Control>();
			childNamingContaierMock.Setup(control => control.NamingContainer).Returns(rootNamingContaierMock.Object);
			childNamingContaierMock.Setup(control => control.FindControl(controlId)).Returns((Control) null);

			Mock<Control> grandChildNamingContaierMock = new Mock<Control>();
			grandChildNamingContaierMock.Setup(control => control.NamingContainer).Returns(childNamingContaierMock.Object);
			grandChildNamingContaierMock.Setup(control => control.FindControl(controlId)).Returns((Control) null);

			Mock<IControlView> viewMock = new Mock<IControlView>();
			viewMock.Setup(control => control.FindControl(controlId)).Returns((Control) null);
			viewMock.Setup(control => control.NamingContainer).Returns(grandChildNamingContaierMock.Object);

			return new Mock<ControlPresenter<IControlView>>(new object[] {viewMock.Object}) {CallBase = true}.Object;
		}

		[TestMethod]
		public void FindControlHierarchic_WithOneParameter_IfViewFindControlDoesNotReturnNull_ShouldNotCallFindControlHierarchicWithTwoParameters()
		{
			Mock<IControlView> viewMock = new Mock<IControlView>();
			viewMock.Setup(view => view.FindControl(It.IsAny<string>())).Returns(new Mock<Control>().Object);

			Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] {viewMock.Object}) {CallBase = true};
			presenterMock.Verify(presenter => presenter.FindControlHierarchic(It.IsAny<Control>(), It.IsAny<string>()), Times.Never());
			presenterMock.Object.FindControlHierarchic("Test");
			presenterMock.Verify(presenter => presenter.FindControlHierarchic(It.IsAny<Control>(), It.IsAny<string>()), Times.Never());
		}

		[TestMethod]
		public void FindControlHierarchic_WithOneParameter_IfViewFindControlReturnsNull_ShouldCallFindControlHierarchicWithTwoParameters()
		{
			Mock<IControlView> viewMock = new Mock<IControlView>();
			viewMock.Setup(view => view.FindControl(It.IsAny<string>())).Returns((Control) null);

			Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] {viewMock.Object}) {CallBase = true};
			presenterMock.Verify(presenter => presenter.FindControlHierarchic(It.IsAny<Control>(), It.IsAny<string>()), Times.Never());
			presenterMock.Object.FindControlHierarchic("Test");
			presenterMock.Verify(presenter => presenter.FindControlHierarchic(It.IsAny<Control>(), It.IsAny<string>()), Times.Once());
		}

		[TestMethod]
		public void FindControlHierarchic_WithOneParameter_Test()
		{
			const string controlId = "Test";
			Control foundControl = Mock.Of<Control>();

			ControlPresenter<IControlView> presenter = CreateFindControlHierarchicPresenter(controlId, foundControl);

			Assert.AreEqual(foundControl, presenter.FindControlHierarchic(controlId));
		}

		[TestMethod]
		public void FindControlHierarchic_WithTwoParameters_IfTheNamingContainerParameterIsNull_ShouldReturnNull()
		{
			ControlPresenter<IControlView> presenter = new Mock<ControlPresenter<IControlView>>(new object[] {Mock.Of<IControlView>()}) {CallBase = true}.Object;
			Assert.IsNull(presenter.FindControlHierarchic(null, "Test"));
		}

		[TestMethod]
		public void FindControlHierarchic_WithTwoParameters_Test()
		{
			const string controlId = "Test";
			Control foundControl = Mock.Of<Control>();

			ControlPresenter<IControlView> presenter = CreateFindControlHierarchicPresenter(controlId, foundControl);

			Assert.AreEqual(foundControl, presenter.FindControlHierarchic(presenter.View.NamingContainer, controlId));
		}

		#endregion

		//[TestMethod]
		//public void OnViewDataBindingChildren_IfTheCancelEventArgsParameterHasCancelSetToFalse_ShouldCallEnsureChildControlsWithTheForceParameterSetToTrue()
		//{
		//    bool ensureChildControlsCalled = false;
		//    bool forceEnsureChildControls = false;
		//    Mock<IControlView> viewMock = new Mock<IControlView>();
		//    viewMock.Setup(view => view.EnsureChildControls(It.IsAny<bool>())).Callback<bool>(delegate(bool force)
		//    {
		//        ensureChildControlsCalled = true;
		//        forceEnsureChildControls = force;
		//    });
		//    Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] { viewMock.Object }) { CallBase = true };
		//    presenterMock.Object.OnViewDataBindingChildren(DateTime.Now.Second % 2 == 0 ? new object() : null, new CancelEventArgs(false));
		//    Assert.IsTrue(ensureChildControlsCalled);
		//    Assert.IsTrue(forceEnsureChildControls);
		//}
		//[TestMethod]
		//public void OnViewDataBindingChildren_IfTheCancelEventArgsParameterHasCancelSetToTrue_ShouldNotCallEnsureChildControls()
		//{
		//    bool ensureChildControlsCalled = false;
		//    Mock<IControlView> viewMock = new Mock<IControlView>();
		//    viewMock.Setup(view => view.EnsureChildControls(It.IsAny<bool>())).Callback(() => ensureChildControlsCalled = true);
		//    Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] { viewMock.Object }) { CallBase = true };
		//    presenterMock.Object.OnViewDataBindingChildren(DateTime.Now.Second % 2 == 0 ? new object() : null, new CancelEventArgs(true));
		//    Assert.IsFalse(ensureChildControlsCalled);
		//}
		//[TestMethod]
		//[ExpectedException(typeof(ArgumentNullException))]
		//public void OnViewDataBindingChildren_IfTheCancelEventArgsParameterIsNull_ShouldThrowAnArgumentNullException()
		//{
		//    Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] { Mock.Of<IControlView>() }) { CallBase = true };
		//    presenterMock.Object.OnViewDataBindingChildren(DateTime.Now.Second % 2 == 0 ? new object() : null, null);
		//}
		//[TestMethod]
		//[ExpectedException(typeof(ArgumentNullException))]
		//public void OnViewEnsuringChildControls_IfTheCancelEventArgsParameterIsNull_ShouldThrowAnArgumentNullException()
		//{
		//    Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] { Mock.Of<IControlView>() }) { CallBase = true };
		//    presenterMock.Object.OnViewDataBindingChildren(DateTime.Now.Second % 2 == 0 ? new object() : null, null);
		//}
		//[TestMethod]
		//public void OnViewEnsuringChildControls_IfTheEnsureChildControlsEnabledPropertyIsSetToFalse_ShouldSetTheCancelEventArgsCancelParameterToTrue()
		//{
		//    Mock<IControlView> viewMock = new Mock<IControlView>();
		//    viewMock.Setup(view => view.EnsureChildControlsEnabled).Returns(false);
		//    CancelEventArgs cancelEventArgs = new CancelEventArgs();
		//    Assert.IsFalse(cancelEventArgs.Cancel);
		//    Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] { viewMock.Object }) { CallBase = true };
		//    presenterMock.Object.OnViewEnsuringChildControls(DateTime.Now.Second % 2 == 0 ? new object() : null, cancelEventArgs);
		//    Assert.IsTrue(cancelEventArgs.Cancel);
		//}
		//[TestMethod]
		//public void OnViewEnsuringChildControls_IfTheEnsureChildControlsEnabledPropertyIsSetToTrue_ShouldSetTheCancelEventArgsCancelParameterToFalse()
		//{
		//    Mock<IControlView> viewMock = new Mock<IControlView>();
		//    viewMock.Setup(view => view.EnsureChildControlsEnabled).Returns(true);
		//    CancelEventArgs cancelEventArgs = new CancelEventArgs();
		//    Assert.IsFalse(cancelEventArgs.Cancel);
		//    Mock<ControlPresenter<IControlView>> presenterMock = new Mock<ControlPresenter<IControlView>>(new object[] { viewMock.Object }) { CallBase = true };
		//    presenterMock.Object.OnViewEnsuringChildControls(DateTime.Now.Second % 2 == 0 ? new object() : null, cancelEventArgs);
		//    Assert.IsFalse(cancelEventArgs.Cancel);
		//}
	}

	//internal class ControlPresenterTestControlView : ControlView { }
	//internal class ControlPresenterTestControlPresenter<TView> : ControlPresenter<TView>
	//    where TView : class, IControlView
	//{
	//    #region Constructors
	//    public ControlPresenterTestControlPresenter(TView view) : base(view) { }
	//    #endregion
	//}
}