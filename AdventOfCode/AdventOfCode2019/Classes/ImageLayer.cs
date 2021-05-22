using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{
   public class ImageLayer
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected int m_NColumns = -1;
      protected int m_NRows = -1;
      protected long[,] m_Values;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Creates an image layer from a string.
      /// </summary>
      /// <param name="nRows">The number of rows in the layer</param>
      /// <param name="nCols">The number of columns in the layer</param>
      /// <param name="layer">The string for this layer</param>
      public ImageLayer( int nRows, int nCols, string layer )
      {
         m_NColumns = nCols;
         m_NRows = nRows;

         if( layer.Length % nRows != 0 || layer.Length % nCols != 0 )
            throw new Exception();
         
         m_Values = new long[nRows,nCols];

         for( int i = 0; i<nRows; i++ )
         {
            string rowSub = layer.Substring( i*nCols, nCols );
            for( int j = 0; j<nCols; j++ )
            {
               m_Values[i,j] = int.Parse( rowSub[j].ToString( ) );
            }

         }

      }
   #endregion

   /*PROPERTIES*/
   #region
      public int ColumnCount => m_NColumns;
      public int RowCount => m_NRows;

   #endregion

   /*OPERATORS*/
   #region

      /// <summary>
      /// Indexer for setting values.
      /// </summary>
      /// <param name="i"></param>
      /// <param name="j"></param>
      /// <returns></returns>
      public long this[int i, int j ]
      {
         get
         {
            if( i > m_NRows-1 || j > m_NColumns - 1 )
               throw new IndexOutOfRangeException( );
            else
               return m_Values[i,j];
         }
         set
         {
            if( i > m_NRows-1 || j > m_NColumns - 1 )
               throw new IndexOutOfRangeException( );
            else
               m_Values[i,j] = value;
         }
      }


   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Gets the number of occurences of a digit in a certain layer.
      /// </summary>
      /// <param name="digit"></param>
      /// <returns></returns>
      public int CountNumberOfOccurencesOfDigitInLayer( int digit )
      {
         int occurrences = 0;
         for( int i = 0; i<m_NRows; i++ )
            for( int j = 0; j<m_NColumns; j++ )
               if( m_Values[i,j] == digit )
                  occurrences++;
         return occurrences;
      }

      /// <summary>
      /// Prints the current layer to the console.
      /// </summary>
      /// <param name="sep"></param>
      public void PrintLayer( char? sep = null )
      {

         for( int i = 0; i<m_NRows; i++ )
         {
            StringBuilder sb = new StringBuilder();
            for( int j = 0; j<m_NColumns;j++ )
               sb.Append( m_Values[i,j].ToString( ) + ( sep == null ? "" : ( ( char ) sep ).ToString( ) ) ); 

            Console.WriteLine( sb.ToString( ) );
         }
      }
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion



   }
}
