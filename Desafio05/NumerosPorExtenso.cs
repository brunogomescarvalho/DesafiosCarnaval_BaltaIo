

public static class NumerosPorExtenso
{
    static readonly string[] unidade = new string[] { "", "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove" };
    static readonly string[] dezena = new string[] { "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezesseis", "Dezessete", "Dezoito", "Dezenove" };
    static readonly string[] dezenas = new string[] { "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
    static readonly string[] centenas = new string[] { "Cem", "Duzentos", "Trezentos", "Quatrocentos", "Quinhentos", "Seiscentos", "Setecentos", "Oitocentos", "Novecentos" };
    static readonly string[] milhares = new string[] { "", " Mil", " Milhão", " Bilhão" };

    private static string TransformaEmExtenso(long n, string nrPorExtenso, int milhar)
    {

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
            var centenaInteira = NumerosPorExtenso.verificaCentena((n));
            nrPorExtenso += TransformaEmExtenso(n % 1000, n % 100 == 0 ? TransformaEmExtenso(n / 1000, " ", milhar + 1) + (centenaInteira ? "" : " e") :
            TransformaEmExtenso(n / 1000, "", milhar + 1), 0);
        }

        return nrPorExtenso + milhares[milhar];
    }

    public static string ConverteNumeroInformado(long n)
    {
        if (n > 999999999999)
            throw new Exception("Número não suportado");

        if (n == 0)
            return "Zero";

        else if (n < 0)
            throw new Exception("Operação não suportada");

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
    public static string RecebeNumeroERetornaString(string numeroParaConverter)
    {
        try
        {
            string[] nr = numeroParaConverter.Split(",");
            long reais = Convert.ToInt64(nr[0]);
            long centavos = Convert.ToInt64(nr[1]);
            if (centavos > 99)
                return "Valor informado inválido";

            var reaisPorExtenso = NumerosPorExtenso.ConverteNumeroInformado(reais);
            var centavosPorExtenso = NumerosPorExtenso.ConverteNumeroInformado(centavos);
            return reais > 0 ? $"\n\n{reaisPorExtenso} {(reais > 1 ? "Reais" : "Real")} {(centavos > 0 ? "e" + centavosPorExtenso + " Centavos" + "\n\n" : "\n\n")}" : centavosPorExtenso + " Centavos" + "\n\n";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}