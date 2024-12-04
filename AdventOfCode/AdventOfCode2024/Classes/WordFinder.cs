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
         m_Puzzle = new string[inp.Length];

         for( int i = 0; i< inp.Length; i++ )
            m_Puzzle[i] = inp[i];

         m_RowCount = inp.Length;
         m_ColCount = inp[0].Length;
      }

      public int P1( )
      {
         char[,] puzzleCopy = new char[m_Puzzle.Length, m_Puzzle[0].Length];
         for( int i = 0; i<m_RowCount; i++ )
            for( int j = 0; j<m_RowCount; j++ )
               puzzleCopy[i,j] = m_Puzzle[i][j];
         int nOfInstancesFound = 0;
         HashSet<(int, int)> locationsWithWords = new HashSet<(int, int)>( );

      //Create a copy of the puzzle to work on
         for( int i = 0; i< m_RowCount; i++ )
         {

            for( int j = 0; j< m_ColCount; j++ )
            {
            //Check for first character, if so, extract substring..
               if( puzzleCopy[i,j] == m_WordToFind[1] )
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
                        if( puzzleCopy[iStart,jStart] != m_WordToFind[k] )
                           foundIt = false;
                        AdjustIndices( ref iStart, ref jStart, dir );
                     }
                     if( foundIt )
                        candidates.Add( dir, indices );
                  }

               //Count candidates and add the locations with solution words in them to the location array for print
                  nOfInstancesFound += candidates.Count;
                  foreach( KeyValuePair<WORDDIRECTION, HashSet<(int, int)>> kvp in candidates )
                     foreach( (int, int) v in kvp.Value )
                        locationsWithWords.Add( v );
               }
            }
         }

      //Invert the array so it looks like the solution from the site..
         char[,] solution = Invert( locationsWithWords );
         GlobalMethods.PrintCharArray( solution );
         return nOfInstancesFound;

      }

      public char[,] Invert( HashSet<(int, int)> locationsWithWords )
      {
         char[ , ] sol = new char[m_RowCount, m_ColCount];
         for( int i = 0; i<m_RowCount; i++ )
         {
            for( int j = 0; j<m_ColCount; j++ )
            {
               if( locationsWithWords.Contains( ( i, j ) ) )
                  sol[ i, j ] = m_Puzzle[i][j];
               else
                  sol[ i, j ] = '.';
            }
         }
         return sol;
      }


      public int P2( )
      {
         char[,] puzzleCopy = new char[m_Puzzle.Length, m_Puzzle[0].Length];

         for( int i = 0; i<m_RowCount; i++ )
            for( int j = 0; j<m_RowCount; j++ )
               puzzleCopy[i,j] = m_Puzzle[i][j];
         HashSet<(int, int)> locationsWithWords = new HashSet<(int, int)>( );
         int nOfInstancesFound = 0;
         for( int i = 1; i< m_RowCount-1; i++ )
         {

            for( int j = 1; j<m_ColCount-1; j++ )
            {
            //Check for first character, if so, extract substring..
               if( puzzleCopy[i,j] == m_WordToFind[1] )
               {
                  Dictionary<WORDDIRECTION, HashSet<(int,int)>> candidates = new Dictionary<WORDDIRECTION, HashSet<(int, int)>>( );

               //Collect directions to check from this index..
                  HashSet<WORDDIRECTION> directionsToCheck = [.. Enum.GetValues<WORDDIRECTION>( )];
                  foreach( WORDDIRECTION dir in directionsToCheck )
                  {
                     bool foundIt = true;
                     HashSet<(int,int)> indices = new HashSet<(int, int)>( );
                     int iStart = i;
                     int jStart = j;
                     PreAdjustIndices( ref iStart, ref jStart, dir );
                     for( int k = 0; k< m_WordToFind.Length; k++ )
                     {
                        indices.Add( ( iStart, jStart ) );
                        if( puzzleCopy[iStart,jStart] != m_WordToFind[k] )
                           foundIt = false;
                        AdjustIndices( ref iStart, ref jStart, dir );
                     }
                     if( foundIt )
                        candidates.Add( dir, indices );
                  }

               //Check if the directions match up..
                  bool containsRegularCross = ( candidates.ContainsKey( WORDDIRECTION.LEFT ) || candidates.ContainsKey( WORDDIRECTION.RIGHT ) ) && ( candidates.ContainsKey( WORDDIRECTION.UP ) || candidates.ContainsKey( WORDDIRECTION.DOWN ) );

                  if( containsRegularCross )
                  {
                     nOfInstancesFound++;
                     locationsWithWords.Add( ( i, j ) );
                     locationsWithWords.Add( ( i, j-1 ) );
                     locationsWithWords.Add( ( i, j+1 ) );
                     locationsWithWords.Add( ( i-1, j ) );
                     locationsWithWords.Add( ( i+1, j ) );
                  }

                  bool containsSkewCross = ( candidates.ContainsKey( WORDDIRECTION.DOWNLEFT ) || candidates.ContainsKey( WORDDIRECTION.UPRIGHT ) ) && ( candidates.ContainsKey( WORDDIRECTION.DOWNRIGHT ) || candidates.ContainsKey( WORDDIRECTION.UPLEFT ) );
                  if( containsSkewCross )
                  {
                     nOfInstancesFound++;
                     locationsWithWords.Add( ( i, j ) );
                     locationsWithWords.Add( ( i-1, j-1 ) );
                     locationsWithWords.Add( ( i-1, j+1 ) );
                     locationsWithWords.Add( ( i+1, j-1 ) );
                     locationsWithWords.Add( ( i+1, j+1 ) );
                  }
               }
            }
         }

      //Invert the array so it looks like the solution from the site..
         char[,] solution = Invert( locationsWithWords );
         GlobalMethods.PrintCharArray( solution );
         return nOfInstancesFound;

      }

      public static void PreAdjustIndices( ref int i, ref int j, WORDDIRECTION dir )
      {
         if( dir == WORDDIRECTION.RIGHT )
            j--;
         else if( dir == WORDDIRECTION.LEFT )
            j++;
         else if( dir == WORDDIRECTION.UP )
            i++;
         else if( dir == WORDDIRECTION.DOWN )
            i--;
         else if( dir == WORDDIRECTION.UPRIGHT )
         {
            j--;
            i++;
         }
         else if( dir == WORDDIRECTION.DOWNRIGHT )
         {
            i--;
            j--;
         }
         else if( dir == WORDDIRECTION.UPLEFT )
         {
            i++;
            j++;
         }
         else if( dir == WORDDIRECTION.DOWNLEFT )
         {
            i--;
            j++;
         }
      }

      public static void AdjustIndices( ref int i, ref int j, WORDDIRECTION dir )
      {
         if( dir == WORDDIRECTION.RIGHT )
            j++;
         else if( dir == WORDDIRECTION.LEFT )
            j--;
         else if( dir == WORDDIRECTION.UP )
            i--;
         else if( dir == WORDDIRECTION.DOWN )
            i++;
         else if( dir == WORDDIRECTION.UPRIGHT )
         {
            i--;
            j++;
         }
         else if( dir == WORDDIRECTION.DOWNRIGHT )
         {
            i++;
            j++;
         }
         else if( dir == WORDDIRECTION.UPLEFT )
         {
            i--;
            j--;
         }
         else if( dir == WORDDIRECTION.DOWNLEFT )
         {
            i++;
            j--;
         }
      }


   }
}
