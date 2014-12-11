<%@ Page Title="Forside" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="headerwrap">
        <header class="clearfix">
            <h1><span>Momon</span>do2</h1>
            <div class="container">
                <div class="row">
                    <div class="span12">
                        <h2>Find den bedste flyrute</h2>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                        </asp:DropDownList>

                        <asp:DropDownList ID="DropDownList2" runat="server">
                        </asp:DropDownList>
                        <input type="date" id="date" name="date" Height="20px" Width="80px" />
                        <asp:TextBox ID="TextBox1" runat="server" Height="30px" Width="80px"></asp:TextBox>
                        <input type="submit" value="Søg" class="cform-submit">
                    </div>
                </div>
                <div class="row">
                    <div class="span12">
                    </div>
                </div>
            </div>
        </header>
    </div>

</asp:Content>
