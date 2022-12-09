using AdventOfCodeLib.Classes;
using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class RopeKnot
   {

   /*ENUMS*/
   #region
      public enum MOVEDIRECTION
      {
         UP,
         DOWN,
         LEFT,
         RIGHT,
         UPRIGHT,
         UPLEFT,
         DOWNRIGHT,
         DOWNLEFT,
         NONE,
      }
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected static Dictionary<int, char> m_IdentifierMap = new Dictionary<int, char>( );


      protected RopeKnot m_Parent = null;
      protected RopeKnot m_Child = null;
      protected int m_ID;
      protected UVector2D m_Position = null;

      protected HashSetUVector2D m_VisitedPositions = new HashSetUVector2D( );

   #endregion

   /*CONSTRUCTORS*/
   #region

      static RopeKnot( )
      {
         m_IdentifierMap.Add( 0, 'H' );
         m_IdentifierMap.Add( 1, '1' );
         m_IdentifierMap.Add( 2, '2' );
         m_IdentifierMap.Add( 3, '3' );
         m_IdentifierMap.Add( 4, '4' );
         m_IdentifierMap.Add( 5, '5' );
         m_IdentifierMap.Add( 6, '6' );
         m_IdentifierMap.Add( 7, '7' );
         m_IdentifierMap.Add( 8, '8' );
         m_IdentifierMap.Add( 9, '9' );
      }

      public RopeKnot( RopeKnot parent, int id, UVector2D startPos )
      {
         m_Parent = parent;
         m_ID = id;
         m_Position = new UVector2D( startPos );
      }

   #endregion

   /*PROPERTIES*/
   #region
      public double X { get { return m_Position.X; } set { m_Position.X = value; } }
      public double Y { get { return m_Position.Y; } set { m_Position.Y = value; } }
      public RopeKnot Child { get { return m_Child; } set { m_Child = value; } }
   #endregion

   /*OPERATORS*/
   #region

   #endregion


   /*METHODS*/
   #region

      public void MoveKnot( MOVEDIRECTION parentMovement )
      {
      //Determine the movement vector..
         MOVEDIRECTION? thisMovement = null;


      //If the parent is null, this is the head knot, it will move regardless of the tail..
         if( m_Parent == null )
         {
            thisMovement = (MOVEDIRECTION) parentMovement;
         }
         else //This is a child knot..
         {
            if( parentMovement == MOVEDIRECTION.NONE )
               thisMovement = MOVEDIRECTION.NONE;
            else if( Math.Abs( m_Parent.X - this.X ) <= 1 && Math.Abs( m_Parent.Y - this.Y ) <= 1 )
               thisMovement = MOVEDIRECTION.NONE;
            else //We know we have to move this knot..
            {
               if( parentMovement == MOVEDIRECTION.UP ) //The parent knot moved directly upwards. Depending on the x-coordinate, the movement of this knot might differ..
               {
                  if( m_Parent.X == this.X )
                     thisMovement = MOVEDIRECTION.UP;
                  else if( m_Parent.X < this.X )
                     thisMovement = MOVEDIRECTION.UPLEFT;
                  else if( m_Parent.X > this.X )
                     thisMovement = MOVEDIRECTION.UPRIGHT;
               }
               else if( parentMovement == MOVEDIRECTION.DOWN )
               {
                  if( m_Parent.X == this.X )
                     thisMovement = MOVEDIRECTION.DOWN;
                  else if( m_Parent.X < this.X )
                     thisMovement = MOVEDIRECTION.DOWNLEFT;
                  else if( m_Parent.X > this.X )
                     thisMovement = MOVEDIRECTION.DOWNRIGHT;
               }
               else if( parentMovement == MOVEDIRECTION.RIGHT )
               {
                  if( m_Parent.Y == this.Y )
                     thisMovement = MOVEDIRECTION.RIGHT;
                  else if( m_Parent.Y < this.Y )
                     thisMovement = MOVEDIRECTION.DOWNRIGHT;
                  else if( m_Parent.Y > this.Y )
                     thisMovement = MOVEDIRECTION.UPRIGHT;
               }
               else if( parentMovement == MOVEDIRECTION.LEFT )
               {
                  if( m_Parent.Y == this.Y )
                     thisMovement = MOVEDIRECTION.LEFT;
                  else if( m_Parent.Y < this.Y )
                     thisMovement = MOVEDIRECTION.DOWNLEFT;
                  else if( m_Parent.Y > this.Y )
                     thisMovement = MOVEDIRECTION.UPLEFT;
               }
               else if( parentMovement == MOVEDIRECTION.UPRIGHT )
               {
               //Parent moved up and right, we have to check if it ended up straight above us, if so we only have to move upwards and not right..
                  if( m_Parent.X == this.X )
                     thisMovement = MOVEDIRECTION.UP;
                  else if( m_Parent.Y == this.Y )
                     thisMovement = MOVEDIRECTION.RIGHT;
                  else
                     thisMovement = MOVEDIRECTION.UPRIGHT;
               }
               else if( parentMovement == MOVEDIRECTION.UPLEFT )
               {
                  if( m_Parent.X == this.X )
                     thisMovement = MOVEDIRECTION.UP;
                  else if( m_Parent.Y == this.Y )
                     thisMovement = MOVEDIRECTION.LEFT;
                  else
                     thisMovement = MOVEDIRECTION.UPLEFT;
               }
               else if( parentMovement == MOVEDIRECTION.DOWNRIGHT )
               {
                  if( m_Parent.X == this.X )
                     thisMovement = MOVEDIRECTION.DOWN;
                  else if( m_Parent.Y == this.Y )
                     thisMovement = MOVEDIRECTION.RIGHT;
                  else
                     thisMovement = MOVEDIRECTION.DOWNRIGHT;
               }
               else if( parentMovement == MOVEDIRECTION.DOWNLEFT )
               {
                  if( m_Parent.X == this.X )
                     thisMovement = MOVEDIRECTION.DOWN;
                  else if( m_Parent.Y == this.Y )
                     thisMovement = MOVEDIRECTION.LEFT;
                  else
                     thisMovement = MOVEDIRECTION.DOWNLEFT;
               }

            } //End we have to move the knot

         } //End is child node

      //Get the movement vector from the current movement..
         UVector2D thisVector = GetMovementFromInstruction( (MOVEDIRECTION) thisMovement );

      //Increment this position..
         m_Position += thisVector;

      //Add the visited positions if this is the tail..
         if( !m_VisitedPositions.Contains( m_Position ) )
            m_VisitedPositions.Add( m_Position );

      //Move child is present
         if( m_Child != null )
            m_Child.MoveKnot( (MOVEDIRECTION) thisMovement );

      }

      /// <summary>
      /// Gets the number of unique position of this rope..
      /// </summary>
      /// <returns></returns>
      public int GetNumberOfUniquePositions( )
      {
         return m_VisitedPositions.Count;
      }


      /// <summary>
      /// Gets the knot positions for this knot and all child knots..
      /// </summary>
      /// <param name="positions"></param>
      private void GetKnotPositions( List<UVector2D> positions )
      {
         positions.Add( m_Position );
         if( m_Child != null )
            m_Child.GetKnotPositions( positions );
      }

      /// <summary>
      /// Prints the current state of the whole rope to a canvas
      /// </summary>
      /// <param name="gridSize">The size of the grid to print to</param>
      /// <param name="rowStart">The index of the row where to start</param>
      /// <param name="colStart">The index of the column where to start.</param>
      public void PrintState( int nOfGridRows, int nOfGridColumns, int rowStart, int colStart )
      {
         List<UVector2D> positions = new List<UVector2D>( );
         GetKnotPositions( positions );

      //Translate to indexes on a map..
         char[,] grid = new char[nOfGridRows,nOfGridColumns];

      //Paint canvas..
         for( int i = 0; i<nOfGridRows; i++ )
            for( int j = 0; j<nOfGridColumns; j++ )
               grid[i,j] = '.';

      //Loop over all positions and paint..
         foreach( UVector2D pos in positions )
         {
            int row = rowStart - (int) pos.Y;
            int col = colStart + (int) pos.X;
            if( grid[row,col] == '.' ) //If it doesnt contain a period, this has been populated already. Skip it
            {
               char ID = m_IdentifierMap[positions.IndexOf( pos )];
               grid[row,col] = ID.ToString()[0];
            }
         }

      //Loop over canvas and paint..
         for( int i = 0; i<nOfGridRows; i++ )
         {
            StringBuilder thisLine = new StringBuilder( );
            for( int j = 0; j<nOfGridColumns; j++ )
               thisLine.Append( grid[i,j].ToString( ) );
            Console.WriteLine( thisLine );
         }

      //Add a blank line at the end of the print..
         Console.WriteLine( );
      }


   #endregion

   /*STATIC METHODS*/
   #region


      /// <summary>
      /// Gets a movement vector from a directional instruction
      /// </summary>
      /// <param name="dir"></param>
      /// <returns></returns>
      /// <exception cref="Exception"></exception>
      public static UVector2D GetMovementFromInstruction( MOVEDIRECTION dir )
      {
         if( dir == MOVEDIRECTION.UP )
            return new UVector2D(  0.0,  1.0 );
         else if( dir == MOVEDIRECTION.DOWN )
            return new UVector2D(  0.0, -1.0 );
         else if( dir == MOVEDIRECTION.LEFT )
            return new UVector2D( -1.0,  0.0 );
         else if( dir == MOVEDIRECTION.RIGHT )
            return new UVector2D(  1.0,  0.0 );
         else if( dir == MOVEDIRECTION.UPLEFT )
            return new UVector2D( -1.0,  1.0 );
         else if( dir == MOVEDIRECTION.UPRIGHT )
            return new UVector2D(  1.0,  1.0 );
         else if( dir == MOVEDIRECTION.DOWNLEFT )
            return new UVector2D( -1.0, -1.0 );
         else if( dir == MOVEDIRECTION.DOWNRIGHT )
            return new UVector2D(  1.0, -1.0 );
         else if( dir == MOVEDIRECTION.NONE )
            return new UVector2D(  0.0, 0.0  );
         else
            throw new Exception( );
      }
   #endregion

   }

}
