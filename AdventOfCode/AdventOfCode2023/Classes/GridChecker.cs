using AdventOfCodeLib.Classes;
using AdventOfCodeLib.Extensions;
using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class GridChecker
   {

      public class HashSetIntegerPairWithPotentialGears
      {

         public HashSet<IntegerPairWithPotentialGears> m_IntegerPairs = new HashSet<IntegerPairWithPotentialGears>( );
         public HashSetIntegerPairWithPotentialGears( ) { }
         public HashSet<IntegerPairWithPotentialGears> Pairs { get { return m_IntegerPairs; } }

         public void Add( int i, int j )
         {
            if( !Contains( i, j ) )
               m_IntegerPairs.Add( new IntegerPairWithPotentialGears( i, j ) );
            else
               throw new Exception( );
         }

         public IntegerPairWithPotentialGears GetPair( int i, int j )
         {
         //Loop over all the integer pairs in the set..
            foreach( IntegerPairWithPotentialGears pair in m_IntegerPairs )
               if( pair.RowIdx == i && pair.ColIdx == j )
                  return pair;
            
            return null;
         }

         public bool Contains( int i, int j )
         {
         //Loop over all the integer pairs in the set..
            foreach( IntegerPairWithPotentialGears pair in m_IntegerPairs )
               if( pair.RowIdx == i && pair.ColIdx == j )
                  return true;

         //If the code reached this point, it is not contained in the set..
            return false;
         }

         public void Remove( int i, int j )
         {
         //Loop over all the integer pairs in the set..
            IntegerPairWithPotentialGears foundPair = null;
            foreach( IntegerPairWithPotentialGears pair in m_IntegerPairs )
            {
               if( pair.RowIdx == i && pair.ColIdx == j )
               {
                  foundPair = pair;
                  break;
               }
            }

         //Remove if found..
            if( foundPair != null )
               m_IntegerPairs.Remove( foundPair );
         }

         public void Clear( ) { m_IntegerPairs.Clear( ); }
      }

      public class IntegerPairWithPotentialGears
      {
         protected int m_RowIdx = -1;
         protected int m_ColIdx = -1;

         public IntegerPairWithPotentialGears( int i, int j )
         {
            m_RowIdx = i;
            m_ColIdx = j;
            this.AdjacentNumbers = new List<long>( );
         }

         public int RowIdx { get { return m_RowIdx; } }
         public int ColIdx { get { return m_ColIdx; } }

         public List<long> AdjacentNumbers { get; set; }

      }

      public static long GetSerialNumberSum( object[,] grid, out long gearRatio )
      {
         gearRatio = -1;

      //Collect all potential gear locations for part 2
         HashSetIntegerPairWithPotentialGears gearLocations = new HashSetIntegerPairWithPotentialGears( );
         for( int i = 0; i<grid.GetLength( 0 ); i++ )
         {
            for( int j = 0; j<grid.GetLength( 1 ); j++ )
            {
               if( grid[i,j] != null )
               {
               //Cast char
                  char c = ( char ) grid[i,j];
                  if( c == '*' )
                     gearLocations.Add( i, j );
               }
            }
         }

      //Loop over all rows. when a number is encountered, check for numbers and extract them to the list..
         List<KeyValuePair<IntegerPair,IntegerPair>> numberCoordinates = new List<KeyValuePair<IntegerPair, IntegerPair>>( );
         for( int i = 0; i<grid.GetLength(0); i++ )
         {
            int? firstNumRowIdx = null;
            int? firstNumColIdx = null;
            for( int j = 0; j<grid.GetLength(1); j++ )
            {

               if( grid[i,j] == null )
               {
                  if( firstNumRowIdx != null && firstNumColIdx != null )
                  {
                     numberCoordinates.Add( ExtractIndices( (int) firstNumColIdx, j-1, (int) firstNumRowIdx ) );
                     firstNumRowIdx = null;
                     firstNumColIdx = null;
                  }
                  continue;
               }
               else //It is not null, it is either a symbol or a number..
               {
               //Cast char
                  char c = ( char ) grid[i,j];

               //Continue if it is a symbol or something else..
                  if( !c.IsInteger( ) )
                  {
                     if( firstNumRowIdx != null && firstNumColIdx != null )
                     {
                        numberCoordinates.Add( ExtractIndices( (int) firstNumColIdx, j-1, (int) firstNumRowIdx ) );
                        firstNumRowIdx = null;
                        firstNumColIdx = null;
                     }
                     continue;
                  }

               //if the code reached this point, it is an integer..
                  if( c.IsInteger( ) )
                  {
                     if( firstNumRowIdx == null && firstNumColIdx == null )
                     {
                        firstNumRowIdx = i;
                        firstNumColIdx = j;
                        continue;
                     }
                     else //We found a previous number, and this is a number, we can continue safely if this it NOT the last column..
                     {
                        if( j == grid.GetLength( 1 ) - 1 )
                        {
                           if( firstNumRowIdx != null && firstNumColIdx != null )
                           {
                              numberCoordinates.Add( ExtractIndices( (int) firstNumColIdx, j, (int) firstNumRowIdx ) );
                              firstNumRowIdx = null;
                              firstNumColIdx = null;
                           }
                        }
                        continue;
                     }
                  }
               }
            }
         } 

      //Part 1
         long serialNumberSum = 0;
         foreach( KeyValuePair<IntegerPair, IntegerPair> c in numberCoordinates )
         {
            bool isSerialNum = IsSerialNumber( c, gearLocations, grid, out long? serial );
            if( isSerialNum )
               serialNumberSum += (long) serial;
         }

      //Part 2
         long gearRatioSum = 0;
         for( int i = 0; i<grid.GetLength( 0 ); i++ )
         {
            for( int j = 0; j<grid.GetLength( 1 ); j++ )
            {
               IntegerPairWithPotentialGears g = gearLocations.GetPair( i, j );
               if( g != null )
               {
                  if( g.AdjacentNumbers.Count == 2 )
                  {
                     long thisGearRatio = g.AdjacentNumbers[0]*g.AdjacentNumbers[1];
                     gearRatioSum += thisGearRatio;
                  }
               }
            }
         }
         gearRatio = gearRatioSum;
         return serialNumberSum;
      }


      /// <summary>
      /// Checks if this number is a serial number
      /// </summary>
      /// <param name="coor"></param>
      /// <param name="potentialGears"></param>
      /// <param name="grid"></param>
      /// <param name="num"></param>
      /// <returns></returns>
      public static bool IsSerialNumber( KeyValuePair<IntegerPair, IntegerPair> coor, HashSetIntegerPairWithPotentialGears potentialGears, object[,] grid, out long? num )
      {
         num = null;
         bool isSerialNum = false;
         List<IntegerPair> coordinatesWithGearsForThisNum = new List<IntegerPair>( );
      //Check top row, not diagonals
         if( coor.Key.RowIdx != 0 )
         {
            for( int i = coor.Key.ColIdx; i <= coor.Value.ColIdx; i++ )
            {
               object up = grid[coor.Key.RowIdx-1, i];
               if( up != null )
               {
                  char c = (char) up;
                  if( c.IsInteger( ) )
                     continue;
                  else //It is not an integer, it has to be a symbol! flag this as a serial number..
                  {
                     isSerialNum = true;
                     if( c == '*' )
                        coordinatesWithGearsForThisNum.Add( new IntegerPair( coor.Key.RowIdx-1,i ) );
                  }
               }
            }
         }

      //Check bottom row
         if( coor.Key.RowIdx != grid.GetLength( 0 ) - 1 )
         {
            for( int i = coor.Key.ColIdx; i <= coor.Value.ColIdx; i++ )
            {
               object up = grid[coor.Key.RowIdx + 1, i];
               if( up != null )
               {
                  char c = (char) up;
                  if( c.IsInteger( ) )
                     continue;
                  else //It is not an integer, it has to be a symbol! flag this as a serial number..
                  {
                     isSerialNum = true;
                     if( c == '*' )
                        coordinatesWithGearsForThisNum.Add( new IntegerPair( coor.Key.RowIdx+1,i ) );
                  }
               }
            }
         }

      //Left Cols..
         if( coor.Key.ColIdx != 0 )
         {
         //Top left..
            if( coor.Key.RowIdx != 0 )
            {
               object tl = grid[coor.Key.RowIdx - 1, coor.Key.ColIdx - 1];
               if( tl != null )
               {
                  char c = (char) tl;
                  if( !c.IsInteger( ) )
                  {
                     isSerialNum = true;
                     if( c == '*' )
                        coordinatesWithGearsForThisNum.Add( new IntegerPair( coor.Key.RowIdx - 1, coor.Key.ColIdx - 1 ) );
                  }
               }
            }

         //Middle left..
            object ml = grid[coor.Key.RowIdx , coor.Key.ColIdx - 1];
            if( ml != null )
            {
               char c = (char) ml;
               if( !c.IsInteger( ) )
               {
                  isSerialNum = true;
                  if( c == '*' )
                     coordinatesWithGearsForThisNum.Add( new IntegerPair( coor.Key.RowIdx, coor.Key.ColIdx - 1 ) );
               }
            }


         //Bottom left..
            if( coor.Key.RowIdx != grid.GetLength( 0 ) - 1 )
            {
               object bl = grid[coor.Key.RowIdx + 1, coor.Key.ColIdx - 1];
               if( bl != null )
               {
                  char c = (char) bl;
                  if( !c.IsInteger( ) )
                  {

                     isSerialNum = true;
                     if( c == '*' )
                        coordinatesWithGearsForThisNum.Add( new IntegerPair( coor.Key.RowIdx + 1, coor.Key.ColIdx - 1 ) );
                  }
               }
            }
         }

      //Right cols
         if( coor.Value.ColIdx != grid.GetLength( 1 ) - 1 )
         {
         //Top right..
            if( coor.Value.RowIdx != 0 )
            {
               object tl = grid[coor.Value.RowIdx - 1, coor.Value.ColIdx + 1];
               if( tl != null )
               {
                  char c = (char) tl;
                  if( !c.IsInteger( ) )
                  {
                     isSerialNum = true;
                     if( c == '*' )
                        coordinatesWithGearsForThisNum.Add( new IntegerPair( coor.Value.RowIdx - 1, coor.Value.ColIdx + 1 ) );
                  }
               }
            }

         //Middle right..
            object ml = grid[coor.Value.RowIdx , coor.Value.ColIdx + 1];
            if( ml != null )
            {
               char c = (char) ml;
               if( !c.IsInteger( ) )
               {
                  isSerialNum = true; 
                  if( c == '*' )
                     coordinatesWithGearsForThisNum.Add( new IntegerPair( coor.Value.RowIdx , coor.Value.ColIdx + 1 ) );
               }
            }

         //Bottom right..
            if( coor.Value.RowIdx != grid.GetLength( 0 ) - 1 )
            {
               object bl = grid[coor.Value.RowIdx + 1, coor.Value.ColIdx + 1];
               if( bl != null )
               {
                  char c = (char) bl;
                  if( !c.IsInteger( ) )
                  {
                     isSerialNum = true;
                     if( c == '*' )
                        coordinatesWithGearsForThisNum.Add( new IntegerPair( coor.Value.RowIdx + 1, coor.Value.ColIdx + 1 ) );
                  }
               }
            }

         }

         if( isSerialNum )
         {
            num = ExtractNumber( coor, grid );

         //Add the number for this gear..
            foreach( IntegerPair pair in coordinatesWithGearsForThisNum )
            {
               IntegerPairWithPotentialGears p = potentialGears.GetPair( pair.RowIdx, pair.ColIdx );
               p.AdjacentNumbers.Add( ( long ) num );
            }
         }

         return isSerialNum;
      }

      public static long ExtractNumber( KeyValuePair<IntegerPair, IntegerPair> coor, object[,] grid )
      {
         StringBuilder sb = new StringBuilder( );
         for( int i = coor.Key.ColIdx; i <= coor.Value.ColIdx; i++ )
            sb.Append( grid[coor.Key.RowIdx,i] );
         return long.Parse( sb.ToString( ) );
      }

      public static KeyValuePair<IntegerPair,IntegerPair> ExtractIndices( int startColIdx, int endColIdx, int rowIdx )
      {
         IntegerPair sv = new IntegerPair( rowIdx, startColIdx );
         IntegerPair ev = new IntegerPair( rowIdx, endColIdx );
         KeyValuePair<IntegerPair,IntegerPair> coordinates = new KeyValuePair<IntegerPair, IntegerPair>( sv, ev );
         return coordinates;
      }

   }
}
