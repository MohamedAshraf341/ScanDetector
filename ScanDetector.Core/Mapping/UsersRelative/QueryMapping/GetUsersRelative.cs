using AutoMapper;
using ScanDetector.Core.Features.UsersRelative.Queries.Results;

namespace ScanDetector.Core.Mapping.UsersRelative
{
    public partial class UsersRelativeProfile
    {
        public void GetUsersRelativeMapping()
        {
            CreateMap<Data.Entities.UsersRelative, GetUsersRelativeResponse>();
        }
    }
}
