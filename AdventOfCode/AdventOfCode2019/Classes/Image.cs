using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{


   /// <summary>
   /// AoC2019 Day 8
   /// </summary>
   public class Image
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected Dictionary<long,ImageLayer> m_Layers = new Dictionary<long, ImageLayer>( );    //The layers for this image.
      protected int m_ColumnCount = -1;
      protected int m_RowCount = -1;

      protected static char[,] m_ColorMap = new char[3,2]{ {'0', ' '},{'1', (char)(2)}, {'2','X'} };
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Creates an image from a string.
      /// </summary>
      /// <param name="nrows">The number of rows</param>
      /// <param name="ncols">The number of columns</param>
      /// <param name="wholeImage">The string for the whole image.</param>
      public Image( int nrows, int ncols, string wholeImage )
      {
         m_ColumnCount = ncols;
         m_RowCount = nrows;

      //Split the string into parts and create the layers.
         long layerNumber = 1;
         string restString = wholeImage;
         int layerStringLength = nrows*ncols;
         while( true )
         {

         //Create the layer substring.
            string newSubstr = restString.Substring( 0, layerStringLength );

         //Create the new layer.
            ImageLayer newLayer = new ImageLayer( nrows, ncols, newSubstr );

         //Add the layer to the dictionary with the number as key
            m_Layers.Add( layerNumber, newLayer );
            layerNumber++;

         //Find the new reststring.
            if( restString.Length == layerStringLength )
            {
               break;
            }
            else
            {
               restString = restString.Substring( layerStringLength, restString.Length-layerStringLength );
            }
         }

      }


   #endregion

   /*PROPERTIES*/
   #region

      public Dictionary<long,ImageLayer> Layers => m_Layers;
      public int LayerCount => m_Layers.Count;
      public int ColumnCount => m_ColumnCount;
      public int RowCount => m_RowCount;
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Prints the whole image..
      /// </summary>
      /// <param name="sep"></param>
      public void PrintImage( char? sep = null )
      {
         for( int i = 0; i<m_RowCount; i++ )
         {
            StringBuilder sb = new StringBuilder();
            for( int j = 0; j<m_ColumnCount;j++ )
               sb.Append( GetSymbolFromNumber( GetColorForPosition( i,j ) ).ToString( ) + ( sep == null ? "" : ( ( char ) sep ).ToString( ) ) );

            Console.WriteLine( sb.ToString( ) );
         }
      }

      /// <summary>
      /// Gets the color for the current position.
      /// </summary>
      /// <param name="rowIdx"></param>
      /// <param name="colIdx"></param>
      /// <returns></returns>
      private long GetColorForPosition( int rowIdx, int colIdx )
      {
      //Checks the layer from top to bottom and returns the visible pixel
         for( int i = 0; i<m_Layers.Count; i++ )
         {
            int layerNumber = i+1;
            ImageLayer thisLayer = m_Layers[layerNumber];
            if( thisLayer[rowIdx, colIdx] != 2 )
               return thisLayer[rowIdx,colIdx];
         }

      //If the code reached this point, it is invisible all the way to the bottom. Then return th pixel of the bottom layer.
         return m_Layers[m_Layers.Count][rowIdx,colIdx];
      }



   #endregion

   /*STATIC METHODS*/
   #region


      /// <summary>
      /// Gets the symbol for a number for decoding an image.
      /// </summary>
      /// <param name="num"></param>
      /// <returns></returns>
      public static char GetSymbolFromNumber( long num )
      {
         if( num.ToString( )[0] == m_ColorMap[0,0] )
            return m_ColorMap[0,1];
         else if( num.ToString( )[0] == m_ColorMap[1,0] )
            return m_ColorMap[1,1];
         else if( num.ToString( )[0] == m_ColorMap[2,0] )
            return m_ColorMap[2,1];
         else
            throw new Exception( );
      }


   #endregion


   }
}
