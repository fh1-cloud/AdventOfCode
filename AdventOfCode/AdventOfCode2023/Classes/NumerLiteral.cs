using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class NumerLiteral
   {

   //Create dictionary with numbers..
      protected static Dictionary<string, int> m_WordLiteral = new Dictionary<string, int>( );

      /// <summary>
      /// Static constructor
      /// </summary>
      static NumerLiteral( )
      {
         m_WordLiteral.Add( "one", 1);
         m_WordLiteral.Add( "two", 2 );
         m_WordLiteral.Add( "three", 3 );
         m_WordLiteral.Add( "four", 4 );
         m_WordLiteral.Add( "five", 5 );
         m_WordLiteral.Add( "six", 6 );
         m_WordLiteral.Add( "seven", 7 );
         m_WordLiteral.Add( "eight", 8 );
         m_WordLiteral.Add( "nine", 9 );

      }


      /// <summary>
      /// Check if number is a literal start.
      /// </summary>
      /// <param name="s"></param>
      /// <param name="startIdx"></param>
      /// <param name="num"></param>
      /// <returns></returns>
      public static bool CheckIfNumerliteralStart( string s, int startIdx, out int num )
      {
      //Set out
         num = 0;

      //Extract substring from startIdx to end..
         string subStr = s.Substring( startIdx );

      //Loop over all the numbers in the numerlateral and check if this is one of them..
         foreach( KeyValuePair<string, int> kvp in m_WordLiteral )
         {

            bool match = true;
            for( int i = 0; i<kvp.Key.Length; i++ )
            {
               if( !kvp.Key[i].Equals( subStr[i] ) )
               {
                  match = false;
                  break;
               }
            }
            if( match == true )
            {
               num = kvp.Value;
               return true;
            }
         }

      //If the code reached this point, no number was created.
         return false;

      }


      /// <summary>
      /// Check for end string start
      /// </summary>
      /// <param name="s"></param>
      /// <param name="endIdx"></param>
      /// <param name="num"></param>
      /// <returns></returns>
      public static bool CheckIfNumerliteralEnd( string s, int endIdx, out int num )
      {
      //Set out
         num = 0;

      //Loop over all the numbers in the numerlateral and check if this is one of them..
         foreach( KeyValuePair<string, int> kvp in m_WordLiteral )
         {
         //Extract substring with length as literal

            int startIdx = endIdx - kvp.Key.Length + 1;
            if( startIdx < 0 )
               continue;

            string subStr = s.Substring( startIdx, kvp.Key.Length );
            bool match = true;
            for( int i = kvp.Key.Length - 1; i>= 0; i-- )
            {
               if( !kvp.Key[i].Equals( subStr[i] ) )
               {
                  match = false;
                  break;
               }
            }

            if( match == true )
            {
               num = kvp.Value;
               return true;
            }
         }

      //If the code reached this point, no number was created.
         return false;

      }



   }
}
