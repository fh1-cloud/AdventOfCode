using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Numerics
{
   public abstract class UMatrixBase
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected double[,] m_Values;

   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor
      /// </summary>
      /// <param name="rows"></param>
      /// <param name="cols"></param>
      protected UMatrixBase( int rows, int cols )
      {
         m_Values = new double[rows,cols];
         for( int i = 0; i < rows; i++ )
            for( int j = 0; j < cols; j++ )
               m_Values[i,j] = 0.0;
      }

      /// <summary>
      /// Copy constructor..
      /// </summary>
      /// <param name="oldMat"></param>
      public UMatrixBase( UMatrixBase oldMat )
      {
         m_Values = new double[ oldMat.RowCount, oldMat.ColCount ];
         for( int i = 0; i < oldMat.RowCount; i++ )
            for( int j = 0; j < oldMat.ColCount; j++ )
               m_Values[i,j] = oldMat[i,j];
      }

   #endregion

   /*PROPERTIES*/
   #region


      /// <summary>
      /// Gets the row dimension of this matrix
      /// </summary>
      public int RowCount
      {
         get
         {
            return m_Values.GetLength( 0 );
         }
      }

      /// <summary>
      /// Gets the column dimension of this matrix
      /// </summary>
      public int ColCount
      {
         get
         {
            return m_Values.GetLength( 1 );
         }
      }

   #endregion

   /*OPERATORS*/
   #region


      /// <summary>
      /// Get or set accessor for the indexers..
      /// </summary>
      /// <param name="i"></param>
      /// <param name="j"></param>
      /// <returns></returns>
      public double this[ int i, int j]
      {
         get
         {
            if( i < 0 || i > m_Values.GetLength( 0 ) || j < 0 || j > m_Values.GetLength( 1 ) )
               throw new Exception( );
            else
               return m_Values[i, j];
         }
         set
         {
            if( i < 0 || i > m_Values.GetLength( 0 ) || j < 0 || j > m_Values.GetLength( 1 ) )
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


   #endregion


   }
}
