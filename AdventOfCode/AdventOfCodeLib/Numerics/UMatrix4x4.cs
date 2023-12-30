using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Numerics
{
   public class UMatrix4x4 : UMatrixBase
   {

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor
      /// </summary>
      public UMatrix4x4( ) : base( 4, 4 )
      {

      }

      public UMatrix4x4( double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23, double m30, double m31, double m32, double m33 ) : base( 4, 4 )
      {
         m_Values[0,0] = m00;
         m_Values[0,1] = m01;
         m_Values[0,2] = m02;
         m_Values[0,3] = m03;

         m_Values[1,0] = m10;
         m_Values[1,1] = m11;
         m_Values[1,2] = m12;
         m_Values[1,3] = m13;

         m_Values[2,0] = m20;
         m_Values[2,1] = m21;
         m_Values[2,2] = m22;
         m_Values[2,3] = m23;

         m_Values[3,0] = m30;
         m_Values[3,1] = m31;
         m_Values[3,2] = m32;
         m_Values[3,3] = m33;
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
      public static UMatrix4x4 operator * ( double a, UMatrix4x4 mat )
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
      public static UMatrix4x4 operator * ( UMatrix4x4 mat, double a )
      {
         return a * mat;
      }


      /// <summary>
      /// Addition operator
      /// </summary>
      /// <param name="lhs">Left hans side matrix</param>
      /// <param name="rhs">Right hand side matrix</param>
      /// <returns></returns>
      public static UMatrix4x4 operator + ( UMatrix4x4 lhs, UMatrix4x4 rhs )
      {
         UMatrix4x4 newMat = new UMatrix4x4( );
         for( int i = 0; i < 4; i++ )
            for( int j = 0; j < 4; j++ )
               newMat[i,j] = lhs[i,j] + rhs[i,j];
         return newMat;
      }

      /// <summary>
      /// Subtraction operator
      /// </summary>
      /// <param name="lhs">Left hand side operator</param>
      /// <param name="rhs">Right hand side operator matrix</param>
      /// <returns></returns>
      public static UMatrix4x4 operator - ( UMatrix4x4 lhs, UMatrix4x4 rhs )
      {
         UMatrix4x4 newMat = new UMatrix4x4( );
         for( int i = 0; i < 4; i++ )
            for( int j = 0; j < 4; j++ )
               newMat[i,j] = lhs[i,j] - rhs[i,j];
         return newMat;
      }


   #endregion

   /*METHODS*/
   #region




   #endregion

   /*STATIC METHODS*/
   #region

      public double[,] GetArray( )
      {
         double[,] ret = new double[4,4];
         for( int i = 0; i< 4; i++ )
            for( int j = 0; j<4; j++ )
               ret[i,j] = this[i,j];
         return ret;
      }


   #endregion



   }
}
