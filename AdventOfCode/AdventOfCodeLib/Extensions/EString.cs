using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Extensions
{
   public static class EString
   {

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// Counts the number of occurences of a character in a string
      /// </summary>
      /// <param name="str">this string</param>
      /// <param name="a">The character to look for.</param>
      /// <returns></returns>
      public static int CountOccurencesOfLetter( this string str, char a )
      {
         int count = 0;
         for( int i = 0; i < str.Length; i++ )
            if( str[i] == a )
               count++;
         return count;
      }

      /// <summary>
      /// Counts the number of times the same character occurs at the same position in two different strings . The strings must be equal in length
      /// </summary>
      /// <param name="str"></param>
      /// <param name="comparer"></param>
      /// <returns></returns>
      public static int CountCommonPositions( this string str, string comparer )
      {
         if( str.Length != comparer.Length )
            throw new Exception( );
         int commonIdx = 0;
         for( int i = 0; i < str.Length; i++ )
            if( str[i] == comparer[i] )
               commonIdx++;
         return commonIdx;
      }


      /// <summary>
      /// Checks if the string consists of unique characters..
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
      public static bool ConsistsOfUniqueCharacters( this string str )
      {
      //Check for unique characters..
         for( int i = 0; i< str.Length; i++ )
            for( int j = i+1; j< str.Length; j++ )
               if( str[i] == str[j] )
                  return false;

      //If we reached this point, it consists of unique characters..
         return true;

      }


   #endregion



   }
}
