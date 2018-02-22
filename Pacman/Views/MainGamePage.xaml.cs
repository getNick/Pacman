using HelixToolkit.Wpf;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication.ViewModel;

namespace WpfApplication.Views
{
    /// <summary>
    /// Interaction logic for MainGamePage.xaml
    /// </summary>
    public partial class MainGamePage : Page
    {
        public MainGamePage()
        {
            InitializeComponent();
            var context = ((MainGameViewModel)DataContext);
            

           /* Binding binding = new Binding();
            binding.Path = new PropertyPath("Position");
            binding.Source = context;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(fmv3D, FileModelVisual3D.t, binding);
            */
            //transformGroup.Children.Add(new TranslateTransform3D(vector));

            
            foreach (var wall in context.Maze.Paths)
            {
                FileModelVisual3D fmv3D = new FileModelVisual3D();
                fmv3D.Source = @"D:\gitHub\Pacman\Pacman\Resources\Models\Gift\defGift.obj";
                var transformGroup = new Transform3DGroup();
                var scale = 0.1;
                Binding binding = new Binding();
                binding.Path = new PropertyPath("HaveGift");
                binding.Source = wall;
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                binding.Converter = FindResource("ConverterBoolToVis") as BooleanToVisibilityConverter;
                BindingOperations.SetBinding(fmv3D, FileModelVisual3D.VisibilityProperty, binding);
                Vector3D vector = new Vector3D(wall.GridPosition.X * 15, 0.0, wall.GridPosition.Y * -15);
                transformGroup.Children.Add(new TranslateTransform3D(vector));
                transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1.0, 0.0, 0.0), 90)));
                transformGroup.Children.Add(new ScaleTransform3D(scale, scale, scale));
                fmv3D.Transform = transformGroup;
                Viewport.Children.Add(fmv3D);
            }
        }
    }
}
