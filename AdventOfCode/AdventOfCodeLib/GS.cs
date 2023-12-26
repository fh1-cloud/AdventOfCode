using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib
{
   public static class GS
   {

   /*MEMBERS*/
   #region
   #endregion
      public static double EPS = 1.0e-8; //!< Numerical accuracy variable. Should be used by mathematical classes when comparing two numbers

   /*CONSTRUCTORS*/
   #region

      static GS( ){ }

   #endregion

   /*STATIC METHODS*/
   #region

      public static bool IsAnyNaN( params double[] vals )
      {
         foreach( double val in vals )
            if( double.IsNaN( val ) )
               return true;
         return false;
      }

      public static bool IsZero( double val )
      {
         return IsZero( val, GS.EPS );
      }

      public static bool IsZero( double val, double tol )
      {
         return Math.Abs( val ) < tol;
      }

      public static bool IsAnyZero( params double[] vals )
      {
         foreach( double val in vals )
            if( IsZero( val ) )
               return true;
         return false;
      }

      public static bool IsAllZero( params double[] vals )
      {
         foreach( double val in vals )
            if( !IsZero( val ) )
               return false;
         return true;
      }

      public static bool Equals( double val1, double val2 )
      {
         return Equals( val1, val2, GS.EPS );
      }


      public static bool Equals( double val1, double val2, double tol )
      {
         return Math.Abs( val1 - val2 ) < tol;
      }

      public static void SetEps( double newEps )
      {
         EPS = newEps;
      }

   #endregion

   }
}
