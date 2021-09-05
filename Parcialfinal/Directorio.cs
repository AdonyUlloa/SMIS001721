using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Parcialfinal
{
        public class Directorio
        {
        static Session session = new Session();
        static Boolean isAdmin = false;

        public static void main()
        {
            Console.SetWindowSize(90, 30);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.Title = "PARCIAL";
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(" ****************************************************************************************");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                                  INICIO DE SESSION                                      ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(" ****************************************************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            login();


        }

        static void login()
        {

            Console.WriteLine("");
            Console.Write("-Ingrese usuario: ");
            String username = Console.ReadLine();
            Console.WriteLine("");
            Console.Write("-Ingrese contraseña: ");
            String password = Console.ReadLine();
            if (session.login(username, password))
            {
                if (username.Equals("admin"))
                {
                    isAdmin = true;
                    printMenuAdmin();
                }
                else
                {
                    isAdmin = false;
                    printMenuOther();
                }

                menus();
            }
            else
            {
                Console.WriteLine("Usuario o contraseña incorrectos");
                login();
            }

        }

        static void menus()
        {
            String option = Console.ReadLine();
            option = option.ToLower();
            String fileName = "";
            String text = "";
            Boolean exit = false;
            if (isAdmin)
            {
                switch (option)
                {
                    case "a":
                        Console.SetWindowSize(90, 30);
                        Console.Clear();
                        Console.Title = "BIENVENIDO ADMIN";
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.Black;
                        string title5 = "****************************************************************************************";
                        Console.SetCursorPosition((Console.WindowWidth - title5.Length) / 2, Console.CursorTop);
                        Console.WriteLine(title5);
                        Console.ForegroundColor = ConsoleColor.Black;
                        string titleA = "|  BIENVENIDO ADMIN  |";
                        string borderA = new string('-', titleA.Length);
                        Console.SetCursorPosition((Console.WindowWidth - titleA.Length) / 2, Console.CursorTop);
                        Console.WriteLine(borderA);
                        Console.SetCursorPosition((Console.WindowWidth - titleA.Length) / 2, Console.CursorTop);
                        Console.WriteLine(titleA);
                        Console.SetCursorPosition((Console.WindowWidth - titleA.Length) / 2, Console.CursorTop);
                        Console.WriteLine(borderA);
                        Console.ForegroundColor = ConsoleColor.Black;
                        string title6 = "****************************************************************************************";
                        Console.SetCursorPosition((Console.WindowWidth - title6.Length) / 2, Console.CursorTop);
                        Console.WriteLine(title6);
                        Console.WriteLine("POR FAVOR LLENE LOS CAMPOS REQUERIDOS");
                        Console.Write("Ingrese el usuario: ");
                        String username = Console.ReadLine();
                        Console.Write("Ingrese la contraseña: ");
                        String paasword = Console.ReadLine();
                        session.addCredential(username, paasword);
                        
                        //La credenciales se crean en libraryRegister\Program_Library\bin\Debug\store
                        break;
                    case "b":
                        Console.WriteLine("Crear archivo");
                        Console.WriteLine("Nombre del archivo? ");
                        fileName = Console.ReadLine();
                        Console.WriteLine("Ingrese el texto del archivo");
                        text = Console.ReadLine();
                        session.newFile(fileName);
                        session.addTextFile(text, fileName);
                        break;
                    case "c":
                        Console.WriteLine("Borrar archivo");
                        Console.WriteLine("Nombre del archivo? ");
                        fileName = Console.ReadLine();
                        session.deleteFile(fileName);
                        break;
                    case "d":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opcion ingresada incorrecta");

                        break;

                }
                if (!exit)
                {
                    printMenuAdmin();
                    menus();
                }
            }
            else
            {
                switch (option)
                {
                    case "a":
                        Console.WriteLine("Leer archivo.");
                        Console.WriteLine("Nombre del archivo: ");
                        fileName = Console.ReadLine();
                        text = session.readFile(fileName);
                        Console.WriteLine(text);
                        break;
                    case "b":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opcion ingresada incorrecta");

                        break;

                }
                if (!exit)
                {
                    printMenuOther();
                    menus();
                }
            }
        }


        static void printMenuAdmin()
        {
            Console.SetWindowSize(90, 30);
            Console.Clear();
            Console.Title = "BIENVENIDO ADMIN";
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Black;
            string title5 = "****************************************************************************************";
            Console.SetCursorPosition((Console.WindowWidth - title5.Length) / 2, Console.CursorTop);
            Console.WriteLine(title5);
            Console.ForegroundColor = ConsoleColor.Black;
            string titleA = "|  BIENVENIDO ADMIN  |";
            string borderA = new string('-', titleA.Length);
            Console.SetCursorPosition((Console.WindowWidth - titleA.Length) / 2, Console.CursorTop);
            Console.WriteLine(borderA);
            Console.SetCursorPosition((Console.WindowWidth - titleA.Length) / 2, Console.CursorTop);
            Console.WriteLine(titleA);
            Console.SetCursorPosition((Console.WindowWidth - titleA.Length) / 2, Console.CursorTop);
            Console.WriteLine(borderA);
            Console.ForegroundColor = ConsoleColor.Black;
            string title6 = "****************************************************************************************";
            Console.SetCursorPosition((Console.WindowWidth - title6.Length) / 2, Console.CursorTop);
            Console.WriteLine(title6);


            Console.WriteLine("Elija que desea realizar\n ");
            Console.WriteLine("a) Registrar nuevo usuario: ");
            Console.WriteLine("b) Crear archivo: ");
            Console.WriteLine("c) Borrar archivo: ");
            Console.WriteLine("d) Salir: ");
            Console.Write("\n Opcion: ");

        }

        static void printMenuOther()
        {
            
            Console.WriteLine("\n--------------- MENU INVITADO--------------");
            Console.WriteLine("\ta. Leer Archivo");
            Console.WriteLine("\tb. Salir: \n");
            Console.Write("\tOpcion: ");

        }
    }

    class Session
    {

        String fileStoreCredentials = "storecredentials.txt";
        DirectorioMain directorioMain = null;
        const string passwordAdmin = "123";
        public Session()
        {
            this.directorioMain = new DirectorioMain();
            if (!directorioMain.existsFile(fileStoreCredentials))
            {
                directorioMain.createFile(fileStoreCredentials);
                directorioMain.writeFile("admin:123", fileStoreCredentials);
            }
        }

        public Boolean login(String username, String password)
        {
            Boolean loginSuccess = false;

            if (username.Equals("admin") && password.Equals(passwordAdmin))
                return true;

            string[] lines = this.directorioMain.getLines(this.fileStoreCredentials);
            for (int index = 0; index < lines.Length; index++)
            {

                string[] credentials = lines[index].Split(':');

                if (credentials.Length > 1)
                {
                    if (loginSuccess)
                        break;

                    if (credentials[0].Equals(username) && credentials[1].Replace("\r", "").Equals(password))
                    {

                        loginSuccess = true;
                    }
                    else
                    {

                        loginSuccess = false;
                    }

                }



            }
            return loginSuccess;
        }

        public void addCredential(String username, String password)
        {
            directorioMain.writeFile(username + ":" + password, fileStoreCredentials);
        }


        public void addTextFile(String text, String fileName)
        {

            directorioMain.writeFile(text, fileName);
        }


        public void deleteFile(String fileName)
        {
            directorioMain.deleteFile(fileName);
        }

        public void newFile(String fileName)
        {
            directorioMain.createFile(fileName);
        }

        public String readFile(String fileName)
        {
            return directorioMain.readFile(fileName);
        }
    }

    class DirectorioMain
    {
        String pathDirectory = "";
        public DirectorioMain()
        {

            DirectoryInfo directoryInfo = System.IO.Directory.CreateDirectory(".\\store");
            pathDirectory = directoryInfo.FullName;

        }

        public String readFile(String fileName)
        {
            fileName = System.IO.Path.Combine(pathDirectory, fileName);
            string text = System.IO.File.ReadAllText(fileName);
            return text;
        }

        public String[] getLines(String file)
        {
            return this.readFile(file).Split('\n');
        }

        public async void writeFile(String text, String fileName)
        {
            fileName = System.IO.Path.Combine(pathDirectory, fileName);
            StreamWriter fileStream = new StreamWriter(fileName, append: true);
            await fileStream.WriteLineAsync(text);
            fileStream.Close();
        }


        public void createFile(String fileName)
        {
            fileName = System.IO.Path.Combine(pathDirectory, fileName);
            System.IO.File.Create(fileName).Close();


        }

        public void deleteFile(String fileName)
        {

            fileName = System.IO.Path.Combine(pathDirectory, fileName);
            System.IO.File.Delete(fileName);

        }

        public Boolean existsFile(String fileName)
        {
            fileName = System.IO.Path.Combine(pathDirectory, fileName);

            return System.IO.File.Exists(fileName);

        }



        //public async void deleteLine(int line, String fileName)
        //{
        //    fileName = System.IO.Path.Combine(pathDirectory, fileName);
        //    string text = this.readFile(fileName);
        //    string[] lines = text.Split('\n');


        //    await File.WriteAllLinesAsync(fileName, lines);
        //}
    }
}
