using SaftyPassword;
using SaftyPassword.FileWorker;

var fileWorker = new FileWorkerModule();

Console.WriteLine("SP | Добро пожаловать в SaftyPassword. Сохраните свои данные надёжно!");
Console.WriteLine("SP | Идёт загрузка данных и инициализация системы хэширования...");

Thread.Sleep(1500);
while (true)
{
    Console.Clear();

    Console.WriteLine("SP | Рабочая панель.");
    Console.WriteLine($"SP | В базе данных найдено {fileWorker.GetCountLines()} зашифрованные записи ваших данных. Что будем с ними делать?");
    Console.WriteLine("\n\t 1. Запишем новые данные.");
    Console.WriteLine("\t 2. Прочитаем данные.");
    Console.WriteLine("\t 3. Информация.");
    Console.WriteLine("\n\t 5. Выход.");
    var key = Console.ReadKey();

    Console.Clear();
    switch(key.Key.ToString())
    {
        case "D1":
            Console.WriteLine("SP | Хорошо, запишем новые данные, для начала запишите уникальный ключ!");
            Console.WriteLine("SP | Уникальный ключ нужен, для дальнейшего поиска данных по вашему ключу.");
            Console.WriteLine("SP | Например: пароль для почты gmail я бы обозначил как 'gmail-password':");
            var indificator = Console.ReadLine();
            if (indificator == null)
            {
                Console.WriteLine("SP | Ошибка: Вы ввели неверный индитификатор.");
                break;
            }

            Console.WriteLine("SP | Хорошо, теперь введём секретный пароль, только вы должны его знать!");
            Console.WriteLine("SP | Именно он защищает ваши данные. Не потеряйте его:");
            var password = ConsoleHelper.ReadPassword();
            Console.WriteLine("SP | Бр-р-р, надёжный пароль. Теперь введите данные, которые хотите сохранить:");
            var data = Console.ReadLine();
            if (data == null)
            {
                Console.WriteLine("SP | Ошибка: Вы ввели неверные данные.");
                break;
            }

            var res = fileWorker.WriteData(password, data, indificator);

            if (res)
            {

                Console.WriteLine("SP | Мы записываем ваши данные...");
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("SP | Данные с таким индификатором уже есть, нажмите любую кнопку для продолжения...");
                Console.ReadKey();
            }
            break;
        case "D2":
            Console.WriteLine("SP | Хорошо, прочтём данные, для начала напомните мне уникальный ключ!");
            var indificatorRead = Console.ReadLine();
            if (indificatorRead == null)
            {
                Console.WriteLine("SP | Ошибка: Вы ввели неверный индитификатор.");
                break;
            }

            Console.WriteLine("SP | Хорошо, теперь введём секретный пароль, вспоминайте.");
            var passwordRead = ConsoleHelper.ReadPassword();

            var dataRead = fileWorker.ReadData(passwordRead, indificatorRead);

            if (dataRead != null)
            {
                Console.WriteLine("SP | Мы нашли какие-то данные. Если вы получили непонятный набор строк, то скорее всего");
                Console.WriteLine("SP | вам не удалось расшифравать данные, вы ввели неверный пароль. Только хозяйн данных");
                Console.WriteLine("SP | знает, правильны ли эти данные:\n\n");
                Console.Write("\tВывод: " + dataRead);
            }
            else
            {
                Console.WriteLine("SP | Увы мы ничего не нашли, проверьте введёные вами данные.");
                Console.WriteLine("SP | Нажмите любую кнопку что бы продолжить...");
            }

            Console.ReadKey();
            break;
        case "D3":
            Console.WriteLine("SP | Данная программа была написана в рамках предмета \"Тестирование и верификация программного обеспечения\"");
            Console.WriteLine("SP | Авторы: студенты группы ИКБО-20-20: Осипов Павел, Ходакова Мария, Гасанова Ровена, Елена Пирогова.\n");
            Console.WriteLine("SP | Программа, для защиты данных, использует: MD5 Хэширование ( при необходимости ), симметричный блочный шифр");
            Console.WriteLine("SP | TripleDES в отличии от обычного DES работает с ключом большей длинны, что увеличивает возможности пользователя");
            Console.WriteLine("SP | Triple DES в выборе пароля и защиту данных. Число раундов: 48 DES. База тестирование nUnit.");
            Console.WriteLine("SP | Нажмите любую кнопку для скрытия инфомрации...");
            Console.ReadKey();
            break;
        case "D5":
            Console.Clear();
            Console.WriteLine("SP | Сохраняем данные и выходим из приожения...");
            Thread.Sleep(1000);
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine(key.Key.ToString());
            break;
    }
}

