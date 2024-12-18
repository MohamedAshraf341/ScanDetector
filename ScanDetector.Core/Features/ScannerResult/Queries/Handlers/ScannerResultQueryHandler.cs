using ScanDetector.Core.Bases;
using ScanDetector.Infrastructure.Abstracts.Base;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;
using MediatR;
using ScanDetector.Core.Features.ScannerResult.Queries.Models;
using ScanDetector.Core.Features.ScannerResult.Queries.Results;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ScanDetector.Core.Features.ScannerResult.Queries.Handlers
{
    public class ScannerResultQueryHandler : ResponseHandler,
        IRequestHandler<GetScannerResultQuery, BaseResponse<List<GetScannerResponse>>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ScannerResultQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<GetScannerResponse>>> Handle(GetScannerResultQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.ScannerResult.GetTableNoTracking();
            query = query.Include(x => x.User);
            if(request.FilterByUserId.HasValue)
            {
                query=query.Where(x => x.UserId== request.FilterByUserId.Value);
            }
            else if(!string.IsNullOrEmpty(request.FilterByCameraId))
            {
                query = query.Where(x => x.User.CameraId.Contains(request.FilterByCameraId));
            }
            var items= await query.ToListAsync();
            var res = _mapper.Map<List<GetScannerResponse>>(items);
            return Success(res);
        }
    }
}
