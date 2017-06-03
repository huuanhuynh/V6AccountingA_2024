using System;
using System.Runtime.Serialization;
using System.Security.Permissions;


namespace V6Soft.Common.ExceptionHandling
{
    /// <summary>
    /// Represents errors that occur during execution of MTP.
    /// </summary>
    [Serializable]
    public sealed class V6Exception : Exception
    {
        /// <summary>
        /// Initializes a new MtpException class with Technical exception type and Normal exception severity.
        /// </summary>
        public V6Exception()
            : base()
        {
            ExceptionType = ExceptionType.Technical;
            ExceptionSeverity = ExceptionSeverity.Normal;
        }

        /// <summary>
        /// Initializes a new MtpException class with a specified message, Technical exception type and Normal exception severity.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public V6Exception(string message)
            : base(message)
        {
            ExceptionType = ExceptionType.Technical;
            ExceptionSeverity = ExceptionSeverity.Normal;
        }

        /// <summary>
        /// Initializes a new MtpException class with a specified message and a reference
        /// to the inner exception that is the cause of this exception. Initialized with Technical exception type and 
        /// Normal exception severity.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public V6Exception(string message, Exception innerException)
            : base(message, innerException)
        {
            ExceptionType = ExceptionType.Technical;
            ExceptionSeverity = ExceptionSeverity.Normal;
        }

        /// <summary>
        /// Initializes a new MtpException class with a specified message, exception type and exception severity.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="exceptionType">The type of exception that occured.</param>
        /// <param name="exceptionSeverity">The severity of the exception that occured.</param>
        public V6Exception(string message, ExceptionType exceptionType, ExceptionSeverity exceptionSeverity)
            : base(message)
        {
            ExceptionType = exceptionType;
            ExceptionSeverity = exceptionSeverity;
        }

        /// <summary>
        /// Initializes a new MtpException class with a specified message, exception type and exception severity.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="errorCode">Error code uniquely describing the error.</param>
        /// <param name="exceptionType">The type of exception that occured.</param>
        /// <param name="exceptionSeverity">The severity of the exception that occured.</param>
        public V6Exception(string message, uint errorCode, ExceptionType exceptionType, ExceptionSeverity exceptionSeverity)
            : base(message)
        {
            ExceptionType = exceptionType;
            ExceptionSeverity = exceptionSeverity;
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new MtpException class with a specified message, a reference
        /// to the inner exception that is the cause of this exception, exception type and exception severity. 
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        /// <param name="exceptionType">The type of exception that occured.</param>
        /// <param name="exceptionSeverity">The severity of the exception that occured.</param>
        public V6Exception(string message, Exception innerException, ExceptionType exceptionType, ExceptionSeverity exceptionSeverity)
            : base(message, innerException)
        {
            ExceptionType = exceptionType;
            ExceptionSeverity = exceptionSeverity;
        }

        /// <summary>
        /// Initializes a new instance of the MtpException class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        private V6Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// Gets or sets the severity of the exception.
        /// </summary>
        public ExceptionSeverity ExceptionSeverity { get; set; }

        /// <summary>
        /// Gets or sets the type of exception.
        /// </summary>
        public ExceptionType ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets error data for the exception.
        /// </summary>
        public string ErrorData { get; set; }

        /// <summary>
        /// Gets or sets error code for the exception.
        /// </summary>
        public uint ErrorCode { get; set; }

        /// <summary>
        /// Sets the System.Runtime.Serialization.SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="info"> The System.Runtime.Serialization.SerializationInfo that holds the serialized 
        /// object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains information 
        /// about the source or destination.</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info != null)
            {
                info.AddValue("ExceptionType", ExceptionType, typeof(ExceptionType));
                info.AddValue("ExceptionSeverity", ExceptionSeverity, typeof(ExceptionSeverity));
                info.AddValue("ErrorData", ErrorData, typeof(string));
            }
            base.GetObjectData(info, context);
        }
    }
}
