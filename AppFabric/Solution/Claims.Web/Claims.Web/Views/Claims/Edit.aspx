<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Claims.Web.Models.ClaimFormViewModel>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Claim: <%= Html.Encode(Model.Claim.ClaimId) %>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Claim</h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

    <fieldset>
        <div id="claimDiv">
            <p>
                <label for="ClaimId">ClaimId: <%= Model.Claim.ClaimId %></label>
            </p>
            <p>
                <label for="Policy Holder's Name">Policy Holder's Name:</label>
                <%= Html.TextBox("FName", Model.Claim.Accidents.FName) %> 
                <%= Html.ValidationMessage("FName", "*") %>
                <%= Html.TextBox("LName", Model.Claim.Accidents.LName) %>
                <%= Html.ValidationMessage("LName", "*") %>
            </p>
            <p>
                <label for="Address">Address of Accident:</label>
                <%= Html.TextBox("Address", Model.Claim.Accidents.Address)%>, 
                <%= Html.ValidationMessage("Address", "*") %>
                <%= Html.TextBox("City", Model.Claim.Accidents.City)%>, 
                <%= Html.ValidationMessage("City", "*") %>
                <%= Html.DropDownList("State", Model.States)%>, 
                <%= Html.ValidationMessage("State", "*") %>
                <%= Html.TextBox("Zip", Model.Claim.Accidents.Zip)%> 
                <%= Html.ValidationMessage("Zip", "*") %>
                <%= Html.Hidden("Latitude", Model.Claim.Accidents.Latitude)%>
                <%= Html.Hidden("Longitude", Model.Claim.Accidents.Longitude)%>
            </p>
            <p>
                <label for="Contact Phone #">Contact Phone #:</label>
                <%= Html.TextBox("ContactPhone", Model.Claim.Accidents.ContactPhone)%> 
                <%= Html.ValidationMessage("ContactPhone", "*") %>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%= Html.TextArea("Description", Model.Claim.Description)%>
               <%= Html.ValidationMessage("Description", "*") %>
            </p>
            <p>
                <label for="Status">Status: <%= Model.Claim.Status %></label>
            </p>
            <p>
                <label for="Status">Rental Car: <%= Model.Claim.RentalCar %></label>
            </p>
            <p>
                <label for="DateCreated">Last Updated: <%= String.Format("{0:g}", Model.Claim.DateCreated)%></label>
            </p>
            <p>
                <input type="submit" value="Process Claim" />
            </p>
        </div>
            
    </fieldset>
    <% } %>

    <div id="ActionLink">
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

