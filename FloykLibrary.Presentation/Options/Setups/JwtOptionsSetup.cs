using FloykLibrary.Presentation.Options.Models;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace FloykLibrary.Presentation.Options.Setups
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            var section = _configuration.GetSection("Jwt")
                                ?? throw new KeyNotFoundException("Can't read jwt from appsettings.json");

            options.Issuer = section["Issuer"]!;
            options.Audience = section["Audience"]!;
            options.SecretKey = section["SecretKey"]!;
        }
    }
}
