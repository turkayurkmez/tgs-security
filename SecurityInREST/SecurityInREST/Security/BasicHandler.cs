using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace SecurityInREST.Security
{
    public class BasicHandler : AuthenticationHandler<BasicOption>
    {
        public BasicHandler(IOptionsMonitor<BasicOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            /*
             *   $.ajax({
                    
                    headers:{
                        'Authorization':'Basic '+btoa('turkay:123')
                    },
                    url: 'https://localhost:7171/WeatherForecast',
                    type: 'GET',
                    success: function (data) {
                        console.log(data);
                    },
                    error: function (err) {
                        console.error(err)
                    }


                });

             * 1. HttpHeader içerisinde Authorization var mı?
             * 2. Doğru formatta mı?
             * 3. Scheme Basic mi?
             * 4. Scheme Parametresini ayır
             * 5. Parametreyi [:] işaretine göre ayır
             * 6. İlk eleman kullanıcı adı diğeri şifredir.
             * 7. Veritabanında denetle
             * 8. Bilet oluştur
             * 
             * 
             */

            //1.HttpHeader içerisinde Authorization var mı?
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //2. Doğru formatta mı?
            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"],out AuthenticationHeaderValue headerValue))
            {
                return Task.FromResult(AuthenticateResult.NoResult());

            }

            //3. Scheme Basic mi?
            if (!headerValue.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.NoResult());

            }


            // 4. Scheme Parametresini ayır
            var bytes = Convert.FromBase64String(headerValue.Parameter);
            var userNameAndPass = Encoding.UTF8.GetString(bytes);

            var userName = userNameAndPass.Split(':')[0];
            var password = userNameAndPass.Split(':')[1];

            if (userName == "test" && password=="123")
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,"test")
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                AuthenticationTicket ticket = new AuthenticationTicket(principal,Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            return Task.FromResult(AuthenticateResult.Fail("Kullanıcı veya şifre hatalı!"));


        }
    }
}
