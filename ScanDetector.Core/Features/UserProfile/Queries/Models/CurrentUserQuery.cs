using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.UserProfile.Queries.Results;
using MediatR;

namespace ScanDetector.Core.Features.UserProfile.Queries.Models
{
    public class CurrentUserQuery:IRequest<BaseResponse<CurrentUserResponse>>
    {
    }
}
