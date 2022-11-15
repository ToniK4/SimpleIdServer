namespace BluePoles.IdentityProvider.OpenId.Management
{
    public record Users();
    public interface IBPRestClient
    {
        Task<Users> GetUsers();
    }
}
