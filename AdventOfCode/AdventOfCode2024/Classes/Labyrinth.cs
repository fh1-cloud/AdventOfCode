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

         public bool Move( char[ , ] labyrinth, HashSet<PositionAndDirection> visited )
         {
            bool moved = false;
            do
            {
               int newColPos = this.Position.Col + this.Direction.X;
               int newRowPos = this.Position.Row + this.Direction.Y;

               //Check if out of bounds
               if( newColPos < 0 || newColPos >= labyrinth.GetLength( 1 ) || newRowPos < 0 || newRowPos >= labyrinth.GetLength( 0 ) )
               {
                  return false;
               }
               else if( labyrinth[newRowPos, newColPos] == '#' )
               {
                  int newColDir = -this.Direction.Y;
                  int newRowDir = this.Direction.X;
                  this.Direction = (newColDir, newRowDir );
               }
               else
               {
                  this.Position = ( newRowPos, newColPos);
                  moved = true;
                  PositionAndDirection pd = new PositionAndDirection{ RowPos = this.Position.Row, ColPos = this.Position.Col, RowDir = this.Direction.Y, ColDir = this.Direction.X };
                  visited.Add( pd );
               }

            } while( !moved );
            return true;
         }

      }


      public static int m_StartDirRow = -1;
      public static int m_StartDirCol = 0;

      protected char[,] m_Labyrinth = null;
      protected HashSet<PositionAndDirection> m_Visited = new HashSet<PositionAndDirection>( );
      protected (int, int) m_StartingPosition = (0, 0);


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


      public int P1( )
      {
         LabyrinthTracer tracer = new LabyrinthTracer( m_StartingPosition );
         bool isMoving = true;
         while( isMoving )
            isMoving = tracer.Move( m_Labyrinth, m_Visited );
         return m_Visited.Count;
      }

      public int P2( )
      {
         //First, trace the labyrinth exaclty as in P1, but keep track of the visited indices together with the direction the guard was facing when visiting it.
         //Keep track if the visited indices together with the direction that the guard was facing. When entering a new square, check if placing an obstacle in this new square would make him turn into another visted square with the same facing direction. If so, this is loopable


         return 0;


      }


   }

}
