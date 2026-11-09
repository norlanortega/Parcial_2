# 🔐 Login de Usuario – Programación en Capas

Aplicación de escritorio en **C# (Windows Forms)** que implementa un sistema de inicio de sesión usando **arquitectura en capas** y una base de datos **SQL Server**. Las contraseñas se almacenan cifradas con **SHA-512**.

> Laboratorio de la materia de Programación – Universidad Santa María La Antigua (USMA).

<p align="center">
  <img src="docs/img/login.png" alt="Pantalla de login" width="420">
</p>

---

## 📋 Tabla de contenido
- [Arquitectura](#-arquitectura)
- [Tecnologías](#-tecnologías)
- [Estructura del proyecto](#-estructura-del-proyecto)
- [Base de datos](#-base-de-datos)
- [Instalación y ejecución](#-instalación-y-ejecución)
- [Capturas](#-capturas)
- [Autor](#-autor)

---

## 🏗 Arquitectura

La solución está dividida en **4 proyectos (capas)**, cada uno con una responsabilidad única:

```
┌──────────────────┐
│   Presentación   │  Formularios Windows Forms (lo que ve el usuario)
└────────┬─────────┘
         │
┌────────▼─────────┐
│     Negocio      │  Intermediario entre la interfaz y los datos
└────────┬─────────┘
         │
┌────────▼─────────┐
│      Datos       │  Conexión y consultas a SQL Server
└────────┬─────────┘
         │
┌────────▼─────────┐
│   SQL Server     │  Tabla usuarios + procedimiento almacenado
└──────────────────┘

        Entidades  →  Clase E_Usuario compartida por todas las capas
```

| Capa | Proyecto | Clases | Referencia a |
|---|---|---|---|
| Entidades | Biblioteca de clases (.NET Framework) | `E_Usuario` | — |
| Datos | Biblioteca de clases (.NET Framework) | `D_Conexion`, `D_Usuario` | Entidades |
| Negocio | Biblioteca de clases (.NET Framework) | `N_Usuario` | Datos, Entidades |
| Presentación | Aplicación de Windows Forms (.NET Framework) | `frmLogin`, `frmPrincipal` | Negocio, Entidades, Datos |

<p align="center">
  <img src="docs/img/solucion.png" alt="Explorador de soluciones con las 4 capas" width="320">
</p>

---

## 🛠 Tecnologías

- **C#** – .NET Framework 4.7.2
- **Windows Forms**
- **SQL Server** + SQL Server Management Studio
- **ADO.NET** (`System.Data.SqlClient`)
- **Visual Studio 2022**

---

## 📁 Estructura del proyecto

```
SolucionCapas/
├── Entidades/
│   └── E_Usuario.cs
├── Datos/
│   ├── D_Conexion.cs
│   └── D_Usuario.cs
├── Negocio/
│   └── N_Usuario.cs
├── Presentacion/
│   ├── frmLogin.cs
│   ├── frmPrincipal.cs
│   └── Program.cs
├── docs/
│   └── img/              ← capturas usadas en este README
└── SolucionCapas.sln
```

---

## 🗄 Base de datos

Ejecuta el siguiente script en **SQL Server Management Studio**:

```sql
CREATE DATABASE db_sistema_login;
GO
USE db_sistema_login;
GO

CREATE TABLE usuarios (
  id INT IDENTITY(1,1) PRIMARY KEY,
  dsId VARCHAR(50) NOT NULL,
  dsUsuario VARCHAR(50) NOT NULL,
  dsPassword VARBINARY(64) NOT NULL,
  dsEstado CHAR(1) NOT NULL DEFAULT '1',
  dsUsuarioCreacion VARCHAR(50) NULL,
  dtFechaCreacion DATETIME DEFAULT GETDATE(),
  idUsuarioModificacion VARCHAR(50) NULL,
  dtFechaModificacion DATETIME DEFAULT GETDATE()
);
GO

-- Usuario de prueba: admin / 123456
INSERT INTO usuarios (dsId, dsUsuario, dsPassword, dsEstado, dsUsuarioCreacion)
VALUES ('001', 'admin', HASHBYTES('SHA2_512', '123456'), '1', 'Sistema');
GO

CREATE PROCEDURE sp_ValidarLoginHash
  @dsUsuario VARCHAR(50),
  @dsPassword VARCHAR(50)
AS
BEGIN
  SELECT id, dsId, dsUsuario, dsPassword, dsEstado, dsUsuarioCreacion, dtFechaCreacion
  FROM usuarios
  WHERE dsUsuario = @dsUsuario
    AND dsPassword = HASHBYTES('SHA2_512', @dsPassword)
    AND dsEstado = '1';
END
GO
```

### 🔒 ¿Cómo se protege la contraseña?
La contraseña **nunca se guarda en texto plano**. `HASHBYTES('SHA2_512', ...)` genera un hash de 512 bits (64 bytes) que se almacena en una columna `VARBINARY(64)`. Al iniciar sesión, el procedimiento almacenado cifra la contraseña ingresada y la compara con el hash guardado.

<p align="center">
  <img src="docs/img/tabla-usuarios.png" alt="Tabla usuarios con la contraseña cifrada" width="700">
</p>

---

## 🚀 Instalación y ejecución

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/TU_USUARIO/TU_REPOSITORIO.git
   ```
2. **Crear la base de datos** ejecutando el script de la sección anterior.
3. **Configurar la cadena de conexión** en `Datos/D_Conexion.cs` con el nombre de tu servidor:
   ```csharp
   public static string cadenaConexion =
       "Server=localhost;Database=db_sistema_login;Trusted_Connection=True;";
   ```
   > Si usas SQL Server Express: `Server=.\\SQLEXPRESS;...`
4. Abrir `SolucionCapas.sln` en Visual Studio.
5. Clic derecho en **Presentacion** → **Establecer como proyecto de inicio**.
6. Presionar **F5**.

### 👤 Usuario de prueba
| Usuario | Contraseña |
|---|---|
| `admin` | `123456` |

---

## 📸 Capturas

### Login exitoso
<p align="center">
  <img src="docs/img/login-exitoso.png" alt="Login exitoso" width="420">
</p>

### Login fallido
<p align="center">
  <img src="docs/img/login-fallido.png" alt="Login fallido" width="420">
</p>

### Formulario principal
<p align="center">
  <img src="docs/img/principal.png" alt="Formulario principal" width="500">
</p>

---



## 📚 Referencias
- [Video de referencia 1](https://www.youtube.com/watch?v=lAKJL3Crvso)
- [Video de referencia 2](https://www.youtube.com/watch?v=rX2AmgIqmy0&t=3171s)
