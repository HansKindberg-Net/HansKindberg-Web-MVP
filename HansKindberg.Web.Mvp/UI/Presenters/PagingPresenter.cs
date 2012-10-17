using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using HansKindberg.Web.Mvp.UI.Views;

namespace HansKindberg.Web.Mvp.UI.Presenters
{
	public class PagingPresenter : TemplatedControlPresenter<IPagingView>
	{
		#region Fields

		private object _pageableView;
		private const string _pageableViewExceptionMessage = " The id \"{0}\" is set to be used as PageableViewId for the \"{1}\"{2}.";
		private const string _pageableViewInvalidCastExceptionMessage = "The pageable control with id \"{0}\" must be of type \"{1}\".";
		private const string _pageableViewNotFoundExceptionMessage = "The pageable control with id \"{0}\" could not be found.";

		#endregion

		#region Constructors

		public PagingPresenter(IPagingView view) : base(view) {}

		#endregion

		#region Properties

		protected internal virtual bool CreateChildControls
		{
			get
			{
				if(this.PageableView == null)
					return false;

				if(!this.PageableView.PagingEnabled)
					return false;

				if(this.PageableView.MaximumDisplayedPagingItems.HasValue && this.PageableView.MaximumDisplayedPagingItems.Value < 1)
					return false;

				if(this.PageableView.ItemsPerPagingItem < 1)
					return false;

				if(this.PageableView.PagingItems < 2)
					return false;

				return true;
			}
		}

		protected internal virtual IPageableView PageableView
		{
			get
			{
				if(this._pageableView == null)
				{
					this._pageableView = this.View.PageableView;

					if(this._pageableView == null && !string.IsNullOrEmpty(this.View.PageableViewId))
					{
						Control pageableView = this.FindControlHierarchic(this.View.PageableViewId);

						if(pageableView == null && !this.View.DesignMode)
							throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, _pageableViewNotFoundExceptionMessage, this.View.PageableViewId) + this.PageableViewExceptionMessage());

						this.ValidatePageableView(pageableView);

						this._pageableView = pageableView as IPageableView;
					}

					if(this._pageableView == null)
						this._pageableView = new object();
				}

