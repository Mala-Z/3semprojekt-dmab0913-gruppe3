<%@ Page Title="Liste over lufthavne" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ListOfAirports.aspx.cs" Inherits="ListOfAirports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <section id="portfolio" class="single-page scrollblock">
        <div class="container">
            <h1 id="folio-headline">Liste over lufthavne</h1>
            <div class="row">
                Her finder du en liste over lufthavne
                <br />
                <br />

                <div class="row">
                    <asp:Repeater ID="repAirport" runat="server">

                        <HeaderTemplate>

                            <div class="row">
                                <div class="col-md-6 h4">Navn</div>
                                <div class="col-md-6 h4">Placering</div>

                            </div>
                            <!-- /.row -->
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="row" style="background-color: white">
                                <div class="col-md-6"><%#Eval("name") %></div>
                                <div class="col-md-6"><%#Eval("location") %></div>
                            </div>
                        </ItemTemplate>
                        
                         <AlternatingItemTemplate>
                            <div class="row">
                                <div class="col-md-6"><%#Eval("name") %></div>
                                <div class="col-md-6"><%#Eval("location") %></div>
                            </div>
                        </AlternatingItemTemplate>
                        
                       


                    </asp:Repeater>
                </div>
                <!-- /.row -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container -->
    </section>

</asp:Content>

