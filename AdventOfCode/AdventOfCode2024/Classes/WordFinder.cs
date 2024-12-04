using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Classes
{
   internal class WordFinder
   {

      public enum WORDDIRECTION
      {
         RIGHT,
         UPRIGHT,
         UP,
         UPLEFT,
         LEFT,
         DOWNLEFT,
         DOWN,
         DOWNRIGHT,
      }

      //public static string m_WordToFind = "XMAS"; //P1
      public static string m_WordToFind = "MAS"; //P2
      public string[] m_Puzzle = null;
      public int m_RowCount = 0;
      public int m_ColCount = 0;

      public WordFinder( string[] inp )
      {
      //Create array
         m_Puzzle = inp;
         m_RowCount = inp.Length;
         m_ColCount = inp[0].Length;
      }

      public int P1( )
      {
         int nOfInstancesFound = 0;

      //Create a copy of the puzzle to work on
         for( int i = 0; i< m_RowCount; i++ )
         {
            for( int j = 0; j< m_ColCount; j++ )
            {
            //Check for first character, if so, extract substring..
               if( m_Puzzle[i][j] == m_WordToFind[0] )
               {
                  Dictionary<WORDDIRECTION, HashSet<(int,int)>> candidates = new Dictionary<WORDDIRECTION, HashSet<(int, int)>>( );

               //Collect directions to check from this index..
                  HashSet<WORDDIRECTION> directionsToCheck = [.. Enum.GetValues<WORDDIRECTION>( )];
                  if( j > m_ColCount - 1 )
                  {
                     directionsToCheck.Remove( WORDDIRECTION.RIGHT );
                     directionsToCheck.Remove( WORDDIRECTION.UPRIGHT );
                     directionsToCheck.Remove( WORDDIRECTION.DOWNRIGHT );
                  }
                  if( j == 0 )
                  {
                     directionsToCheck.Remove( WORDDIRECTION.LEFT );
                     directionsToCheck.Remove( WORDDIRECTION.DOWNLEFT );
                     directionsToCheck.Remove( WORDDIRECTION.UPLEFT );
                  }
                  if( i > m_RowCount - 1 )
                  {
                     directionsToCheck.Remove( WORDDIRECTION.DOWN );
                     directionsToCheck.Remove( WORDDIRECTION.DOWNRIGHT );
                     directionsToCheck.Remove( WORDDIRECTION.DOWNLEFT );
                  }
                  if( i == 0 )
                  {
                     directionsToCheck.Remove( WORDDIRECTION.UP );
                     directionsToCheck.Remove( WORDDIRECTION.UPRIGHT );
                     directionsToCheck.Remove( WORDDIRECTION.UPLEFT );
                  }

                  foreach( WORDDIRECTION dir in directionsToCheck )
                  {
                     bool foundIt = true;
                     HashSet<(int,int)> indices = new HashSet<(int, int)>( );
                     int iStart = i;
                     int jStart = j;
                     for( int k = 0; k< m_WordToFind.Length; k++ )
                     {
                        indices.Add( ( iStart, jStart ) );
                        if( m_Puzzle[iStart][jStart] != m_WordToFind[k] )
                           foundIt = false;

                        (int, int) idxModifNext = ModifyIdx( dir );
                        iStart += idxModifNext.Item1;
                        jStart += idxModifNext.Item2;
                     }
                     if( foundIt )
                        candidates.Add( dir, indices );
                  }

               //Count candidates and add the locations with solution words in them to the location array for print
                  nOfInstancesFound += candidates.Count;
               }
            }
         }

      //Invert the array so it looks like the solution from the site..
         return nOfInstancesFound;

      }


      public int P2( )
      {
         int nOfInstancesFound = 0;
         for( int i = 1; i< m_RowCount-1; i++ )
         {
            for( int j = 1; j<m_ColCount-1; j++ )
            {
            //Check for first character, if so, extract substring..
               if( m_Puzzle[i][j] == m_WordToFind[1] )
               {
                  HashSet<WORDDIRECTION> candidates = new HashSet<WORDDIRECTION>( );

               //Collect directions to check from this index..
                  foreach( WORDDIRECTION dir in Enum.GetValues( typeof( WORDDIRECTION ) ) )
                  {
                     bool foundIt = true;

                  //Find start point of word for this direction..
                     (int, int) idxModifier = ModifyIdx( dir );

                  //Subtract to find start point
                     int rowLetterIdx = i - idxModifier.Item1;
                     int colLetterIdx = j - idxModifier.Item2;

                  //Loop over characters in word
                     for( int k = 0; k < m_WordToFind.Length; k++ )
                     {
                        if( m_Puzzle[rowLetterIdx][colLetterIdx] != m_WordToFind[k] )
                        {
                           foundIt = false;
                           break;
                        }

                     //Find next character
                        (int, int) idxModifNext = ModifyIdx( dir );
                        rowLetterIdx += idxModifNext.Item1;
                        colLetterIdx += idxModifNext.Item2;
                     }
                     if( foundIt )
                        candidates.Add( dir );
                  }

               //Check if the candidates satisfies a cross
                  if( ( candidates.Contains( WORDDIRECTION.DOWNLEFT ) || candidates.Contains( WORDDIRECTION.UPRIGHT ) ) && ( candidates.Contains( WORDDIRECTION.DOWNRIGHT ) || candidates.Contains( WORDDIRECTION.UPLEFT ) ) )
                     nOfInstancesFound++;

               }
            }
         }
         return nOfInstancesFound;
      }


      /// <summary>
      /// Adjust index based on the word direction and current position
      /// </summary>
      /// <param name="dir"></param>
      /// <returns></returns>
      public static ( int rowAdj, int colAdj ) ModifyIdx( WORDDIRECTION dir )
      {
         int i = 0;
         int j = 0;
         if( dir == WORDDIRECTION.RIGHT || dir == WORDDIRECTION.UPRIGHT || dir == WORDDIRECTION.DOWNRIGHT )
            j = 1;
         else if( dir == WORDDIRECTION.LEFT || dir == WORDDIRECTION.UPLEFT || dir == WORDDIRECTION.DOWNLEFT )
            j = -1;
         if( dir == WORDDIRECTION.DOWN || dir == WORDDIRECTION.DOWNRIGHT || dir == WORDDIRECTION.DOWNLEFT )
            i = 1;
         else if( dir == WORDDIRECTION.UP || dir == WORDDIRECTION.UPRIGHT || dir == WORDDIRECTION.UPLEFT )
            i = -1;

         return ( i, j );
      }
   }

}
