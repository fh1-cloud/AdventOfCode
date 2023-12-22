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
      }

      protected bool IsPositionInBrick( Position pos )
      {
      //Check the max bounds..
         if( this.MaxZ < pos.Z || this.MinZ > pos.Z || this.MaxX < pos.X || this.MinX > pos.X || this.MaxY < pos.Y || this.MinY > pos.Y )
            return false;

      //If the codre reached this point, we need to check if the position is on the line..



      }


      protected bool HasBrickDirectlyUnder( Dictionary<int,WoodBrick> allBricks )
      {
      //Get all horizontal plane points..
         HashSet<Position> planePoints = GetHorizontalPlanePoints( true );


         foreach( KeyValuePair<int, WoodBrick> kvp in allBricks )
         {
         //Check z coordinate first.
            if( kvp.Value.MaxZ != this.MinZ - 1)
               continue;




         }

         

      }

   #endregion

   /*STATIC METHODS*/
   #region

      public static List<WoodBrick> CreateAllBricksAndSettle( string[] bricks )
      {
         List<WoodBrick> allBricks = new List<WoodBrick>( );
         for( int i = 0; i<bricks.Length; i++ )
            allBricks.Add( new WoodBrick( bricks[i], i+1 ) );
         return allBricks;
      }

      public static void Settle( Dictionary<int,WoodBrick> allBricks ) //Settles all the bricks in the dictionary..
      {

         bool movedSomething = true;

         while( movedSomething )
         {




         }




      }


      #endregion
   }
}
