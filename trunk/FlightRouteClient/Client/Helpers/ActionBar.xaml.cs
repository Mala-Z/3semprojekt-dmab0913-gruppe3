using System.Windows;
using System.Windows.Controls;

namespace Client.Helpers
{
    /// <summary>
    /// Interaction logic for ActionBar.xaml
    /// </summary>
    public partial class ActionBar : UserControl
    {
        public event RoutedEventHandler AddClick;
        public event RoutedEventHandler RefreshClick;
        
        public ActionBar()
        {
            InitializeComponent();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (this.RefreshClick != null)
                this.RefreshClick(this, e);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (this.AddClick != null)
                this.AddClick(this, e);
        }
    }
}
