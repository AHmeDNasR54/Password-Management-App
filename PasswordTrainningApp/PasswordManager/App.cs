using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordTrainningApp.PasswordManager
{
    public class App
    {
        /*
          [Passwword Manager]
            1.List all passwords
            2.add or change password
            3.Get password
            4.Delete password
         */
        private static bool testSecurity=false;
        private static readonly Dictionary<string, string>PasswordEntries=new Dictionary<string, string>(); 
        public static   void Run(string[] args)
        {
            // websitename=password
            // websitename=password
            
            ReadPasswords();
            if (!testSecurity)
                return;

            while (true)
            {
                Console.WriteLine("Please select an option :");
                Console.WriteLine("\t1.List all passwords\n\t2.add or change password\n\t3.Get password\n\t4.Delete password");
               var selected= Console.ReadLine();
                switch (selected)
                {
                    case "1":
                        ListAllPasswords();
                        break;

                    case "2":
                        AddOrChangePassword();
                        break;

                    case "3":
                        GetPassword();
                        break; 
                    case "4":
                        DeletePassword();
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
                Console.WriteLine("--------------------");
            }
        }


        private static void ListAllPasswords()
        {
            foreach (var pass in PasswordEntries)
            {
                if (pass.Key != "masterKey")
                {
                    Console.WriteLine($"Website Name:{pass.Key}\tPassword:{pass.Value}");
                }
            }
        }
        private static void AddOrChangePassword()
        {
         
                Console.Write("Please Enter Website/App name:  ");
                var appName=Console.ReadLine();              
                Console.Write("Please Enter Password:  ");
                var password=Console.ReadLine();
            if (PasswordEntries.ContainsKey(appName))
            {
                PasswordEntries[appName] = password;
               
            }
            else PasswordEntries.Add(appName, password);
            SavePasswords();
           // Console.WriteLine("Called Save Methods");
        }
        private static void GetPassword()
        {
            Console.Write("Please Enter Website/App name:  ");
            var appName = Console.ReadLine();
            if (PasswordEntries.ContainsKey(appName))
            {
                Console.WriteLine($"Password {PasswordEntries[appName]}");
            }
            else
            {
                Console.WriteLine("There is no password for this App");
            }
        }
        private static void DeletePassword()
        {
            Console.Write("Please Enter Website/App name:  ");
            var appName = Console.ReadLine();
            if (PasswordEntries.ContainsKey(appName))
            {
                PasswordEntries.Remove(appName);
                SavePasswords();
            }
            else
            {
                Console.WriteLine("There is no password for this App");
            }
        }
        private static void ReadPasswords()
        {
            if (File.Exists("passwords.txt"))
            {

                var passordLines = File.ReadAllText("passwords.txt");
                foreach (var line in passordLines.Split(Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        //websitename=password
                        var indexToSplit = line.IndexOf("=");
                        var appName = line.Substring(0, indexToSplit);
                        var password = line.Substring(indexToSplit + 1);

                        PasswordEntries.Add(appName, EncriptionUtilize.Decript(password));
                    }
                }
                Console.Write("Enter the master Key for this file : ");
                var mkey = Console.ReadLine();

                if (PasswordEntries["masterKey"] != mkey)
                {
                    Console.WriteLine("Invalid Password");

                }
                else testSecurity = true;
            }
            else
            {
                testSecurity = true;
                Console.Write("Enter the master Key for the new file : ");
                var mkey = "";
                mkey = Console.ReadLine();
                 PasswordEntries.Add("masterKey", mkey);
                SavePasswords();

            }


        }
        private static void SavePasswords()
        {
           var sb = new StringBuilder();
            foreach (var entry in PasswordEntries)
            {
                sb.AppendLine($"{entry.Key}={EncriptionUtilize.Encript(entry.Value)}");

            }
            File.WriteAllText("passwords.txt",sb.ToString());
            //Console.WriteLine("Saved To File");
        }
    }
}
