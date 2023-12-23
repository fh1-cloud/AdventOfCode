using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   internal class WoodBrick
   {
   /*LOCAL CLASSES*/
   #region

      internal struct Direction
      {
         internal Direction( int x, int y, int z )
         {
            this.X = x;
            this.Y = y;
            this.Z = z;
         }

         internal int X { get; set; }
         internal int Y { get; set; }
         internal int Z { get; set; }

         public static Direction NegZ( ) { return new Direction( 0, 0,-1 ); }
         public static Direction PosZ( ) { return new Direction( 0, 0,-1 ); }
         public static Direction NegX( ) { return new Direction(-1, 0, 0 ); }
         public static Direction PosX( ) { return new Direction( 1, 0, 0 ); }
         public static Direction NegY( ) { return new Direction( 0,-1, 0 ); }
         public static Direction PosY( ) { return new Direction( 0, 1, 0 ); }
         public static Direction Zero( ) { return new Direction( 0, 0, 0 ); }
         public static bool operator == ( Direction lhs, Direction rhs ) { return ( lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z ); }
         public static bool operator != ( Direction lhs, Direction rhs ) { return !( lhs == rhs ); }

      }



      internal struct Position
      {

         public Position( int x, int y, int z )
         {
            this.X = x;
            this.Y = y;
            this.Z = z;
         }

         public int X { get; set; }
         public int Y { get; set; }
         public int Z { get; set; }

         public Position GetPositionFrom( Direction dir, int length )
         {
            return new Position( this.X + dir.X*length, this.Y + dir.Y*length, this.Z + dir.Z*length );
         }

      }

   #endregion

   /*ENUMS*/
   #region
      public enum WOODBRICKORIENTATION
      {
         X,
         Y,
         Z,
         POINT,
      }
   #endregion

   /*MEMBERS*/
   #region

      protected internal Position m_P1;
      protected internal Position m_P2;
      protected internal int m_Length;
      protected internal int m_ID;
      protected internal Direction m_Dir;

   #endregion

   /*CONSTRUCTORS*/
   #region
      internal WoodBrick( string inpLine, int id )
      {
         string[] spl = inpLine.Split( new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries );
         string[] startSpl = spl[0].Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
         string[] endSpl = spl[1].Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
         m_P1 = new Position( int.Parse( startSpl[0] ), int.Parse( startSpl[1] ), int.Parse( startSpl[2] ) );
         m_P2 = new Position( int.Parse( endSpl[0] ), int.Parse( endSpl[1] ), int.Parse( endSpl[2] ) );
         m_Length = Math.Abs( m_P1.X - m_P2.X ) + Math.Abs( m_P1.Y - m_P2.Y ) + Math.Abs( m_P1.Z - m_P2.Z ) + 1;
         m_ID = id;

         if( m_P1.X == m_P2.X && m_P1.Y == m_P2.Y && m_P1.Z == m_P2.Z )
            m_Dir = Direction.PosX( );
         else if( m_P1.X == m_P2.X && m_P1.Y == m_P2.Y )
         {
            if( m_P1.Z > m_P2.Z )
               m_Dir = Direction.NegZ( );
            else
               m_Dir = Direction.PosZ( );
         }
         else if( m_P1.X == m_P2.X && m_P1.Z == m_P2.Z )
         {
            if( m_P1.Y > m_P2.Y )
               m_Dir = Direction.NegY( );
            else
               m_Dir = Direction.PosY( );
         }
         else
         {
            if( m_P1.X > m_P2.X )
               m_Dir = Direction.NegX( );
            else
               m_Dir = Direction.PosX( );
         }

      }

      internal WoodBrick( WoodBrick oldBrick )
      {
         m_P1 = oldBrick.m_P1;
         m_P2 = oldBrick.m_P2;
         m_Length = oldBrick.m_Length;
         m_ID = oldBrick.m_ID;
         m_Dir = oldBrick.m_Dir;
      }


   #endregion


   /*PROPERTIES*/
   #region
      internal int MinZ { get { return Math.Min( m_P1.Z, m_P2.Z ); } }
      internal int MaxZ { get { return Math.Max( m_P1.Z, m_P2.Z ); } }
      internal int MinX { get { return Math.Min( m_P1.X, m_P2.X ); } }
      internal int MaxX { get { return Math.Max( m_P1.X, m_P2.X ); } }
      internal int MinY { get { return Math.Min( m_P1.Y, m_P2.Y ); } }
      internal int MaxY { get { return Math.Max( m_P1.Y, m_P2.Y ); } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      protected HashSet<Position> GetHorizontalPlanePoints( bool getPlaneBelow ) //Get the points in the horizontal plane that this brick covers
      {
         HashSet<Position> p = new HashSet<Position>( );

      //Get the plane below if requested
         Direction belowVec = Direction.Zero( );
         if( getPlaneBelow )
            belowVec = Direction.NegZ( );

         if( m_Dir == Direction.PosZ( ) )
            p.Add( m_P1.GetPositionFrom( belowVec, 1) );
         else if( m_Dir == Direction.NegZ( ) )
            p.Add( m_P2.GetPositionFrom( belowVec, 1) );
         else //Either x or y brick
            for( int i = 0; i<m_Length; i++ )
               p.Add( m_P1.GetPositionFrom( m_Dir, i ).GetPositionFrom( belowVec, 1 ) );

         return p;
      }

      protected void Move( Direction dir, int length )
      {
         m_P1 = new Position( m_P1.X + dir.X*length, m_P1.Y + dir.Y*length, m_P1.Z + dir.Z*length );
         m_P2 = new Position( m_P2.X + dir.X*length, m_P2.Y + dir.Y*length, m_P2.Z + dir.Z*length );
         Console.WriteLine( $"Moved brick {m_ID} to {this.MinZ}" );
      }


      protected bool IsPointOnBrick( Position point )
      {
         return IsPointOnLine( point, m_P1, m_P2 );
      }


      protected bool HasBrickDirectlyUnder( Dictionary<int,WoodBrick> allBricks )
      {
      //Get all horizontal plane points..
         HashSet<Position> planePoints = GetHorizontalPlanePoints( true );

         foreach( KeyValuePair<int, WoodBrick> kvp in allBricks )
         {
         //Skip if same brick
            if( m_ID == kvp.Key )
               continue;

         //Check z coordinate first.
            if( kvp.Value.MaxZ != this.MinZ - 1)
               continue;

         //Check if the points in the plance points is on the brick..
            foreach( Position p in planePoints )
               if( kvp.Value.IsPointOnBrick( p ) )
                  return true;
         }

      //If the code reached this point, this brick does not have a brick under it.
         return false;

      }

   #endregion

   /*STATIC METHODS*/
   #region
      protected static bool IsPointOnLine( Position point, Position startLine, Position endLine )
      {

         double ab = Math.Sqrt( ( endLine.X - startLine.X ) * ( endLine.X - startLine.X ) + ( endLine.Y - startLine.Y ) * ( endLine.Y - startLine.Y ) + ( endLine.Z - startLine.Z ) * ( endLine.Z - startLine.Z ) );
         double ap = Math.Sqrt( ( point.X - startLine.X ) * ( point.X - startLine.X ) + ( point.Y - startLine.Y ) * ( point.Y - startLine.Y) + ( point.Z-startLine.Z ) * ( point.Z - startLine.Z ) );
         double pb = Math.Sqrt( ( endLine.X - point.X ) * ( endLine.X - point.X ) + ( endLine.Y - point.Y )*( endLine.Y - point.Y ) + ( endLine.Z - point.Z ) * ( endLine.Z - point.Z ) );
         if( Math.Abs( ap + pb - ab ) < 0.01 )
            return true;

      //If the code reached here, its not on the line.
         return false;
      }

      public static Dictionary<int,WoodBrick> CreateAllBricksAndSettle( string[] bricks )
      {
         Dictionary<int,WoodBrick> allBricks = new Dictionary<int, WoodBrick>( );
         for( int i = 0; i<bricks.Length; i++ )
            allBricks.Add( i+1, new WoodBrick( bricks[i], i+1 ) );

      //Settles the bricks after reading input..
         Settle( allBricks );

         return allBricks;
      }

      public static Dictionary<int, WoodBrick> DeepCopy( Dictionary<int,WoodBrick> allBricks )
      {
         Dictionary<int, WoodBrick> res = new Dictionary<int, WoodBrick>( );
         foreach( KeyValuePair<int, WoodBrick> b in allBricks  )
            res.Add( b.Key, new WoodBrick( b.Value ) );
         return res;
      }

      public static Dictionary<int,WoodBrick> FindBricksThatCanBeDisintegrated( Dictionary<int,WoodBrick> allBricks )
      {

         Dictionary<int,WoodBrick> canBeDeleted = new Dictionary<int, WoodBrick>( );

         foreach( KeyValuePair<int, WoodBrick> b in allBricks )
         {
         //Create copy.
            Dictionary<int,WoodBrick> copy = DeepCopy( allBricks );

         //Remove brick.
            copy.Remove( b.Key );

         //Try to settle
            bool somethingMoved = Settle( copy, true );
            if( !somethingMoved )
               canBeDeleted.Add( b.Key, b.Value );
         }

         return canBeDeleted;

      }


      public static bool Settle( Dictionary<int,WoodBrick> allBricks, bool abortAtFirstMove = false ) //Settles all the bricks in the dictionary.. Returns a bool indicating wheter or not something was moved.
      {
         bool movedSomething = true;
         Direction moveDir = Direction.NegZ( );
         int moveCounter = 0;
         while( movedSomething )
         {
            movedSomething = false;

         //Outer loop over bricks..
            foreach( KeyValuePair<int, WoodBrick> kvp in allBricks )
            {
               if( kvp.Value.MinZ > 1 )
               {
                  if( !kvp.Value.HasBrickDirectlyUnder( allBricks ) )
                  {
                  //If this is the checker method, return true that something moved
                     if( abortAtFirstMove )
                        return true;

                     moveCounter++;
                     kvp.Value.Move( moveDir, 1 );
                     movedSomething = true;
                     break;
                  }

               }
            }
         }

      //Return true if all was settled.
         return moveCounter > 0;
      }


      #endregion
   }
}
