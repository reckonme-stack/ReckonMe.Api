﻿namespace ReckonMe.Api.Cryptography.Abstract
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}
