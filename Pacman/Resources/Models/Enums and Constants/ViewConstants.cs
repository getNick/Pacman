using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Resources.Models.Enums_and_Constants
{
    public class ViewConstants
    {
        public const string RuLanguage = "ru-RU";
        public const string EnLanguage = "en-US";
        public const string PropertyPathEating = "Eating";
        public const string PropertyPathRow = "Row";
        public const string PropertyPathCell = "Cell";
        public const string PropertyPathHaveGift = "HaveGift";
        public const string PropertyPathDirection = "Direction";
        public static string EnLanguagePath { get; set; }
        public static string RuLanguagePath { get; set; }
        public static string LanguageBasePath { get; set; }
        public static string PacmanModelPath { get; set; }
        public static string EatingPacmanModelPath { get; set; }
        public static string EnemyModelPath { get; set; }
        public static string WallModelPath { get; set; }
        public static string GiftModelPath { get; set; }
        public static string HeartsImagePath { get; set; }
        public static int OneTailSize = 15;
    }
}
