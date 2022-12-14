using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuceZBronksuConsole
{
    internal class Application
    {
        public void Start()
        {
            RunMainMenu();
            
        }
        private void RunMainMenu()
        {
            string[] prompt = { "Witaj w Aplikacji Kulinarnej." ,"Co chcesz zrobić?"};
            string[] options = { "Wyszukiwanie smaków dopasowanych do podanego", "Wyszukiwanie przepisów do posiadanych składników", "Wyszukiwarka przepisów z podanym smakiem/smakami","Wprowadzanie danych z konsoli","Wczytanie danych z pliku","Edycja danych z konsoli","Wyjście z aplikacji"};
            ConsoleInterface aplicationinterface = new ConsoleInterface(options, prompt);
            int selectedIndex = aplicationinterface.Run();
            switch (selectedIndex)
            {
                case 0:break;
                case 1: break;
                case 2: break;
                case 3: break;
                case 4: break;
                case 5: break;
                case 6: ExitApplication(); break;
            }
        }
        private void ExitApplication()
        {        
            Console.Clear();
            Console.WriteLine("Czy na pewno chcesz wyjść z aplikacji? [T/N]");

            ConsoleKeyInfo Keyinfo = Console.ReadKey(true);
            var keypressed = Keyinfo.Key;


            if (keypressed == ConsoleKey.T )
            {
                Environment.Exit(0);
            }
            else if (keypressed == ConsoleKey.N || keypressed == ConsoleKey.Escape)
            {
                Console.Clear();
                RunMainMenu();
            }
            else
            { 
                Console.WriteLine("Zła opcja!");
                Console.ReadKey();
                RunMainMenu();
            }
        }

    }
}
