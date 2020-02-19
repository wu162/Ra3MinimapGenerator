using System.Windows;
using MinimapGen.MapGenerator;

namespace MinimapGen
{
    public partial class ChooseMapDialog : Window
    {
        private string[] mapNames;

        public ChooseMapDialog(string[] mapNames)
        {
            InitializeComponent();
            this.mapNames = mapNames;
            mapList.ItemsSource = IOUtility.getFilesNames(mapNames);
            mapList.SelectedIndex = 0;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string selectMap
        {
            get { return (string) mapList.SelectedItem; }
        }
    }
}