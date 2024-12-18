﻿using MediatR;
using ScanDetector.Core.Bases;

namespace ScanDetector.Core.Features.UsersRelative.Commands.Models
{
    public class UpdateUsersRelativeCommand:IRequest<BaseResponse<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
