using System;
using ConsoleUtils;

namespace ConsoleUI
{
     public class MenuManager
     {
          private MainMenu m_Menu = new MainMenu();

          public void StartProgram()
          {
               bool isStopProgram = false;
               string userChoice = string.Empty;

               Console.WriteLine("Welcome to The Garage! What do you want to do:");

               while (isStopProgram == false)
               {
                    m_Menu.Show();
                    isStopProgram = m_Menu.HandleUserInput();
               }
          }
     }
}
