using System;

namespace HansKindberg.Web.Mvp
{
	public class TypeWrapper : IType
	{
		#region Fields

		private readonly Type _type;

		#endregion

		#region Constructors

		public TypeWrapper(Type type)
		{
			if(type == null)
				throw new ArgumentNullException("type");

			this._type = type;
		}

		#endregion

		#region Properties

		public virtual string Namespace
		{
			get { return this._type.Namespace; }
		}

		#endregion
	}
}