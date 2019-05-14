using System;

namespace WebStore.Domain.DTO
{
    public class SetLockoutDTO : UserDTO
    {
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}