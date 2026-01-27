using System.Security.Claims;

namespace Gerenciamento_Financeiro.Data
{
    public static class UserContext
    {
        public static int GetUsuarioId(ClaimsPrincipal user)
        {
            return int.Parse(
                user.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );
        }
    }
}
