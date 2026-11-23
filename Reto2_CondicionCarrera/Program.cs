// Reto 2. Condicion de carrera
// Dos hilos incrementan 100.000 veces (o las que se indiquen) una variable compartida.
//
// Uso:  dotnet run -- [iteraciones]      (por defecto 100000)

int iteraciones = args.Length > 0 ? int.Parse(args[0]) : 100_000;
int ejecuciones = 5;
long esperado = 2L * iteraciones;

Console.WriteLine($"Iteraciones por hilo: {iteraciones:N0} | Resultado esperado: {esperado:N0}");
Console.WriteLine();

Ejecutar("SIN proteccion", SinProteccion);
Ejecutar("Con lock", ConLock);
Ejecutar("Con Interlocked.Increment", ConInterlocked);

void Ejecutar(string nombre, Func<long> prueba)
{
    Console.WriteLine($"--- {nombre} ---");
    int correctas = 0;
    var cronometro = System.Diagnostics.Stopwatch.StartNew();
    for (int i = 1; i <= ejecuciones; i++)
    {
        long r = prueba();
        bool ok = r == esperado;
        if (ok) correctas++;
        Console.WriteLine($"Ejecucion {i}: {r:N0} {(ok ? "(OK)" : $"(MAL, faltan {esperado - r:N0})")}");
    }
    cronometro.Stop();
    Console.WriteLine($"Correctas: {correctas}/{ejecuciones} | Tiempo de las {ejecuciones} ejecuciones: {cronometro.ElapsedMilliseconds} ms");
    Console.WriteLine();
}

long SinProteccion()
{
    int contador = 0;
    var h1 = new Thread(() => { for (int i = 0; i < iteraciones; i++) contador++; });
    var h2 = new Thread(() => { for (int i = 0; i < iteraciones; i++) contador++; });
    h1.Start(); h2.Start();
    h1.Join(); h2.Join();
    return contador;
}

long ConLock()
{
    int contador = 0;
    object cerrojo = new();
    var h1 = new Thread(() => { for (int i = 0; i < iteraciones; i++) lock (cerrojo) { contador++; } });
    var h2 = new Thread(() => { for (int i = 0; i < iteraciones; i++) lock (cerrojo) { contador++; } });
    h1.Start(); h2.Start();
    h1.Join(); h2.Join();
    return contador;
}

long ConInterlocked()
{
    int contador = 0;
    var h1 = new Thread(() => { for (int i = 0; i < iteraciones; i++) Interlocked.Increment(ref contador); });
    var h2 = new Thread(() => { for (int i = 0; i < iteraciones; i++) Interlocked.Increment(ref contador); });
    h1.Start(); h2.Start();
    h1.Join(); h2.Join();
    return contador;
}
