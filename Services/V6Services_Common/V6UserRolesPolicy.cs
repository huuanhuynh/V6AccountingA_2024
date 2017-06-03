using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;

using V6Soft.Common.ExceptionHandling;
using V6Soft.Common.Security.Principal;



namespace V6Soft.Services.Common
{
    internal class V6UserRolesPolicy : IAuthorizationPolicy
    {
        private Guid m_Id = Guid.NewGuid();

        /// <summary>
        /// Evaluates the identity in the evaluation context, setting roles for the authenticated user.
        /// </summary>
        /// <param name="evaluationContext">EvaluationContext set by WCF infrastructure.</param>
        /// <param name="state"></param>
        /// <returns>True if roles set</returns>
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            // List that will hold the roles
            List<string> roles = new List<string>();

            // Get the authenticated client identity
            IIdentity client = GetClientIdentity(evaluationContext);

            GenericIdentity identity = client as GenericIdentity;
            if (identity == null)
            {
                // This is a special case for metadata requests, creating a mock identity for the developer.
                GenericIdentity genericIdentity = new GenericIdentity("developer");
                evaluationContext.Properties["Principal"] = new V6Principal(genericIdentity, null);
                return true;
            }

            // Add application roles
            roles.AddRange(GetApplicationRoles(client));

            // Set a new principal object holding the application roles
            evaluationContext.Properties["Principal"] = new V6Principal(client, roles.ToArray());

            return true;
        }

        /// <summary>
        /// Gets the ClaimSet for the identity.
        /// </summary>
        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        /// <summary>
        /// Unique identity of the UserPolicy object.
        /// </summary>
        public string Id
        {
            get { return m_Id.ToString(); }
        }

        private static IEnumerable<string> GetApplicationRoles(IIdentity client)
        {
            List<string> roles = new List<string>();

            return roles;
        }

        private static IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj = null;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
            {
                return null;
            }

            IList<IIdentity> identities = obj as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
            {
                throw new V6Exception("No identity found", ExceptionType.Technical, ExceptionSeverity.Normal);
            }

            return identities[0];
        }

    }
}
