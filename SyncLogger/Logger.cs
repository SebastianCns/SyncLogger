using System;
using SyncLogger.Classes;
using SyncLogger.Enums;
using SyncLogger.StorageObjects;

namespace SyncLogger
{
    public class Logger
    {
        private LogBase logbase;
        private ConfigObj config;
        private bool readyToLog = false;

        public Logger(string applicationname, Targets target)
        {
            config = new ConfigObj();
            if(ConfigReader.GetXmlConfig(config) == true)
            {
                switch (target)
                {
                    case Targets.file:
                        logbase = new FileLog(config, applicationname);
                        readyToLog = true;
                        break;
                    case Targets.database:
                        //Needed to implement logic for database logging
                        break;
                }
            }
            else
            {
                ErrorMessenger.AddFileMessage("Logger.cs",
                    "New instance of logger is needed");
            }

        }

        public void Log(LogTypes type, string message)
        {
            LogObj log = new LogObj(type, message);

            if (readyToLog == true)
            {
                logbase.Logging(log);
            }
        }
    }
}

