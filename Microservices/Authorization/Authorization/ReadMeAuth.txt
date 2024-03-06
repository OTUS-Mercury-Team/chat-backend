Файл Настроек запуска  launchSettings.json
http swagger тут "http://localhost:5227"
Доступы по http  "http://localhost:5227"

Список контролеров 
http://localhost:5227/api/Auth/login
Контролер для логирования принимающий  LoginModel в качестве пэйлода 
возвращает
LoginDto
http://localhost:5227/api/Validate/validate
принемает в себя строку string токен
возвращает
uint

UsersDbWorker заполняет таблицу и создает бд если таковой нет.

LoginModel LoginDto вынес за пределы сервиса чтобы могли сипользывать их в других местах.
в Common  храним проекты связанные с поко классами для храннеия данных 

Authorization.DataAccess тоже можно вынести в Common



