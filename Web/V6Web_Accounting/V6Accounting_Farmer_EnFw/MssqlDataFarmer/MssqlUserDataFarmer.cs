using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.User;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Accounting.Farmers.EnFw.Extension;
using V6Soft.Accounting.Membership.Farmers;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.Membership.Dto;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.INhanVienDataFarmer"/>
    /// </summary>
    public class MssqlUserDataFarmer : EnFwDataFarmerBase<V6User, User>, IUserDataFarmer
    {
        private readonly IMappingRelatedToUser m_MappingRelatedToUser;
        public MssqlUserDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToUser mappingRelatedToUser)
            : base(dbContext)
        {
            m_MappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<User> GetUsers(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("UserId")
                };
            }
            return FindByCriteria(criteria);
        }

        public User Authenticate(string username, string password)
        {
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Username", username));
                parameters.Add(new SqlParameter("@Password", password));
                var connectionString = m_DbContext.Database.Connection.ConnectionString;
                var ds = ExecuteSqlStoredProcedure("SP_AUTHENTICATE", connectionString, parameters);
                if (ds.Tables.Count > 0)
                {
                    var dbUser = ds.Tables[0].ConvertToObject<V6User>();
                    return m_MappingRelatedToUser.MapToUser<User>(dbUser);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool AddUser(User user)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ModuleId", user.ModuleId));
            parameters.Add(new SqlParameter("@Username", user.UserName));
            parameters.Add(new SqlParameter("@Password", user.PlainPassword));
            parameters.Add(new SqlParameter("@UserPre", user.UserPre));
            parameters.Add(new SqlParameter("@Comment", user.Comment));
            parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
            parameters.Add(new SqlParameter("@IsVAdmin", user.IsVAdmin));
            parameters.Add(new SqlParameter("@Rights", user.Rights));
            parameters.Add(new SqlParameter("@DelY_N", user.IsDelete));
            parameters.Add(new SqlParameter("@DeletionRights", user.DeletionRights));
            parameters.Add(new SqlParameter("@ModificationRights", user.ModificationRights));
            parameters.Add(new SqlParameter("@AdditionRights", user.@AdditionRights));
            parameters.Add(new SqlParameter("@UserId2", user.UserId2));
            parameters.Add(new SqlParameter("@UserAcc", user.UserAcc));
            parameters.Add(new SqlParameter("@UserInv", user.UserInv));
            parameters.Add(new SqlParameter("@Pagedefa", user.Pagedefa));
            parameters.Add(new SqlParameter("@WarehouseRights", user.WarehouseRights));
            parameters.Add(new SqlParameter("@DvcsRights", user.DvcsRights));
            parameters.Add(new SqlParameter("@UserOther", user.UserOther));
            parameters.Add(new SqlParameter("@InheritId", user.InheritId));
            parameters.Add(new SqlParameter("@InheritCh", user.InheritCh));
            parameters.Add(new SqlParameter("@CheckSync", user.CheckSync));
            parameters.Add(new SqlParameter("@Level", user.Level));
            var connectionString = m_DbContext.Database.Connection.ConnectionString;
            var ds = ExecuteSqlStoredProcedure("SP_CREATEUSER", connectionString, parameters);
            //var dbUser = ds.Tables[0].ConvertToObject<V6User>();
            return true;
        }

        protected override User ToAppModel(V6User dbModel)
        {
            return m_MappingRelatedToUser.MapToUser<User>(dbModel);
        }

        protected override V6User ToEntityModel(User appModel)
        {
            throw new NotImplementedException();
        }
    }
}
