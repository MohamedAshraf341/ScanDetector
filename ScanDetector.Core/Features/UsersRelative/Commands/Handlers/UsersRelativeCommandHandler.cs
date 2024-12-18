using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.UsersRelative.Commands.Models;
using ScanDetector.Core.Resources;
using ScanDetector.Infrastructure.Abstracts.Base;
using ScanDetector.Service.Abstracts;

namespace ScanDetector.Core.Features.UsersRelative.Commands.Handlers
{
    public class UsersRelativeCommandHandler: ResponseHandler,
        IRequestHandler<AddUsersRelativeCommand, BaseResponse<string>>,
        IRequestHandler<UpdateUsersRelativeCommand, BaseResponse<string>>,
        IRequestHandler<DeleteUsersRelativeCommand, BaseResponse<string>>

    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UsersRelativeCommandHandler(IMapper mapper,IStringLocalizer<SharedResources> stringLocalizer, IUnitOfWork unitOfWork, ICurrentUserService currentUserService):base(stringLocalizer) 
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<string>> Handle(AddUsersRelativeCommand request, CancellationToken cancellationToken)
        {
            var userId=_currentUserService.GetUserId();
            var items = _mapper.Map<List<Data.Entities.UsersRelative>>(request.Data);
            items.ForEach(item => {item.UserId = userId; });
            await _unitOfWork.UsersRelative.AddRangeAsync(items);
            _unitOfWork.Complete();
            return Created(string.Empty);
        }

        public async Task<BaseResponse<string>> Handle(UpdateUsersRelativeCommand request, CancellationToken cancellationToken)
        {
            var item=await _unitOfWork.UsersRelative.GetByIdAsync(request.Id);
            if (item == null) return NotFound<string>();
            _mapper.Map(request, item);
            _unitOfWork.UsersRelative.Update(item);
            _unitOfWork.Complete();
            return Updated(string.Empty);
        }
        public async Task<BaseResponse<string>> Handle(DeleteUsersRelativeCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.UsersRelative.GetByIdAsync(request.Id);
            if (item == null) return NotFound<string>();
            _unitOfWork.UsersRelative.Delete(item);
            _unitOfWork.Complete();
            return Updated(string.Empty);
        }
    }
}
