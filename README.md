# T01 - Introducción al desarrollo con hilos y procesos

Actividad AEA2 · Programació de serveis i processos (DAM2) · ILERNA

Cuatro programas de consola en C# (.NET 8) con hilos, condiciones de carrera, tareas y procesos externos.

| Carpeta | Contenido |
|---|---|
| `Reto1_MiPrimerHilo` | `Thread`, `Join` y `Environment.CurrentManagedThreadId` |
| `Reto2_CondicionCarrera` | Variable compartida sin protección, con `lock` y con `Interlocked.Increment` |
| `Reto3_TareasYTiempo` | `Task.Delay`, `Task.WhenAll` y `Stopwatch` |
| `Reto4_LanzarProceso` | `Process`, captura de salida, `WaitForExit` y `ExitCode` |
| `resultados/` | Salidas reales de las ejecuciones |

## Ejecución

```bash
cd Reto1_MiPrimerHilo
dotnet run -- join-final      # también: sin-join | join-inmediato

cd ../Reto2_CondicionCarrera
dotnet run -- 100000          # o 1000000

cd ../Reto3_TareasYTiempo && dotnet run
cd ../Reto4_LanzarProceso && dotnet run
```
