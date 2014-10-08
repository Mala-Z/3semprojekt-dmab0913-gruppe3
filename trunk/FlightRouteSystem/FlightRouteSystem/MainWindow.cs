using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseLayer;

namespace FlightRouteSystem
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            dmab0913_3DataContext db = DBConnection.GetInstance().GetConnection();

            var result = from p in db.Persons
                         where p.Gender == "m"
                         orderby p.Fname
                         select p;
                         //select new { Navn = c.Name, c.Email, Adresse = c.Address, Postnr = c.ZipCode };

            dgvPersons.DataSource = result;


        }
    }
}
