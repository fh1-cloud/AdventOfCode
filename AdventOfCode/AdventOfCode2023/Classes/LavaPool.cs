using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   internal class LavaPool
   {

   /*LOCAL CLASSES*/
   #region

      /// <summary>
      /// A class that represents a path taken in the pathfinding algorithm
      /// </summary>
      internal class Path
      {
         internal Path( Position pos, Direction dir, int lineLength )
         {
            this.Position = pos;
            this.Direction = dir;
            this.StraightLineLength = lineLength;
         }
         internal Position Position { get; set; }     //The position of this path currently
         internal Direction Direction { get; set; }   //The direction this path is currently traveling
         internal int StraightLineLength { get; set; }          //The distance traveled since last turn
         internal int Heat { get; set; }              //The total accumulated heat for this path.
      }


      /// <summary>
      /// A class that represents a position in the grid.
      /// </summary>
      internal class Position
      {
         internal Position( int rowIdx, int colIdx )
         {
            this.RowIdx = rowIdx;
            this.ColIdx = colIdx;
         }
         internal int RowIdx { get; set; } //The current row position
         internal int ColIdx { get; set; } //The current col position
         internal Position Move( Direction dir ) { return new Position( this.RowIdx + dir.Row, this.ColIdx + dir.Col ); } //Move this position by the direction input vector
      }

      /// <summary>
      /// A class that represents a direction in the pathfinding grid
      /// </summary>
      internal class Direction
      {
         internal Direction( int row, int col )
         {
            this.Row = row;
            this.Col = col;
         }
         internal int Row { get; set; }
         internal int Col { get; set; }
         public Direction TurnLeft( ) { return new Direction( -this.Col, this.Row ); } //A clever way to get the current direction as a unit vector by turning left or right
         public Direction TurnRight( ) { return new Direction( this.Col, -this.Row ); }
         public static Direction Right( ) { return new Direction( 0, 1 ); }
         public static Direction Down( ) { return new Direction( -1, 0 ); }
      }

   #endregion

   /*MEMBERS*/
   #region

      protected int[,] m_Map = null;

      protected PriorityQueue<Path,int> m_Queue = null;
      protected HashSet<string> m_VisitedNodes = null;

   #endregion

   /*CONSTRUCTORS*/
   #region
      public LavaPool( int[,] map )
      {
         m_Map = map;
      }
   #endregion

   /*METHODS*/
   #region

      public void P2( )
      {
      //Set up the priority queue
         m_Queue = new PriorityQueue<Path, int>( );

      //Set up the set of visited nodes
         m_VisitedNodes = new HashSet<string>( );

      //Add the first point..
         Path firstPoint = new Path( new Position( 0, 0 ), Direction.Right( ), 0 );
         Path secondPoint = new Path( new Position( 0, 0 ), Direction.Down( ), 0 );
         m_Queue.Enqueue( firstPoint, 0 );
         m_Queue.Enqueue( secondPoint, 0 );

         //Start the loop.
         long totalHeat = 0;
         while( m_Queue.Count > 0 )
         {
            Path p = m_Queue.Dequeue( );

         //Exit condition..
            if( p.Position.RowIdx == m_Map.GetLength( 0 ) - 1 && p.Position.ColIdx == m_Map.GetLength( 1 ) - 1 && p.StraightLineLength >= 4 )
            {
               totalHeat = p.Heat;
               break;
            }
         //Need to move forward at least 4 times. Is ensured by not turning before distance is at least 4
            if( p.StraightLineLength < 10 )
               TryMove( p, p.Direction ); //Can continue straight

         //Can turn if moved at least 4 forward.
            if( p.StraightLineLength >= 4 )
            {
               TryMove( p, p.Direction.TurnLeft( ) );
               TryMove( p, p.Direction.TurnRight( ) );
            }

         }

      //WHen the code reached here, we have the total heat.
         Console.WriteLine( $"Part 2: {totalHeat}" );
      }


      public void P1( )
      {
      //Set up the priority queue
         m_Queue = new PriorityQueue<Path, int>( );

      //Set up the set of visited nodes
         m_VisitedNodes = new HashSet<string>( );

      //Add the first point..
         Path firstPoint = new Path( new Position( 0, 0 ), Direction.Right( ), 0 );
         m_Queue.Enqueue( firstPoint, 0 );

      //Start the loop..
         long totalHeat = 0;
         while( m_Queue.Count > 0 )
         {
            Path p = m_Queue.Dequeue( );

         //Exit condition..
            if( p.Position.RowIdx == m_Map.GetLength( 0 ) - 1 && p.Position.ColIdx == m_Map.GetLength( 1 ) - 1 )
            {
               totalHeat = p.Heat;
               break;
            }

         //Continue straight IF we havent gone three straight moves..
            if( p.StraightLineLength < 3 )
               TryMove( p, p.Direction );

         //Other than that, try to turn both left and right..
            TryMove( p, p.Direction.TurnLeft( ) );
            TryMove( p, p.Direction.TurnRight( ) );
         }

      //WHen the code reached here, we have the total heat.
         Console.WriteLine( $"Part 1: {totalHeat}" );

      }



      /// <summary>
      /// Tries to move the path p in the direction dir
      /// </summary>
      /// <param name="p"></param>
      /// <param name="dir"></param>
      public void TryMove( Path p, Direction dir )
      {

      //Increment the distance of this path..
         int distance = 0;
         if( dir == p.Direction )
            distance = p.StraightLineLength + 1;
         else
            distance = 1;

      //Find the candidate to travel from this point

      //Get the new position for this path.
         Position newPos = p.Position.Move( dir );
         Path candidate = new Path( newPos, dir, distance );

      //Check if this candidate is outside the bounds of the map. If so, nothing should be added to the priority queue
         if( candidate.Position.RowIdx < 0 || candidate.Position.ColIdx < 0 || candidate.Position.RowIdx >= m_Map.GetLength( 0 ) || candidate.Position.ColIdx >= m_Map.GetLength( 1 ) )
            return; //We moved out of bounds. Dont add anything. This is not a valid path..

      //Create the key of visited nodes. If we reached this exact node in this direction with this distance, there is no need to go again. The reason we add the distance is because we dont know if two paths lead here at the same distance or not
         string key = $"{candidate.Position.RowIdx},{candidate.Position.ColIdx},{candidate.Direction.Row},{candidate.Direction.Col},{candidate.StraightLineLength}";
         if( m_VisitedNodes.Contains( key ) )
            return;

      //Add the key.
         m_VisitedNodes.Add( key );

      //If the code reached here, we know we need to add it to the queue. Add this new position to the set of visited nodes with the direction.
         candidate.Heat = p.Heat + m_Map[candidate.Position.RowIdx,candidate.Position.ColIdx];
         m_Queue.Enqueue( candidate, candidate.Heat );
      }


   #endregion
   }
}
