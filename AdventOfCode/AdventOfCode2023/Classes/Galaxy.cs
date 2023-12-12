using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeLib.Numerics;

namespace AdventOfCode2023.Classes
{
   public class Galaxy
   {
   /*MEMBERS*/
   #region
      protected long m_RowIdx = -1;
      protected long m_ColIdx = -1;
      protected long m_ID = -1;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public Galaxy( long rowIdx, long colIdx, long id )
      {
         m_RowIdx = rowIdx;
         m_ColIdx = colIdx;
         m_ID = id;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public long RowIdx { get { return m_RowIdx; } set { m_RowIdx = value; } }
      public long ColIdx { get { return m_ColIdx; } set { m_ColIdx = value; } }
      public long ID { get { return m_ID; } }
   #endregion

   /*STATIC METHODS*/
   #region
      public static long GetManhattanDistance( Galaxy g1, Galaxy g2 )
      {
         UVector2D vec = new UVector2D( new UVector2D( g1.ColIdx, g1.RowIdx ), new UVector2D( g2.ColIdx, g2.RowIdx ) );
         return ( long ) vec.GetManhattanLength( );
      }

      public static void PrintGalaxy( HashSet<Galaxy> galaxies )
      {
         long maxRowIdx = 0;
         long maxColIdx = 0;
         foreach( Galaxy g in galaxies )
         {
            if( g.RowIdx > maxRowIdx )
               maxRowIdx = g.RowIdx;
            if( g.ColIdx > maxColIdx )
               maxColIdx = g.ColIdx;
         }

         char[,] canvas = new char[maxRowIdx+1, maxColIdx+1];
         for( int i = 0; i<maxRowIdx+1; i++ )
            for( int j = 0; j<maxColIdx+1; j++ )
               canvas[i,j] = '.';

         foreach( Galaxy g in galaxies )
            canvas[g.RowIdx,g.ColIdx] = '#';

         for( int i = 0; i < canvas.GetLength( 0 ); i++ )
         {
            StringBuilder sb = new StringBuilder( );
            for( int j = 0; j < canvas.GetLength( 1 ); j++ )
               sb.Append( canvas[i,j].ToString( ) );
            Console.WriteLine( sb.ToString( ) );
         }
      }

      public static HashSet<Galaxy> GetAllGalaxies( string[] inp, long totalExpansionWidth )
      {
         totalExpansionWidth--;
         List<int> rowIdxWithExpansion = new List<int>( );
         List<int> colIdxWithExpansion = new List<int>( );
         for( int rowIdx = 0; rowIdx < inp.Length; rowIdx++ )
         {
         //Check if this row contains only periods..
            bool foundGalaxyOnRow = false;
            for( int colIdx = 0; colIdx< inp[rowIdx].Length; colIdx++ )
            {
               if( inp[rowIdx][colIdx] == '#' )
               {
                  foundGalaxyOnRow = true;
                  break;
               }
            }
            if( !foundGalaxyOnRow ) 
               rowIdxWithExpansion.Add( rowIdx );

         }
         for( int colIdx = 0; colIdx < inp[0].Length; colIdx++ )
         {
         //Check if this column only contains columns..
            bool foundGalaxyOnColumn = false;
            for( int rowIdx = 0; rowIdx < inp.Length; rowIdx++ )
            {
               if( inp[rowIdx][colIdx] == '#' )
               {
                  foundGalaxyOnColumn = true;
                  break;
               }
            }
            if( !foundGalaxyOnColumn ) 
               colIdxWithExpansion.Add( colIdx );
         }

      //Create all the galaxies and adjust the row and column indexes..
         HashSet<Galaxy> allGalaxies = new HashSet<Galaxy>( );
         int id = 0;
         for( int rowIdx = 0; rowIdx < inp.Length; rowIdx++ )
         {
            for( int colIdx = 0; colIdx< inp[rowIdx].Length; colIdx++ )
            {
               if( inp[rowIdx][colIdx] == '#' )
               {
                  long newRowIdx = rowIdx;
                  foreach( int i in rowIdxWithExpansion )
                     if( i <= rowIdx )
                        newRowIdx = newRowIdx + totalExpansionWidth;
                  long newColIdx = colIdx;
                  foreach( int i in colIdxWithExpansion )
                     if( i <= colIdx )
                        newColIdx = newColIdx + totalExpansionWidth;
                  allGalaxies.Add( new Galaxy( newRowIdx, newColIdx, id++ ) );
               }
            }
         }
         return allGalaxies;

      }

   #endregion
   }
}
