using System.IdentityModel.Tokens.Jwt;

namespace Meepl.Util;

public class HttpUtils
{
    public static ulong ParseIdentityClaim(HttpContext context)
    {
        var token = context.Request.Headers.Authorization.ToString();
        if (token == "") return 0;
        token = token.Split(" ")[1];
        var parsedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var id = parsedToken.Claims.First(claim => claim.Type == "unique_name").Value;
        Console.WriteLine(id);
        Console.WriteLine(ulong.TryParse(id, out ulong ids));
        if (ulong.TryParse(id, out ulong identity)) return identity;
        return 0; //todo dig a logger in right here, this is an error purely on our side, and the only way it wouldnt be is if someone stole the secret and forged a JWT with an invalid ID
    }
}