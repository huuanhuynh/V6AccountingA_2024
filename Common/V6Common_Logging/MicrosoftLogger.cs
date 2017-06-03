using System;
using System.Net;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace V6Soft.Common.Logging
{
    /// <summary>
    /// MicrosoftLogger is a utility class to write logs using Microsoft logging
    /// </summary>
    public class MicrosoftLogger : ILogger
    {
        private readonly string m_DnsHostName;

        /// <summary>
        /// Creates a new instance of MicrosoftLogger.
        /// </summary>
        public MicrosoftLogger()
        {
            Logger.SetLogWriter(new LogWriterFactory().Create());
            m_DnsHostName = Dns.GetHostName();
        }

        /// <summary>
        /// Executes a method with logging entry, exit and exception if any.
        /// </summary>
        public void ExecuteWithFullLogging(Action action, string actionName, params object[] parameters)
        {
            ExecuteWithFullLogging(() =>
            {
                action();
                return 0;
            },
                                   actionName,
                                   parameters);
        }

        /// <summary>
        /// Executes a method with logging entry, exit and exception if any.
        /// </summary>
        public T ExecuteWithFullLogging<T>(Func<T> action, string actionName, params object[] parameters)
        {
            LogTraceEntry(actionName);

            LogTraceDump(parameters);

            try
            {
                return action();
            }
            catch (Exception exception)
            {
                LogException("Exception occured during " + actionName, exception);

                throw;
            }
            finally
            {
                LogTraceExit(actionName);
            }
        }

        /// <summary>
        /// Executes a method with logging entry and exit.
        /// </summary>
        public void ExecuteWithLogging(Action action, string actionName, params object[] parameters)
        {
            ExecuteWithLogging(() =>
            {
                action();
                return 0;
            },
                               actionName,
                               parameters);
        }

        /// <summary>
        /// Executes a method with logging entry and exit.
        /// </summary>
        public T ExecuteWithLogging<T>(Func<T> action, string actionName, params object[] parameters)
        {
            LogTraceEntry(actionName);

            LogTraceDump(parameters);

            try
            {
                return action();
            }
            finally
            {
                LogTraceExit(actionName);
            }
        }

        /// <summary>
        /// Executes a method with logging exception if any.
        /// </summary>
        public void ExecuteWithExceptionLogging(Action action, string actionName, params object[] parameters)
        {
            ExecuteWithExceptionLogging(() =>
            {
                action();
                return 0;
            },
                                        actionName,
                                        parameters);
        }

        /// <summary>
        /// Executes a method with logging exception if any.
        /// </summary>
        public T ExecuteWithExceptionLogging<T>(Func<T> action, string actionName, params object[] parameters)
        {
            try
            {
                return action();
            }
            catch (Exception exception)
            {
                LogException("Exception occured during " + actionName, exception);

                throw;
            }
        }

        /// <summary>
        /// Creates a info log entry with specified message.
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="parameters">An object array containing zero or more objects to format.</param>
        public void LogInfo(string message, params object[] parameters)
        {
            Write(LoggingConstants.EventIdInfo,
                    LoggingConstants.PriorityInfo,
                    LoggingConstants.CategoryInfo,
                    message,
                    parameters);
        }

        /// <summary>   
        /// Logs a trace entry with parameter dumps. This means the objects and their properties will be written.
        /// Note: This can consume a lot of logspace, so use it with caution.
        /// </summary>
        /// <param name="parameters">Objects to dump.</param>
        public void LogTraceDump(params object[] parameters)
        {
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    var entryParameter = parameters[i];
                    LogTraceDump("Parameter " + (i + 1), entryParameter);
                }
            }
        }

        /// <summary>   
        /// Logs a trace entry with an object data dump. This means the object and its properties will be written.
        /// Note: This can consume a lot of logspace, so use it with caution.
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="value">An object to dump.</param>
        public void LogTraceDump(string message, object value)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(message);
            builder.Append(Environment.NewLine);
            builder.Append("ObjectDump:");
            builder.Append(Environment.NewLine);
            builder.Append(SafelyDumpObject(value));

            LogTrace(builder.ToString());
        }

        /// <summary>
        /// Creates a log trace entry with specified message for a method entry.
        /// </summary>
        /// <param name="method">The method that is entered.</param>
        public void LogTraceEntry(string method)
        {
            LogTrace("ENTRY: " + method);
        }

        /// <summary>
        /// Creates a log trace entry with specified message for a method exit.
        /// </summary>
        /// <param name="method">The method that is entered.</param>
        public void LogTraceExit(string method)
        {
            LogTrace("EXIT: " + method);
        }

        /// <summary>
        /// Creates a log trace entry with specified message.
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="parameters">An object array containing zero or more objects to format.</param>
        public void LogTrace(string message, params object[] parameters)
        {
            Write(LoggingConstants.EventIdTrace,
                    LoggingConstants.PriorityTrace,
                    LoggingConstants.CategoryTrace,
                    message,
                    parameters);
        }

        /// <summary>
        /// Creates a log warning entry with specified message.
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="parameters">An object array containing zero or more objects to format.</param>
        public void LogWarning(string message, params object[] parameters)
        {
            Write(LoggingConstants.EventIdWarning,
                    LoggingConstants.PriorityWarning,
                    LoggingConstants.CategoryWarning,
                    message,
                    parameters);
        }

        /// <summary>
        /// Creates a log exception entry with specified message and exception to be logged.
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="exception">The exception that caused logging to occur.</param>
        /// <param name="parameters">An object array containing zero or more objects to format.</param>
        public void LogException(string message, Exception exception, params object[] parameters)
        {
            message = SafelyFormatMessage(message, parameters); // We need to do it here because we add exception text that can break the formatting

            if (exception != null)
            {
                message += Environment.NewLine + exception;

                if (exception.InnerException != null)
                {
                    message += Environment.NewLine + SafelyDumpObject(exception.InnerException);
                }
            }

            LogException(message);
        }

        /// <summary>
        /// Creates a log exception entry with specified message
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="parameters">An object array containing zero or more objects to format.</param>
        public void LogException(string message, params object[] parameters)
        {
            Write(LoggingConstants.EventIdException,
                    LoggingConstants.PriorityException,
                    LoggingConstants.CategoryException,
                    message,
                    parameters);
        }

        private void Write(int eventId, int priority, string category, string message, params object[] parameters)
        {
            message = SafelyFormatMessage(message, parameters);

            LogEntry entry = new LogEntry();
            entry.EventId = eventId;
            entry.Priority = priority;
            entry.Categories.Add(category);
            entry.MachineName = m_DnsHostName;
            entry.Message = message;
            Logger.Write(entry);
        }

        private string SafelyFormatMessage(string message, object[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                try
                {
                    message = string.Format(message, parameters);
                }
                catch
                {
                    LogWarning("Formatting of the message string has failed. Raw message will be written");
                }
            }
            return message;
        }

        private string SafelyDumpObject(object value)
        {
            string result;
            try
            {
                result = ObjectDumper.Dump(value);
            }
            catch (Exception exception)
            {
                result = string.Format("Dumping object has failed: {0}", exception);
            }

            return result;
        }
    }
}
