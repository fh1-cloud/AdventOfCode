using AdventOfCodeLib;
using AdventOfCodeLib.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   internal class Garden
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region


      internal struct Direction
      {
         public Direction( int row, int col )
         {
            this.Row = row;
            this.Col = col;
         }
         public int Row { get; set; }
         public int Col { get; set; }
         public static Direction Up { get { return new Direction( -1, 0 ); } }
         public static Direction Down { get { return new Direction( 1, 0 ); } }
         public static Direction Left { get { return new Direction( 0, -1 ); } }
         public static Direction Right { get { return new Direction( 0, 1 ); } }
      }

      internal struct Position
      {
         public Position( int row, int col )
         {
            this.Row = row;
            this.Col = col;
         }
         public int Row { get; set; }
         public int Col { get; set; }
         public Position Move( Direction dir ) { return new Position( this.Row + dir.Row, this.Col + dir.Col ); }
      }

   #endregion

   /*MEMBERS*/
   #region
      protected char[,] m_GardenPlots = null;
      protected IntegerPair m_StartLocation = null;
   #endregion

   /*CONSTRUCTORS*/
   #region
      internal Garden( string[] inp )
      {
         m_GardenPlots = new char[inp.Length,inp[0].Length];
         for( int i = 0; i<inp.Length; i++ )
         {
            for( int j = 0; j<inp.Length; j++ )
            {
               if( inp[i][j] == 'S' )
               {
                  m_StartLocation = new IntegerPair( i, j );
                  m_GardenPlots[i,j] = '.';
               }
               else
               {
                  m_GardenPlots[i,j] = inp[i][j];
               }
            }
         }

      }

   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// A fancy way to copy the grid without copying the grid.
      /// </summary>
      /// <param name="number"></param>
      /// <returns></returns>
      static int Modulo( int number )
      {
         return (( number % 131 ) + 131 ) % 131;
      }

   #endregion

   /*METHODS*/
   #region


      internal void P2( )
      {
      //The grid is a square of 131x131 tiles. S is in the exact center at 65,65
      //The edge rows and columns are all open, and S has a straight path to all of them
      //I takes 65 steps to reach the first set of edges, then 131 more to reach every next set.
      //When we reach the first edges, the points form a diamond
      //Then we run to the next edges, and to the ones after that, making the diamond grow
      //For each of those 3 runs, we will store the number of steps taken (x) and the number of open tiles at that step (y)
      //3 pairs are enough to interpolate the growth function y=f(x)
      //Use a normal lagrange interpolation
      // I found this, and it helped: https://www.dcode.fr/lagrange-interpolating-polynomial
      // It's a quadratic formula! (it's a square grid, and with every step we form a perfect diamond, so it makes sense)
      // So we can just calculate the formula for X = 26501365, and we get the answer.
         long totalSteps = 26501365;

         List<(int x, int y)> sequenceCounts = new List< ( int x, int y ) >( );
         int startPosition = m_GardenPlots.GetLength( 0 ) / 2; //65

         Position start = new Position( startPosition, startPosition );
         HashSet<Position> visited = new HashSet<Position> { start };
         int steps = 0;

         for( int run = 0; run < 3; run++ )
         {

            for( ; steps<run * 131 + 65; steps++ ) //First run is 65 steps, rest are 131 each
            {

               HashSet<Position> nextOpen = new HashSet<Position>( );
               foreach( Position position in visited )
               {

                  foreach( Direction dir in new[] { Direction.Up, Direction.Down, Direction.Left, Direction.Right } )
                  {

                     Position dest = position.Move( dir );
                     if( m_GardenPlots[Modulo( dest.Row),Modulo(dest.Col)] != '#' )
                        nextOpen.Add( dest );
                  }
               }

               visited = nextOpen;
               if( steps == 63 )
                  Console.WriteLine( $"Part 1: {visited.Count}" );
            }
            sequenceCounts.Add( ( steps, visited.Count ) );
         }

      //Lagrange interpolation
         double result = 0;
         for( int i = 0; i<3; i++ )
         {
         //Compute individual terms of formula
            double term = sequenceCounts[i].y;
            for( int j = 0; j<3; j++ )
               if( j != i )
                  term = term * ( totalSteps - sequenceCounts[j].x ) / ( sequenceCounts[i].x - sequenceCounts[j].x );
         //Add term to result
            result += term;
         }
         Console.WriteLine( $"Part 2: {result}" );
      }


      internal long TakeSteps( int nOfSteps )
      {
      //Declare current locations
         HashSet<Tuple<int,int>> currentLocations = new HashSet<Tuple<int, int>>( );

      //Add starting location
         currentLocations.Add( Tuple.Create( m_StartLocation.RowIdx, m_StartLocation.ColIdx ) );
         for( int i = 0; i<nOfSteps; i++ )
         {
            currentLocations = TakeStep( currentLocations );
            Console.WriteLine( currentLocations.Count );
         }

         return currentLocations.Count;
      }


      protected HashSet<Tuple<int,int>> TakeStep( HashSet<Tuple<int,int>> currentLocations )
      {
         HashSet<Tuple<int, int>> newLocations = new HashSet<Tuple<int, int>>( );
         foreach( Tuple<int, int> location in currentLocations )
         {
            HashSet<Tuple<int, int>> n = GetNeighbours( location );
            foreach( Tuple<int, int> tn in n )
               if( !newLocations.Contains( tn ) )
                  newLocations.Add( tn );
         }
         return newLocations;
      }


      protected HashSet<Tuple<int,int>> GetNeighbours( Tuple<int,int> location )
      {
         HashSet<Tuple<int, int>> neighbours = new HashSet<Tuple<int, int>>( );

         if( location.Item1 != 0 && m_GardenPlots[ location.Item1 - 1, location.Item2 ] != '#' )
            neighbours.Add( Tuple.Create( location.Item1 - 1, location.Item2 ) );
         if( location.Item1 != m_GardenPlots.GetLength( 0 ) - 1 && m_GardenPlots[ location.Item1 + 1, location.Item2 ] != '#' )
            neighbours.Add( Tuple.Create( location.Item1 + 1, location.Item2 ) );
         if( location.Item2 != 0 && m_GardenPlots[ location.Item1, location.Item2 - 1] != '#' )
            neighbours.Add( Tuple.Create( location.Item1, location.Item2 - 1) );
         if( location.Item2 != m_GardenPlots.GetLength( 1 ) - 1 && m_GardenPlots[ location.Item1, location.Item2 + 1 ] != '#' )
            neighbours.Add( Tuple.Create( location.Item1, location.Item2 + 1) );
         return neighbours;
      }

      #endregion



   }
}
