using ScanDetector.Core.Bases;
using ScanDetector.Infrastructure.Abstracts.Base;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;
using MediatR;
using ScanDetector.Core.Features.UsersRelative.Queries.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ScanDetector.Core.Features.ScannerResult.Queries.Results;

namespace ScanDetector.Core.Features.UsersRelative.Queries.Handlers
{
    public class UsersRelativeQueryHandler : ResponseHandler,
        IRequestHandler<GetUsersRelativeQuery, BaseResponse<List<GetScannerResponse>>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UsersRelativeQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<GetScannerResponse>>> Handle(GetUsersRelativeQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.UsersRelative.GetTableNoTracking();
            query = query.Where(x => x.UserId==request.FilterByUserId);
            var items= await query.ToListAsync();
            var res = _mapper.Map<List<GetScannerResponse>>(items);
            return Success(res);
        }
    }
}
