# Api приложения с авторизацией через jwt токен
Тренировочный проект. Суть проекта в том, чтобы научиться авторизовывать приложения и выдавать им разную информацию в зависимости от их ролей.
## Что необходимо для запуска
- .NET 6 + ASP.NET 6 
- Docker
- PostgreSQL
## Действия для запуска
Необходимо в корень добавить файл Config.cs. Он должен выглядеть следующим образом:
````
namespace ApiApp
{
    public static class Config
    {
        public const string CONNECTION_STRING = "Ваша строка подключения";
        public const string SECRET = "Ключ";
    }
}

````
### Продолжение следует...
