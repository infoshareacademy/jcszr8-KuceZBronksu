using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuceZBronksuConsole
{
    class ConsoleInterface
    {
        private int SelectedIndex;
        private string[] Options;
        private string[] Prompt;
        private string title = "Aplikacja kulinarna"; /* title of aplication*/
        public ConsoleInterface(string[] options, string[] prompt)
        {
            Options = options;
            Prompt = prompt;
            SelectedIndex = 0;
        }
        private void DisplayOptions()
        {
            
            var defaultColorMain = Console.ForegroundColor;    /*default console foreground color variable for these people who customzie his console*/
            var defaultColorSecond = Console.BackgroundColor;    /*default console background color variable for these people who customzie his console*/
            
            foreach (var line in Prompt)
            {
                Console.WriteLine(line);
            }
            
            for(int i = 0; i < Options.Length; i++)
            {
                string currentoption = Options[i];
                string prefix;
                if( i == SelectedIndex)
                {
                    prefix = " <--";
                    Console.ForegroundColor = defaultColorSecond;
                    Console.BackgroundColor = defaultColorMain;
                }
                else
                {
                    
                    prefix = "";
                    Console.ForegroundColor = defaultColorMain;
                    Console.BackgroundColor = defaultColorSecond;
                }
                Console.WriteLine($"{currentoption}{prefix}");
            }
            Console.ResetColor();
        }
        public int Run()
        {
            ConsoleKey keypressed;
            do
            {
                Console.Title = title;
                Console.Clear();
                DisplayOptions();
                ConsoleKeyInfo Keyinfo = Console.ReadKey(true);
                keypressed = Keyinfo.Key;
                if (keypressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if(keypressed== ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
            }  
            while (keypressed != ConsoleKey.Enter);
            return SelectedIndex;
        }
    }
}
