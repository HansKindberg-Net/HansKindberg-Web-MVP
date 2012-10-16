<%@ Page Title="Paging sample" Language="C#" MasterPageFile="~/Views/MasterViews/DefaultMasterView.master" AutoEventWireup="false" CodeBehind="PagingSampleView.aspx.cs" Inherits="HansKindberg.Web.Mvp.WebApplication.Views.PagingSampleView" %><asp:Content ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
  	<p>This sample shows a repater/templated control inheriting from HansKindberg.Web.Mvp.UI.Views.TemplatedControlView and a HansKindberg.Web.Mvp.UI.Views.PagingView as pager.</p>
  	<h2>TemplatedControlView with PagingView</h2>
	<WebControls:RepeaterView
		AutoDataBind="true"
		DataSource="<%# this.DataSource %>"
		id="repeaterView"
		ItemsPerPagingItem="<%# this.ItemsPerPagingItem %>"
		MaximumDisplayedPagingItems="10"
		PagingEnabled="true"
		runat="server"
	 >
		<HeaderTemplate>
			<HansKindbergMvp:PagingView
				id="headerPager"
				PageableViewId="repeaterView"
				runat="server"
			>
				<HeaderTemplate>
					<div class="pagination pagination-centered">
						<ul>
				</HeaderTemplate>
				<FirstItemTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="First">|&laquo;</a></li>
				</FirstItemTemplate>
				<PreviousItemGroupTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="Previous 10">...</a></li>
				</PreviousItemGroupTemplate>
				<PreviousItemTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="Previous">&laquo;</a></li>
				</PreviousItemTemplate>
				<ItemTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>"><%# Container.Position + 1 %></a></li>
				</ItemTemplate>
				<SelectedItemTemplate>
					<li class="active"><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>"><%# Container.Position + 1 %></a></li>
				</SelectedItemTemplate>
				<SeparatorTemplate><!-- SeparatorTemplate --></SeparatorTemplate>
				<NextItemTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="Next">&raquo;</a></li>
				</NextItemTemplate>
				<NextItemGroupTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="Next 10">...</a></li>
				</NextItemGroupTemplate>
				<LastItemTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="Last">&raquo;|</a></li>
				</LastItemTemplate>
				<FooterTemplate>
						</ul>
					</div>
				</FooterTemplate>
			</HansKindbergMvp:PagingView>
			<ul class="nav nav-tabs nav-stacked">
		</HeaderTemplate>
		<ItemTemplate>
			<li><a href="#"><strong>Item number <%# Container.DataItem %></strong> - and som more text here</a></li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
			<HansKindbergMvp:PagingView
				id="footerPager"
				PageableViewId="repeaterView"
				runat="server"
			>
				<HeaderTemplate>
					<div class="pagination pagination-centered">
						<ul>
				</HeaderTemplate>
				<FirstItemTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="First">|&laquo;</a></li>
				</FirstItemTemplate>
				<PreviousItemGroupTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="Previous 10">...</a></li>
				</PreviousItemGroupTemplate>
				<PreviousItemTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="Previous">&laquo;</a></li>
				</PreviousItemTemplate>
				<ItemTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>"><%# Container.Position + 1 %></a></li>
				</ItemTemplate>
				<SelectedItemTemplate>
					<li class="active"><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>"><%# Container.Position + 1 %></a></li>
				</SelectedItemTemplate>
				<SeparatorTemplate><!-- SeparatorTemplate --></SeparatorTemplate>
				<NextItemTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="Next">&raquo;</a></li>
				</NextItemTemplate>
				<NextItemGroupTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="Next 10">...</a></li>
				</NextItemGroupTemplate>
				<LastItemTemplate>
					<li><a href="<%# string.Format(this.Model.PagingPositionHref, Container.Position) %>" title="Last">&raquo;|</a></li>
				</LastItemTemplate>
				<FooterTemplate>
						</ul>
					</div>
				</FooterTemplate>
			</HansKindbergMvp:PagingView>
		</FooterTemplate>
	</WebControls:RepeaterView>
</asp:Content>