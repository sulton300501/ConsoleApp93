using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        string username = "sulton";
        string password = "user@123";
        SavePassword(username, password);
    }

    static byte[] GenerateSalt()
    {
      
        using (var rng = new RNGCryptoServiceProvider())
        {
            var salt = new byte[16];
            rng.GetBytes(salt);
            return salt;
        }
    }

    static string HashPassword(string password, byte[] salt)
    {
       
        using (var sha256 = SHA256.Create())
        {
            var passwordSalt = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();
            var hashedPassword = sha256.ComputeHash(passwordSalt);
            return BitConverter.ToString(hashedPassword).Replace("-", "").ToLower();
        }
    }

    static void SavePassword(string username, string password)
    {
      
        var salt = GenerateSalt();

      
        var hashedPassword = HashPassword(password, salt);
        SaveToDatabase(username, hashedPassword, salt);
    }

    static void SaveToDatabase(string username, string hashedPassword, byte[] salt)
    {
      
        Console.WriteLine($"Username: {username}, Hashed Password: {hashedPassword}, Salt: {BitConverter.ToString(salt)} saqlangan");
    }
}
