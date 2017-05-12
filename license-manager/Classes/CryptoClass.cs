﻿namespace licensemanager.Classes
{
    public static class CryptoClass
    {
        public static string CreateHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        
        public static bool CheckPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
