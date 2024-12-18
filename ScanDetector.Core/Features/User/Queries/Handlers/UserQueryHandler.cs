using AutoMapper;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.User.Queries.Models;
using ScanDetector.Core.Features.User.Queries.Results;
using ScanDetector.Core.Wrappers;
using ScanDetector.Infrastructure.Abstracts.Base;
using ScanDetector.Service.Abstracts;
using MediatR;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.User.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUsersQuery, BaseResponse<IEnumerable<GetUsersResponse>>>,
        IRequestHandler<GetUserByIdQuery, BaseResponse<GetUserByIdResponse>>,
        IRequestHandler<GetUserPaginatedListQuery, PaginatedResult<GetUsersResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<IEnumerable<GetUsersResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var userId=_currentUserService.GetUserId();
            var items = await _unitOfWork.Users.GetAllIncludeRole(userId);
            var res=_mapper.Map<IEnumerable<GetUsersResponse>>(items);
            return Success(res);
        }

        public async Task<PaginatedResult<GetUsersResponse>> Handle(GetUserPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();
            var FilterQuery = _unitOfWork.Users.FilterStudentPaginatedQueryable(currentUserId, request.OrderBy, request.Search);            
            var PaginatedList = await _mapper.ProjectTo<GetUsersResponse>(FilterQuery).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            return PaginatedList;
        }

        public async Task<BaseResponse<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByID(request.Id);
            if (user == null)
                return NotFound<GetUserByIdResponse>();
            var res = _mapper.Map<GetUserByIdResponse>(user);
            return Success(res);
        }
    }
}
