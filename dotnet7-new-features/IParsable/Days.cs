using System.Diagnostics.CodeAnalysis;

namespace dotnet7_new_features.IParseable
{
    // /WeatherForecast/1-3 to be able to use like that
    // this is like a model binder but a new way to do it.
    // any string coming to an endpoint could be converted as an object just implementing this TryParse method
    public readonly struct Days : IParsable<Days>
    {
        public int From { get; }
        public int To { get; }

        public Days(int from, int to)
        {
            From = from;
            To = to;
        }
      
        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Days result)
        {
            if (s is not null)
            {
                var separator = s.IndexOf("-");
                if (separator > 0 && separator < s.Length - 1)
                {
                    var fromSpan = s.AsSpan().Slice(0, separator);
                    var toSpan = s.AsSpan(separator + 1);

                    if (int.TryParse(fromSpan, System.Globalization.NumberStyles.None, provider, out var from) && int.TryParse(toSpan, System.Globalization.NumberStyles.None, provider, out var to))
                    {
                        result = new Days(from, to);
                        return true;
                    }
                }
            }

            result = default;
            return false;
        }

        public static Days Parse(string s, IFormatProvider? provider)
        {
            if (!TryParse(s, provider, out var result))
            {
                throw new ArgumentException("Could not parse supplied value.", nameof(s));
            }

            return result;
        }

    }
}
