using System;

namespace HansKindberg.Web.Mvp
{
	internal static class ViewHelper
	{
		#region Methods

		internal static void ThrowExceptionIfModelIsNull(object model)
		{
			if(model == null)
				throw new InvalidOperationException("The Model property is currently null, however it should have been automatically initialized by the presenter. This most likely indicates that no presenter was bound to the control. For more information, check the ASP.NET tracing output at ~/Trace.axd.");
		}

		#endregion
	}
}