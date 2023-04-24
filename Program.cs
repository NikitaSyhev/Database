//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.SqlClient;
//using Database;
//using System.Text.RegularExpressions;
//using static System.Net.Mime.MediaTypeNames;
//using System.IO;
//using System.Threading;
//namespace Database
//{

//    public class User
//    {
//        public User(string name, string mail, string numberPhone) { _name = name; _mail = mail; _numberPhone = numberPhone; }
//        public string _name { get; }
//        public string _mail { get; }
//        public string _numberPhone { get; }
//    }
//    delegate void myDel(string _text);
//    public class myConnectToMSSQLDB
//    {
//        public SqlConnection connection;
//        public myConnectToMSSQLDB()
//        {
//            string conStr = @"Data Source=(localdb)\MSSQLLocalDB;" + /* Имя сервера */
//                        "Initial Catalog=master;" + /* БД подключения*/
//                        "Integrated Security=True;" + /* Использование уч.записи Windows */
//                        "Connect Timeout=30;" + /* Таймаут в секундах*/
//                        "Encrypt=False;" + /* Поддержка шифрования, работает в паре со сл.параметром */
//                        "TrustServerCertificate=False;" + /* Только при подключении к экземпляру SQL Server с допустимым сертификатом. Если ключевому слову TrustServerCertificate присвоено значение true, то транспортный уровень будет использовать протокол SSL для шифрования канала и не пойдет по цепочке сертификатов для проверки доверия. */
//                        "ApplicationIntent=ReadWrite;" + /* Режим подключения*/
//                        "MultiSubnetFailover=False;"; /* true - поддержка уровня доступности: оптимизирует работу для пользователей одной подсети*/
//            var myConn = new SqlConnection(conStr);
//            try
//            {
//                myConn.Open();
//                Console.WriteLine($"Установлено соединение с параметрами {conStr}");
//            }
//            catch
//            {
//                Console.WriteLine($"Не удалось установить соединение с параметрами {conStr}");
//            }
//            finally
//            {
//                //myConn.Close();
//                connection = myConn;
//                Console.WriteLine($"Закрыто соединение с параметрами {conStr}");

//            }

//        }

//    }

//}
//internal class Program
//{
//    static void Print(string _text)
//    {
//        Console.WriteLine(_text);
//    }
//    static void WriteToFile(string _text)
//    {
//        string _name = "Users.txt";
//        var sw = new StreamWriter(_name, true);
//        sw.WriteLine(_text);
//        sw.Close();
//        //using (var sw01 = new StreamWriter(_name))
//        //{
//        //    sw01.WriteLine(_text, true);
//        //}

//    }
//    static void WriteToDB(string _text)
//    {
//        var connect = new myConnectToMSSQLDB();
//        string _cmd = "USE users;\n";
//        _cmd += @"INSERT INTO [dateUs] ([name],[phone],[mail]) ";
//        _cmd += @"VALUES (N";
//        _cmd += _text;
//        _cmd += ");";

//        /* Конструетор по умолчанию */
//        SqlCommand sqlCommand = new SqlCommand();
//        sqlCommand.Connection = connect.connection;
//        sqlCommand.CommandText = _cmd;

//        /* Использование нестатического метода SqlConnection */
//        //var sqlCommand = myConn.CreateCommand();
//        //sqlCommand.CommandText = _cmd;

//        /* Использование перегрузки конструктора с 2-мя параметрами */
//        //SqlCommand sqlCommand = new SqlCommand(_cmd, myConn);

//        var dataReader = sqlCommand.ExecuteReader();
//        Console.WriteLine(_cmd);
//        while (dataReader.Read())
//        {
//            int row = dataReader.FieldCount; // Вспомогательная переменная, количество возвращённых столбцов
//            for (int i = 0; i < row; i++)
//            {
//                Console.Write("  " + dataReader[i].ToString());
//            }
//            Console.WriteLine();
//        }
//    }
//    static void Main(string[] args)
//    {
//        myDel putText;
//        putText = WriteToDB;
//        putText += Print;
//        putText += WriteToFile;
//        string exit = "end_input";
//        string input = "", name, phone, mail;
//        int nUser = 1;
//        List<User> users = new List<User>();
//        Regex rPhone = new Regex(@"((\+7|8)\d{10})");
//        Regex rName = new Regex(@"((\s|^)[A-Z|a-z]+\D(\s|$))");
//        Regex rMail = new Regex(@"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)");
//        do
//        {
//            Console.Clear();
//            Console.WriteLine($"Введите имя ,номер телефона и mail пользователя №{nUser++} через пробел:");
//            input = Console.ReadLine();
//            if (input.ToLower() == exit) break;
//            MatchCollection matchFindName = rName.Matches(input);
//            try
//            {
//                name = matchFindName[0].ToString();
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("Имя неуказанно");
//                name = @"<<none>>";
//            }
//            MatchCollection matchFindPhone = rPhone.Matches(input);
//            try
//            {
//                phone = matchFindPhone[0].ToString();
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("Номер телефона неуказан или указан неверно");
//                phone = @"<<none>>";
//            }
//            MatchCollection matchFindMail = rMail.Matches(input);
//            try
//            {
//                mail = matchFindMail[0].ToString();
//            }
//            catch (Exception)
//            {
//                Console.WriteLine("почта неуказана или указана неверно");
//                mail = @"<<none>>";
//            }
//            User user = new User(name, mail, phone);
//            users.Add(user);
//            string user_list = $"'{name}','{phone}','{mail}'";
//            putText.Invoke(user_list);
//            Thread.Sleep(3000);
//        } while (input.ToLower() != exit);
//        for (int i = 0; i < users.Count; i++)
//        {
//            Console.WriteLine($"Пользователь: {users[i]._name}\t номер телефона: {users[i]._numberPhone}\t mail: {users[0]._mail}");
//        }
//    }
//}