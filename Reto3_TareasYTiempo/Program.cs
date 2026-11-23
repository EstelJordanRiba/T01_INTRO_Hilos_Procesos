// Reto 3. Tareas y tiempo
// Tres "descargas" simuladas con Task.Delay de 1, 2 y 3 segundos.

using System.Diagnostics;

Console.WriteLine("=== A) Una detras de otra (secuencial) ===");
var sw = Stopwatch.StartNew();
await Descargar("Archivo 1", 1000);
await Descargar("Archivo 2", 2000);
await Descargar("Archivo 3", 3000);
sw.Stop();
Console.WriteLine($"Tiempo total secuencial: {sw.Elapsed.TotalSeconds:F2} s\n");

Console.WriteLine("=== B) A la vez con Task.WhenAll ===");
sw.Restart();
await Task.WhenAll(
    Descargar("Archivo 1", 1000),
    Descargar("Archivo 2", 2000),
    Descargar("Archivo 3", 3000));
sw.Stop();
Console.WriteLine($"Tiempo total con WhenAll: {sw.Elapsed.TotalSeconds:F2} s\n");

Console.WriteLine("=== C) Pregunta: Thread.Sleep en lugar de Task.Delay (WhenAll) ===");
sw.Restart();
await Task.WhenAll(
    DescargarBloqueando("Archivo 1", 1000),
    DescargarBloqueando("Archivo 2", 2000),
    DescargarBloqueando("Archivo 3", 3000));
sw.Stop();
Console.WriteLine($"Tiempo total con Thread.Sleep: {sw.Elapsed.TotalSeconds:F2} s");
Console.WriteLine("(Thread.Sleep bloquea el hilo que llama, asi que las 'descargas' se acaban ejecutando una tras otra.)");

static async Task Descargar(string nombre, int ms)
{
    Console.WriteLine($"  Inicio {nombre} (hilo {Environment.CurrentManagedThreadId})");
    await Task.Delay(ms);
    Console.WriteLine($"  Fin    {nombre}");
}

// Version "mala": Thread.Sleep dentro de un metodo async sin ceder el hilo
static Task DescargarBloqueando(string nombre, int ms)
{
    Console.WriteLine($"  Inicio {nombre} (hilo {Environment.CurrentManagedThreadId})");
    Thread.Sleep(ms);
    Console.WriteLine($"  Fin    {nombre}");
    return Task.CompletedTask;
}
