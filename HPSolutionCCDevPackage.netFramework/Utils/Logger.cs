using HPSolutionCCDevPackage.netFramework.Atrributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HPSolutionCCDevPackage.netFramework.Utils
{
    public class Logger
    {
        private enum LogLv
        {
            [StringValue("V")]
            VERBOSE = 0,

            [StringValue("I")]
            INFO = 1,

            [StringValue("D")]
            DEBUG = 2,

            [StringValue("W")]
            WARNING = 3,

            [StringValue("F")]
            FATAL = 4,

            [StringValue("E")]
            ERROR = 5
        }

        private const string TAG = "HPSCCDP";
        private static object LogLocker = new object();

        private static StringBuilder _logBuilder { get; set; }
        private static StringBuilder _userLogBuilder { get; set; }
        private static string filePath { get; set; }
        private static string fileName { get; set; }
        private static string directory { get; set; }
        private static string folderName { get; set; }

        private string className { get; set; }
        private int PId { get; set; }
        private int TId { get; set; }

        static Logger()
        {
            Console.WriteLine("Init static Logger");


#if DEBUG
            InitLogDebug();
#else
            InitUserLog();
#endif

            try
            {
                var dateTimeNow = DateTime.Now.ToString("ddMMyyHHmmss");
                var attribs = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
                if (attribs.Length > 0)
                {
                    folderName = ((AssemblyCompanyAttribute)attribs[0]).Company + @"\" + Assembly.GetCallingAssembly().GetName().Name + @"\" + "log";
                }
                else
                {
                    folderName = TAG + @"\" + Assembly.GetCallingAssembly().GetName().Name + @"\" + "log";
                }

                fileName =
                   Assembly.GetCallingAssembly().GetName().Name + "_" +
                   Assembly.GetCallingAssembly().GetName().Version + "_" +
                   dateTimeNow + ".txt";

                directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                directory = directory + @"\" + folderName;

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                filePath = directory + @"\" + fileName;

                AppDomain.CurrentDomain.ProcessExit -= CurrentDomain_ProcessExit;
                AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

                AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            }
            catch (Exception e)
            {
                try
                {
                    var dateTimeNow = DateTime.Now.ToString("ddMMyyHHmmss");
                    fileName =
                    Assembly.GetCallingAssembly().GetName().Name + "_" +
                    Assembly.GetCallingAssembly().GetName().Version + "_" +
                    dateTimeNow + ".txt";

                    directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + "Data";

                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    filePath = directory + @"\" + fileName;

                    AppDomain.CurrentDomain.ProcessExit -= CurrentDomain_ProcessExit;
                    AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

                    AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
                    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                }
                catch
                {

                }
            }

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            WriteLog("F", TAG, "[UnhandledException]:" + e.ExceptionObject.ToString());
            ExportLogFile();
        }

        public Logger(string className)
        {
            this.className = className;

            PId = Process.GetCurrentProcess().Id;
            TId = Thread.CurrentThread.ManagedThreadId;
        }

        private static void InitUserLog()
        {
            Console.WriteLine("Init release logger");
            _userLogBuilder = new StringBuilder();
        }

        private static void InitLogDebug()
        {
            Console.WriteLine("Init debug logger");
            _logBuilder = new StringBuilder();
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            ExportLogFile();
        }

        public async void I(string message, [CallerMemberName] string callerMeberName = null)
        {
            WriteLog("I", TAG, className, callerMeberName, message);
        }
        public async void D(string message, [CallerMemberName] string callerMeberName = null)
        {
            WriteLog("D", TAG, className, callerMeberName, message);
        }

        public async void E(string message, [CallerMemberName] string callerMeberName = null)
        {
            WriteLog("E", TAG, className, callerMeberName, message);
        }

        public async void W(string message, [CallerMemberName] string callerMeberName = null)
        {
            WriteLog("W", TAG, className, callerMeberName, message);
        }

        public async void F(string message, [CallerMemberName] string callerMeberName = null)
        {
            WriteLog("F", TAG, className, callerMeberName, message);
        }

        public async void V(string message, [CallerMemberName] string callerMeberName = null)
        {
            WriteLog("V", TAG, className, callerMeberName, message);
        }

        private void WriteLog(string logLv, string tag, string className, string methodName, string message)
        {
            lock (LogLocker)
            {
                // Log format
                // (dd-MM HH:mm:ss) (Log lv) (Pid) (Tid) (Tag) (Class name:Method name:Message)
                var dateTimeNow = DateTime.Now.ToString("dd-MM HH:mm:ss:ffffff");
                var newLogLine = dateTimeNow + " " +
                    logLv + " " +
                    PId + " " +
                    TId + " " +
                    tag + " " +
                    className + ":" + methodName + ":" + message;

                if (_logBuilder != null)
                {
                    _logBuilder.AppendLine(newLogLine);
                    ClearBuffer(_logBuilder);
                }

                if (_userLogBuilder != null)
                {
                    switch (logLv)
                    {
                        case "D":
                            break;
                        default:
                            _userLogBuilder.AppendLine(newLogLine);
                            break;
                    }
                    ClearBuffer(_userLogBuilder);
                }
            }
        }

        private static void WriteLog(string logLv, string tag, string message)
        {
            lock (LogLocker)
            {
                var dateTimeNow = DateTime.Now.ToString("dd-MM HH:mm:ss:ffffff");
                var newLogLine = dateTimeNow + " " +
                    logLv + " " +
                    tag + " " +
                    message;

                if (_logBuilder != null)
                {
                    _logBuilder.AppendLine(newLogLine);
                    ClearBuffer(_logBuilder);
                }

                if (_userLogBuilder != null)
                {
                    switch (logLv)
                    {
                        case "D":
                            break;
                        default:
                            _userLogBuilder.AppendLine(newLogLine);
                            break;
                    }
                    ClearBuffer(_userLogBuilder);
                }
            }
        }

        private static void ClearBuffer(StringBuilder builder)
        {
            if (builder.Capacity >= builder.MaxCapacity - 100000)
            {
                ExportLogFile();
                builder.Clear();
            }
        }

        public static void ExportLogFile()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                }

                if (_logBuilder != null)
                {
                    File.AppendAllText(filePath, _logBuilder.ToString());
                }
                else if (_userLogBuilder != null)
                {
                    File.AppendAllText(filePath, _userLogBuilder.ToString());
                }
            }
            catch (Exception e)
            {

            }
        }
    }

}
