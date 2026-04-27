# 📦 Sistema de Gestión de Inventario API

Este proyecto es una API RESTful profesional construida con **.NET 8/10**, diseñada para la gestión de productos y categorías. El objetivo de este proyecto es implementar estándares de la industria y buenas prácticas de desarrollo backend.

## 🚀 Tecnologías Utilizadas

* **Backend:** ASP.NET Core Web API
* **Base de Datos:** SQLite (Entity Framework Core)
* **Arquitectura:** Patrón DTO, Middleware de Excepciones, Carga Ansiosa (Eager Loading).
* **Documentación:** Swagger (OpenAPI)

## 🛠️ Características Principales

- **Validación Estricta:** Uso de *Data Annotations* en DTOs para asegurar la integridad de los datos.
- **Relaciones Robustas:** Manejo de relaciones 1:N entre Categorías y Productos con *Entity Framework*.
- **Middleware Global:** Manejo centralizado de errores para respuestas consistentes y profesionales.
- **Seguridad:** Protección contra datos malformados y errores no controlados.

## 📁 Estructura del Proyecto

```text
/
├── Controllers/      # Controladores de la API (Endpoints)
├── DTOs/             # Objetos de transferencia de datos (Modelos de entrada)
├── Models/           # Entidades de la Base de Datos
├── Middleware/       # Manejo global de excepciones
└── Program.cs        # Configuración principal
