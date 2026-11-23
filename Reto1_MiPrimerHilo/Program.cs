// Reto 1. Mi primer hilo
// Un hilo secundario muestra los numeros del 1 al 5 con pausa de 500 ms.
// Mientras tanto el hilo principal muestra un mensaje propio.
//
// Uso:  dotnet run -- [sin-join | join-inmediato | join-final]
//   sin-join        -> el hilo principal no espera al secundario
//   join-inmediato  -> Join justo despues de arrancar el hilo
//   join-final      -> Join al final del programa (por defecto)

using System.Diagnostics;

string modo = args.Length > 0 ? args[0] : "join-final";
Console.WriteLine($"=== Modo: {modo} ===");
Console.WriteLine($"[Principal] Id del hilo principal: {Environment.CurrentManagedThreadId}");

var hilo = new Thread(() =>
{
    Console.WriteLine($"[Hilo] Arranco. Mi Id es {Environment.CurrentManagedThreadId}");
    for (int i = 1; i <= 5; i++)
    {
        Console.WriteLine($"[Hilo] Numero {i}");
        Thread.Sleep(500);
    }
    Console.WriteLine("[Hilo] Termino");
});

hilo.Start();

if (modo == "join-inmediato")
{
    hilo.Join(); // El principal se bloquea hasta que el hilo acabe
}

// Trabajo propio del hilo principal
for (int j = 1; j <= 3; j++)
{
    Console.WriteLine($"[Principal] Mensaje propio {j} (Id {Environment.CurrentManagedThreadId})");
    Thread.Sleep(400);
}

if (modo == "join-final")
{
    hilo.Join(); // El principal trabaja y al final espera al hilo
}

Console.WriteLine("[Principal] Fin del programa");
