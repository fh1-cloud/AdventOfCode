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
      /// Counts the number of times the same character occurs at the same position in a string. The strings must be equal in lenth or this method returns 0;
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


   #endregion



   }
}
