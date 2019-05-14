using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.DTO
{
    public class AddLoginDTO : UserDTO
    {
        public UserLoginInfo UserLoginInfo { get; set; }
    }
}