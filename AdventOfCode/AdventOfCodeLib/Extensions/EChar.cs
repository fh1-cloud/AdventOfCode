using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Extensions
{

   /// <summary>
   /// Extensino methods for the char class.
   /// </summary>
   public static class EChar
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
   #endregion

   /*CONSTRUCTORS*/
   #region
   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// A method that returns wheter or not the char is an integer or not..
      /// </summary>
      /// <param name="a"></param>
      /// <returns></returns>
      public static bool IsInteger( this char a )
      {
         return ( ( int ) a >= ( int ) '0' && ( int ) a <= ( int ) '9' );
      }

      /// <summary>
      /// Extension for the IsUpper so its easier to write..
      /// </summary>
      /// <param name="a"></param>
      /// <returns></returns>
      public static bool IsUpper( this char a )
      {
         return char.IsUpper( a );
      }

      /// <summary>
      /// Checks if the char is an upper case letter..
      /// </summary>
      /// <param name="a"></param>
      /// <returns></returns>
      public static bool IsUpperCaseLetter( this char a )
      {
         int cVal = ( int ) a;
         return cVal >= 65 && cVal <= 90;
      }

      /// <summary>
      /// Checks if the character is a lower case letter..
      /// </summary>
      /// <param name="a"></param>
      /// <returns></returns>
      public static bool IsLowerCaseLetter( this char a )
      {
         int cVal = ( int ) a;
         return cVal >= 97 && cVal <= 122;
      }

      /// <summary>
      /// Checks if the character is a letter..
      /// </summary>
      /// <param name="a"></param>
      /// <returns></returns>
      public static bool IsLetter( this char a )
      {
         return ( a.IsUpperCaseLetter( ) || a.IsLowerCaseLetter( ) );
      }

   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   }
}
