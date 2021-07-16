using System.ComponentModel;

namespace Codebiz.Domain.Common.Model
{
    public enum LogEventTitles
    {
        [Description("Request Start")]
        RequestStart = 1,
        [Description("Request End")]
        RequestEnd = 2,
        [Description("Request Details")]
        RequestDetails = 3,
        [Description("Login")]
        Login = 4,
        [Description("Logout")]
        Logout = 5,


    
  

        [Description("Update PMOSeminar")]
        UpdatePMOSeminar = 78,

        #region App User

        [Description("User created")]
        UserCreated = 79,

        [Description("User updated")]
        UserUpdated = 80,

        [Description("User reactivated")]
        UserReactivated = 81,

        [Description("User deactivated")]
        UserDeactivated = 82,

        [Description("Activation link sent")]
        ActivationLinkSent = 83,

        [Description("Reset password link sent")]
        ResetPasswordLinkSent = 84,

        #endregion

        [Description("User Group Created")]
        UserGroupCreated = 373,
        [Description("User Group Updated")]
        UserGroupUpdated = 374,
        [Description("User Group Reactivate Deactivate")]
        UserGroupReactivateDeactivate = 375,
        [Description("User Group Deleted")]
        UserGroupDeleted = 376,

        [Description("Configuration Settings Updated")]
        ConfigurationSettingsUpdated = 377,

        #region NavLink

        [Description("Nav Link Deleted")]
        NavLinkDeleted = 157,

        [Description("Nav Link Created")]
        NavlinkCreated = 378,
        [Description("Nav Link Updated")]
        NavlinkUpdated = 379,

        #endregion
        [Description("Permission Updated")]
        PermissionUpdated = 380,



        #region App User AppUser

        [Description("AppUser Created")]
        AppUserCreated = 503,
        [Description("AppUser Updated")]
        AppUserUpdated = 504,
        [Description("AppUser Deleted")]
        AppUserDeleted = 505,
        [Description("AppUser Deactivated")]
        AppUserDeactivated = 506,
        [Description("AppUser Activated")]
        AppUserActivated = 507,

        #endregion

        #region RS3
        [Description("Ticket Created")]
        TicketCreated = 508,
        [Description("Ticket Updated")]
        TicketUpdated = 509,
        [Description("Ticket Commented")]
        TicketCommented = 510,
        [Description("Ticket Commented and Resolved")]
        TicketCommentedAndResolved = 511,
        [Description("Ticket Resolved")]
        TicketResolved = 512,
        [Description("Ticket Reopened")]
        TicketReopened = 513,
        [Description("Ticket Taken")]
        TicketTaken = 514,
        #endregion
        [Description("Change Password")]
        ChangePassword = 515,

        

    }
}