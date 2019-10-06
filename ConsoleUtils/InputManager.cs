using System;

namespace ConsoleUtils
{
     public class InputManager
     {
          public static string GetUserChoice(string i_MsgForUser, params object[] i_ValidInputs)
          {
               string userChoice = string.Empty;
               bool isValidInput = false;
               Console.Write(i_MsgForUser);

               userChoice = Console.ReadLine();

               foreach (object obj in i_ValidInputs)
               {
                    if (userChoice == obj.ToString())
                    {
                         isValidInput = true;
                         break;
                    }
               }

               if (isValidInput == false)
               {
                    throw new Exception("Invalid data input");
               }

               return userChoice;
          }
     }
}
