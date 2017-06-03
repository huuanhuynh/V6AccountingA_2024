using System;



namespace V6Soft.Common.Logging
{
    /// <summary>
    /// An interfacte for logging
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Creates a info log entry with specified message.
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="parameters">An object array containing zero or more objects to format.</param>
        void LogInfo(string message, params object[] parameters);

        /// <summary>   
        /// Logs a trace entry with parameter dumps. This means the objects and their properties will be written.
        /// Note: This can consume a lot of logspace, so use it with caution.
        /// </summary>
        /// <param name="parameters">Parameters to dump.</param>
        void LogTraceDump(params object[] parameters);

        /// <summary>   
        /// Logs a trace entry with a object data dump. This means the object and its properties will be written.
        /// Note: This can consume a lot of logspace, so use it with caution.
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="value">An object to dump.</param>
        void LogTraceDump(string message, object value);

        /// <summary>
        /// Creates a log trace entry with specified message for a method entry.
        /// </summary>
        /// <param name="method">The method that is entered.</param>
        void LogTraceEntry(string method);

        /// <summary>
        /// Creates a log trace entry with specified message for a method exit.
        /// </summary>
        /// <param name="method">The method that is entered.</param>
        void LogTraceExit(string method);

        /// <summary>
        /// Creates a log trace entry with specified message.
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="parameters">An object array containing zero or more objects to format.</param>
        void LogTrace(string message, params object[] parameters);

        /// <summary>
        /// Creates a log warning entry with specified message.
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="parameters">An object array containing zero or more objects to format.</param>
        void LogWarning(string message, params object[] parameters);

        /// <summary>
        /// Creates a log exception entry with specified message
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="parameters">An object array containing zero or more objects to format.</param>
        void LogException(string message, params object[] parameters);

        /// <summary>
        /// Creates a log exception entry with specified message and exception to be logged.
        /// </summary>
        /// <param name="message">The message of the log entry.</param>
        /// <param name="exception">The exception that caused logging to occur.</param>
        /// <param name="parameters">An object array containing zero or more objects to format.</param>
        void LogException(string message, Exception exception, params object[] parameters);

        /// <summary>
        /// Executes a method with logging entry, exit and exception if any.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <param name="actionName">Description of the executed action.</param>
        /// <param name="parameters">Parameters to dump when logging method entry.</param>
        void ExecuteWithFullLogging(Action action, string actionName, params object[] parameters);

        /// <summary>
        /// Executes a method with logging entry, exit and exception if any.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <param name="actionName">Description of the executed action.</param>
        /// <param name="parameters">Parameters to dump when logging method entry.</param>
        T ExecuteWithFullLogging<T>(Func<T> action, string actionName, params object[] parameters);

        /// <summary>
        /// Executes a method with logging entry and exit.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <param name="actionName">Description of the executed action.</param>
        /// <param name="parameters">Parameters to dump when logging method entry.</param>
        void ExecuteWithLogging(Action action, string actionName, params object[] parameters);

        /// <summary>
        /// Executes a method with logging entry and exit.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <param name="actionName">Description of the executed action.</param>
        /// <param name="parameters">Parameters to dump when logging method entry.</param>
        T ExecuteWithLogging<T>(Func<T> action, string actionName, params object[] parameters);

        /// <summary>
        /// Executes a method with logging exception if any.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <param name="actionName">Description of the executed action.</param>
        /// <param name="parameters">Parameters to dump when logging method entry.</param>
        void ExecuteWithExceptionLogging(Action action, string actionName, params object[] parameters);

        /// <summary>
        /// Executes a method with logging exception if any.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <param name="actionName">Description of the executed action.</param>
        /// <param name="parameters">Parameters to dump when logging method entry.</param>
        T ExecuteWithExceptionLogging<T>(Func<T> action, string actionName, params object[] parameters);
    }
}
