using Angus.ISoft.Boilerplate.Infrastructure;
using Angus.ISoft.Boilerplate.WebApi.Core;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

namespace Angus.ISoft.Boilerplate.WebApi
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = AppSettings.GetValue("issuer");
            var secret = TextEncodings.Base64Url.Decode(Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(AppSettings.GetValue("secret"))));

            //用jwt进行身份认证
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[] {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                }
            });

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                //生产环境设为false
                AllowInsecureHttp = true,
                //请求token的路径
                TokenEndpointPath = new PathString("/oauth2/token"), //username:admin password:admin grant_type:password
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),
                //请求获取token时，验证username, password
                Provider = new AuthorizationServerProvider(),
                //定义token信息格式 
                AccessTokenFormat = new JwtTokenFormat(issuer, secret),
            });
        }
    }
}