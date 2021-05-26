using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib
{
   public static class UMath
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
      /// Gets the greates common factor of two whole numbers
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static long GCD(long a, long b )
      {
         while (b != 0)
         {
            long temp = b;
            b = a % b;
            a = temp;
         }
         return a;
      }


      /// <summary>
      /// Gets the least common multiple between two numbers
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static long LCM(long a, long b)
      {
         return ( a / GCD( a, b ) ) * b;
      }


   #endregion

   /*STATIC METHODS*/
   #region
   #endregion


   }
}
