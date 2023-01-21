﻿using System;
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
            string prompt = @"Witaj w Aplikacji Kulinarnej. Co chcesz zrobić?
(Użyj strzałek do nawigowania po menu - naciśnij ENTER by zaakceptować)
";
            string[] options = { "1. Wyszukiwanie przepisu po kaloryczności", "2. Wyszukiwanie przepisu po podanych składnikach ", "3.Wyszukiwanie przepisu do określonej pory dnia","4. Wprowadzanie danych z konsoli ","5. Wczytanie danych z pliku","7. Wyjście z aplikacji "};
            ConsoleInterface aplicationinterface = new ConsoleInterface(options, prompt);
            int selectedIndex = aplicationinterface.Run();
            switch (selectedIndex)
            {
                case 0: break;
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
            Console.WriteLine("Wybrano wyjście z aplikacji");
            Console.WriteLine("Naciśnij dowolny przycisk by wyjść");
            Console.ReadKey();
            Environment.Exit(0);
        }

    }
}
