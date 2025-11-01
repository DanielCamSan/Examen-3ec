dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet ef migrations add InitSqlite
dotnet ef database update

# TecWebFest — Examen (90 minutos)

**Objetivo:** Evaluar dominio de relaciones 1:N, N:M y 1:1 en EF Core y el manejo de arquitectura por capas (Controller ➜ Service ➜ Repository ➜ DbContext) implementando endpoints clave para un mini‑sistema de festival de música.

## Contexto del dominio
    Una Conference tiene muchas Rooms (1:N).

    Un Speaker puede dar charlas en muchas Rooms, y una Room recibe muchos Speakers a través de Talk, con horario (N:M con payload StartTime/EndTime).


## Lo que ya tienes listo

Este starter incluye la solución con:
- Capas: `Controllers`, `Services`, `Repositories`, `Data (DbContext)`, `Entities`, `DTOs`.
- DI configurado en `Program.cs` y proveedor **InMemory** para rapidez en aula.
- Repos genéricos y específicos con métodos base y consultas incluídas (`Include`).

## Criterios de evaluación (100 pts)

    Create Conference → crea una conferencia con 2 salas. 15pts

    Create Speaker y Create Another Speaker → añade ponentes. 15pts

    Add Talk → crea charla para el primer speaker. 10pts

    Add Another Talk → otra charla.  10pts

    Get Conference Agenda → ver la agenda completa. 25pts

    Get Speaker Schedule → ver las charlas de un ponente. 25pts

## Notas
- El **foco** es demostrar dominio de **relaciones** y **capas** en 90 minutos.

