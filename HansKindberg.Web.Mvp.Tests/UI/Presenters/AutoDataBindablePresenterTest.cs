using System;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using HansKindberg.Web.Mvp.UI.Presenters;
using HansKindberg.Web.Mvp.UI.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

		#endregion
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