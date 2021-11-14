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


      /// <summary>
      /// COnverte the number to radians from degrees
      /// </summary>
      /// <param name="d"></param>
      /// <returns></returns>
      public static double ToRad( this double d )
      {
         return d*2.0*Math.PI/360.0;
      }

      /// <summary>
      /// Converte the number to degrees from radians
      /// </summary>
      /// <param name="d"></param>
      /// <returns></returns>
      public static double ToDeg( this double d )
      {
         return d*360.0/(2.0*Math.PI );
      }

      public static bool IsNaN( this double d )
      {
         return double.IsNaN( d );
      }

   #endregion


   }
}
