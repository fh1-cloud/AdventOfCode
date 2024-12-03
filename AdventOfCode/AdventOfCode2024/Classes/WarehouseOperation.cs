using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Classes
{

   public class WarehouseOperation
   {
      public enum CURRENTSTATE
      {
         DO,
         DONT,
      }

      public static CURRENTSTATE m_State = CURRENTSTATE.DO;

      public static bool Operate( string line, ref int currIdx, out int result )
      {

      //CHeck for state switchers..
         if( line.Length - currIdx > 4 && line.Substring( currIdx, 4 ) == "do()" )
         {
            m_State = CURRENTSTATE.DO;
            currIdx = currIdx + 4;
            result = 0;
            return false;
         }
         if( line.Length - currIdx > 7 && line.Substring( currIdx, 7 ) == "don't()" )
         {
            m_State = CURRENTSTATE.DONT;
            currIdx += 7;
            result = 0;
            return false;
         }

      //Check if this is a multiplier..
         if( m_State == CURRENTSTATE.DO && line.Length - currIdx > 4 && line.Substring( currIdx, 4 ) == "mul(" )
         {
         //Potential multiplier. Extract number until a comma, and see if it parses..
            bool foundComma = false;
            int commaIdx = -1;
            bool foundClosing = false;
            int closingIdx = -1;
            for( int i = currIdx + 4; i < line.Length; i++ )
            {
               if( line[i] == ',' )
               {
                  foundComma = true;
                  commaIdx = i;
                  break;
               }
            }

            for( int i = currIdx + 4; i < line.Length; i++ )
            {
               if( line[i] == ')' )
               {
                  foundClosing = true;
                  closingIdx = i;
                  break;
               }
            }

            if( foundComma && foundClosing && closingIdx > commaIdx )
            {
            //Try to parse the numbers in between..
               int val1 = -1;
               int val2 = -1;

               string firstNum = line.Substring( currIdx + 4, commaIdx - currIdx - 4 );
               string secondNum = line.Substring( currIdx + 4 + firstNum.Length + 1, closingIdx - commaIdx - 1  );
               bool parsedVal1 = int.TryParse( firstNum, out val1 );
               bool parsedVal2 = int.TryParse( secondNum, out val2 );

               if( parsedVal1 && parsedVal2 )
               {
                  currIdx = closingIdx+1;
                  result = val1*val2;
                  return true;
               }
            }
         }

      //If the code reached this point, it was not successful
         currIdx++;
         result = 0;
         return false;
      }

   }
}
