using HansKindberg.Web.Mvp.WebApplication.Core;

namespace HansKindberg.Web.Mvp.WebApplication.Models.MasterModels
{
	public class DefaultMasterModel
	{
		#region Properties

		public virtual string Heading { get; set; }
		public virtual bool HomeIsActive { get; set; }
		public virtual bool IncludeSubNavigation { get; set; }
		public virtual IIsActiveFilePath IsActiveFilePath { get; set; }
		public virtual bool SamplesIsActive { get; set; }
		public virtual string Title { get; set; }

		#endregion
	}
}