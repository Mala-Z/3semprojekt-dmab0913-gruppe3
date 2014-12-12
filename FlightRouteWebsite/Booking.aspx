<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Booking.aspx.cs" Inherits="Booking" %>

<%@ register src="~/UserControls/AddPassenger.ascx" tagprefix="uc1" tagname="AddPassenger" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <section id="portfolio" class="single-page scrollblock">
      <div class="container">
        <h1 id="folio-headline">Bestilling</h1>
        <div class="row">
        <p>Du har valgt følgende flyrute fra <b>Aalborg</b> til <b>New York</b> d, <b>01.01.2015</b> for <b>3</b> passagerer</p>
        <table class="table table-striped" >
          <tr>
            <th>Fra</th>
            <th>Til</th>
            <th>Flyver d.</th>
            <th>Lander d.</th>
            <th>Flyvetid</th>
            <th>Pris</th>
              <th>Samlet pris</th>
          </tr>

          <tr>
            <td>Aalborg</td>
            <td>London</td>
            <td>01/01/2015 12:00</td>
            <td>01/01/2015 13:30</td>
            <td>1,5 timer</td>
            <td>450 kr</td>
              <td>450 kr</td>
          </tr>
          <tr>
            <td>Aalborg</td>
            <td>London</td>
            <td>01/01/2015 12:00</td>
            <td>01/01/2015 13:30</td>
            <td>1,5 timer</td>
            <td>450 kr</td>
              <td>450 kr</td>
          </tr>
        </table>
        <p>
          <b>Samlet pris: 1000 kr.</b> <br />
          <b>Samlet flyvetid: 5,5 time</b>
        </p> 


        <p>Antal personer</p>

       <h2>Køber information</h2>
       <div class="cform" id="theme-form">
            <div class="row">
                <div class="span4"> <span class="fName">
                <asp:TextBox ID="txtFName" runat="server" placeholder="Fornavn" class="cform-text" size="50"></asp:TextBox>
                </span> </div>
                <div class="span4"> <span class="lName">
                <asp:TextBox ID="txtLName" runat="server" placeholder="Efternavn" class="cform-text" size="50"></asp:TextBox>
                </span> </div>
                <div class="span4"> <span class="address">
                <asp:TextBox ID="txtAddress" runat="server" placeholder="Adresse" class="cform-text" size="50"></asp:TextBox>
                </span> </div>
            </div>
            <div class="row">
                <div class="span4"> <span class="gender">
                <asp:TextBox ID="txtGender" runat="server" placeholder="Køn" class="cform-text" size="50"></asp:TextBox>
                </span> </div>
                <div class="span4"> <span class="email">
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" class="cform-text" size="50"></asp:TextBox>
                </span> </div>
                <div class="span4"> <span class="phoneNo">
                <asp:TextBox ID="txtPhoneNo" runat="server" placeholder="Telefon nr" class="cform-text" size="50"></asp:TextBox>
                </span> </div>
            </div>
            
           <h2 id="h2RestPass" runat="server">Resterende passagerer</h2>
           <asp:PlaceHolder runat="server" ID="otherPassengers" />

            <div>
                <asp:Button ID="btnBook" runat="server" Text="Bestil" OnClick="btnBook_Click" class="cform-submit pull-left" />
            </div>
        </div>


        </div>
        <!-- /.row -->
      </div>
      <!-- /.container -->
    </section>

</asp:Content>

