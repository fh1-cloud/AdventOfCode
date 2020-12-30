using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020.Numerics
{
   public class Matrix2x2
   {




   /*MEMBERS*/
      #region

      protected double[,] m_Values = new double[2, 2];

      #endregion

   /*CONSTRUCTORS*/
      #region


      /// <summary>
      /// Set constructor for the individual matrix values
      /// </summary>
      /// <param name="m00"></param>
      /// <param name="m01"></param>
      /// <param name="m10"></param>
      /// <param name="m11"></param>
      public Matrix2x2( double m00, double m01, double m10, double m11 )
      {
         m_Values[0, 0] = m00;
         m_Values[0, 1] = m01;
         m_Values[1, 0] = m10;
         m_Values[1, 1] = m11;
      }


      /// <summary>
      /// Default constructor. everything is 0.0
      /// </summary>
      public Matrix2x2( )
      {
         for( int i = 0; i < 2; i++ )
            for( int j = 0; j < 2; j++ )
               m_Values[i, j] = 0.0;
      }


      /// <summary>
      /// Copy constryctor
      /// </summary>
      /// <param name="old"></param>
      public Matrix2x2( Matrix2x2 old )
      {
         for( int i = 0; i < 2; i++ )
            for( int j = 0; j < 2; j++ )
               m_Values[i,j] = old.m_Values[i, j];
      }
      #endregion

   /*OPERATORS*/
      #region

      public double this[int i, int j]
      {
         get
         {
            if( i < 0 || i > 2 || j < 0 || j > 2 )
               throw new Exception( );
            else
               return m_Values[i, j];
         }
         set
         {
            if( i < 0 || i > 2 || j < 0 || j > 2 )
               throw new Exception( );
            else
               m_Values[i, j] = value;
         }
      }

      #endregion

   /*METHODS*/
      #region

      #endregion


   /*STATIC METHODS*/
      #region

      /// <summary>
      /// Gets a two dimensional rotation matrix. Input in radians
      /// </summary>
      /// <param name="angle"></param>
      /// <returns></returns>
      public static Matrix2x2 GetRotationMatrix( double anglerad )
      {
         Matrix2x2 newMat = new Matrix2x2( );
         newMat[0, 0] = Math.Cos( anglerad );
         newMat[0, 1] = -Math.Sin( anglerad );
         newMat[1, 0] = Math.Sin( anglerad );
         newMat[1, 1] = Math.Cos( anglerad );
         return newMat;
      }

      #endregion



   }
}
