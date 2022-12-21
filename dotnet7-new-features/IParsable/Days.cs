using System.Diagnostics.CodeAnalysis;

namespace dotnet7_new_features.IParseable
{
    // /WeatherForecast/1-3 to be able to use like that
    // this is like a model binder but a new way to do it.
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
            throw new NotImplementedException();
        }

        public static Days Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

    }
}
