# Api приложения с авторизацией через jwt токен
Тренировочный проект. Суть проекта в том, чтобы научиться авторизовывать приложения и выдавать им разную информацию в зависимости от их ролей.  
Приложение представляет собой сервис для создания, редактирования и удаления заметок.
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
## Открытые маршруты
[Проверка доступности сервера](endpoints/visibility.md) : ```GET /api/visibility```   
  
[Авторизация](endpoints/login.md) : ```POST /api/auth/login```  
[Регистрация](endpoints/register.md) : ```POST /api/auth/register```  

## Маршруты, требующие авторизации
[Получение данных пользователя](endpoints/info.md) : ```GET /api/auth/info```  
  
[Вывод всех заметок](endpoints/showAllNotes.md) : ```GET /api/notes```  
  
[Создание заметки](endpoints/createNote.md) : ```POST /api/notes/create```  
  
[Вывод заметки](endpoints/showNote.md) : ```GET /api/notes/{id}```  
[Удаление заметки](endpoints/deleteNote.md) : ```DELETE /api/notes/{id}```  
[Изменение заметки](endpoints/updateNote.md) : ```PUT /api/notes/{id}```

### ...
