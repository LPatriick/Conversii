using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("b1");
        int b1 = int.Parse(Console.ReadLine());
        Console.WriteLine("b2");
        int b2 = int.Parse(Console.ReadLine());
        if (b1 < 2 || b1 > 16 || b2 < 2 || b2 > 16)
        {
            Console.WriteLine("baze intre 2 si 16");
            return;
        }
        Console.WriteLine("scrie un nr");
        string nrInB1 = Console.ReadLine();
        try
        {
            double nrInB10 = ConvertToBase10(nrInB1, b1);
            string nrInB2 = ConvertFromBase10(nrInB10, b2);
            Console.WriteLine($"in b2 este: {nrInB2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
    }
    static double ConvertToBase10(string nr, int baza)
    {
        string[] parts = nr.Split('.');
        int parteaIntreaga = 0;
        string parteaIntreagaStr = parts[0];
        for (int i = 0; i < parteaIntreagaStr.Length; i++)
        {
            int valoareCifra = GetValue(parteaIntreagaStr[i]);
            if (valoareCifra >= baza)
                throw new ArgumentException($"Cifra {parteaIntreagaStr[i]} nu este valida in baza {baza}.");

            parteaIntreaga = parteaIntreaga * baza + valoareCifra;
        }
        double parteaFractionara = 0;
        if (parts.Length > 1)
        {
            string fractiune = parts[1];
            for (int i = 0; i < fractiune.Length; i++)
            {
                int valoareCifra = GetValue(fractiune[i]);
                if (valoareCifra >= baza)
                    throw new ArgumentException($"Cifra {fractiune[i]} nu este valida in baza {baza}.");
                parteaFractionara += valoareCifra / Math.Pow(baza, i + 1);
            }
        }
        return parteaIntreaga + parteaFractionara;
    }
    static string ConvertFromBase10(double nr, int baza)
    {
        int parteaIntreaga = (int)Math.Floor(nr);
        string rezultatIntreg = "";
        while (parteaIntreaga > 0)
        {
            int rest = parteaIntreaga % baza;
            rezultatIntreg = GetChar(rest) + rezultatIntreg;
            parteaIntreaga /= baza;
        }
        if (rezultatIntreg == "") rezultatIntreg = "0";
        double fractiune = nr - Math.Floor(nr);
        string resultFr = "";
        int maxCifre = 10;
        while (fractiune > 0 && resultFr.Length < maxCifre)
        {
            fractiune *= baza;
            int cifra = (int)Math.Floor(fractiune);
            fractiune -= cifra;

            resultFr += GetChar(cifra);
        }
        return resultFr.Length > 0 ? $"{rezultatIntreg}.{resultFr}" : rezultatIntreg;
    }

    static int GetValue(char c)
    {
        if (c >= '0' && c <= '9') return c - '0';
        if (c >= 'A' && c <= 'F') return c - 'A' + 10;
        throw new ArgumentException($"Caracter invalid: {c}");
    }
    static char GetChar(int val)
    {
        if (val >= 0 && val <= 9) return (char)('0' + val);
        if (val >= 10 && val <= 15) return (char)('A' + val - 10);
        throw new ArgumentException($"Valoare invalida: {val}");
    }
}