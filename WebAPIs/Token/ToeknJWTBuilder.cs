using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebAPIs.Token
{
    public class ToeknJWTBuilder
    {
        private SecurityKey securityKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private Dictionary<string,string> claims  = new Dictionary<string,string>();
        private int expiryInMinutes = 5;


        public ToeknJWTBuilder AddSecuriryKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }

        public ToeknJWTBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public ToeknJWTBuilder Addissuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

        public ToeknJWTBuilder Addaudience(string audience)
        {
            this.audience = audience;
            return this;
        }

        public ToeknJWTBuilder AddClaims(string Type, string Value)
        {
            this.claims.Add(Type, Value);
            return this;
        }

        public ToeknJWTBuilder AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }

        private void EnsureArguments()
        {
            if (this.securityKey == null)
                throw new ArgumentNullException("Security Key");
            if (string.IsNullOrEmpty(this.subject))
                throw new ArgumentNullException("Subject");
            if (string.IsNullOrEmpty(this.issuer))
                throw new ArgumentNullException("Issuer");
            if (string.IsNullOrEmpty(this.audience))
                throw new ArgumentNullException("Audience");
        }

        public TokenJWT Builder()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, this.subject),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: this.issuer,
                audience: this.audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: new SigningCredentials(
                                                           this.securityKey,
                                                           SecurityAlgorithms.HmacSha256));

            return new TokenJWT(token);
        }




    }
}
