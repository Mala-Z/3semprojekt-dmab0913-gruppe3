using System.Windows.Controls;

namespace Client.Helpers
{
    /// <summary>
    /// Interaction logic for DisplaySuccess.xaml
    /// </summary>
    public partial class DisplaySuccess : UserControl
    {
        public DisplaySuccess(string lblSuccessMsg)
        {
            InitializeComponent();
            SetLabelText(lblSuccessMsg);
        }

        private void SetLabelText(string txt)
        {
            lblSuccessMsg.Content = txt;
        }
    }
}
