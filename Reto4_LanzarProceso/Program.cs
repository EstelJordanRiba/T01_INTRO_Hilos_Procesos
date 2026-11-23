// Reto 4. Lanzar un proceso
// Ejecuta un programa externo (dotnet --version), captura su salida y la muestra.

using System.ComponentModel;
using System.Diagnostics;

Console.WriteLine("=== 1) dotnet --version (todo va bien) ===");
EjecutarExterno("dotnet", "--version");

Console.WriteLine("\n=== 2) Programa que falla (dotnet con un comando inexistente) ===");
EjecutarExterno("dotnet", "comando-que-no-existe");

Console.WriteLine("\n=== 3) Programa que no existe ===");
EjecutarExterno("programa_que_no_existe_xyz", "");

static void EjecutarExterno(string programa, string argumentos)
{
    var info = new ProcessStartInfo
    {
        FileName = programa,
        Arguments = argumentos,
        RedirectStandardOutput = true,   // capturamos lo que escribe en pantalla
        RedirectStandardError = true,
        UseShellExecute = false,         // necesario para poder redirigir
        CreateNoWindow = true
    };

    try
    {
        using var proceso = Process.Start(info)!;
        string salida = proceso.StandardOutput.ReadToEnd();
        string error = proceso.StandardError.ReadToEnd();
        proceso.WaitForExit();            // esperamos a que termine

        Console.WriteLine($"Salida capturada : {salida.Trim().Split('\n')[0]}");
        if (!string.IsNullOrWhiteSpace(error))
            Console.WriteLine($"Error capturado  : {error.Trim().Split('\n')[0]}");
        Console.WriteLine($"ExitCode         : {proceso.ExitCode}");
    }
    catch (Win32Exception ex)
    {
        Console.WriteLine($"No se pudo lanzar '{programa}': {ex.Message}");
    }
}
