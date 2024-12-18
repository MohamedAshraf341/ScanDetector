using AutoMapper;
namespace ScanDetector.Core.Mapping.User
{
    public partial class UserProfile : Profile
    {
        public UserProfile() 
        {
            AddUserMapping();
            GetUsersMapping();
            UpdateUserMapping();
            SimpleAddUserMapping();
            GetUserByIdMapping();
        }
    }
}
