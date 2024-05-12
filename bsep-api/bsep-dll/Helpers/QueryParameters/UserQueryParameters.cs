using bsep_dll.Helpers.Pagination;

namespace bsep_dll.Helpers.QueryParameters;

public class UserQueryParameters : QueryPageParameters
{
    public string Email { get; set; } = "";
}