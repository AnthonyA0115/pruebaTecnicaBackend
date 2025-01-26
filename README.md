# Proyecto Backend y Frontend - Gestión de Usuarios y Tareas
📋 Instrucciones para instalar y ejecutar la aplicación
1. Requisitos previos
Asegúrate de tener instalados los siguientes componentes en tu máquina:

Visual Studio 2019 o superior (compatible con .NET Framework 4.5)
Dado que el criterio en esta prueba era utilizar una base de datos relacional se implemento Mysql (XAMP)

Configuración del Backend

Abre el archivo del proyecto backend (.sln) en Visual Studio.
Configura la conexión a la base de datos en el archivo Web.config (opcional) dependiendo de las credenciales de la bd.
Importar la bd dejada en archivo adicional en el proyecto o ejecutar las migraciones con los comandos siguientes aunque dicha importacion no traera datos de pruebas. (consola del Administrador de Paquetes NuGet )
-Enable-Migrations
-Add-Migration InitialCreate
-Update-Database

Ejecuta el backend desde Visual Studio presionando Ctrl+F5. El servidor escuchará en http://localhost:5000 (puerto configurable en el archivo Web.config).

Documentación del sistema
Arquitectura del Sistema
Backend: Construido con ASP.NET Framework 4.5, sigue un diseño basado en controladores (ApiController) que exponen endpoints RESTful. Utiliza Entity Framework para manejar la persistencia en la base de datos SQL Server.
Frontend: Implementado con Angular, utiliza servicios para interactuar con el backend. Se sigue una arquitectura modular con separación de responsabilidades entre componentes, servicios y modelos.
Base de datos: SQL Server con un modelo relacional que incluye las tablas Usuarios y Tareas conectadas por una relación 1:N.

Justificación de decisiones técnicas

ASP.NET Framework 4.5: Utilizado por ser compatible con los requisitos del proyecto y por su facilidad para integrar con SQL Server, además de estar alineado con el entorno corporativo.
Angular: Elegido para el frontend por su robustez, modularidad y capacidad de construir aplicaciones SPA que ofrecen una experiencia fluida al usuario.
Entity Framework: Adoptado para simplificar las operaciones CRUD y acelerar el desarrollo mediante mapeo objeto-relacional (ORM).
XAMP: Utilizado por su fiabilidad, capacidad para manejar datos relacionales y compatibilidad nativa con el backend.
Relación Usuario-Tarea: Implementada con una relación 1:N para representar adecuadamente el caso de uso de que un usuario pueda gestionar múltiples tareas.

Un punto que no se pudo tomar en cuenta en el desarrollo de la prueba fue el punto 3 de tareas automaticas (cron job) puesto la version de visual studio 2019 requerida para poder dar solucion a los puntos anteriores me trajo conflicto en cuanto a la descarga y instalacion del sdk del .net 6.0 el cual trae la opcion Aplicación web ASP.NET Core al dar creacion de un proyecto. dicha solucion de un microservicio aparte solo se podria facilitar actualizando visual studio a una version posterior pero dado que la version de 4.5 framework ya no tiene soporte en esa version no podria a ejecutar dicho proyecto el cual era el principal.
aun asiendo no caso omiso a ello al siguiente detallo los pasos para la creacion del cron job y su respectiva configuracion.

Implementar una Tarea en Segundo Plano (CronJob)
En la carpeta Services, crea un archivo TareasCronJob.cs:

using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class TareasCronJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public TareasCronJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var fechaActual = DateTime.Now;
            foreach (var tarea in tareas)
            {
                if (tarea.Estado == "pendiente" && tarea.FechaLimite < fechaActual)
                {
                    tarea.Estado = "atrasada";
                }
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Ejecutar cada hora
        }
    }
}
Registrar el Servicio: En Program.cs, añade el servicio:
builder.Services.AddHostedService<TareasCronJob>();

esta seria la logica planteada para crear un servicio que se pueda ejecutar con un cron job de manera configurable para asi no tener que consumir recursos en el host y que el proceso de consulta sea mas fluido. este metodo lo implemente en la version actual de .net por siguiente no se como se comporte en versiones inferiores.


