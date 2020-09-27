using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barter.Impl
{
    public class TranslateService : ITranslateService
    {
        /// <summary>
        /// для неправильной раскладки клавиатуры
        /// </summary>
        public static readonly IReadOnlyCollection<Tuple<char, char>> KeyboardPairs = new[]
        {
            new Tuple<char, char>('q', 'й'),
            new Tuple<char, char>('w', 'ц'),
            new Tuple<char, char>('e', 'у'),
            new Tuple<char, char>('r', 'к'),
            new Tuple<char, char>('t', 'е'),
            new Tuple<char, char>('y', 'н'),
            new Tuple<char, char>('u', 'г'),
            new Tuple<char, char>('i', 'ш'),
            new Tuple<char, char>('o', 'щ'),
            new Tuple<char, char>('p', 'з'),
            new Tuple<char, char>('[', 'х'),
            new Tuple<char, char>(']', 'ъ'),
            new Tuple<char, char>('a', 'ф'),
            new Tuple<char, char>('s', 'ы'),
            new Tuple<char, char>('d', 'в'),
            new Tuple<char, char>('f', 'а'),
            new Tuple<char, char>('g', 'п'),
            new Tuple<char, char>('h', 'р'),
            new Tuple<char, char>('j', 'о'),
            new Tuple<char, char>('k', 'л'),
            new Tuple<char, char>('l', 'д'),
            new Tuple<char, char>(';', 'ж'),
            new Tuple<char, char>('\'', 'э'),
            new Tuple<char, char>('z', 'я'),
            new Tuple<char, char>('x', 'ч'),
            new Tuple<char, char>('c', 'с'),
            new Tuple<char, char>('v', 'м'),
            new Tuple<char, char>('b', 'и'),
            new Tuple<char, char>('n', 'т'),
            new Tuple<char, char>('m', 'ь'),
            new Tuple<char, char>(',', 'б'),
            new Tuple<char, char>('.', 'ю'),

            new Tuple<char, char>('{', 'Х'),
            new Tuple<char, char>('}', 'Ъ'),
            new Tuple<char, char>('"', 'Э'),
            new Tuple<char, char>(':', 'Ж'),
            new Tuple<char, char>('<', 'Б'),
            new Tuple<char, char>('>', 'Ю'),

            new Tuple<char, char>('#', '№'),
        };

        /// <inheritdoc />
        public string ToAnotherKeyboardLayout(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            var sb = new StringBuilder(value.Length);
            foreach (var ch in value)
                sb.Append(ToAnotherKeyboardLayout(ch));
            return sb.ToString();
        }

        private static char ToAnotherKeyboardLayout(char ch)
        {
            var c = char.ToLowerInvariant(ch);

            var t = KeyboardPairs.FirstOrDefault(p => p.Item1 == c || p.Item2 == c);
            if (t == null)
                return ch;

            c = t.Item1 == c ? t.Item2 : t.Item1;

            if (char.IsUpper(ch))
                c = char.ToUpper(c);

            return c;
        }
    }
}
