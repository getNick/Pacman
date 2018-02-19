using HelixToolkit.Wpf;
using Pacman.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            
            var context = ((MainWindowViewModel)DataContext);

            TextBlock tb = new TextBlock();
            /* < TextBlock Grid.Row = "0" Grid.Column = "1"  VerticalAlignment = "Center" HorizontalAlignment = "Center" 
             * Name = "ScoreText" Foreground = "White" FontSize = "25"
            Text = "{Binding Path=Score, StringFormat='Score: {0}', UpdateSourceTrigger=PropertyChanged}" >
        </ TextBlock >
        < TextBlock Grid.Row = "0" Grid.Column = "2" VerticalAlignment = "Center" HorizontalAlignment = "Center" 
                x: Name = "TimeLeftText" Foreground = "White" FontSize = "25" >< Run Text = "Time left:100" /></ TextBlock >*/
            Grid.SetRow(tb,0);
            Grid.SetColumn(tb, 2);
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.Foreground = Brushes.White;
            tb.FontSize = 25;
            Binding binding = new Binding();
            binding.Path = new PropertyPath("TimeLeft");
            binding.StringFormat="Time left:{0}";
            binding.Source = context;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(tb, TextBlock.TextProperty, binding);

            Binding bindVis = new Binding();
            bindVis.Path = new PropertyPath("IsVisible");
            bindVis.Source = context;
            bindVis.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            bindVis.Converter = FindResource("ConverterBoolToVis") as BooleanToVisibilityConverter;
            //Visibility = "{Binding Path=IsVisible, Converter={StaticResource Converter}, UpdateSourceTrigger=PropertyChanged}"
            BindingOperations.SetBinding(tb, TextBlock.VisibilityProperty, bindVis);
            GridHandler.Children.Add(tb);

            ObservableCollection<Visual3D> collection = new ObservableCollection<Visual3D>();

            
            foreach (var wall in context.Maze.Walls)
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
                collection.Add(fmv3D);
            }
            foreach (var gift in context.Maze.Gifts)
            {

                FileModelVisual3D fmv3D = new FileModelVisual3D();
                fmv3D.SetName("Gift");
                fmv3D.Source = @"D:\gitHub\Pacman\Pacman\Resources\Models\Gift\defGift.obj";
                /*Binding bind = new Binding();
                bind.Path = new PropertyPath("isVisible");
                bind.Source = context;
                var a = Visibility.Hidden;
                BindingOperations.SetBinding(fmv3D, FileModelVisual3D.VisibilityProperty,Visibility.Hidden);*/
                var transformGroup = new Transform3DGroup();
                var scale = 0.1;
                Vector3D vector = new Vector3D(gift.GridPosition.X * 15, 5.0, gift.GridPosition.Y * -15);
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
