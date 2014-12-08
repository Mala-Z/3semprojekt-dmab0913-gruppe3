<%@ Page Title="Forside" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="headerwrap">
      <header class="clearfix">
        <h1><span>Momon</span>do2</h1>
        <div class="container">
          <div class="row">
            <div class="span12">
              <h2>Find den bedste flyrute</h2>
              <input type="text" name="comboTravelFrom" placeholder="Rejs fra" class="cform-text" size="10" title="Rejs fra">
              <input type="text" name="comboTravelTo" placeholder="Rejs til" class="cform-text" size="10" title="Rejs til">
              <input type="text" name="dpTravelDate" placeholder="Dato" class="cform-text" size="10" title="Rejse dato">
              <input type="text" name="comboNoOfPassengers" placeholder="Antal passenger" class="cform-text" size="10" title="">
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
