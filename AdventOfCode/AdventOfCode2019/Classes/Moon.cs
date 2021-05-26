using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{

   /// <summary>
   /// A class that represents a moon in AoC2019 D12
   /// </summary>
   public class Moon
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected UVector3D m_Position = new UVector3D( );
      protected UVector3D m_Velocity = new UVector3D( );

   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor
      /// </summary>
      public Moon( long xpos, long ypos, long zpos )
      {
         m_Position.X = xpos;
         m_Position.Z = zpos;
         m_Position.Y = ypos;
      }


      public Moon( string inp )
      {
      //Remove triangles..
         inp = inp.Remove( 0, 1 );
         inp = inp.Remove( inp.Length - 1, 1 );

      //Split by space.
         string[] spl = inp.Split( ' ' );


      //Remove xyz
         string[] trimmed = new string[3];
         int i = 0;
         foreach( string s in spl )
            trimmed[i++] = s.Remove(0,2 );

      //Remove the commas..
         trimmed[0] = trimmed[0].Remove( trimmed[0].Length - 1, 1 );
         trimmed[1] = trimmed[1].Remove( trimmed[1].Length - 1, 1 );

      //Parse to double for the position..
         m_Position.X = double.Parse( trimmed[0] );
         m_Position.Y = double.Parse( trimmed[1] );
         m_Position.Z = double.Parse( trimmed[2] );

      }

   #endregion

   /*PROPERTIES*/
   #region

      public double XCoor => m_Position.X;
      public double YCoor => m_Position.Y;
      public double ZCoor => m_Position.Z;
      public double XVel => m_Velocity.X;
      public double YVel => m_Velocity.Y;
      public double ZVel => m_Velocity.Z;

   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region


      public string PrintState( )
      {
         return String.Format( "pos=<x= {0,4}, y= {1,4}, z={2,4}>, vel=<x= {3,4}, y= {4,4}, z={5,4}>", m_Position.X, m_Position.Y, m_Position.Z, m_Velocity.X, m_Velocity.Y, m_Velocity.Z );
      }

      public void ApplyVelocity( )
      {
         m_Position += m_Velocity;
      }

      public double GetPotentialEnergy( )
      {
         return Math.Abs( m_Position.X ) + Math.Abs( m_Position.Y ) + Math.Abs( m_Position.Z );
      }

      public double GetKineticEnergy( )
      {
         return Math.Abs( m_Velocity.X ) + Math.Abs( m_Velocity.Y ) + Math.Abs( m_Velocity.Z );
      }


   #endregion

   /*STATIC METHODS*/
   #region

      public static void ApplyGravityOnPair( KeyValuePair<Moon,Moon> pair )
      {

      //X velocity
         if( pair.Key.m_Position.X < pair.Value.m_Position.X ) //Key position is less.
         {
            pair.Key.m_Velocity.X++;
            pair.Value.m_Velocity.X--;
         }
         else if( pair.Key.m_Position.X > pair.Value.m_Position.X ) //Key position is larger
         {
            pair.Key.m_Velocity.X--;
            pair.Value.m_Velocity.X++;
         }
         
      //Y velocity
         if( pair.Key.m_Position.Y < pair.Value.m_Position.Y ) //Key position is less.
         {
            pair.Key.m_Velocity.Y++;
            pair.Value.m_Velocity.Y--;
         }
         else if( pair.Key.m_Position.Y > pair.Value.m_Position.Y ) //Key position is larger
         {
            pair.Key.m_Velocity.Y--;
            pair.Value.m_Velocity.Y++;
         }

      //Z velocity
         if( pair.Key.m_Position.Z < pair.Value.m_Position.Z ) //Key position is less.
         {
            pair.Key.m_Velocity.Z++;
            pair.Value.m_Velocity.Z--;
         }
         else if( pair.Key.m_Position.Z > pair.Value.m_Position.Z ) //Key position is larger
         {
            pair.Key.m_Velocity.Z--;
            pair.Value.m_Velocity.Z++;
         }
      }


   #endregion


   }
}
