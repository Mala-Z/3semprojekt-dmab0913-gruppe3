<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Booking.aspx.cs" Inherits="Booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <section id="portfolio" class="single-page scrollblock">
      <div class="container">
        <h1 id="folio-headline">Bestilling</h1>
        <div class="row">
        <p>Du har valgt følgende flyrute fra <b>Aalborg</b> til <b>New York</b> d, <b>01.01.2015</b></p>
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
                <div class="span4"> <span class="your-name">
                <input type="text" name="your-name" placeholder="Fornavn" class="cform-text" size="40" title="your name">
                </span> </div>
                <div class="span4"> <span class="your-email">
                <input type="text" name="your-email" placeholder="Efternavn" class="cform-text" size="40" title="your email">
                </span> </div>
                <div class="span4"> <span class="your-name">
                <input type="text" name="your-name" placeholder="Adresse" class="cform-text" size="40" title="your name">
                </span> </div>
            </div>
            <div class="row">
                <div class="span4"> <span class="your-email">
                <input type="text" name="your-email" placeholder="Køn" class="cform-text" size="40" title="your email">
                </span> </div>
                <div class="span4"> <span class="company">
                <input type="text" name="company" placeholder="Email" class="cform-text" size="40" title="company">
                </span> </div>
                <div class="span4"> <span class="website">
                <input type="text" name="website" placeholder="Telefon nr." class="cform-text" size="40" title="website">
                </span> </div>
            </div>
            
            <div>
                <input type="submit" value="Bestil" class="cform-submit pull-left">
            </div>
        </div>


        </div>
        <!-- /.row -->
      </div>
      <!-- /.container -->
    </section>

</asp:Content>

