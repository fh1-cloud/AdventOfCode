using AdventOfCodeLib.Classes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class RockTetris
   {

   /*ENUMS*/
   #region

      public enum BRICKSHAPE
      {
         LINE,
         PLUSS,
         J,
         I,
         BLOCK
      }

      public enum MOVEINSTRUCTION
      {
         DOWN,
         LEFT,
         RIGHT
      }

   #endregion

   /*LOCAL CLASSES*/
   #region
      public class RockTetrisBrick
      {

         public RockTetrisBrick( BRICKSHAPE shape, long startCol, long startHeight )
         {
            this.OccupiedIndexes = new HashSet<(long, long)>( );
            this.Shape = shape;
            this.Height = startHeight;
            this.ColIdx = startCol;
            if( this.Shape == BRICKSHAPE.LINE )
            {
               this.Width = 4;
               this.InternalHeight = 1;
               for( long i = 0; i<4; i++ )
                  this.OccupiedIndexes.Add( ( this.Height, startCol + i ) );
            }
            else if( this.Shape == BRICKSHAPE.PLUSS )
            {
               this.Width = 3;
               this.InternalHeight = 3;
               this.OccupiedIndexes.Add( ( this.Height, startCol + 1 ) );
               this.OccupiedIndexes.Add( ( this.Height+2, startCol + 1 ) );
               for( long i = 0; i<3; i++ )
                  this.OccupiedIndexes.Add( ( this.Height + 1, startCol + i ) );
            }
            else if( this.Shape == BRICKSHAPE.J )
            {
               this.Width = 3;
               this.InternalHeight = 3;
               for( long i = 0; i<3; i++ )
                  this.OccupiedIndexes.Add( ( this.Height, startCol + i ) );
               this.OccupiedIndexes.Add( (this.Height + 1, startCol + 2) );
               this.OccupiedIndexes.Add( (this.Height + 2, startCol + 2) );
            }
            else if( this.Shape == BRICKSHAPE.BLOCK )
            {
               this.Width = 2;
               this.InternalHeight = 2;
               this.OccupiedIndexes.Add( (this.Height + 1, startCol + 1) );
               this.OccupiedIndexes.Add( (this.Height, startCol + 1) );
               this.OccupiedIndexes.Add( (this.Height + 1, startCol) );
               this.OccupiedIndexes.Add( (this.Height, startCol) );
            }
            else if( this.Shape == BRICKSHAPE.I )
            {
               this.Width = 1;
               this.InternalHeight = 4;
               for( long i = 0; i<4; i++ )
                  this.OccupiedIndexes.Add( ( this.Height + i, startCol ) );
            }

         }

         public BRICKSHAPE Shape { get; private set; }
         public long Height { get; set; } //Left bottom end for all.
         public long InternalHeight { get; set; }
         public long Width { get; set; }
         public long ColIdx { get; set; }
         public HashSet<(long,long)> OccupiedIndexes { get; private set; }


         public long GetMaxHeight( )
         {
            if( this.Shape == BRICKSHAPE.LINE )
               return this.Height;
            else if( this.Shape == BRICKSHAPE.PLUSS )
               return this.Height + 2;
            else if( this.Shape == BRICKSHAPE.J )
               return this.Height + 2;
            else if( this.Shape == BRICKSHAPE.I )
               return this.Height + 3;
            else if( this.Shape == BRICKSHAPE.BLOCK )
               return this.Height + 1;

            throw new Exception( ); //Should not reach here.

         }

         public bool Move( MOVEINSTRUCTION ins, HashSet<(long,long)> occupied )
         {
            HashSet<(long,long)> newPositions = GetNewPosition( ins, out bool moved );

         //Check if it collides..
            bool collison = false;
            foreach( (long, long) v in newPositions )
            {
               if( occupied.Contains( v ) )
               {
                  collison = true;
                  break;
               }
            }

            if( !collison ) //No collision between bricks. We can continue moving.
            {
               if( moved )
               {
                  if( ins == MOVEINSTRUCTION.DOWN )
                     this.Height--;
                  else if( ins == MOVEINSTRUCTION.LEFT )
                     this.ColIdx--;
                  else if( ins == MOVEINSTRUCTION.RIGHT )
                     this.ColIdx++;
               }
               this.OccupiedIndexes = newPositions;
               return true;
            }
            else //There is a collision, we cannot move here.
            {
               if( ins == MOVEINSTRUCTION.DOWN )
                  return false;
               else //Its not down, its either left or right. Continue.
                  return true;
            }

         }

         private HashSet<(long,long)> GetNewPosition( MOVEINSTRUCTION ins, out bool moved )
         {
            long colDelta = 0;
            long rowDelta = 0;
            if( ins == MOVEINSTRUCTION.DOWN )
               rowDelta = -1;
            else //it should move left or right
            {
               if( ins == MOVEINSTRUCTION.LEFT )
               {
                  if( this.ColIdx > 0 )
                     colDelta = -1;
               }
               else if( ins == MOVEINSTRUCTION.RIGHT )
               {
                  if( this.ColIdx + this.Width - 1 < 6 )
                     colDelta = 1;
               }
            }
            HashSet<(long,long)> newIndexes = new HashSet<(long, long)>( );
            foreach( var p in this.OccupiedIndexes )
               newIndexes.Add( ( p.Item1 + rowDelta, p.Item2 + colDelta ) );

            moved = ( colDelta != 0 || rowDelta != 0 );
            return newIndexes;
         }

      }
   #endregion

   /*MEMBERS*/
   #region
   #endregion

   /*CONSTRUCTORS*/
   #region

   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region

      public static void PrintState( HashSet<(long,long)> occupied, HashSet<(long,long)> fallingBlock, long maxHeight, long width, bool pauseOnDraw = false )
      {

         char[,] table = new char[maxHeight+1,width+2];

         for( long i = 0; i<table.GetLength( 0 ); i++ )
         {
            if( i == table.GetLength( 0 ) - 1 )
            {
               for( long j = 0; j<table.GetLength( 1 ); j++ )
               {
                  if( j == 0 || j == table.GetLength( 1 ) - 1 )
                     table[i,j] = '+';
                  else
                     table[i,j] = '-';
               }
            }
            else
            {
               for( long j = 0; j<table.GetLength( 1 ); j++ )
               {
                  if( j == 0 || j == table.GetLength( 1 ) - 1 )
                     table[i,j] = '|';
                  else
                     table[i,j] = '.';
               }
            }
         }
         foreach( (long, long) p in occupied )
         {
            long tableHeight = table.GetLength( 0 ) - p.Item1 - 1;
            long tableWidth = p.Item2 + 1;
            table[tableHeight,tableWidth] = '#';
         }

         if( fallingBlock != null )
         {
            foreach( (long, long) p in fallingBlock )
            {
               long tableHeight = table.GetLength( 0 ) - p.Item1 - 1;
               long tableWidth = p.Item2 + 1;
               table[tableHeight,tableWidth] = '@';
            }
         }

         StringBuilder sb = new StringBuilder( );
         for( long i = 0; i<table.GetLength( 0 ); i++ )
         {
            for( long j = 0; j<table.GetLength( 1 ); j++ )
               sb.Append( table[i,j].ToString( ) );
            sb.Append( "\n" );
         }
         //Console.Clear( );
         Console.WriteLine( sb.ToString( ) );

         if( pauseOnDraw )
            Console.ReadKey( );
      }


      //public static long P1( string[] inp )
      //{

      ////Parse instructionrs
      //   List<MOVEINSTRUCTION> instructions = new List<MOVEINSTRUCTION>( );
      //   foreach( char c in inp[0] )
      //   {
      //      if( c == '<' )
      //         instructions.Add( MOVEINSTRUCTION.LEFT );
      //      else if( c == '>' )
      //         instructions.Add( MOVEINSTRUCTION.RIGHT );
      //      else
      //         throw new Exception( );
      //   }

      ////Create base bricks
      //   List<BRICKSHAPE> shapes = new List<BRICKSHAPE>{ BRICKSHAPE.LINE, BRICKSHAPE.PLUSS, BRICKSHAPE.J, BRICKSHAPE.I, BRICKSHAPE.BLOCK };
      //   HashSet<(long,long)> occupied = new HashSet<(long, long)> { (0, 0), (0, 1), (0, 2), (0, 3), (0, 4), (0, 5), (0, 6) };

      //   long nOfRocks = 2022;
      //   long currentMaxHeight = 0;
      //   int insIdx = 0;
      //   int shapeIdx = 0;
      //   for( long i = 0; i<nOfRocks; i++ )
      //   {
      //   //Spawn a rock
      //      RockTetrisBrick n = new RockTetrisBrick( shapes[shapeIdx++ % 5], 2, currentMaxHeight + 4 );
      //      bool continueFalling = true;
      //      while( continueFalling )
      //      {
      //         continueFalling = n.Move( instructions[insIdx++ % instructions.Count], occupied );
      //         continueFalling = n.Move( MOVEINSTRUCTION.DOWN, occupied );
      //         if( !continueFalling ) //Did stop. Add its indexes to the occupied indexes. Also, update the maxheight
      //         {
      //            foreach( (long, long) p in n.OccupiedIndexes )
      //               occupied.Add( p );
      //            currentMaxHeight = Math.Max( currentMaxHeight, n.GetMaxHeight( ) );
      //         }
      //      }
      //   }
      //   return currentMaxHeight;
      //}


      public static long P2( string[ ] inp )
      {

         //Parse instructionrs
         List<MOVEINSTRUCTION> instructions = new List<MOVEINSTRUCTION>( );
         foreach( char c in inp[0] )
         {
            if( c == '<' )
               instructions.Add( MOVEINSTRUCTION.LEFT );
            else if( c == '>' )
               instructions.Add( MOVEINSTRUCTION.RIGHT );
            else
               throw new Exception( );
         }

         //Create base bricks
         List<BRICKSHAPE> shapes = new List<BRICKSHAPE> { BRICKSHAPE.LINE, BRICKSHAPE.PLUSS, BRICKSHAPE.J, BRICKSHAPE.I, BRICKSHAPE.BLOCK };
         HashSet<(long, long)> occupied = new HashSet<(long, long)> { (0, 0), (0, 1), (0, 2), (0, 3), (0, 4), (0, 5), (0, 6) };

         long nOfRocksBeforeSteadyState = 50000;
         long currentMaxHeight = 0;
         int insIdx = 0;
         int shapeIdx = 0;
         List<long> maxHeights = new List<long>( );
         for( long i = 0; i < nOfRocksBeforeSteadyState; i++ )
         {
         //Spawn a rock
            RockTetrisBrick n = new RockTetrisBrick( shapes[shapeIdx++ % 5], 2, currentMaxHeight + 4 );
            bool continueFalling = true;
            while( continueFalling )
            {
               continueFalling = n.Move( instructions[insIdx++ % instructions.Count], occupied );
               continueFalling = n.Move( MOVEINSTRUCTION.DOWN, occupied );
               if( !continueFalling ) //Did stop. Add its indexes to the occupied indexes. Also, update the maxheight
               {
                  foreach( (long, long) p in n.OccupiedIndexes )
                     occupied.Add( p );
                  currentMaxHeight = Math.Max( currentMaxHeight, n.GetMaxHeight( ) );

               }
            }
            maxHeights.Add( currentMaxHeight );
         }


         //Create a list of differences.
            List<long> deltas = new List<long>( );
         for( int i = 0; i < maxHeights.Count - 1; i++ )
            deltas.Add( maxHeights[i + 1] - maxHeights[i] );


         //List<long> deltas = new List<long>( ){ 2,3,1,2, 1,2,1,3,1,4,1,5,1,2,1,3,1,4,1,5 };

      //Assume that it reached a steady state at 1000 cycles. Find the cycle length
         bool foundCycle = false;


         List<int> testList = new List<int>( ) { 2, 3, 1, 2, 1, 2, 1, 3, 1, 4, 1, 5, 1, 2, 1, 3, 1, 4, 1, 5 };

      //Implement a cycle finding algorithm for a list of integers in the library class.

         //var test = Cycles<int>.FindCycle( 3, testList );


         //int cycleStartIdx = 10000;
         //int finalCycle = -1;
         //int thisCycleLength = 2;
         //while( !foundCycle )
         //{

         ////Create this cycle.
         //   List<long> cycle = new List<long>( );
         //   for( int i = cycleStartIdx; i<cycleStartIdx+thisCycleLength; i++ )
         //      cycle.Add( deltas[i] );

         ////Checks if this cycle fits the data..
         //   bool thisIsCycle = true;
         //   int cycleIdx = 0;
         //   for( int i = cycleStartIdx; i<deltas.Count; i++ )
         //   {
         //      long num1 = cycle[cycleIdx++%thisCycleLength];
         //      long num2 = deltas[i];

         //      if( deltas[i] != cycle[i%thisCycleLength] )
         //      {
         //         thisIsCycle = false;
         //         break;
         //      }
         //   }

         //   if( !thisIsCycle )
         //   {
         //      thisCycleLength++;
         //      continue;
         //   }
         //   else
         //   {
         //      foundCycle = true;
         //      finalCycle = thisCycleLength;
         //   }
         //}





         return currentMaxHeight;
      }




   #endregion

   /*METHODS*/
   #region
   #endregion

   }
}
