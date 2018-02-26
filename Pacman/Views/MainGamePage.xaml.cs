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
using WpfApplication.Utils;
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
            foreach (var wall in context.ListBricks)
            {
                FileModelVisual3D fmv3D = new FileModelVisual3D();
                fmv3D.Source = @"D:\gitHub\Pacman\Pacman\Resources\Models\Wall\wall1.obj";
                var transformGroup = new Transform3DGroup();
                var scale = 0.1;
                Vector3D vector = new Vector3D(wall.X * 15, 0.0, wall.Y * -15);
                transformGroup.Children.Add(new TranslateTransform3D(vector));
                transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1.0, 0.0, 0.0), 90)));
                transformGroup.Children.Add(new ScaleTransform3D(scale, scale, scale));
                fmv3D.Transform = transformGroup;
                Viewport.Children.Add(fmv3D);
            }

            foreach (var enemy in context.ListEnemies)
            {
                FileModelVisual3D fmv3D = new FileModelVisual3D();
                fmv3D.Source = @"D:\gitHub\Pacman\Pacman\Resources\Models\Enemy\enemy.obj";
                var transformGroup = new Transform3DGroup();
                var scale = 0.1;
                var translateTransform = new TranslateTransform3D();
                Binding bindingX = new Binding();
                bindingX.Path = new PropertyPath("Row");
                bindingX.Source = enemy;
                bindingX.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                bindingX.Converter = new GridToPosXConverter();
                BindingOperations.SetBinding(translateTransform, TranslateTransform3D.OffsetXProperty, bindingX);

                Binding bindingZ = new Binding();
                bindingZ.Path = new PropertyPath("Cell");
                bindingZ.Source = enemy;
                bindingZ.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                bindingZ.Converter = new GridToPosZConverter();
                BindingOperations.SetBinding(translateTransform, TranslateTransform3D.OffsetZProperty, bindingZ);

                transformGroup.Children.Add(translateTransform);
                transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1.0, 0.0, 0.0), 90)));
                transformGroup.Children.Add(new ScaleTransform3D(scale, scale, scale));
                fmv3D.Transform = transformGroup;
                Viewport.Children.Add(fmv3D);
            }

            foreach (var path in context.Maze.Paths)
            {
                FileModelVisual3D fmv3D = new FileModelVisual3D();
                fmv3D.Source = @"D:\gitHub\Pacman\Pacman\Resources\Models\Gift\defGift.obj";
                var transformGroup = new Transform3DGroup();
                var scale = 0.1;
                Binding binding = new Binding();
                binding.Path = new PropertyPath("HaveGift");
                binding.Source = path;
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                binding.Converter = FindResource("ConverterBoolToVis") as BooleanToVisibilityConverter;
                BindingOperations.SetBinding(fmv3D, FileModelVisual3D.VisibilityProperty, binding);
                Vector3D vector = new Vector3D(path.Row * 15, 7.5, path.Cell * -15);
                transformGroup.Children.Add(new TranslateTransform3D(vector));
                transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1.0, 0.0, 0.0), 90)));
                transformGroup.Children.Add(new ScaleTransform3D(scale, scale, scale));
                fmv3D.Transform = transformGroup;
                Viewport.Children.Add(fmv3D);
            }
        }
    }
}
