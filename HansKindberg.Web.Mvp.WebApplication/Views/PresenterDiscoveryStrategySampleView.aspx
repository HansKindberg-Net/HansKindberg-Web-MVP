<%@ Page Title="Presenter discovery strategy sample" Language="C#" MasterPageFile="~/Views/MasterViews/DefaultMasterView.master" AutoEventWireup="false" CodeBehind="PresenterDiscoveryStrategySampleView.aspx.cs" Inherits="HansKindberg.Web.Mvp.WebApplication.Views.PresenterDiscoveryStrategySampleView" %>
<%@ Register TagPrefix="WebControls" Namespace="HansKindberg.Web.Mvp.WebApplication.WebControls.Views" Assembly="HansKindberg.Web.Mvp.WebApplication"
%><asp:Content ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
	<p>This sample shows a MVP control where its presenter is found by the customized ConventionBasedPresenterDiscoveryStrategy, HansKindberg.Web.Mvp.Binder.ConventionBasedPresenterDiscoveryStrategy that inherits from WebFormsMvp.Binder.ConventionBasedPresenterDiscoveryStrategy.</p>
	<p>The customized ConventionBasedPresenterDiscoveryStrategy is registered in HansKindberg.Web.Mvp.WebApplication.Bootstrapper.Bootstrap().</p>
	<h2>PresenterDiscoveryStrategySampleControlView</h2>
	<p><strong>There should be text here, otherwhise something is wrong</strong>: <WebControls:PresenterDiscoveryStrategySampleControlView runat="server" /></p>
</asp:Content>