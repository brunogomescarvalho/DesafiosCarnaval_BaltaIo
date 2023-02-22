﻿bool continuar = true;

while (continuar)
{
    Console.Clear();
    System.Console.WriteLine("Digite o número que gostaria de escrever por extenso ou digite sair");
    var numeroParaConverter = Convert.ToInt64(Console.ReadLine());

    var nrPorExtenso = NumerosPorExtenso.RecebeNrInformado(numeroParaConverter);
    System.Console.WriteLine($"\n\n{nrPorExtenso}\n\n");

    System.Console.WriteLine("Deseja continuar? [s/n] ");
    var resposta = Console.ReadLine()!;
    if (resposta.ToLower() == "n")
    {
        continuar = false;
    }
}