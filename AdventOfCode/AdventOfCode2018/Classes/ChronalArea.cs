using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018.Classes
{
   public class ChronalArea
   {


   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region


      protected static char m_Part2AreaChar = '@';       //The character that is populated as the area if it is part 2.

      protected char[,] m_Area;                 //The underlying area.
      protected int m_OffsetRows = -1;          //The offset size for this array in the row direction
      protected int m_OffsetCols = -1;          //The offset size for this array in the column direction

      protected Dictionary<string,Tuple<char,UVector2D>> m_OriginalPointList = new Dictionary<string, Tuple<char, UVector2D>>( );      //A list of the original points..
      protected Dictionary<char,long> m_Sizes = new Dictionary<char, long>( );   //A dictionary for all the different chars in the grid, with corresponding sizes.
      protected HashSet<char> m_Characters = new HashSet<char>( );      //A hashset of all the unique characters..

   #endregion

   /*CONSTRUCTORS*/
   #region


      /// <summary>
      /// Creates a Chronal area from i list of coordinates. 
      /// </summary>
      /// <param name="coordinates">The coordinates for the different patches as a string array. Values seperated by comma</param>
      /// <param name="rowOffset">The row offset. This is to make sure that the arrays are large enough</param>
      /// <param name="colOffset">The column offset. This is to make sure that the arrays are large enough</param>
      public ChronalArea( string[] coordinates, int rowOffset, int colOffset, bool part2 = false, int p2SumLimit = 32 )
      {

      //Set the offsets. This value may be adjusted.
         m_OffsetRows = rowOffset;
         m_OffsetCols = colOffset;

         int charOffset = ( int ) 'A';

         List<Tuple<char, UVector2D>> pointList = new List<Tuple<char, UVector2D>>( );
         for( int i = 0; i < coordinates.Length; i++ )
         {
            string[ ] split = coordinates[ i ].Split( new char[ ] { ',' } );
            int thisColIdx = int.Parse( split[ 0 ] );
            int thisRowIdx = int.Parse( split[ 1 ] );
            char thisSymbol = ( char ) ( charOffset + i );

         //We set the value as 
            UVector2D thisPoint = new UVector2D( thisColIdx, thisRowIdx );
            pointList.Add( new Tuple<char, UVector2D>( thisSymbol, thisPoint ) );
            m_Characters.Add( thisSymbol );

         }

      //Add symbol if it is part 2.
         if( part2 )
            m_Characters.Add( m_Part2AreaChar );

      //Find the maximum and minimum X or Y coordinate to create the area..
         int maxX = ( int ) pointList.Select( x => x.Item2.X ).ToList( ).Max( );
         int maxY = ( int ) pointList.Select( x => x.Item2.Y ).ToList( ).Max( );
         int maxDim = Math.Max( maxX, maxY );
         m_Area = new char[ maxDim + 1 + 2*m_OffsetRows, maxDim + 1 + 2*m_OffsetCols];

      //Set all the char values to dot..
         for( int i = 0; i < m_Area.GetLength( 0 ); i++ )
            for( int j = 0; j < m_Area.GetLength( 1 ); j++ )
               m_Area[ i, j ] = '.';

      //Loop through all the points and add the char value..
         for( int i = 0; i < pointList.Count; i++ )
         {
            int thisRowIdx = ( int ) pointList[i].Item2.Y + m_OffsetRows;
            int thisColIdx = ( int ) pointList[i].Item2.X + m_OffsetCols;

         //Create the ID to check for unique points..
            string id = GetID( thisRowIdx, thisColIdx );
            UVector2D orgPoint = new UVector2D( thisColIdx, thisRowIdx );
            m_OriginalPointList.Add( id, new Tuple<char, UVector2D>( pointList[i].Item1, orgPoint ) );

         //Add the char value..
            m_Area[ thisRowIdx, thisColIdx ] = pointList[i].Item1;
         }

      //populate the area..
         if( !part2 )
         {
            PopulatePart1( );

         }
         else
         {
            PopulatePart2( p2SumLimit );
         }

         //Count the number of entries for each original point..
            PopulateSizes( );

      }
   #endregion

   /*PROPERTIES*/
   #region


      /// <summary>
      /// Gets the size for part 2
      /// </summary>
      public long Part2Size
      {
         get
         {
            return m_Sizes[m_Part2AreaChar];
         }
      }

      /// <summary>
      /// Gets all the unique characters in this array.
      /// </summary>
      public HashSet<char> UniqueCharacters
      {
         get
         {
            return m_Characters;
         }
      }
      
      /// <summary>
      /// Gets the area sizes for this
      /// </summary>
      public Dictionary<char, long> AreaSizes
      {
         get
         {
            return m_Sizes;
         }
      }

   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region


      /// <summary>
      /// Populate the size arrays..
      /// </summary>
      private void PopulateSizes( )
      {

      //Loop over all the different symbols and count..
         foreach( char c in m_Characters )
         {
            long cCount = 0;
            for( int rowIdx = 0; rowIdx < m_Area.GetLength( 0 ); rowIdx++ )
               for( int colIdx = 0; colIdx < m_Area.GetLength( 1 ); colIdx++ )
                  if( c.Equals( m_Area[rowIdx, colIdx ] ) )
                     cCount++;

            m_Sizes.Add( c, cCount );
         }

      }

      /// <summary>
      /// Gets the ID for this row and this column
      /// </summary>
      /// <param name="rowIdx"></param>
      /// <param name="colIdx"></param>
      /// <returns></returns>
      private string GetID( int rowIdx, int colIdx )
      {
         return "R" + rowIdx + "_C" + colIdx;
      }


      /// <summary>
      /// Populates the area for part 2.
      /// </summary>
      /// <param name="sumLimit"></param>
      private void PopulatePart2( long sumLimit )
      {
      //Loop through all the point in the array and create the patch..
         for( int rowIdx = 0; rowIdx < m_Area.GetLength( 0 ); rowIdx++ )
         {
            for( int colIdx = 0; colIdx < m_Area.GetLength( 1 ); colIdx++ )
            {
            //Create a vector for this point..
               UVector2D thisPoint = new UVector2D( colIdx, rowIdx );

            //Declare the manhattan sum..
               long manhattanSum = 0;

               bool populateThisEntry = true;
               foreach( KeyValuePair<string, Tuple<char, UVector2D>> k in m_OriginalPointList )
               {
                  UVector2D distVector = new UVector2D( thisPoint.X - k.Value.Item2.X, thisPoint.Y - k.Value.Item2.Y );
                  int thisDistance = ( int ) distVector.GetManhattanLength( );

                  manhattanSum += thisDistance;
                  if( manhattanSum >= sumLimit )
                  {
                     populateThisEntry = false;
                     break;
                  }
               }

            //Check if this should be added..
               if( populateThisEntry )
                  m_Area[rowIdx,colIdx] = m_Part2AreaChar;
            }

         }


      }

      /// <summary>
      /// Populates the array..
      /// </summary>
      private void PopulatePart1( )
      {
         for( int rowIdx = 0; rowIdx < m_Area.GetLength( 0 ); rowIdx++ )
         {
            for( int colIdx = 0; colIdx < m_Area.GetLength( 1 ); colIdx++ )
            {

            //Get the ID for this placement..
               string thisID = GetID( rowIdx, colIdx );

               UVector2D thisPoint = new UVector2D( colIdx, rowIdx );

            //If this is an original point. continue..
               if( m_OriginalPointList.ContainsKey( thisID ) )
                  continue;
               
            //If the code reached this point, we know that it is not an original point. Then check distances..
               List<Tuple<char,int>> idWithDistances = new List<Tuple<char, int>>( );
               foreach( KeyValuePair<string, Tuple<char, UVector2D>> k in m_OriginalPointList )
               {
                  UVector2D distVector = new UVector2D( thisPoint.X - k.Value.Item2.X, thisPoint.Y - k.Value.Item2.Y );
                  int thisDistance = ( int ) distVector.GetManhattanLength( );
                  idWithDistances.Add( new Tuple<char, int>( k.Value.Item1, thisDistance ) );
               }

            //Find the entries with the least distance..
               int minDist = idWithDistances.Select( x => x.Item2 ).ToList( ).Min( );
               List<Tuple<char, int>> minDistVals = idWithDistances.Where( x => x.Item2 == minDist ).ToList( );
               if( minDistVals.Count == 1 )
                  m_Area[rowIdx, colIdx] = minDistVals[0].Item1;
            }
         }
      }


      /// <summary>
      /// Gets the array printed as a string.
      /// </summary>
      /// <returns></returns>
      public string GetArray( )
      {
         StringBuilder sb = new StringBuilder( );
         for( int i = 0; i < m_Area.GetLength( 0 ); i++ )
         {
            for( int j = 0; j < m_Area.GetLength( 1 ); j++ )
               sb.Append( m_Area[ i, j ] );

            sb.Append( "\n" );
         }
         return sb.ToString( );
      }

   #endregion

   /*STATIC METHODS*/
   #region
   #endregion



   }
}
