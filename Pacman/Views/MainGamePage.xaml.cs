using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using WpfApplication.Resources.Models.Enums_and_Constants;

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
            LoadPathsAndWalls(context);
            //pacman
            var pacman = LoadModelPath(context,ViewConstants.PacmanModelPath);
            pacman.Transform = LoadModelPositionScaleAndBaseRot(context, context.Pacman, 0.1);
            Binding binding = new Binding();
            binding.Path = new PropertyPath(ViewConstants.PropertyPathEating);
            binding.Source = context.Pacman;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.Converter = new InversBoolToVisibilityConv();
            BindingOperations.SetBinding(pacman, FileModelVisual3D.VisibilityProperty, binding);
            Viewport.Children.Add(pacman);
            //EatingPacman
            var eatingPacman = LoadModelPath(context, ViewConstants.EatingPacmanModelPath);
            eatingPacman.Transform = LoadModelPositionScaleAndBaseRot(context, context.Pacman, 0.1);
            Binding binding2 = new Binding();
            binding2.Path = new PropertyPath(ViewConstants.PropertyPathEating);
            binding2.Source = context.Pacman;
            binding2.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding2.Converter = FindResource("ConverterBoolToVis") as BooleanToVisibilityConverter;
            BindingOperations.SetBinding(eatingPacman, FileModelVisual3D.VisibilityProperty, binding2);
            Viewport.Children.Add(eatingPacman);
            //Enemies
            foreach (var enemy in context.ListEnemies)
            {
                var enemyModel = LoadModelPath(context, ViewConstants.EnemyModelPath);
                enemyModel.Transform = LoadModelPositionScaleAndBaseRot(context, enemy, 0.1);
                Viewport.Children.Add(enemyModel);
            }
            Viewport.Focus();

        }
        FileModelVisual3D LoadModelPath(MainGameViewModel context, string path)
        {
            FileModelVisual3D fmv3D = new FileModelVisual3D();
            fmv3D.Source = context.AssemblyPath + path;
            return fmv3D;
        }
        Transform3DGroup LoadModelPositionScaleAndBaseRot(MainGameViewModel context,object Source,double scale)
        {
            var transformGroup = new Transform3DGroup();
            Binding bindingAngleRot = new Binding();
            var axisAngleRotation3D = new AxisAngleRotation3D();
            axisAngleRotation3D.Axis = new Vector3D(0.0, 1.0, 0.0);
            bindingAngleRot.Path = new PropertyPath(ViewConstants.PropertyPathDirection);
            bindingAngleRot.Source = Source;
            bindingAngleRot.Converter = new DirectionToRotationConverter();
            bindingAngleRot.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(axisAngleRotation3D, AxisAngleRotation3D.AngleProperty, bindingAngleRot);
            transformGroup.Children.Add(new RotateTransform3D(axisAngleRotation3D));

            var translateTransform = new TranslateTransform3D();
            Binding bindingX = new Binding();
            bindingX.Path = new PropertyPath(ViewConstants.PropertyPathRow);
            bindingX.Source = Source;
            bindingX.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            bindingX.Converter = new GridToPosXConverter();
            BindingOperations.SetBinding(translateTransform, TranslateTransform3D.OffsetXProperty, bindingX);

            Binding bindingZ = new Binding();
            bindingZ.Path = new PropertyPath(ViewConstants.PropertyPathCell);
            bindingZ.Source = Source;
            bindingZ.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            bindingZ.Converter = new GridToPosZConverter();
            BindingOperations.SetBinding(translateTransform, TranslateTransform3D.OffsetZProperty, bindingZ);

            transformGroup.Children.Add(translateTransform);
            transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1.0, 0.0, 0.0), 90)));
            transformGroup.Children.Add(new ScaleTransform3D(scale, scale, scale));
            return transformGroup;
        }
        void LoadPathsAndWalls(MainGameViewModel context)
        {
            foreach (var path in context.Maze.Paths)
            {
                FileModelVisual3D fmv3D = new FileModelVisual3D();
                fmv3D.Source = context.AssemblyPath +ViewConstants.GiftModelPath;
                var transformGroup = new Transform3DGroup();
                var scale = 0.1;
                Binding binding = new Binding();
                binding.Path = new PropertyPath(ViewConstants.PropertyPathHaveGift);
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
            foreach (var wall in context.ListBricks)
            {
                FileModelVisual3D fmv3D = new FileModelVisual3D();
                fmv3D.Source = context.AssemblyPath + ViewConstants.WallModelPath;
                var transformGroup = new Transform3DGroup();
                var scale = 0.1;
                Vector3D vector = new Vector3D(wall.X * 15, 0.0, wall.Y * -15);
                transformGroup.Children.Add(new TranslateTransform3D(vector));
                transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1.0, 0.0, 0.0), 90)));
                transformGroup.Children.Add(new ScaleTransform3D(scale, scale, scale));
                fmv3D.Transform = transformGroup;
                Viewport.Children.Add(fmv3D);
            }
        }
    }
}
