using System;
using System.Reflection;
using System.Web.UI;
using HansKindberg.Web.Mvp.UI.Presenters;
using HansKindberg.Web.Mvp.UI.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansKindberg.Web.Mvp.Tests.UI.Presenters
{
	[TestClass]
	public class TemplatedControlPresenterTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddTemplate_IfTheContainerParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			Mock<TemplatedControlPresenter<ITemplatedControlView>> presenterMock = new Mock<TemplatedControlPresenter<ITemplatedControlView>>(new object[] {Mock.Of<ITemplatedControlView>()}) {CallBase = true};
			presenterMock.Object.AddTemplate(null, Mock.Of<ITemplate>());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddTemplate_IfTheTemplateParameterIsNull_ShouldThrowAnArgumentNullException()
		{
			Mock<TemplatedControlPresenter<ITemplatedControlView>> presenterMock = new Mock<TemplatedControlPresenter<ITemplatedControlView>>(new object[] {Mock.Of<ITemplatedControlView>()}) {CallBase = true};
			presenterMock.Object.AddTemplate(Mock.Of<Control>(), null);
		}

		[TestMethod]
		public void AddTemplate_ShouldAddTheContainerToTheControlsCollectionOfTheTemplatedControl()
		{
			ControlCollection controls = new ControlCollection(Mock.Of<Control>());

			Mock<ITemplatedControlView> viewMock = new Mock<ITemplatedControlView>();
			viewMock.Setup(view => view.Controls).Returns(controls);

			Mock<TemplatedControlPresenter<ITemplatedControlView>> presenterMock = new Mock<TemplatedControlPresenter<ITemplatedControlView>>(new object[] {viewMock.Object}) {CallBase = true};

			Assert.AreEqual(0, viewMock.Object.Controls.Count);

			presenterMock.Object.AddTemplate(Mock.Of<Control>(), Mock.Of<ITemplate>());

			Assert.AreEqual(1, viewMock.Object.Controls.Count);
		}

		[TestMethod]
		public void AddTemplate_ShouldInstantiateTheTemplateInTheContainer()
		{
			ControlCollection controls = new ControlCollection(Mock.Of<Control>());

			Mock<ITemplatedControlView> viewMock = new Mock<ITemplatedControlView>();
			viewMock.Setup(view => view.Controls).Returns(controls);

			Mock<TemplatedControlPresenter<ITemplatedControlView>> presenterMock = new Mock<TemplatedControlPresenter<ITemplatedControlView>>(new object[] {viewMock.Object}) {CallBase = true};

			Control container = Mock.Of<Control>();
			Mock<ITemplate> templateMock = new Mock<ITemplate>();

			templateMock.Verify(template => template.InstantiateIn(container), Times.Never());
			presenterMock.Object.AddTemplate(container, templateMock.Object);
			templateMock.Verify(template => template.InstantiateIn(container), Times.Once());
		}

		private static void ConstructorShouldAddAnEventHandler(string eventName, Type eventFieldType)
		{
			using(TemplatedControlPresenterTestTemplatedControlView view = new TemplatedControlPresenterTestTemplatedControlView())
			{
				FieldInfo eventField = eventFieldType.GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic);
				Assert.IsNotNull(eventField);
				Assert.IsNull(eventField.GetValue(view));

				TemplatedControlPresenterTestTemplatedControlPresenter<TemplatedControlPresenterTestTemplatedControlView> presenter = new TemplatedControlPresenterTestTemplatedControlPresenter<TemplatedControlPresenterTestTemplatedControlView>(view);
				Assert.IsNotNull(presenter);

				Delegate eventDelegate = (Delegate) eventField.GetValue(view);
				Assert.AreEqual(1, eventDelegate.GetInvocationList().Length);
				Assert.AreEqual("OnView" + eventName, eventDelegate.GetInvocationList()[0].Method.Name);
			}
		}

		[TestMethod]
		public void Constructor_ShouldAddACreatingChildControlsEventHandler()
		{
			ConstructorShouldAddAnEventHandler("CreatingChildControls", typeof(ControlView));
		}

		[TestMethod]
		public void Constructor_ShouldAddAnInvalidatingInternalStateEventHandler()
		{
			ConstructorShouldAddAnEventHandler("InvalidatingInternalState", typeof(TemplatedControlView));
		}

		#endregion
	}

	internal class TemplatedControlPresenterTestTemplatedControlView : TemplatedControlView {}

	internal class TemplatedControlPresenterTestTemplatedControlPresenter<TView> : TemplatedControlPresenter<TView>
		where TView : class, ITemplatedControlView
	{
		#region Constructors

		public TemplatedControlPresenterTestTemplatedControlPresenter(TView view) : base(view) {}

		#endregion

		#region Methods

		protected internal override void OnViewCreatingChildControls(object sender, EventArgs e) {}
		protected internal override void OnViewInvalidatingInternalState(object sender, EventArgs e) {}

		#endregion
	}
}