<%@ Page Title="Liste over lufthavne" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ListOfAirports.aspx.cs" Inherits="ListOfAirports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <section id="portfolio" class="single-page scrollblock">
        <div class="container">
            <h1 id="folio-headline">Liste over lufthavne</h1>
            <div>
            Her finder du en liste over lufthavne
                
            <br />
            <br />

            <table class="table-striped">
                <tr>
                    <th>Navn</th>
                    <th>Placering</th>
                </tr>
                <asp:Repeater ID="repAirport" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("name") %></td>
                            <td><%#Eval("location") %></td>
                        </tr>
                        </ItemTemplate>
                </asp:Repeater>

            </table>
                </div>
        </div>
        <!-- /.row -->

        <!-- /.container -->
    </section>

</asp:Content>

