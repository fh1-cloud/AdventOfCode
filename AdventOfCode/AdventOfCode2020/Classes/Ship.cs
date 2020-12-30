using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2020.Extensions;
using AdventOfCode2020.Numerics;

namespace AdventOfCode2020
{

   /// <summary>
   /// A class that represents the ferry in AdventOfCode Dec12
   /// </summary>
   public class Ship
   {


      /*ENUMS*/


      #region

      #endregion


      /*LOCAL CLASSES*/
      #region


      #endregion



   /*MEMBERS*/
      #region

      protected Vector2D m_Direction;
      protected Vector2D m_Position;

      protected bool m_WayPointMode = false;
      protected Vector2D m_WayPointPosition;          //The waypoint. Relative to the ship.

      #endregion


   /*CONSTRUCTORS*/
      #region

      /// <summary>
      /// Creates a new ship
      /// </summary>
      public Ship( bool wpm = false )
      {
         m_Position = new Vector2D( 0.0, 0.0 );       //The position the ship is currently at
         m_Direction = new Vector2D( 1.0, 0.0 );     //The direction the ship is currently facing
         m_WayPointMode = wpm;
         if( m_WayPointMode )
         {
            m_WayPointPosition = new Vector2D( 10.0, 1.0 );
         }
      }

      #endregion


   /*METHODS*/
      #region

      /// <summary>
      /// Carries out an instruction
      /// </summary>
      /// <param name="ins"></param>
      public void CarryOutInstruction( ShipInstruction ins )
      {
         if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.TURNLEFT || ins.Instruction == ShipInstruction.SHIPINSTRUCTION.TURNRIGHT )
            TurnCommand( ins );
         else
            MoveCommand( ins );
      }


      /// <summary>
      /// Turn the ship according to the instruction
      /// </summary>
      /// <param name="ins"></param>
      protected void TurnCommand( ShipInstruction ins )
      {
         if( !m_WayPointMode )
         {
         //Get the turn direction..
            double modifier = 1.0;
            if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.TURNRIGHT )
               modifier = -1.0;
         //Turns the ship in the direction entered.
            double angleRad = ins.Value.ToRad( ) * modifier;
            Matrix2x2 rotMat = Matrix2x2.GetRotationMatrix( angleRad );

         //Get the new direction..
            m_Direction = rotMat * m_Direction;
         }
         else //Waypoint mode..
         {
         //Get the turn direction..
            double modifier = 1.0;
            if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.TURNRIGHT )
               modifier = -1.0;

         //Rotates the waypoint. The waypoint is defined relative to the ship, such that it suffices to rotate the waypoint as before
            double angleRad = ins.Value.ToRad( ) * modifier;
            Matrix2x2 rotMat = Matrix2x2.GetRotationMatrix( angleRad );
            m_WayPointPosition = rotMat * m_WayPointPosition;
         }

      }

      /// <summary>
      /// Move the ship according to the instruction
      /// </summary>
      /// <param name="ins"></param>
      protected void MoveCommand( ShipInstruction ins )
      {
         if( !m_WayPointMode )
         {
         //Determine the direction the ship is moving
            Vector2D dir;
            if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.MOVEFORWARD )
               dir = new Vector2D( m_Direction );
            else if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.MOVENORTH )
               dir = new Vector2D( 0.0, 1.0 );
            else if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.MOVESOUTH )
               dir = new Vector2D( 0.0, -1.0 );
            else if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.MOVEEAST )
               dir = new Vector2D( 1.0, 0.0 );
            else if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.MOVEWEST )
               dir = new Vector2D( -1.0, 0.0 );
            else
               throw new Exception( );

         //Create the vector displacement.
            Vector2D displacementVector = ins.Value * dir;

         //Update the current position of the ship
            m_Position += displacementVector;

         }
         else //Waypoint mode..
         {
            if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.MOVEFORWARD )
            {
            //The ship should move towards the waypoint. 
               for( int i = 0; i < ins.Value; i++ )
                  m_Position += m_WayPointPosition;
            }
            else //The waypoint should be moved
            {
               Vector2D dir;
               if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.MOVENORTH )
                  dir = new Vector2D( 0.0, 1.0 );
               else if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.MOVESOUTH )
                  dir = new Vector2D( 0.0, -1.0 );
               else if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.MOVEEAST )
                  dir = new Vector2D( 1.0, 0.0 );
               else if( ins.Instruction == ShipInstruction.SHIPINSTRUCTION.MOVEWEST )
                  dir = new Vector2D( -1.0, 0.0 );
               else
                  throw new Exception( );

            //Create the vector displacement.
               Vector2D displacementVector = ins.Value * dir;

            //Update the current position of the ship
               m_WayPointPosition += displacementVector;

            }


         }
      }



      /// <summary>
      /// Gets the manhattan distance for the ship
      /// </summary>
      /// <returns></returns>
      public double GetManhattanDistance( )
      {
         return Math.Abs( m_Position.X ) + Math.Abs( m_Position.Y );
      }


      #endregion

   }
}
