using System.Windows.Controls;

namespace Client.Helpers
{
    /// <summary>
    /// Interaction logic for DisplayError.xaml
    /// </summary>
    public partial class DisplayError : UserControl
    {
        public DisplayError(string lblErrorMsg)
        {
            InitializeComponent();
            SetLabelText(lblErrorMsg);
        }

        private void SetLabelText(string txt)
        {
            lblErrorMsg.Content = txt;
        }
    }
}
