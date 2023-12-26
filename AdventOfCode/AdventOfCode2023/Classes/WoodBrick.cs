using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class WoodBrick
   {

   /*LOCAL CLASSES*/
   #region

      public class Brick
      {
         public Brick( int[] p1, int[] p2, int id )
         {
            this.X1 = Math.Min( p1[0], p2[0] );
            this.X2 = Math.Max( p1[0], p2[0] );
            this.Y1 = Math.Min( p1[1], p2[1] );
            this.Y2 = Math.Max( p1[1], p2[1] );
            this.Z1 = Math.Min( p1[2], p2[2] );
            this.Z2 = Math.Max( p1[2], p2[2] );
            Supports = new HashSet<Brick>( );
            SupportedBy = new HashSet<Brick>( );
            this.ID = id;
         }
         public int ID { get; private set; }
         public int X1 { get; private set; }
         public int Y1 { get; private set; }
         public int Z1 { get; private set; }
         public int X2 { get; private set; }
         public int Y2 { get; private set; }
         public int Z2 { get; private set; }
         public HashSet<Brick> Supports { get; set; } //A set of all the bricks that lay directly on top of this brick
         public HashSet<Brick> SupportedBy { get; set; } //A set of all the bricks that directly support this brick
         public bool Intersects( Brick other ) { return this.X1 <= other.X2 && this.X2 >= other.X1 && this.Y1 <= other.Y2 && this.Y2 >= other.Y1 && this.Z1 <= other.Z2 && this.Z2 >= other.Z1; }
         public bool IsSupportedBy( Brick other ) { return this.X1 <= other.X2 && this.X2 >= other.X1 && this.Y1 <= other.Y2 && this.Y2 >= other.Y1 && this.Z1 - 1 <= other.Z2 && this.Z2 - 1 >= other.Z1; }
         public void MoveZTo( int z )
         {
            int length = this.Z2 - this.Z1;
            this.Z1 = z;
            this.Z2 = z + length;
         }
      }

   #endregion

      //Count the chain reaction for a single brick. Is done by traversing up the structure 
      public static int ChainReaction( Brick brick )
      {
         HashSet<Brick> fallen = new HashSet<Brick>( ); //All the fallen bricks
         Queue<Brick> work = new Queue<Brick>( ); //All the work left
         work.Enqueue( brick ); //Add this brick to the fallen que. It has been disintegrated.

         while( work.Count > 0 )
         {
            Brick b = work.Dequeue( ); //GEt the current brick

         //Add it to the list of fallen bricks.
            fallen.Add( b );

         //FOr each brick that this brick supports, que the bricks that are still supported by another brick.
            foreach( Brick s in b.Supports )
            {
            //Skip if supported by other bricks. They stay in place.
               if( s.SupportedBy.Except( fallen ).Any( ) ) //Check if there are any bricks that have supports that have not fallen. If so, they should not be traversed through
                  continue;

            //If code reached this point, we know that it is only supported by other bricks that has fallen, we need to add it
               work.Enqueue( s );
            }
         }
         return fallen.Count - 1;
      }

      //P2
      public static int CountChainReactionForAllBricks( List<Brick> bricks )
      {
         int ans2 = 0;
         foreach( Brick b in bricks )
            ans2 += ChainReaction( b );
         return ans2;
      }

      //P1
      public static int CountDisintegratables( List<Brick> bricks )
      {
         int safeBlocks = 0;
         for( int i = 0; i < bricks.Count; i++ )
         {
            bool isSafe = true;
            foreach( var b in bricks[i].Supports )
            {
               if( b.SupportedBy.Count <= 1 )
               {
                  isSafe = false;
                  break;
               }
            }
            if( isSafe )
               safeBlocks++;
         }
         return safeBlocks;
      }

      //Fall to place.
      public static List<Brick> Fall( List<Brick> brickList )
      {
      //Sort the brick array from lowest z1 coordinate to highest z1 coordinate. 
         Brick[ ] bricks = brickList.ToArray( );
         Array.Sort( bricks, ( b1, b2 ) => b1.Z1 - b2.Z1 ); //Sorts array by comparing z1 coordinate of two bricks.

      //Move first brick.
         bricks[0].MoveZTo( 1 );
         List<Brick> fallen = new List<Brick>( );
         fallen.Add( bricks[0] );

         for( int j = 1; j < bricks.Length; j++ )
         {
            Brick brick = bricks[j];

         //Find the highest Z2 of the bricks that has fallen to speed up falling of current brick.
            int startZ = fallen.Select( x => x.Z2 ).ToList( ).Max( );
            brick.MoveZTo( startZ + 1 );

         //Fall until brick is supported by another brick
            while( brick.Z1 > 1 )
            {
            //Move brick one down..
               brick.MoveZTo( brick.Z1 - 1 );

            //Check if the current brick intersects with any of the fallen, if it does, add ned support to list and move to next brick.
               if( fallen.Any( brick.Intersects ) )
               {

               //We moved one too far. Move it up again.
                  brick.MoveZTo( brick.Z1 + 1 );

               //Add new support to this brick, and add the support for the increment brick
                  foreach( Brick b in fallen )
                  {
                     if( brick.IsSupportedBy( b ) )
                     {
                        brick.SupportedBy.Add( b );
                        b.Supports.Add( brick );
                     }
                  }

               //We have populated all the supports for this brick, and we should not move it any more. Jump to next brick.
                  break;
               }
            }
         //We have fallen the brick downward to completion. Add this to the list of fallen bricks.
            fallen.Add( brick );
         }
         return bricks.ToList( );
      }

      //Create all bricks from the input list
      public static List<Brick> ParseBricks( string[] inpBricks )
      {
         Brick[] allBricks = new Brick[inpBricks.Length];
         for( int i = 0; i<inpBricks.Length; i++ )
         {
            string[ ] spl = inpBricks[i].Split( new char[ ] { '~' }, StringSplitOptions.RemoveEmptyEntries );
            string[ ] startSpl = spl[0].Split( new char[ ] { ',' }, StringSplitOptions.RemoveEmptyEntries );
            string[ ] endSpl = spl[1].Split( new char[ ] { ',' }, StringSplitOptions.RemoveEmptyEntries );
            int[] startVec = new int[3];
            int[] endVec = new int[3];
            for( int j = 0; j<3; j++ )
            {
               startVec[j] = int.Parse( startSpl[j] );
               endVec[j] = int.Parse( endSpl[j] );
            }
            allBricks[i] = new Brick( startVec, endVec, i+1 );
         }
         return allBricks.ToList( );
      }

   /*METHODS*/
   #region
   #endregion
   }
}
