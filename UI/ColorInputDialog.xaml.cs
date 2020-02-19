using System.Drawing;
using System.Windows;
using MinimapGen.MapGenerator;

namespace MinimapGen
{
    public partial class colorInputDialog : Window
    {
        
        public colorInputDialog(Color[] color)
        {
            InitializeComponent();
            color1.SelectedColor = System.Windows.Media.Color.FromRgb(color[0].R, color[0].G, color[0].B);
            color2.SelectedColor = System.Windows.Media.Color.FromRgb(color[1].R, color[1].G, color[1].B);
            color3.SelectedColor = System.Windows.Media.Color.FromRgb(color[2].R, color[2].G, color[2].B);
            color4.SelectedColor = System.Windows.Media.Color.FromRgb(color[3].R, color[3].G, color[3].B);
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            MapHelper.customColor = new[]
            {
                MapHelper.parseColor(color1.SelectedColor.ToString()),
                MapHelper.parseColor(color2.SelectedColor.ToString()),
                MapHelper.parseColor(color3.SelectedColor.ToString()),
                MapHelper.parseColor(color4.SelectedColor.ToString())
            };
            this.DialogResult = true;
        }
    }
}