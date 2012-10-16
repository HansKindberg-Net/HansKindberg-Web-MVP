using System;

namespace HansKindberg.Web.Mvp.Extensions
{
	public static class StringExtension
	{
		#region Methods

		public static string TrimFromEnd(this string value, string trimValue)
		{
			if(string.IsNullOrEmpty(value) || string.IsNullOrEmpty(trimValue))
				return value;

			int length = value.LastIndexOf(trimValue, StringComparison.OrdinalIgnoreCase);
			return length > 0 ? value.Substring(0, length) : value;
		}

		#endregion
	}
}