using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib
{
   public static class UMath
   {

   /*MEMBERS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region

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


      /// <summary>
      /// Gets the greates common factor of two whole numbers
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static long GCD(long a, long b )
      {
         while( b != 0 )
         {
            long temp = b;
            b = a % b;
            a = temp;
         }
         return a;
      }



      /// <summary>
      /// Performs a gauss elinimation on the input coefficients and the right hand side.
      /// </summary>
      /// <param name="matrix"></param>
      /// <param name="rhs"></param>
      public static void GaussElimination( double[,] matrix, double[] rhs )
      {
         int nOfVariables = matrix.GetLength( 0 );
         for( int i = 0; i< nOfVariables; i++ )
         {
         //Select pivot
            double pivot = matrix[i, i];

         //Normalize row i
            for( int j = 0; j<nOfVariables; j++ )
               matrix[i,j] = matrix[i,j]/pivot;

         //Normalize rhs
            rhs[i] = rhs[i]/pivot;

         //Sweep using row i
            for( int k = 0; k<nOfVariables; k++ )
            {
               if( k != i )
               {
                  double factor = matrix[k,i];

                  for( int j = 0; j<nOfVariables; j++ )
                  {
                     matrix[k,j] = matrix[k,j] - factor*matrix[i,j];
                  }
                  rhs[k] = rhs[k] - factor*rhs[i];
               }
            }
         }

      }


   #endregion


   }
}
