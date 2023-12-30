using AdventOfCodeLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Numerics
{
   public class UMatrix2x2 : UMatrixBase
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

      /// <summary>
      /// Default constructor
      /// </summary>
      public UMatrix2x2( ) : base( 2, 2 )
      {

      }

      /// <summary>
      /// Set constructor for setting all four entries of the matrix
      /// </summary>
      /// <param name="m00">Entry 0,0</param>
      /// <param name="m01">Entry 1,0</param>
      /// <param name="m10">Entry 0,1</param>
      /// <param name="m11">Entry 1,1</param>
      public UMatrix2x2( double m00, double m01, double m10, double m11 ) : base( 2, 2 )
      {
         m_Values[0,0] = m00;
         m_Values[1,0] = m10;
         m_Values[0,1] = m01;
         m_Values[1,1] = m11;
      }


   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region


      /// <summary>
      /// matrix premultiplixation
      /// </summary>
      /// <param name="a">Constant to be multiplied</param>
      /// <param name="mat">The matrix</param>
      /// <returns></returns>
      public static UMatrix2x2 operator * ( double a, UMatrix2x2 mat )
      {
         for( int i = 0; i < mat.RowCount; i++ )
            for( int j = 0; j < mat.ColCount; j++ )
               mat[i,j] = a * mat[i,j];
         return mat;
      }

      /// <summary>
      /// Post multiplication with a constant
      /// </summary>
      /// <param name="mat">The matrix to be multiplied</param>
      /// <param name="a">The constant</param>
      /// <returns></returns>
      public static UMatrix2x2 operator * ( UMatrix2x2 mat, double a )
      {
         return a * mat;
      }


      /// <summary>
      /// Addition operator
      /// </summary>
      /// <param name="lhs">Left hans side matrix</param>
      /// <param name="rhs">Right hand side matrix</param>
      /// <returns></returns>
      public static UMatrix2x2 operator + ( UMatrix2x2 lhs, UMatrix2x2 rhs )
      {
         UMatrix2x2 newMat = new UMatrix2x2( );
         for( int i = 0; i < 2; i++ )
            for( int j = 0; j < 2; j++ )
               newMat[i,j] = lhs[i,j] + rhs[i,j];
         return newMat;
      }

      /// <summary>
      /// Subtraction operator
      /// </summary>
      /// <param name="lhs">Left hand side operator</param>
      /// <param name="rhs">Right hand side operator matrix</param>
      /// <returns></returns>
      public static UMatrix2x2 operator - ( UMatrix2x2 lhs, UMatrix2x2 rhs )
      {
         UMatrix2x2 newMat = new UMatrix2x2( );
         for( int i = 0; i < 2; i++ )
            for( int j = 0; j < 2; j++ )
               newMat[i,j] = lhs[i,j] - rhs[i,j];
         return newMat;
      }

      /// <summary>
      /// Multiplication between two 2x2 matrices.
      /// </summary>
      /// <param name="lhs">The left hand side matrix</param>
      /// <param name="rhs">The right hand side matrix</param>
      /// <returns></returns>
      public static UMatrix2x2 operator * ( UMatrix2x2 lhs, UMatrix2x2 rhs )
      {
         UMatrix2x2 newMat = new UMatrix2x2( );
         newMat[0,0] = lhs[0,0]*rhs[0,0] + lhs[0,1]*rhs[1,0];
         newMat[1,0] = lhs[1,0]*rhs[0,0] + lhs[1,1]*rhs[1,0];
         newMat[0,1] = lhs[0,0]*rhs[0,1] + lhs[0,1]*rhs[1,1];
         newMat[1,1] = lhs[1,0]*rhs[0,1] + lhs[1,1]*rhs[1,1];
         return newMat;
      }


   #endregion

   /*METHODS*/
   #region



      /// <summary>
      /// Gets the determinant of this matrix
      /// </summary>
      /// <returns></returns>
      public double Determinant( )
      {
         return m_Values[0,0]*m_Values[1,1] - m_Values[0,1]*m_Values[1,0];
      }


      /// <summary>
      /// Gets the inverse of this 2x2 matrix
      /// </summary>
      /// <returns></returns>
      public UMatrix2x2 GetInverse( )
      {

         double det = this.Determinant( ); //Determinant is zero
         if( det.IsZero( ) )
            return null;

         double fac = 1.0/this.Determinant( );
         UMatrix2x2 ret = new UMatrix2x2( m_Values[1,1], -m_Values[0,1], -m_Values[1,0], m_Values[0,0] );
         return fac * ret;
      }

      /// <summary>
      /// Transposes this matrix.
      /// </summary>
      /// <returns></returns>
      public UMatrix2x2 GetTransposed( )
      {
         return new UMatrix2x2( this[0,0], this[1,0], this[0,1], this[1,1] );
      }


   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// Gets a two dimensional rotation matrix. Input in radians
      /// </summary>
      /// <param name="angle"></param>
      /// <returns></returns>
      public static UMatrix2x2 GetRotationMatrix( double anglerad )
      {
         UMatrix2x2 newMat = new UMatrix2x2( );
         newMat[0, 0] = Math.Cos( anglerad );
         newMat[0, 1] = -Math.Sin( anglerad );
         newMat[1, 0] = Math.Sin( anglerad );
         newMat[1, 1] = Math.Cos( anglerad );
         return newMat;
      }
   #endregion


   }
}
