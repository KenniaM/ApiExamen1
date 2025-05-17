# ApiExamen1

Para la creación de  ApiExamen con .NET estos son los siguientes pasos:
  
  1.En Github hacer git clone del repositorio ApiExamen1:

      git clone https://github.com/KenniaM/ApiExamen1.git

      Luego, guardar ese repositorio en un archivo en cualquier lugar de la computadora 

      Despues,al clonar el archivo abrir visual studio y agregar en la terminal este comando:
      ->cd ApiExamen

  2.Cambiar el ConnectionString con los credenciales de SQLServer:
      "ConnectionStrings": {
          "DefaultConnection": "Server=localhost,1433;Database=ExamenUnoDB;User Id=sa;Password=MyPass@word;TrustServerCertificate=True;"
       }

       
  3. Comandos Importantes

      dotnet add package Microsoft.EntityFrameworkCore.SqlServer
      dotnet add package Microsoft.EntityFrameworkCore.Tools
      dotnet add package Microsoft.EntityFrameworkCore.Design

      Para correr las migraciones:

      dotnet tool install --global dotnet-ef //instalar dotnet-ef

          dotnet ef migrations add Init //para iniciar migraciones
  
        dotnet ef database update //para actualizar la base de datos

      Para hacer el build del app:
        dotner build

      Para correr la aplicación:
        dotnet watch run
        dotnet run

  4.  Abrir el archivo .zip llamado "Firebase", luego extraer el archivo y pasar el archivo dentro el proyecto 
      dentro de la carpeta extraída a la carpeta de la api llamada "ApiExamen".
