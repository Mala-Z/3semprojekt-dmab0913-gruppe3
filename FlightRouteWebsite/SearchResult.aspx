<%@ Page Title="Søgeresultater" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SearchResult.aspx.cs" Inherits="SearchResult" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <section id="portfolio" class="single-page scrollblock">
      <div class="container">
        <h1 id="folio-headline">Søgeresultat</h1>
        <div class="row">
        <p>Her er de to bedste ruter fra <b>Aalborg</b> til <b>New York</b> d, <b>01.01.2015</b></p>
        <h2>Billigst</h2>
        <table class="table table-striped" >
          <tr>
            <th>Fra</th>
            <th>Til</th>
            <th>Flyver d.</th>
            <th>Lander d.</th>
            <th>Flyvetid</th>
            <th>Pris</th>
          </tr>
            
            <asp:Repeater ID="repCheapest" runat="server">
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
        <div class="span10">
        <p>
          <b>Samlet pris: <asp:Label ID="lblCTotalCost" runat="server" Text="Label"></asp:Label> </b><br/>
          <b>Samlet flyvetid: <asp:Label ID="lblCTotalTime" runat="server" Text="Label"></asp:Label> </b>
        </p> 
        </div>
        <div class="span2">
        <p>
          &nbsp;<asp:Button ID="btnFastest" runat="server" Text="Vælg denne rute" OnClick="Button1_Click" />
        </p>
        </div>
        
        <h2>Hurtigst</h2>
        <table class="table table-striped" >
          <tr>
            <th>Fra</th>
            <th>Til</th>
            <th>Flyver d.</th>
            <th>Lander d.</th>
            <th>Flyvetid</th>
            <th>Pris</th>
          </tr>
            <asp:Repeater ID="repFastest" runat="server">
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
        <div class="span10">
        <p>
          <b>Samlet pris: <asp:Label ID="lblFTotalCost" runat="server" Text="Label"></asp:Label> </b><br/>
          <b>Samlet flyvetid: <asp:Label ID="lblFTotalTime" runat="server" Text="Label"></asp:Label> </b>
        </p> 
        </div>
        <div class="span2">
        <p>
          &nbsp;<asp:Button ID="btnCheapest" runat="server" Text="Vælg denne rute" OnClick="Button2_Click" />
        </p>
        </div>
        



        </div>
        <!-- /.row -->
      </div>
      <!-- /.container -->
    </section>

</asp:Content>

