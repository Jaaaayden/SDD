using System.Text.RegularExpressions;

namespace ChatSystem.Utils
{
    public class TextFormatter
    {
        private readonly int maxLength = 200;

        public string FormatText(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Trim excess whitespace
            string formatted = input.Trim();

            // Limit length
            if (formatted.Length > maxLength)
            {
                formatted = formatted.Substring(0, maxLength);
            }

            // Remove multiple spaces
            formatted = Regex.Replace(formatted, @"\s+", " ");

            // Basic HTML tag removal for security
            formatted = Regex.Replace(formatted, "<.*?>", string.Empty);

            return formatted;
        }

        public string ApplyBold(string text)
        {
            return $"<b>{text}</b>";
        }

        public string ApplyItalic(string text)
        {
            return $"<i>{text}</i>";
        }

        public string ApplyColor(string text, string colorHex)
        {
            return $"<color={colorHex}>{text}</color>";
        }
    }
}
