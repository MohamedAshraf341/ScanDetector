namespace ScanDetector.Core.Middleware
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : Attribute
    {
        public string[] Permissions { get; }

        public CustomAuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }
    }
}
