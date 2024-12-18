using AutoMapper;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.UserProfile.Queries.Models;
using ScanDetector.Core.Features.UserProfile.Queries.Results;
using ScanDetector.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.UserProfile.Queries.Handlers
{
    public class UserProfileQueryHandler : ResponseHandler,
        IRequestHandler<CurrentUserQuery, BaseResponse<CurrentUserResponse>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public UserProfileQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, ICurrentUserService currentUserService, IMapper mapper):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<BaseResponse<CurrentUserResponse>> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _currentUserService.GetUserAsync();
            var res = _mapper.Map<CurrentUserResponse>(user);
            return Success(res);
        }
    }
}
