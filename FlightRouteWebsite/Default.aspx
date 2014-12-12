<%@ Page Title="Forside" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="obout_Calendar2_Net" Namespace="OboutInc.Calendar2" TagPrefix="obout" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        var datefield = document.createElement("input")
        datefield.setAttribute("type", "date")
        if (datefield.type != "date") { //if browser doesn't support input type="date", load files for jQuery UI Date Picker
            document.write('<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />\n')
            document.write('<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"><\/script>\n')
            document.write('<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"><\/script>\n')
        }
    </script>

    <script>
        if (datefield.type != "date") { //if browser doesn't support input type="date", initialize date picker widget:
            jQuery(function ($) { //on document.ready
                $('#birthday').datepicker();
            })
        }
    </script>


    <div id="headerwrap">
        <header class="clearfix">
            <h1><span>Momon</span>do2</h1>
            <div class="container">
                <div class="row">
                    <div class="span12">
                        <h2>Find den bedste flyrute</h2>
                        <asp:DropDownList ID="airportFrom" runat="server">
                        </asp:DropDownList>

                        <asp:DropDownList ID="airportTo" runat="server">
                        </asp:DropDownList>

                        <obout:Calendar ID="dateBox" runat="server" DatePickerMode="True" DateFormat="dd-MM-yyyy" TextBoxId="txtDate"></obout:Calendar>
                        <asp:TextBox ID="txtDate" runat="server" Height="30px" Width="120px" Font-Size="13px"></asp:TextBox>
                        <asp:TextBox ID="txtNoOfPassengers" runat="server" Height="30px" Width="120px" Font-Size="13px" placeholder="Antal Passagerer"></asp:TextBox>
                        &nbsp;<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Søg" />
                        <asp:TextBox ID="TextBox1" runat="server" Visible="false">BINGO!</asp:TextBox>
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
