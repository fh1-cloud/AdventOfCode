using AdventOfCodeLib;
using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdventOfCode2023.Classes
{
   public class HailStone
   {

   /*CONSTRUCTORS*/
   #region
      public HailStone( string line, bool ignoreZ = false ) 
      { 
         string[] spla = line.Split( new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries );
         string[] pS = spla[0].Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
         string[] vS = spla[1].Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
         this.X = decimal.Parse( pS[0] );
         this.Y = decimal.Parse( pS[1] );
         this.Z = decimal.Parse( pS[2] );
         this.U = decimal.Parse( vS[0] );
         this.V = decimal.Parse( vS[1] );
         this.W = decimal.Parse( vS[2] );
      }
   #endregion

   /*PROPERTIES*/
   #region
      public decimal X { get; private set; }
      public decimal Y { get; private set; }
      public decimal Z { get; private set; }
      public decimal U { get; private set; }
      public decimal V { get; private set; }
      public decimal W { get; private set; }
   #endregion

   /*STATIC METHODS*/
   #region

      public static BigInteger FindMagicVector( List<HailStone> st )
      {
      //Find x and y by gauss elimination of the 4 first hailstones
         decimal[] rhs = new decimal[4];
         decimal[,] aMat = new decimal[4,4];
         for( int i = 0; i<4; i++ )
         {
            aMat[i, 0] = st[i + 1].V - st[i].V;
            aMat[i, 1] = st[i].U - st[i + 1].U;
            aMat[i, 2] = st[i].Y - st[i + 1].Y;
            aMat[i, 3] = st[i + 1].X - st[i].X;
            rhs[i] = st[i].U*st[i].Y - st[i].X*st[i].V - st[i+1].U*st[i+1].Y + st[i+1].X*st[i+1].V;
         }
         GaussElimination( aMat, rhs );

      //Find z by inserting the answer for x y u v in the first two hailstones
         decimal a = ( rhs[0] - st[0].X ) / ( st[0].U - rhs[2] );
         decimal b = ( rhs[1] - st[1].Y ) / ( st[1].V - rhs[3] );
         decimal[ , ] zMat = new decimal[2, 2] { { 1.0M, a }, { 1.0M, b } };
         decimal[ ] zrhs = new decimal[2] { st[0].Z + a * st[0].W, st[1].Z + b * st[1].W };
         decimal fac = 1.0M / ( zMat[0, 0] * zMat[1, 1] - zMat[0, 1] * zMat[1, 0] );
         decimal[ , ] zInv = new decimal[2, 2] { { fac * zMat[1, 1], fac * -1.0M * zMat[0, 1] }, { fac * -1.0M * zMat[1, 0], fac * zMat[1, 1] } };
         decimal[ ] zAn = new decimal[2] { zInv[0, 0] * zrhs[0] + zInv[0, 1] * zrhs[1], zInv[1, 0] * zrhs[0] + zInv[1, 1] * zrhs[1] };

      //Round answers to nearest whole number
         List<BigInteger> xyz = new List<BigInteger> { ( BigInteger ) ( Math.Round( rhs[0] ) ), ( BigInteger ) ( Math.Round( rhs[1] ) ), ( BigInteger ) ( Math.Round( zAn[0] ) ) };

      //Return sum of coordinates
         return xyz[0] + xyz[1] + xyz[2];
      }

      /// <summary>
      /// Performs a gauss elinimation on the input coefficients and the right hand side.
      /// </summary>
      /// <param name="matrix"></param>
      /// <param name="rhs"></param>
      public static void GaussElimination( decimal[,] matrix, decimal[] rhs )
      {
         int nOfVariables = matrix.GetLength( 0 );
         for( int i = 0; i< nOfVariables; i++ )
         {
         //Select pivot
            decimal pivot = matrix[i, i];

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
                  decimal factor = matrix[k,i];
                  for( int j = 0; j<nOfVariables; j++ )
                     matrix[k,j] = matrix[k,j] - factor*matrix[i,j];
                  rhs[k] = rhs[k] - factor*rhs[i];
               }
            }
         }

      }

   #endregion

   }
}