				return this._pageableView as IPageableView;
			}
		}

		#endregion

		#region Methods

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		protected internal virtual void CreatePagingItems(IEnumerable<int> positions)
		{
			if(positions == null)
				throw new ArgumentNullException("positions");

			foreach(int i in positions)
			{
				if(this.View.SelectedItemTemplate != null && this.PageableView.PagingPosition == i)
					this.AddTemplate(new PagingItemContainer(i), this.View.SelectedItemTemplate);
				else if(this.View.ItemTemplate != null)
					this.AddTemplate(new PagingItemContainer(i), this.View.ItemTemplate);

				if(this.View.SeparatorTemplate != null && i < this.PageableView.PagingItems - 1)
					this.AddTemplate(new PagingItemContainer(0), this.View.SeparatorTemplate);
			}
		}

		protected internal virtual string PageableViewExceptionMessage()
		{
			string messageEnd = !string.IsNullOrEmpty(this.View.ID) ? string.Format(CultureInfo.InvariantCulture, " with id \"{0}\"", this.View.ID) : string.Empty;

			return string.Format(CultureInfo.InvariantCulture, _pageableViewExceptionMessage, this.View.PageableViewId, this.GetType(), messageEnd);
		}

		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
		[SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#")]
		protected internal virtual IEnumerable<int> ResolvePositions(IPageableView pageableView, out int? previousItemGroupPosition, out int? nextItemGroupPosition)
		{
			if(pageableView == null)
				throw new ArgumentNullException("pageableView");

			List<int> positions = new List<int>();
			previousItemGroupPosition = null;
			nextItemGroupPosition = null;

			if(pageableView.PagingItems < 1 || (pageableView.MaximumDisplayedPagingItems.HasValue && pageableView.MaximumDisplayedPagingItems.Value < 1))
				return positions;

			List<int> allPositions = new List<int>();
			for(int i = 0; i < pageableView.PagingItems; i++)
			{
				allPositions.Add(i);
			}

			if(pageableView.MaximumDisplayedPagingItems.HasValue)
			{
				int maximumDisplayedPagingItems = pageableView.MaximumDisplayedPagingItems.Value;

				int quotient = pageableView.PagingPosition/maximumDisplayedPagingItems;

				positions = new List<int>(allPositions.Skip(quotient*maximumDisplayedPagingItems).Take(maximumDisplayedPagingItems));

				if(quotient > 0)
					previousItemGroupPosition = allPositions[quotient*maximumDisplayedPagingItems - 1];

				int firstIndexInNextItemGroup = quotient*maximumDisplayedPagingItems + maximumDisplayedPagingItems;

				if(firstIndexInNextItemGroup < pageableView.PagingItems)
					nextItemGroupPosition = firstIndexInNextItemGroup;
			}
			else
			{
				positions = allPositions;
			}

			return positions.ToArray();
		}

		protected internal virtual void ValidatePageableView(object pageableView)
		{
			if(pageableView != null && !(pageableView is IPageableView) && !this.View.DesignMode)
				throw new InvalidCastException(string.Format(CultureInfo.InvariantCulture, _pageableViewInvalidCastExceptionMessage, this.View.PageableViewId, typeof(IPageableView)) + this.PageableViewExceptionMessage());
		}

		#endregion

		#region Eventhandlers

		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		protected internal override void OnViewCreatingChildControls(object sender, EventArgs e)
		{
			base.OnViewCreatingChildControls(sender, e);

			if(!this.CreateChildControls)
				return;

			int? previousItemGroupPosition;
			int? nextItemGroupPosition;

			IEnumerable<int> positions = this.ResolvePositions(this.PageableView, out previousItemGroupPosition, out nextItemGroupPosition);

			if(this.View.HeaderTemplate != null)
				this.AddTemplate(new PagingSummaryContainer(this.PageableView.PagingItems), this.View.HeaderTemplate);

			if(this.PageableView.PagingPosition > 0)
			{
				if(this.View.FirstItemTemplate != null)
					this.AddTemplate(new PagingItemContainer(0), this.View.FirstItemTemplate);
			}

			if(this.View.PreviousItemGroupTemplate != null && previousItemGroupPosition.HasValue)
				this.AddTemplate(new PagingItemContainer(previousItemGroupPosition.Value), this.View.PreviousItemGroupTemplate);

			if(this.PageableView.PagingPosition > 0)
			{
				if(this.View.PreviousItemTemplate != null && this.PageableView.PagingPosition > 0)
					this.AddTemplate(new PagingItemContainer(this.PageableView.PagingPosition - 1), this.View.PreviousItemTemplate);
			}

			this.CreatePagingItems(positions);

			if(this.PageableView.PagingPosition < this.PageableView.PagingItems - 1)
			{
				if(this.View.NextItemTemplate != null)
					this.AddTemplate(new PagingItemContainer(this.PageableView.PagingPosition + 1), this.View.NextItemTemplate);
			}

			if(this.View.NextItemGroupTemplate != null && nextItemGroupPosition.HasValue)
				this.AddTemplate(new PagingItemContainer(nextItemGroupPosition.Value), this.View.NextItemGroupTemplate);

			if(this.PageableView.PagingPosition < this.PageableView.PagingItems - 1)
			{
				if(this.View.LastItemTemplate != null)
					this.AddTemplate(new PagingItemContainer(this.PageableView.PagingItems - 1), this.View.LastItemTemplate);
			}

			if(this.View.FooterTemplate != null)
				this.AddTemplate(new PagingSummaryContainer(this.PageableView.PagingItems), this.View.FooterTemplate);
		}

		protected internal override void OnViewInvalidatingInternalState(object sender, EventArgs e) {}

		#endregion
	}
}