namespace OperatorMethod
{
    public class LicensePlateA
    {
        public string CardNumber { get; set; }

        public LicensePlateA(string number)
        {
            CardNumber = number;
        }

        public static bool operator ==(LicensePlateA a, LicensePlateA b)
        {
            return (a.CardNumber == b.CardNumber);
        }

        public static bool operator !=(LicensePlateA a, LicensePlateA b)
        {
            return (a.CardNumber != b.CardNumber);
        }
    }
}
