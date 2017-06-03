using System;
using System.Security.Principal;



namespace V6Soft.Common.Security.Principal
{
    /// <summary>
    /// Principal class for MTP.
    /// </summary>
    public class V6Principal : IPrincipal
    {
        private IIdentity m_Identity;
        private DateTime m_Created;
        private string[] m_Roles;

        /// <summary>
        /// Initializes a new instance of the MtpPrincipal class from a user identity and 
        /// an array of role names to which the user represented by that identity belongs.
        /// </summary>
        /// <param name="identity">A basic implementation of IIdentity that represents any user.</param>
        /// <param name="roles">An array of role names to which the user represented by the identity parameter belongs.</param>
        /// <exception cref="ArgumentNullException">The identity parameter is null.</exception>
        public V6Principal(IIdentity identity, string[] roles)
        {
            m_Created = DateTime.Now;

            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            m_Identity = identity;

            if (roles != null)
            {
                m_Roles = new string[roles.Length];
                for (int i = 0; i < roles.Length; i++)
                {
                    m_Roles[i] = roles[i];
                }
            }
            
        }

        /// <summary>
        /// Gets the roles of the current identity.
        /// </summary>
        public string[] GetRoles()
        {
            return m_Roles;
        }

        /// <summary>
        /// Gets the time when the principal object was created.
        /// </summary>
        public DateTime CreationTime
        {
            get { return m_Created; }
        }

        #region IPrincipal Members

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        public IIdentity Identity
        {
            get { return m_Identity; }
        }

        /// <summary>
        /// Determines whether the current MtpPrincipal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        /// <returns>true if the current MtpPrincipal is a member of the specified role; otherwise, false.</returns>
        public bool IsInRole(string role)
        {
            if ((role != null) && (m_Roles != null))
            {
                for (int i = 0; i < m_Roles.Length; i++)
                {
                    if ((m_Roles[i] != null) && (string.Compare(m_Roles[i], role, StringComparison.OrdinalIgnoreCase) == 0))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        #endregion
    }
}
