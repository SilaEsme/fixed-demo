﻿namespace FixedDemo.Shared.Dtos.User
{
    public class RegisterRequestDto
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
