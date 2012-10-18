using System;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using HansKindberg.Web.Mvp.UI.Presenters;
using HansKindberg.Web.Mvp.UI.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Web.Mvp.Tests.UI.Presenters
{
	[TestClass]
	public class AutoDataBindablePresenterTest
	{
		#region Fields

		private static readonly object _eventDataBinding = typeof(Control).GetField("EventDataBinding", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
		private static readonly object _eventPreRender = typeof(Control).GetField("EventPreRender", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

		#endregion

		#region Methods

		private static void ConstructorShouldAddAnEventHandler(string eventName, Type eventFieldType)
		{
			using(AutoDataBindablePresenterTestAutoDataBindableView view = new AutoDataBindablePresenterTestAutoDataBindableView())
			{
				FieldInfo eventField = eventFieldType.GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic);
				Assert.IsNotNull(eventField);
				Assert.IsNull(eventField.GetValue(view));

				AutoDataBindablePresenterTestAutoDataBindablePresenter<AutoDataBindablePresenterTestAutoDataBindableView> presenter = new AutoDataBindablePresenterTestAutoDataBindablePresenter<AutoDataBindablePresenterTestAutoDataBindableView>(view);
				Assert.IsNotNull(presenter);

				Delegate eventDelegate = (Delegate) eventField.GetValue(view);
				Assert.AreEqual(1, eventDelegate.GetInvocationList().Length);
				Assert.AreEqual("OnView" + eventName, eventDelegate.GetInvocationList()[0].Method.Name);
			}
		}

		private static void ConstructorShouldAddAnEventHandler(object eventHandlerObject)
		{
			using(AutoDataBindablePresenterTestAutoDataBindableView view = new AutoDataBindablePresenterTestAutoDataBindableView())
			{
				Assert.IsNull(GetEventHandler(view, eventHandlerObject));

				AutoDataBindablePresenterTestAutoDataBindablePresenter<AutoDataBindablePresenterTestAutoDataBindableView> presenter = new AutoDataBindablePresenterTestAutoDataBindablePresenter<AutoDataBindablePresenterTestAutoDataBindableView>(view);
				Assert.IsNotNull(presenter);

				EventHandler eventHandler = GetEventHandler(view, eventHandlerObject);

				Assert.IsNotNull(eventHandler);
				Assert.AreEqual(1, eventHandler.GetInvocationList().Length);
			}
		}

		[TestMethod]
		public void Constructor_ShouldAddADataBindingChildrenEventHandler()
		{
			ConstructorShouldAddAnEventHandler("DataBindingChildren", typeof(ControlView));
		}

		[TestMethod]
		public void Constructor_ShouldAddADataBindingEventHandler()
		{
			ConstructorShouldAddAnEventHandler(_eventDataBinding);
		}

		[TestMethod]
		public void Constructor_ShouldAddAPreRenderEventHandler()
		{
			ConstructorShouldAddAnEventHandler(_eventPreRender);
		}

		[TestMethod]
		public void Constructor_ShouldAddAnEnsuringChildControlsEventHandler()
		{
			ConstructorShouldAddAnEventHandler("EnsuringChildControls", typeof(ControlView));
		}

		private static EventHandler GetEventHandler(Control control, object eventHandlerObject)
		{
			if(control == null)
				throw new ArgumentNullException("control");

			if(eventHandlerObject == null)
				throw new ArgumentNullException("eventHandlerObject");

			// Ensure occasional fields
			typeof(Control).GetMethod("EnsureOccasionalFields", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(control, null);

			EventHandlerList events = (EventHandlerList) typeof(Control).GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(control, null);

			return events[eventHandlerObject] as EventHandler;
		}

		[TestMethod]
		public void OnViewDataBindingChildren_IfTheCancelEventArgsCancelPropertyIsFalse_ShouldSetTheChildrenAreDataBoundPropertyToTrue()
		{
			AutoDataBindablePresenter<IAutoDataBindableView> presenter = new Mock<AutoDataBindablePresenter<IAutoDataBindableView>>(new object[] {Mock.Of<IAutoDataBindableView>()}) {CallBase = true}.Object;
			Assert.IsFalse(presenter.ChildrenAreDataBound);
			presenter.OnViewDataBindingChildren(new object(), new CancelEventArgs {Cancel = false});
			Assert.IsTrue(presenter.ChildrenAreDataBound);
		}

		[TestMethod]
		public void OnViewDataBindingChildren_IfTheCancelEventArgsCancelPropertyIsTrue_ShouldNotSetTheChildrenAreDataBoundPropertyToTrue()
		{
			AutoDataBindablePresenter<IAutoDataBindableView> presenter = new Mock<AutoDataBindablePresenter<IAutoDataBindableView>>(new object[] {Mock.Of<IAutoDataBindableView>()}) {CallBase = true}.Object;
			Assert.IsFalse(presenter.ChildrenAreDataBound);
			presenter.OnViewDataBindingChildren(new object(), new CancelEventArgs {Cancel = true});
			Assert.IsFalse(presenter.ChildrenAreDataBound);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void OnViewDataBindingChildren_IfTheCancelEventArgsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new Mock<AutoDataBindablePresenter<IAutoDataBindableView>>(new object[] {Mock.Of<IAutoDataBindableView>()}) {CallBase = true}.Object.OnViewDataBindingChildren(new object(), null);
		}

		[TestMethod]
		public void OnViewDataBinding_ShouldSetTheOnDataBindingRaisedPropertyToTrue()
		{
			AutoDataBindablePresenter<IAutoDataBindableView> presenter = new Mock<AutoDataBindablePresenter<IAutoDataBindableView>>(new object[] {Mock.Of<IAutoDataBindableView>()}) {CallBase = true}.Object;
			Assert.IsFalse(presenter.OnDataBindingRaised);
			presenter.OnViewDataBinding(new object(), EventArgs.Empty);
			Assert.IsTrue(presenter.OnDataBindingRaised);
		}

		[TestMethod]
		public void OnViewEnsuringChildControls_IfTheCancelEventArgsCancelPropertyIsFalse_ShouldSetTheChildControlsAreEnsuredPropertyToTrue()
		{
			AutoDataBindablePresenter<IAutoDataBindableView> presenter = new Mock<AutoDataBindablePresenter<IAutoDataBindableView>>(new object[] {Mock.Of<IAutoDataBindableView>()}) {CallBase = true}.Object;
			Assert.IsFalse(presenter.ChildControlsAreEnsured);
			presenter.OnViewEnsuringChildControls(new object(), new CancelEventArgs {Cancel = false});
			Assert.IsTrue(presenter.ChildControlsAreEnsured);
		}

		[TestMethod]
		public void OnViewEnsuringChildControls_IfTheCancelEventArgsCancelPropertyIsTrue_ShouldNotSetTheChildControlsAreEnsuredPropertyToTrue()
		{
			AutoDataBindablePresenter<IAutoDataBindableView> presenter = new Mock<AutoDataBindablePresenter<IAutoDataBindableView>>(new object[] {Mock.Of<IAutoDataBindableView>()}) {CallBase = true}.Object;
			Assert.IsFalse(presenter.ChildControlsAreEnsured);
			presenter.OnViewEnsuringChildControls(new object(), new CancelEventArgs {Cancel = true});
			Assert.IsFalse(presenter.ChildControlsAreEnsured);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void OnViewEnsuringChildControls_IfTheCancelEventArgsParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			new Mock<AutoDataBindablePresenter<IAutoDataBindableView>>(new object[] {Mock.Of<IAutoDataBindableView>()}) {CallBase = true}.Object.OnViewEnsuringChildControls(new object(), null);
		}

		#endregion

		//[TestMethod]
		//public void OnViewPreRender_IfTheAutoDataBindPropertyOfTheViewIsSetToFalse_ShouldNotR_____________()
		//{
		//    Mock<IAutoDataBindableView> viewMock = new Mock<IAutoDataBindableView>();
		//    viewMock.Setup(view => view.AutoDataBind).Returns(false);
		//    //if (!this.View.AutoDataBind)
		//    //    return;
		//    //if (!this.OnDataBindingRaised || !this.ChildrenAreDataBound)
		//    //    this.View.DataBind(!this.OnDataBindingRaised, !this.ChildControlsAreEnsured, !this.ChildrenAreDataBound);
		//}
	}

	internal class AutoDataBindablePresenterTestAutoDataBindableView : AutoDataBindableControlView {}

	internal class AutoDataBindablePresenterTestAutoDataBindablePresenter<TView> : AutoDataBindablePresenter<TView>
		where TView : class, IAutoDataBindableView
	{
		#region Constructors

		public AutoDataBindablePresenterTestAutoDataBindablePresenter(TView view) : base(view) {}

		#endregion
	}
}