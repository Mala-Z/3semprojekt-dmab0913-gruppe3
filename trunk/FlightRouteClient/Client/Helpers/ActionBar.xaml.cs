using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Helpers
{
    /// <summary>
    /// Interaction logic for ActionBar.xaml
    /// </summary>
    public partial class ActionBar : UserControl
    {
        public event RoutedEventHandler AddClick;
        public event RoutedEventHandler RefreshClick;
        public event RoutedEventHandler DeleteClick;
        
        public ActionBar()
        {
            InitializeComponent();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (this.RefreshClick != null)
                this.RefreshClick(this, e);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.DeleteClick != null)
                this.DeleteClick(this, e);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (this.AddClick != null)
                this.AddClick(this, e);
        }
    }
}
