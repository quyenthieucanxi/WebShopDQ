using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebShopDQ.App.Utils
{
    public static class StringHelper
    {
        public static string RemoveDiacriticsAndConvert(string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr))
                return string.Empty;

            string vietnameseChars = "ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝàáâãèéêìíòóôõùúýĂăĐđĨĩŨũƠơƯưỳừửữỳỹ";
            string englishChars = "AAAAEEEIIOOOOUUYaaaaeeeiioooouuyAaDdIiUuOoUuuuu";

            // Check if the inputStr has any characters
            if (inputStr.All(char.IsWhiteSpace))
            {
                // Log or handle the case where the input string contains only whitespace
                return string.Empty;
            }

            // Tạo một bảng ánh xạ giữa chữ cái Tiếng Việt và Tiếng Anh
            var charMap = new Dictionary<char, char>();
            for (int i = 0; i < vietnameseChars.Length; i++)
            {
                charMap[vietnameseChars[i]] = englishChars[i];
            }

            // Chuyển đổi chuỗi
            char[] chars = inputStr
                .ToCharArray()
                .Select(c => charMap.TryGetValue(c, out var replacement) ? replacement : c)
                .ToArray();

            // Log or handle the case where the chars array is empty
            if (chars.Length == 0)
            {
                // Log or handle the case where the chars array is empty
                return string.Empty;
            }

            // Loại bỏ dấu thanh
            string removedDiacriticsStr = new string(chars).Normalize(NormalizationForm.FormC);

            // Log or print the removedDiacriticsStr for debugging
            Console.WriteLine($"Original: {inputStr}, Modified: {removedDiacriticsStr}");

            return removedDiacriticsStr;
        }
        public static string MakeSlug(string inputStr)
        {
            
            // Chuyển đổi và loại bỏ dấu thanh
            string slug = RemoveDiacriticsAndConvert(inputStr);

            // Chuyển đổi thành chữ thường và thay thế khoảng trắng bằng dấu gạch ngang
            slug = slug.ToLower().Replace(" ", "-");

            return slug;
        }

    }
}
