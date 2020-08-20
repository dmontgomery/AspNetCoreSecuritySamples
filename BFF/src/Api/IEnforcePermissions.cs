namespace Api
{
    public interface IEnforcePermissions
    {
        void AssertAuthorized(string sessionIdFromClaim, string permissionName);
    }
}