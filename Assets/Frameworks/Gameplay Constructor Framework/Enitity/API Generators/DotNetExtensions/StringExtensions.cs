namespace GameplayConstructorFramework.APIs.DotNetExtensions
{
    public static class StringExtensions
    {
        public static string FromPascalToCamelCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            
            return char.ToLower(input[0]) + input[1..];
        }
    }
}