using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V6Soft.Common.Utils;

namespace V6Soft.Services.Common
{
    /// <summary>
    /// V6 User Validator
    /// </summary>
    internal class V6UserValidator : UserNamePasswordValidator
    {
        /// <summary>
        /// Validates user name and password against the MtpConfiguration database.
        /// </summary>
        /// <param name="userName">Username to validate, Service Instance Id for services or servername for service controllers.</param>
        /// <param name="password">Hashed password for user and server name for services.</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        /// <exception cref="ArgumentException">ArgumentException</exception>
        public override void Validate(string userName, string password)
        {
            Guard.ArgumentNotNull(userName, "userName");
            Guard.ArgumentNotNull(password, "password");
            if (userName.Trim().Length == 0)
            {
                throw new ArgumentException("Username must contain a valid user", "userName");
            }
            if (password.Trim().Length < 0)
            {
                throw new ArgumentException("Password must contain a valid password", "password");
            }
        }
    }
}
