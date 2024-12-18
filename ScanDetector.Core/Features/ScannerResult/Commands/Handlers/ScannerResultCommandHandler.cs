using AutoMapper;
using ScanDetector.Core.Bases;
using ScanDetector.Infrastructure.Abstracts.Base;
using ScanDetector.Service.Abstracts;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Resources;
using MediatR;
using ScanDetector.Core.Features.ScannerResult.Commands.Models;
using Microsoft.EntityFrameworkCore;

namespace ScanDetector.Core.Features.ScannerResult.Commands.Handlers
{
    public class ScannerResultCommandHandler : ResponseHandler,
        IRequestHandler<AddScannerResultCommand, BaseResponse<string>>
        

    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ScannerResultCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IUnitOfWork unitOfWork, IMapper mapper):base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<string>> Handle(AddScannerResultCommand request, CancellationToken cancellationToken)
        {
            var userquery = _unitOfWork.Users.GetTableNoTracking();
            var user= await userquery.Where( x=> x.CameraId == request.CameraId ).FirstOrDefaultAsync();
            if (user == null) return NotFound<string>("The user who owns this camera does not exist.");
            var item = _mapper.Map<Data.Entities.ScannerResult>(request);
            item.UserId = user.Id;
            await _unitOfWork.ScannerResult.AddAsync(item);
            _unitOfWork.Complete();

            return Created(string.Empty);
        }
    }
}
