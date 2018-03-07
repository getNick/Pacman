using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.EnumsAndConstant
{
    public class GameConstants
    {
        public static int PacmanLifes { get; set; }
        public static int MaxCountUnsuccessfulInstall { get; set; }
        public static int MazeHeight { get; set; }
        public static int MazeWidth { get; set; }
        public static int PacmanCatchPause { get; set; }
        public static int EatingTime { get; set; }
        public static int CountRowsInRecords { get; set; }
        public static int PauseBetweenSteps { get; set; }
        public static int PacmanRespointRow { get; set; }
        public static int PacmanRespointCell { get; set; }
        public static int MinRandomBlockLength { get; set; }
        public static int MaxRandomBlockLength { get; set; }
        public static int MinRandomBlockBranchLength { get; set; }
        public static int MaxRandomBlockBranchLength { get; set; }
        public const string NamedParameterConnectionString = "connectionString";
        public const string NamedParameterRow = "row";
        public const string NamedParameterCell = "cell";
        public const string NamedParameterPacmanCatchPause = "pacmanCatchPause";
        public const string NamedParameterMazeHeight = "height";
        public const string NamedParameterMazeWidth = "width";
        public const string NamedParameterPacmanCountLifes = "countLifes";
        public const string NamedParameterPacmanTimeInvulnerable = "timeInvulnerable";
        public const string NamedParameterPlayer = "player";
        public const string PropertyScore = "Score";
        public static string ConnectionString { get; set; }

    }
}
