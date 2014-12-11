using System.Windows.Controls;

namespace Client.Helpers
{
    /// <summary>
    /// Interaction logic for ContentTitle.xaml
    /// </summary>
    public partial class ContentTitle : UserControl
    {
        public ContentTitle(string title)
        {
            InitializeComponent();
            lblTitle.Content = title;
        }
    }
}
