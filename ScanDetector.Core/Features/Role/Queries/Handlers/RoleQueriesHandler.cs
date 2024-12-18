

using AutoMapper;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.Role.Queries.Models;
using ScanDetector.Core.Features.Role.Queries.Results;
using ScanDetector.Data.AppMetaData;
using ScanDetector.Infrastructure.Abstracts.Base;
using MediatR;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;

namespace ScanDetector.Core.Features.Role.Queries.Handlers
{
    public class RoleQueriesHandler : ResponseHandler,
        IRequestHandler<GetRolesQuery, BaseResponse<List<GetRolesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public RoleQueriesHandler(IStringLocalizer<SharedResources> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<GetRolesResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.Roles.GetAllAsync();
            //items = items.Where(x => x.Id != RoleData.Admin.Id).ToList();
            var res=_mapper.Map<List<GetRolesResponse>>(items);
            return Success(res);
        }
    }
}
