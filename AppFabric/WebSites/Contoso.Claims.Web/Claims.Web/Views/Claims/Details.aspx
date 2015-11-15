<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BusinessEntities.Claim>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Claim '<%= Html.Encode(Model.ClaimId) %>' Details
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Claim '<%= Html.Encode(Model.ClaimId) %>' Details</h2>

<table style="width:920px; border-style:none; border-width:0px">
    <tr>
        <td style="width:400px; border-style:none; border-width:0px">
            <div id="claimDiv">
                <p>
                    <strong>Created On:</strong>
                    <%= ((DateTime)Model.DateCreated).ToShortDateString() %>

                    <strong>@</strong>
                    <%= ((DateTime)Model.DateCreated).ToShortTimeString() %>
                </p>
                <p>
                    <strong>Where:</strong>
                    <%= Html.Encode(Model.Accidents.Address) %>,  
                    <%= Html.Encode(Model.Accidents.City) %>,  
                    <%= Html.Encode(Model.Accidents.State.ToUpper()) %>, 
                    <%= Html.Encode(Model.Accidents.Zip) %>
                    <%= Html.Hidden(String.Format("{0:F}", Model.Accidents.Latitude))%>
                    <%= Html.Hidden(String.Format("{0:F}", Model.Accidents.Longitude))%>
                </p>
                <p>
                    <strong>Claimer:</strong>
                    <%= Html.Encode(Model.Accidents.LName)%>,  
                    <%= Html.Encode(Model.Accidents.FName) %>
                </p>
                <p>
                    <strong>Contact phone:</strong>
                    <%= Html.Encode(Model.Accidents.ContactPhone) %>
                </p> 
                <p>
                    <strong>Description:</strong>
                    <%= Html.Encode(Model.Description) %>
                </p>
                <p>
                    <strong>Status:</strong>
                    <%= Html.Encode(Model.Status) %>
                </p>
                <p>
                    <strong>Rental Car:</strong>
                    <%= Html.Encode(Model.RentalCar) %>
                </p>
                <p>
                    <strong>IsValid:</strong>
                    <%= Html.Encode(Model.IsValid) %>
                </p>


                <%=Html.ActionLink("Edit Claim", "Edit", new { id=Model.ClaimId }) %> |
                <%=Html.ActionLink("Back to List", "Index") %>

            </div>
        </td>
        <td style="width:520px; border-style:none; border-width:0px">
            <div id="mapDiv">
                <% Html.RenderPartial("BingMap", Model.Accidents); %>
            </div>
        </td>
    </tr>
</table>


</asp:Content>

