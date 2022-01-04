# Вывод всех заметок
**URL** : `/api/auth/notes`  
  
**Метод** : `GET`  
  
**Необходима авторизация** : Да  

**Необходимые заголовки** : 
| Заголовок       | Значение           |
|-----------------|--------------------|
| "Content-Type"  | "application/json" |
| "Authorization" | "Bearer {Токен}"   |

## Успешный ответ
**Статусный код** : `200 OK`
**Пример ответа** :  
````
{
    "result": [
        {
            "id": "5635fa44-7941-4e33-b2fd-ee8d9289a465",
            "name": "Hello",
            "text": "This is note",
            "owner": "user"
        },
        {
            "id": "4bd56da5-756c-4254-b810-9c8b570f3d33",
            "name": "Second note",
            "text": "Second note",
            "owner": "user"
        }
    ]
}
````

## Неуспешный ответ
**Статусный код** : `401 Unauthorized`
