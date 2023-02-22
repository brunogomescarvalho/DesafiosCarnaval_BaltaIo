

public static class NumerosPorExtenso
{
    static readonly string[] unidade = new string[] { "", "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove" };
    static readonly string[] dezena = new string[] { "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezesseis", "Dezessete", "Dezoito", "Dezenove" };
    static readonly string[] dezenas = new string[] { "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
    static readonly string[] centenas = new string[] { "Cem", "Duzentos", "Trezentos", "Quatrocentos", "Quinhentos", "Seiscentos", "Setecentos", "Oitocentos", "Novecentos" };
    static readonly string[] milhares = new string[] { "", " Mil", " Milhão", " Bilhão" };

    private static string TransformaEmExtenso(long n, string concatenacao, int milhar)
    {
        if (n == 0)
        {
            return concatenacao;
        }

        string nrPorExtenso = concatenacao;

        if (nrPorExtenso.Length > 0)
        {
            nrPorExtenso += " ";
        }

        if (n < 10)
        {
            nrPorExtenso += unidade[n];
        }
        else if (n < 20)
        {
            nrPorExtenso += dezena[n - 10];
        }
        else if (n < 100)
        {
            nrPorExtenso += TransformaEmExtenso(n % 10, n % 10 > 0 ? dezenas[n / 10 - 2] + " e" : dezenas[n / 10 - 2], 0);
        }
        else if (n < 1000)
        {
            nrPorExtenso += n < 200 ? TransformaEmExtenso(n % 100, n % 100 > 0 ? ("Cento e") : (centenas[(n / 100) - 1]), 0) :
            TransformaEmExtenso(n % 100, n % 100 > 0 ? ((centenas[(n / 100) - 1] + " e")) : (centenas[(n / 100) - 1]), 0);
        }
        else
        {
            var verificaCentena = NumerosPorExtenso.verificaCentena((n));
        
            nrPorExtenso += TransformaEmExtenso(n % 1000, n % 100 == 0 ? TransformaEmExtenso(n / 1000, " ", milhar + 1) + (verificaCentena ? " " : " e") :
            TransformaEmExtenso(n / 1000, " ", milhar + 1), 0);

            if (n % 1000 == 0)
            {
                return nrPorExtenso;
            }
        }

        return nrPorExtenso + milhares[milhar];
    }

    public static string RecebeNrInformado(long n)
    {
        if (n == 0)
        {
            return "Zero";
        }
        else if (n < 0)
        {
            return RecebeNrInformado(-n) + "Negativos";
        }

        return TransformaEmExtenso(n, " ", 0);
    }

    public static bool verificaCentena(long valor)
    {

        double db = Convert.ToDouble(valor);
        double _db = db / 1000;

        if (Int64.TryParse(_db.ToString(), out valor))
            return true;

        return false;
    }
}