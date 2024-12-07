using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Classes
{
   public class Labyrinth
   {

   /*ENUMS*/
   #region
      public enum TRACERMOVESTATUS
      {
         MOVED,
         TURNED,
         OUTOFBOUNDS,
         FOUNDLOOP
      }

   #endregion

   /*LOCAL CLASSES*/
   #region
      public struct PositionAndDirection
      {
         public int RowPos { get; set; }
         public int ColPos { get; set; }
         public int RowDir { get; set; }
         public int ColDir { get; set; }
      }
      
      public class LabyrinthTracer
      {

         public LabyrinthTracer( (int, int) startingPosition )
         {
            this.Position = startingPosition;
            this.Direction = ( Labyrinth.m_StartDirCol, Labyrinth.m_StartDirRow);
         }

         public (int X, int Y) Direction { get; set; }
         public (int Row, int Col) Position { get; set; }

         public ( int Row, int Col ) GetLocationInFront( )
         {
            int inFrontColPos = this.Position.Col + this.Direction.X;
            int inFrontRowPos = this.Position.Row + this.Direction.Y;

            return ( inFrontRowPos, inFrontColPos );
         }

         public TRACERMOVESTATUS Move( char[ , ] labyrinth, HashSet<PositionAndDirection> visited )
         {
            int newColPos = this.Position.Col + this.Direction.X;
            int newRowPos = this.Position.Row + this.Direction.Y;

         //Check if out of bounds
            if( newColPos < 0 || newColPos > labyrinth.GetLength( 1 ) - 1 || newRowPos < 0 || newRowPos > labyrinth.GetLength( 0 ) - 1 )
               return TRACERMOVESTATUS.OUTOFBOUNDS;
            else if( labyrinth[newRowPos, newColPos] == '#' )
            {
               int newColDir = -this.Direction.Y;
               int newRowDir = this.Direction.X;
               this.Direction = (newColDir, newRowDir );

            //Check if we found loop.
               PositionAndDirection pd = new PositionAndDirection{ RowPos = this.Position.Row, ColPos = this.Position.Col, RowDir = this.Direction.Y, ColDir = this.Direction.X };
               if( visited.Contains( pd ) )
                  return TRACERMOVESTATUS.FOUNDLOOP;

               visited.Add( pd );
               return TRACERMOVESTATUS.TURNED;
            }
            else //No obstacles, moved
            {
               this.Position = ( newRowPos, newColPos);
               PositionAndDirection pd = new PositionAndDirection{ RowPos = this.Position.Row, ColPos = this.Position.Col, RowDir = this.Direction.Y, ColDir = this.Direction.X };

            //Check if we found loop.
               if( visited.Contains( pd ) )
                  return TRACERMOVESTATUS.FOUNDLOOP;

               visited.Add( pd );

               return TRACERMOVESTATUS.MOVED;
            }

         }

      }
   #endregion

   /*MEMBERS*/
   #region

      public static int m_StartDirRow = -1;
      public static int m_StartDirCol = 0;

      protected char[,] m_Labyrinth = null;
      protected HashSet<PositionAndDirection> m_Visited = new HashSet<PositionAndDirection>( );
      protected (int, int) m_StartingPosition = (0, 0);

   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Copy constructor
      /// </summary>
      /// <param name="inp"></param>
      public Labyrinth( string[] inp )
      {
         m_Labyrinth = new char[inp.Length, inp[0].Length];
         for( int i = 0; i< inp.Length; i++ )
            for( int j = 0; j < inp[0].Length; j++ )
            {
               m_Labyrinth[i, j] = inp[i][j];
               if( inp[i][j] == '^' )
               {
                  PositionAndDirection pd = new PositionAndDirection{ RowPos = i, ColPos = j, RowDir = m_StartDirRow, ColDir = m_StartDirCol };
                  m_Visited.Add( pd );
                  m_StartingPosition = (i, j);
                  m_Labyrinth[i, j] = '.';
               }
            }
      }

      /// <summary>
      /// Copy constructor for the altered labyrinth..
      /// </summary>
      /// <param name="oldLab"></param>
      /// <param name="startPosDir"></param>
      /// <param name="newObstacleLocation"></param>
      public Labyrinth( Labyrinth oldLab, (int rowPos, int colPos) newObstacleLocation )
      {
      //Create a copy of the old lab..
         m_Labyrinth = new char[oldLab.m_Labyrinth.GetLength( 0 ), oldLab.m_Labyrinth.GetLength( 1 )];
         for( int i = 0; i < oldLab.m_Labyrinth.GetLength( 0 ); i++ )
            for( int j = 0; j<oldLab.m_Labyrinth.GetLength( 1 ); j++ )
               m_Labyrinth[i,j] = oldLab.m_Labyrinth[i,j];

         this.m_StartingPosition = oldLab.m_StartingPosition;
         m_Visited.Add( oldLab.m_Visited.First( ) );

      //Create an obstacle at the new location..
         m_Labyrinth[newObstacleLocation.rowPos,newObstacleLocation.colPos] = '#';
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
   #endregion

   /*METHODS*/
   #region
      public int P1( )
      {
         LabyrinthTracer tracer = new LabyrinthTracer( m_StartingPosition );
         TRACERMOVESTATUS isMoving = TRACERMOVESTATUS.MOVED;
         while( isMoving == TRACERMOVESTATUS.MOVED )
            isMoving = tracer.Move( m_Labyrinth, m_Visited );
         return m_Visited.Count;
      }

   #endregion


      public int P2( )
      {
      //Create new tracer
         LabyrinthTracer tracer = new LabyrinthTracer( m_StartingPosition );

      //Initialize mover and the set of possible squares to put an obstacle..
         HashSet<(int,int)> loopSquares = new HashSet<(int, int)>( );
         while( true )
         {
            if( IsSquareInFrontLoopSquare( tracer, loopSquares ) )
               loopSquares.Add( tracer.GetLocationInFront( ) );

            TRACERMOVESTATUS isMoving = tracer.Move( m_Labyrinth, m_Visited );
            if( isMoving == TRACERMOVESTATUS.OUTOFBOUNDS )
               break;

            if( isMoving == TRACERMOVESTATUS.FOUNDLOOP )
               throw new Exception( ); //LOOP IN INPUT DATA??!
         }

      //Remove the original starting square if it is in the loop square list..
         loopSquares.Remove( m_StartingPosition );
         return loopSquares.Count;
      }


      /// <summary>
      /// Checks if the square currently in front of the input tracer is a square that creates a loop if placing an obstacle in it/>
      /// </summary>
      /// <param name="tracer">The tracer in question. Used to get direction and location</param>
      /// <param name="existingLoopSquares">The existing loop squares to prevent checking of duplicates</param>
      /// <returns></returns>
      /// <exception cref="Exception"></exception>
      public bool IsSquareInFrontLoopSquare( LabyrinthTracer tracer, HashSet<(int,int)> existingLoopSquares )
      {

      //Create a copy of the tracer and create a new labyrinth with the square in front as an obstacle
         ( int row, int col ) frontSquare = tracer.GetLocationInFront( );
         LabyrinthTracer loopTracer = new LabyrinthTracer( m_StartingPosition );

      //Check if the square in front is out of bounds. If so, it cant be a loop square.. Also, if it is already an obstacle there it shouldnt be checked anyways..
         if( frontSquare.row < 0 || frontSquare.row > m_Labyrinth.GetLength( 0 ) - 1 || frontSquare.col < 0 || frontSquare.col > m_Labyrinth.GetLength( 1 ) - 1 || m_Labyrinth[frontSquare.row,frontSquare.col] == '#' )
            return false;

      //If we already checked this square, dont do anything.
         if( existingLoopSquares.Contains( frontSquare ) )
            return false;

      //Create a copy of the lab with the front square as an obstacle..
         Labyrinth alteredLab = new Labyrinth( this, frontSquare );

      //Move the loop tracer..
         while( true )
         {
            TRACERMOVESTATUS moveStatus = loopTracer.Move( alteredLab.m_Labyrinth, alteredLab.m_Visited );
            if( moveStatus == TRACERMOVESTATUS.OUTOFBOUNDS )
               return false;
            
            if( moveStatus == TRACERMOVESTATUS.FOUNDLOOP )
               return true;
         }

      //If the code reached this point, something is really wrong..
         throw new Exception( );

      }


   }

}
