namespace SocialNetwork.Security.Internal
{
    public class AuthorizationResult
    {
        private AuthorizationResult()
        {
            
        }
        
        public bool Succeeded { get; private set; }
        
        public static AuthorizationResult Success => new AuthorizationResult{ Succeeded =  true};
        public static AuthorizationResult Failed => new AuthorizationResult{ Succeeded =  false};
    }
}