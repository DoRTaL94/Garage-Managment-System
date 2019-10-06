using System.Collections.Generic;
using System.Text;
using System;

namespace ConsoleUtils
{
     public class StringManager
     {
          public static bool IsValidLength(string i_Str, int i_Length)
          {
               return i_Str.Length == i_Length;
          }

          public static bool IsOnlyCharacters(string i_Str)
          {
               bool isNotOnlyChar = false;

               foreach (char ch in i_Str)
               {
                    if ((ch < 'a' || ch > 'z') && (ch < 'A' || ch > 'Z'))
                    {
                         isNotOnlyChar = true;
                         break;
                    }
               }

               return !isNotOnlyChar;
          }

          public static bool IsOnlyDigits(string i_Str)
          {
               bool isNotOnlyDigits = false;

               foreach (char ch in i_Str)
               {
                    if (ch < '0' || ch > '9')
                    {
                         isNotOnlyDigits = true;
                         break;
                    }
               }

               return !isNotOnlyDigits;
          }

          public static bool IsOnlyDigitsAndCapLetters(string i_Str)
          {
               bool isNotOnlyDigitsAndCapLetters = false;

               foreach (char ch in i_Str)
               {
                    if ((ch < '0' || ch > '9') && (ch < 'A' || ch > 'Z'))
                    {
                         isNotOnlyDigitsAndCapLetters = true;
                         break;
                    }
               }

               return !isNotOnlyDigitsAndCapLetters;
          }

          public static bool ConvertToInt(string i_Str, out int o_Number)
          {
               return int.TryParse(i_Str, out o_Number);
          }

          public static bool ConvertToFloat(string i_Str, out float o_Number)
          {
               return float.TryParse(i_Str, out o_Number);
          }

          public static bool CheckStringRange(string i_Str, int i_MaxRange)
          {
               return i_Str.Length >= 1 && i_Str.Length <= i_MaxRange;
          }

          public static string AddSpaceBetweenWordsWithCapLetters(string i_Str)
          {
               StringBuilder sb = new StringBuilder();

               if (i_Str.Length > 0)
               {
                    char Letter = ' ';
                    List<int> indices = new List<int>();

                    for (int i = 0; i < i_Str.Length; i++)
                    {
                         Letter = i_Str[i];

                         if (Letter >= 'A' && Letter <= 'Z')
                         {
                              indices.Add(i);
                         }
                    }

                    for (int i = 0; i < indices.Count; i++)
                    {
                         if (i != 0)
                         {
                              sb.Append(" ");
                         }

                         if (i + 1 == indices.Count)
                         {
                              sb.Append(i_Str.Substring(indices[i]));
                         }
                         else
                         {
                              int start = indices[i];
                              int end = indices[i + 1];
                              sb.Append(i_Str.Substring(start, end - start));
                         }
                    }
               }
               else
               {
                    throw new ArgumentException("String empty cannot add spaces");
               }

               return sb.ToString();
          }
     }
}
