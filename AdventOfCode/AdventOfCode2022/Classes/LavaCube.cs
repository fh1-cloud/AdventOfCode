using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   internal class LavaCube
   {
   /*ENUMS*/
   #region

      public enum CUBEFACE
      {
         POSX = 1,
         NEGX = 2,
         POSY = 3,
         NEGY = 4,
         POSZ = 5,
         NEGZ = 6,
      }

   #endregion

   /*CONSTRUCTORS*/
   #region
      public LavaCube( string line )
      {
         string[] spl1 = line.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
         this.X = int.Parse( spl1[0] );
         this.Y = int.Parse( spl1[1] );
         this.Z = int.Parse( spl1[2] );
      }
   #endregion

   /*PROPERTIES*/
   #region
      public int X { get; set; }
      public int Y { get; set; }
      public int Z { get; set; }
   #endregion

   /*STATIC METHODS*/
   #region
      public static long P2( string[] inp )
      {
      //Create lava cubes.
         List<LavaCube> cubes = new List<LavaCube>( );
         foreach( string l in inp )
            cubes.Add( new LavaCube( l ) );

      //Find extreme negative x, and a cube that has that coordinate.
         int minX = int.MaxValue;
         int maxX = int.MinValue;
         int minY = int.MaxValue;
         int maxY = int.MinValue;
         int minZ = int.MaxValue;
         int maxZ = int.MinValue;
         foreach( LavaCube c in cubes )
         {
            if( c.X < minX ) minX = c.X;
            if( c.X > maxX ) maxX = c.X;
            if( c.Y < minY ) minY = c.Y;
            if( c.Y > maxY ) maxY = c.Y;
            if( c.Z < minZ ) minZ = c.Z;
            if( c.Z > maxZ ) maxZ = c.Z;
         }
            
      //Create the grid.
         Dictionary<(int,int,int),int> grid = new Dictionary<(int, int, int), int>( );
         for( int i = minX-1; i<= maxX+1; i++ )
            for( int j = minY-1; j<= maxY+1; j++ )
               for( int k = minZ-1; k<= maxZ+1; k++ )
                  grid.Add( (i, j, k), -1 );

      //Populate the cubes with 1s
         foreach( LavaCube c in cubes )
            grid[(c.X, c.Y, c.Z)] = 1;

      //Create a hash set of all the external surfaces..
         HashSet<string> externalSurfaces = new HashSet<string>( );

      //Start the external flood fill algorithm..
         Queue<(int,int,int)> work = new Queue<(int, int, int)>( );
         work.Enqueue( ( minX - 1, minY - 1, minZ - 1) );

         while( work.Count > 0 )
         {
         //Get next value
            (int x,int y,int z) p = work.Dequeue( );

         //Skip if already checked.
            if( grid[(p.x, p.y, p.z)] == 2 )
               continue;

         //Flag as visited
            grid[(p.x, p.y, p.z)] = 2;

         //Get neighbours if they havent been visited.
            ( int X, int Y, int Z ) negX = ( p.x - 1, p.y, p.z );
            if( grid.TryGetValue( ( negX ), out int negXRes ) )
            {
               if( negXRes == 1 )
                  externalSurfaces.Add( GetCubeStringFace( p, CUBEFACE.NEGX ) );
               else if( negXRes == -1 )
                  work.Enqueue( negX );
            }

            ( int X, int Y, int Z ) posX = ( p.x + 1, p.y, p.z );
            if( grid.TryGetValue( ( posX ), out int posXRes ) )
            {
               if( posXRes == 1 )
                  externalSurfaces.Add( GetCubeStringFace( p, CUBEFACE.POSX ) );
               else if( posXRes == -1 )
                  work.Enqueue( posX );
            }

            ( int X, int Y, int Z ) negY = ( p.x, p.y - 1, p.z );
            if( grid.TryGetValue( ( negY ), out int negYRes ) )
            {
               if( negYRes == 1 )
                  externalSurfaces.Add( GetCubeStringFace( p, CUBEFACE.NEGY ) );
               else if( negYRes == -1 )
                  work.Enqueue( negY );
            }

            ( int X, int Y, int Z ) posY = ( p.x, p.y + 1, p.z );
            if( grid.TryGetValue( ( posY ), out int posYRes ) )
            {
               if( posYRes == 1 )
                  externalSurfaces.Add( GetCubeStringFace( p, CUBEFACE.POSY ) );
               else if( posYRes == -1 )
                  work.Enqueue( posY );
            }

            ( int X, int Y, int Z ) negZ = ( p.x, p.y, p.z - 1);
            if( grid.TryGetValue( ( negZ ), out int negZRes ) )
            {
               if( negZRes == 1 )
                  externalSurfaces.Add( GetCubeStringFace( p, CUBEFACE.NEGZ ) );
               else if( negZRes == -1 )
                  work.Enqueue( negZ );
            }

            ( int X, int Y, int Z ) posZ = ( p.x, p.y, p.z+1 );
            if( grid.TryGetValue( ( posZ ), out int posZRes ) )
            {
               if( posZRes == 1 )
                  externalSurfaces.Add( GetCubeStringFace( p, CUBEFACE.POSZ ) );
               else if( posZRes == -1 )
                  work.Enqueue( posZ );
            }

         }
         return externalSurfaces.Count;
      }

      public static string GetCubeStringFace( ( int X, int Y, int Z ) cubePoint, CUBEFACE face )
      {
         return $"{cubePoint.X},{cubePoint.Y},{cubePoint.Z},{face}";
      }

      #endregion

   }
}
