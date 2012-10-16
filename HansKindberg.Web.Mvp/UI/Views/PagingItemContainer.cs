using System;
using System.Web.UI;

namespace HansKindberg.Web.Mvp.UI.Views
{
	public class PagingItemContainer : Control, INamingContainer
	{
		#region Fields

		private int _position;

		#endregion

		#region Constructors

		public PagingItemContainer(int position)
		{
			ThrowExceptionIfPositionIsLessThanZero("position", position);
			this._position = position;
		}

		#endregion

		#region Properties

		public virtual int Position
		{
			get { return this._position; }
			set
			{
				ThrowExceptionIfPositionIsLessThanZero("value", value);
				this._position = value;
			}
		}

		#endregion

		#region Methods

		private static void ThrowExceptionIfPositionIsLessThanZero(string parameterName, int position)
		{
			if(position < 0)
				throw new ArgumentOutOfRangeException(parameterName, "Position can not be less than 0.");
		}

		#endregion
	}
}