using System.Threading.Tasks;

namespace CO.AcessControl.AcessClient
{
    public interface IAcessControlHttpClient
    {
        Task AuthorizeAsync(string policyAuth, string authenticationBearer);
    }
}