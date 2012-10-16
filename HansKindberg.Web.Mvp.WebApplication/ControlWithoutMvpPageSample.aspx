<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="ControlWithoutMvpPageSample.aspx.cs" Inherits="HansKindberg.Web.Mvp.WebApplication.ControlWithoutMvpPageSample"
%><!DOCTYPE html>
<html>
	<head>
		<title>Control without MVP page sample</title>
	</head>
	<body>
		<h1>Control without MVP page sample</h1>
		<p>This sample use a custom control inheriting from HansKindberg.Web.Mvp.UI.Views.ControlView. The control is put inside the HeaderTemplate of a System.Web.UI.WebControls.Repeater. It works even if no other WebFormsMvp controls are used on the page and even if the page inherits from System.Web.UI.Page. This is because of a customization for WebFormsMvp.Web.PageViewHost, "HansKindberg.Web.Mvp.PageViewHostWrapper", see HansKindberg.Web.Mvp.UI.Views.ControlView.OnInit(EventArgs e).</p>
		<h2>SimpleMvpControlView</h2>
		<asp:Repeater id="repeater" DataSource="<%# new int[] {1, 2, 3, 4, 5} %>" runat="server">
			<HeaderTemplate>
				<WebControls:SimpleMvpControlView runat="server" />
				<ul>
			</HeaderTemplate>
			<ItemTemplate>
				<li><%# Container.DataItem %></li>
			</ItemTemplate>
			<FooterTemplate>
				</ul>
			</FooterTemplate>
		</asp:Repeater>
		<button onclick="history.back();">Back</button>
	</body>
</html>