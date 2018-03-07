using Autofac;
using WpfApplication.ViewModel;
using System.Windows;
using WpfApplication.Views;
using GameService.Services;
using System;
using NLog;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using GameCore.EnumsAndConstant;
using System.Configuration;
using WpfApplication.Resources.Models.Enums_and_Constants;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Autofac.IContainer ViewContainer;
        private static List<CultureInfo> m_Languages = new List<CultureInfo>();

        public static List<CultureInfo> Languages
        {
            get
            {
                return m_Languages;
            }
        }

        public App()
        {
            InitializeComponent();
            SetViewConstant();
            App.LanguageChanged += App_LanguageChanged;

            m_Languages.Clear();
            m_Languages.Add(new CultureInfo(ViewConstants.EnLanguage)); 
            m_Languages.Add(new CultureInfo(ViewConstants.RuLanguage));

            Language = WpfApplication.Properties.Settings.Default.DefaultLanguage;
        }

        public static event EventHandler LanguageChanged;


        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case ViewConstants.RuLanguage:
                        dict.Source = new Uri(ViewConstants.RuLanguagePath, UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri(ViewConstants.EnLanguagePath, UriKind.Relative);
                        break;
                }

                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith(ViewConstants.LanguageBasePath)
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                LanguageChanged(Application.Current, new EventArgs());
            }
        }
        void SetViewConstant()
        {
            ViewConstants.EnLanguagePath = ConfigurationManager.AppSettings.Get("EnLanguagePath");
            ViewConstants.RuLanguagePath = ConfigurationManager.AppSettings.Get("RuLanguagePath");
            ViewConstants.LanguageBasePath = ConfigurationManager.AppSettings.Get("LanguageBasePath");
            ViewConstants.HeartsImagePath = ConfigurationManager.AppSettings.Get("HeartsImagePath");
            ViewConstants.PacmanModelPath = ConfigurationManager.AppSettings.Get("PacmanModelPath");
            ViewConstants.EatingPacmanModelPath = ConfigurationManager.AppSettings.Get("EatingPacmanModelPath");
            ViewConstants.EnemyModelPath = ConfigurationManager.AppSettings.Get("EnemyModelPath");
            ViewConstants.GiftModelPath = ConfigurationManager.AppSettings.Get("GiftModelPath");
            ViewConstants.WallModelPath = ConfigurationManager.AppSettings.Get("WallModelPath");

        }
        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            WpfApplication.Properties.Settings.Default.DefaultLanguage = Language;
            WpfApplication.Properties.Settings.Default.Save();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
           var builder = new ContainerBuilder();
                builder.RegisterType<MainWindowViewModel>().AsSelf().SingleInstance();
                builder.RegisterType<LayerService>().AsSelf().SingleInstance();
                ViewContainer = builder.Build();
                var model = ViewContainer.Resolve<MainWindowViewModel>();
                var view = new StartWindow { DataContext = model };
                view.Show();
        }
    }
}
