using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Common.Logs
{
    public class CustomLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;
        public CustomLogger(string name, CustomLoggerProviderConfiguration config)
        {
            this.loggerName = name;
            loggerConfig = config;
        }
#pragma warning disable CS8633 // La nulabilidad de las restricciones del parámetro de tipo no coincide con las restricciones del parámetro de tipo del método de interfaz implementado de forma implícita
        public IDisposable BeginScope<TState>(TState state)
#pragma warning restore CS8633 // La nulabilidad de las restricciones del parámetro de tipo no coincide con las restricciones del parámetro de tipo del método de interfaz implementado de forma implícita
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = string.Format("{0}: {1} - {2} - {3}", logLevel.ToString(), DateTime.Now.ToString("HH:mm:ss"), eventId.Id, formatter(state, exception));
            WriteTextToFile(message);
        }
        private void WriteTextToFile(string message)
        {
            string directorioLogs = $"Logs";
            string archivoLogs = "log_{date}.log";
            if (!Directory.Exists(directorioLogs))
            {
                Directory.CreateDirectory(directorioLogs);
            }
            //string filePath = "D:\\IDGLog.txt";
            var filePath = string.Format("{0}/{1}", directorioLogs, archivoLogs.Replace("{date}", DateTime.UtcNow.ToString("yyyyMMdd")));
            
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }
    }
}
