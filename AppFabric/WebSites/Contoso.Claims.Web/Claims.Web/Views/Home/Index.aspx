﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Contoso Insurance Claims Processing Application
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <%= Html.ActionLink("The Claims Queue", "Index", "Claims") %>
<!--    Response.Redirect("/Contoso/Claims.Web/Claims"); %> -->
</asp:Content>
