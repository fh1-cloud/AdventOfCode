using AdventOfCodeLib.Classes;
using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class PipeTraverser
   {
   /*ENUMS*/
   #region
      public enum DIRECTION
      {
         UP,
         DOWN,
         LEFT,
         RIGHT
      }
   #endregion

   /*MEMBERS*/
   #region
      protected IntegerPair m_StartLocation = null;
      protected IntegerPair m_CurrentLocation = null;
      protected DIRECTION m_CurrentlyFacingDirection = DIRECTION.UP;
      protected bool m_ReachedEnd = false;
      protected long m_NumberOfSteps = 0;
   #endregion

   /*CONSTRUCTORS*/
   #region

      public PipeTraverser( IntegerPair startLoc, PipePart[,] grid )
      {
      //Set the current location and start location
         m_StartLocation = new IntegerPair( startLoc );
         m_CurrentLocation = new IntegerPair( m_StartLocation );

      //Check upwards..
         if( m_StartLocation.RowIdx != 0 )
         {
            char candidate = grid[ m_StartLocation.RowIdx-1, m_StartLocation.ColIdx ].C;
            if( candidate == '|' || candidate == 'F' || candidate == '7' ) 
            {
               m_CurrentlyFacingDirection = DIRECTION.UP;
               return;
            }
         }

      //Check downwards..
         if( m_StartLocation.RowIdx != grid.GetLength( 0 ) - 1 )
         {
            char candidate = grid[ m_StartLocation.RowIdx+1, m_StartLocation.ColIdx ].C;
            if( candidate == '|' || candidate == 'L' || candidate == 'J' ) 
            {
               m_CurrentlyFacingDirection = DIRECTION.DOWN;
               return;
            }
         }

      //Check LEFT..
         if( m_StartLocation.ColIdx != 0 )
         {
            char candidate = grid[ m_StartLocation.RowIdx, m_StartLocation.ColIdx-1].C;
            if( candidate == '-' || candidate == 'F' || candidate == 'L' ) 
            {
               m_CurrentlyFacingDirection = DIRECTION.LEFT;
               return;
            }
         }

      //Check Right..
         if( m_StartLocation.ColIdx != grid.GetLength( 1 ) - 1 )
         {
            char candidate = grid[ m_StartLocation.RowIdx, m_StartLocation.ColIdx+1].C;
            if( candidate == '-' || candidate == '7' || candidate == 'J' ) 
            {
               m_CurrentlyFacingDirection = DIRECTION.RIGHT;
               return;
            }
         }

      //If the code reached here, throw.
         throw new Exception( );
         
      }
   #endregion

   /*PROPERTIES*/
   #region
      public long CycleLength { get { return m_NumberOfSteps; } }
   #endregion

   /*METHODS*/
   #region
      
      /// <summary>
      /// Replace the start symbol for P2 so we dont have to calculate edge cases
      /// </summary>
      /// <param name="grid"></param>
      /// <param name="symb"></param>
      public void ReplaceStartSymbol( PipePart[,] grid, char? symb = null )
      {
         if( symb == null )
            grid[m_StartLocation.RowIdx, m_StartLocation.ColIdx].C = '-';
         else
            grid[m_StartLocation.RowIdx, m_StartLocation.ColIdx].C = ( char ) symb;
      }

      public bool Move( PipePart[,] grid )
      {

      //Increment the location. This should not crash if the input is valid..
         if( m_CurrentlyFacingDirection == DIRECTION.UP )
            m_CurrentLocation.RowIdx--;
         else if( m_CurrentlyFacingDirection == DIRECTION.RIGHT )
            m_CurrentLocation.ColIdx++;
         else if( m_CurrentlyFacingDirection == DIRECTION.DOWN )
            m_CurrentLocation.RowIdx++;
         else if( m_CurrentlyFacingDirection == DIRECTION.LEFT )
            m_CurrentLocation.ColIdx--;

         m_NumberOfSteps++;
         grid[m_CurrentLocation.RowIdx, m_CurrentLocation.ColIdx].IsOnLoop = true; //Set flag for p2

      //Update facing direction..
         char current = grid[m_CurrentLocation.RowIdx, m_CurrentLocation.ColIdx].C;
         if( current == 'S' ) //Exit if start.
            return true;

         if( m_CurrentlyFacingDirection == DIRECTION.UP )
         {
            if( current == '|' )
               m_CurrentlyFacingDirection = DIRECTION.UP;
            else if( current == '7' )
               m_CurrentlyFacingDirection = DIRECTION.LEFT;
            else if( current == 'F' )
               m_CurrentlyFacingDirection = DIRECTION.RIGHT;
            else
               throw new Exception( );

         }
         else if( m_CurrentlyFacingDirection == DIRECTION.RIGHT )
         {
            if( current == '-' )
               m_CurrentlyFacingDirection = DIRECTION.RIGHT;
            else if( current == '7' )
               m_CurrentlyFacingDirection = DIRECTION.DOWN;
            else if( current == 'J' )
               m_CurrentlyFacingDirection = DIRECTION.UP;
            else
               throw new Exception( );
         }
         else if( m_CurrentlyFacingDirection == DIRECTION.DOWN )
         {
            if( current == '|' )
               m_CurrentlyFacingDirection = DIRECTION.DOWN;
            else if( current == 'L' )
               m_CurrentlyFacingDirection = DIRECTION.RIGHT;
            else if( current == 'J' )
               m_CurrentlyFacingDirection = DIRECTION.LEFT;
            else
               throw new Exception( );

         }
         else if( m_CurrentlyFacingDirection == DIRECTION.LEFT )
         {
            if( current == '-' )
               m_CurrentlyFacingDirection = DIRECTION.LEFT;
            else if( current == 'L' )
               m_CurrentlyFacingDirection = DIRECTION.UP;
            else if( current == 'F' )
               m_CurrentlyFacingDirection = DIRECTION.DOWN;
            else
               throw new Exception( );
         }

      //If the code reached this point and its not S, throw
         return false;
      }
   #endregion
   }
}
