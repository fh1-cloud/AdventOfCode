using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Extensions
{

   /// <summary>
   /// Extension methods for the double class
   /// </summary>
   public static class EDouble
   {

   /*METHODS*/
   #region

      /// <summary>
      /// Checks if a number is zero or not.
      /// </summary>
      /// <param name="d"></param>
      /// <param name="eps"></param>
      /// <returns></returns>
      public static bool IsZero( this double d, double eps = 1.0e-8 )
      {
         if( Math.Abs( d ) < eps )
            return true;
         else
            return false;
      }

   #endregion


   }
}
