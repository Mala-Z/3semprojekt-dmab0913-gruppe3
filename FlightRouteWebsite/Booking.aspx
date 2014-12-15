<%@ Page Title="Bestilling" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Booking.aspx.cs" Inherits="Booking" %>
<%@ Import Namespace="FlightServiceReference" %>

<%@ register src="~/UserControls/AddPassenger.ascx" tagprefix="uc1" tagname="AddPassenger" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <section id="portfolio" class="single-page scrollblock">
      <div class="container">
        <h1 id="folio-headline">Bestilling</h1>
        <div class="row">
        <p>Du har valgt følgende flyrute fra <b runat="server" id="_fromA1"></b> til <b runat="server" id="_toA1"></b> d, <b runat="server" id="_date1"></b> for <b runat="server" id="_noOfPass1"></b></p>
        <table class="table table-striped" >
          <asp:Repeater ID="repRoute" runat="server">
              <HeaderTemplate>
                  <tr>
                    <th>Fra</th>
                    <th>Til</th>
                    <th>Flyver d.</th>
                    <th>Lander d.</th>
                    <th>Flyvetid</th>
                    <th>Pris</th>
                  </tr>
              </HeaderTemplate>
              <ItemTemplate>
                <tr>
                    <td><%#getAirportName((int)Eval("from"))%></td>
                    <td><%#getAirportName((int)Eval("to"))%></td>
                    <td><%#Eval( "timeOfDeparture") %></td>
                    <td><%#Eval( "timeOfArrival") %></td>
                    <td><%#Eval( "traveltime") %></td>
                    <td><%#Eval( "price") %></td>
                </tr>
              </ItemTemplate>
          </asp:Repeater>
        </table>

        <div class="span12">
        <p>
          <b>Samlet pris: <asp:Label ID="lblCTotalCost" runat="server" Text="Label"></asp:Label> </b><br/>
          <b>Samlet flyvetid: <asp:Label ID="lblCTotalTime" runat="server" Text="Label"></asp:Label> </b>
        </p> 
        </div>


       <h2>Køber information</h2>
       <div class="cform" id="theme-form">
            <div class="row">
                <div class="span4"> <span class="fName">
                <asp:TextBox ID="txtFName" runat="server"  placeholder="Fornavn" class="cform-text" size="50"></asp:TextBox>
                </span> </div>
                <div class="span4"> <span class="lName">
                <asp:TextBox ID="txtLName" runat="server"  placeholder="Efternavn" class="cform-text" size="50"></asp:TextBox>
                </span> </div>
                <div class="span4"> <span class="address">
                <asp:TextBox ID="txtAddress" runat="server"  placeholder="Adresse" class="cform-text" size="50"></asp:TextBox>
                </span> </div>
            </div>
            <div class="row">
                <div class="span4"> <span class="gender">
                <asp:DropDownList ID="ddlGender" runat="server">
                    <asp:ListItem Value="m">Mand</asp:ListItem>
                    <asp:ListItem Value="f">Kvinde</asp:ListItem>
                </asp:DropDownList>
                </span> </div>
                <div class="span4"> <span class="email">
                <asp:TextBox ID="txtEmail" runat="server"  placeholder="Email" class="cform-text" size="50"></asp:TextBox>
                </span> </div>
                <div class="span4"> <span class="phoneNo">
                <asp:TextBox ID="txtPhoneNo" runat="server"  placeholder="Telefon nr" class="cform-text" size="50"></asp:TextBox>
                </span> </div>
            </div>
            
           <h2 id="h2RestPass" runat="server">Resterende passagerer</h2>
           <asp:PlaceHolder runat="server" ID="otherPassengers" />

            <div>
                <asp:Button ID="btnBook" runat="server" Text="Bestil" OnClick="btnBook_Click" class="cform-submit pull-left" />
            </div>
        </div>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ErrorMessage="Skriv et fornavn" ForeColor="red" Text="*" ControlToValidate="txtFName" InitialValue="" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Skriv et efternavn" ForeColor="red" Text="*" ControlToValidate="txtLName" InitialValue="" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  runat="server" ErrorMessage="Skriv en adresse" ForeColor="red" Text="*" ControlToValidate="txtAddress" InitialValue="" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ErrorMessage="Skriv en email" ForeColor="red" Text="*" ControlToValidate="txtEmail" InitialValue="" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  runat="server" ErrorMessage="Skriv et telefon nummer" ForeColor="red" ControlToValidate="txtPhoneNo" Text="*" InitialValue="" Display="None"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Skriv et gyldigt telefonum nummer" ControlToValidate="txtPhoneNo" ValidationExpression="^((\(?\+45\)?)?)(\s?\d{2}\s?\d{2}\s?\d{2}\s?\d{2})$"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Skriv en gyldig email" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
             <asp:ValidationSummary ID="ValidationSummary1"  ForeColor="red" runat="server" HeaderText="Du skal indsætte følgende værdier:" DisplayMode="List" EnableClientScript="True" />


        </div>
        <!-- /.row -->
      </div>
      <!-- /.container -->
    </section>

</asp:Content>

