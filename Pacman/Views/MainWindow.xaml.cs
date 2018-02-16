using HelixToolkit.Wpf;
using Pacman.ViewModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pacman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Binding binding = new Binding();
            binding.Path = new PropertyPath("Score");
            var list = ((MainWindowViewModel)DataContext).Maze;
            // var listdd= list.Maze.Walls.Where(wall => wall.GrigPosition.X == 5 && wall.GrigPosition.Z == 0);
            foreach (var wall in list.Walls)
            {
                
                FileModelVisual3D fmv3D = new FileModelVisual3D();
                fmv3D.Source = @"D:\gitHub\Pacman\Pacman\Resources\Models\Wall\wall1.obj";
                var transformGroup = new Transform3DGroup();
                var scale = 0.1;
                Vector3D vector = new Vector3D(wall.X* 15,0.0, wall.Y* -15);
                transformGroup.Children.Add(new TranslateTransform3D(vector));
                
                transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1.0,0.0,0.0),90)));
                transformGroup.Children.Add(new ScaleTransform3D(scale, scale, scale));
                fmv3D.Transform = transformGroup;
                Viewport.Children.Add(fmv3D);
            }
            foreach (var gift in list.GetAllPath())
            {

                FileModelVisual3D fmv3D = new FileModelVisual3D();
                fmv3D.SetName("Gift");
                fmv3D.Source = @"D:\gitHub\Pacman\Pacman\Resources\Models\Gift\defGift.obj";
                var transformGroup = new Transform3DGroup();
                var scale = 0.1;
                Vector3D vector = new Vector3D(gift.X * 15, 5.0, gift.Y * -15);
                var transTransf = new TranslateTransform3D(vector);
                transformGroup.Children.Add(transTransf);
                var rotateTransf = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1.0, 0.0, 0.0), 90));
                transformGroup.Children.Add(rotateTransf);
                transformGroup.Children.Add(new ScaleTransform3D(scale, scale, scale));
                fmv3D.Transform = transformGroup;
                Viewport.Children.Add(fmv3D);

            }
        }
    }
}
