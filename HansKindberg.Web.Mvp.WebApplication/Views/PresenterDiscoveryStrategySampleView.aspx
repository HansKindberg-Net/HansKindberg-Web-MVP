<%@ Page Title="Presenter discovery strategy sample" Language="C#" MasterPageFile="~/Views/MasterViews/DefaultMasterView.master" AutoEventWireup="false" CodeBehind="PresenterDiscoveryStrategySampleView.aspx.cs" Inherits="HansKindberg.Web.Mvp.WebApplication.Views.PresenterDiscoveryStrategySampleView" %>
<asp:Content ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
	<p>This sample shows a MVP control where its presenter is found by the customized ConventionBasedPresenterDiscoveryStrategy, HansKindberg.Web.Mvp.Binder.ConventionBasedPresenterDiscoveryStrategy that inherits from WebFormsMvp.Binder.ConventionBasedPresenterDiscoveryStrategy.</p>
	<p>In HansKindberg.Web.Mvp.Binder.ConventionBasedPresenterDiscoveryStrategy the following members are overridden:</p>
	<ul>
		<li><strong>Constructor</strong>: ConventionBasedPresenterDiscoveryStrategy(IEnumerable&lt;string&gt; presenterNamespaces, IBuildManager buildManager)</li>
		<li><strong>Property</strong>: IEnumerable&lt;string&gt; CandidatePresenterTypeFullNameFormats</li>
	</ul>
	<p>The customized ConventionBasedPresenterDiscoveryStrategy is registered in HansKindberg.Web.Mvp.WebApplication.Bootstrapper.Bootstrap().</p>
	<h2>PresenterDiscoveryStrategySampleControlView</h2>
	<p><strong>There should be text here, otherwhise something is wrong</strong>: <WebControls:PresenterDiscoveryStrategySampleControlView runat="server" /></p>
</asp:Content>