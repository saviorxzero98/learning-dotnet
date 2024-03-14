using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim("Token", Guid.NewGuid().ToString())
            };

            
            var key = GetTokenSecretKey("123456");
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("2efe66058eb24e079f488f6f49af281b"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            Console.WriteLine(jwt);
        }

        static SymmetricSecurityKey GetTokenSecretKey(string tokenSecret)
        {
            // 將 Token Secret 補足到 256 bits
            byte[] secretBytes = Encoding.UTF8.GetBytes(tokenSecret);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(secretBytes);

                // 建立 Secret Key
                var key = new SymmetricSecurityKey(hashValue);
                return key;
            }
        }
    }
}