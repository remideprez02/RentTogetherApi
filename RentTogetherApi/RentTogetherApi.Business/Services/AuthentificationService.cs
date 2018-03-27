using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RentTogetherApi.Interfaces;
using RentTogetherApi.Interfaces.Business;

public class AuthentificationService : IAuthentificationService{


    public AuthentificationService() {
        
    }

    //public void RequestToken(){
    //    var claims = new[]
    //    {
    //        new Claim(ClaimTypes.Name, request.Username)
    //    };

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var token = new JwtSecurityToken(
    //        issuer: "yourdomain.com",
    //        audience: "yourdomain.com",
    //        claims: claims,
    //        expires: DateTime.Now.AddMinutes(30),
    //        signingCredentials: creds);;
    //}


}