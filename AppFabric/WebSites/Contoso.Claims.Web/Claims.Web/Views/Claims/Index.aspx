<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<BusinessEntities.Claim>>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Contoso Insurance Claims Processing Application
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Pending Claims</h2>

    <ul>
        <% foreach (var claim in Model) {
               if (claim.Status == "Pending")
               {
        %>
        <li>
            Submitted On: 
            <%= Html.ActionLink(String.Format("{0} @ {1}",
                            ((DateTime)claim.DateCreated).ToShortDateString(),
                            ((DateTime)claim.DateCreated).ToShortTimeString()), "Details", new { id = claim.ClaimId }) %>
            by 
            <strong><%= Html.Encode(claim.Accidents.FName) %> <%= Html.Encode(claim.Accidents.LName) %></strong>, Description: 
            "<strong><%= Html.Encode(claim.Description) %></strong>"
        </li>
        <%           
               } 
         } %>
    </ul>
    <br />
    <br />
    <h2>Processed Claims</h2>

    <ul>
        <% foreach (var claim in Model) {
               if (claim.Status == "Complete")
               {
        %>
        <li>
            Submitted On: 
            <%= Html.ActionLink(String.Format("{0} @ {1}",
                            ((DateTime)claim.DateCreated).ToShortDateString(),
                            ((DateTime)claim.DateCreated).ToShortTimeString()), "Details", new { id = claim.ClaimId }) %>
            by 
            <strong><%= Html.Encode(claim.Accidents.FName) %> <%= Html.Encode(claim.Accidents.LName) %></strong>, Description: 
            "<strong><%= Html.Encode(claim.Description) %></strong>"
        </li>
        <%           
               } 
         } %>
    </ul>


</asp:Content>

