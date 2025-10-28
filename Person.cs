using System.Text.RegularExpressions;

namespace PersonApp
{
    public class Person
    {
        public string Förnamn { get; }
        public string Efternamn { get; }
        public string Personnummer { get; }

        public Person(string f, string e, string p)
        {
            Förnamn = f?.Trim() ?? "";
            Efternamn = e?.Trim() ?? "";
            Personnummer = p?.Trim() ?? "";
        }

        private static string Ten(string p)
        {
            var s = Regex.Replace(p ?? "", "[^0-9]", "");
            if (s.Length == 12) s = s.Substring(2);
            return s.Length == 10 ? s : "";
        }

        public bool ÄrGiltigtPnr21()
        {
            var s = Ten(Personnummer);
            if (s == "") return false;
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                int d = s[i] - '0';
                int f = (i % 2 == 0) ? 2 : 1;
                int prod = d * f;
                sum += (prod / 10) + (prod % 10);
            }
            return sum % 10 == 0;
        }

        public string Kön()
        {
            var s = Ten(Personnummer);
            if (s == "") return "Okänt";
            int k = s[8] - '0';
            return (k % 2 == 1) ? "Man" : "Kvinna";
        }
    }
}
