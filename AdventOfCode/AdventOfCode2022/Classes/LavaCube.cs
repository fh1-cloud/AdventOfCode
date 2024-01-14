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

   /*LOCAL CLASSES*/
   #region

   #endregion

   /*MEMBERS*/
   #region
   #endregion

   /*CONSTRUCTORS*/
   #region
      public LavaCube( string line )
      {
         string[] spl1 = line.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
         this.X = int.Parse( spl1[0] );
         this.Y = int.Parse( spl1[1] );
         this.Z = int.Parse( spl1[2] );
         this.Connections = new Dictionary<CUBEFACE, LavaCube>( );
      }
   #endregion

   /*PROPERTIES*/
   #region
      public int X { get; set; }
      public int Y { get; set; }
      public int Z { get; set; }
      public Dictionary<CUBEFACE,LavaCube> Connections { get; set; }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region


      public static void Connect( LavaCube c1, LavaCube c2 )
      {

         if( Math.Abs( c1.X - c2.X ) == 1 && c1.Y == c2.Y && c1.Z == c2.Z ) //Connected in x direction..
         {
            if( c1.X > c2.X )
            {
               c1.Connections[CUBEFACE.NEGX] = c2;
               c2.Connections[CUBEFACE.POSX] = c1;
            }
            else
            {
               c1.Connections[CUBEFACE.POSX] = c2;
               c2.Connections[CUBEFACE.NEGX] = c1;
            }
         }
         if( Math.Abs( c1.Y - c2.Y ) == 1 && c1.X == c2.X && c1.Z == c2.Z ) //Connected in y direction..
         {
            if( c1.Y > c2.Y )
            {
               c1.Connections[CUBEFACE.NEGY] = c2;
               c2.Connections[CUBEFACE.POSY] = c1;
            }
            else
            {
               c1.Connections[CUBEFACE.POSY] = c2;
               c2.Connections[CUBEFACE.NEGY] = c1;
            }
         }
         if( Math.Abs( c1.Z - c2.Z ) == 1 && c1.Y == c2.Y && c1.X == c2.X ) //Connected in z direction..
         {
            if( c1.Z > c2.Z )
            {
               c1.Connections[CUBEFACE.NEGZ] = c2;
               c2.Connections[CUBEFACE.POSZ] = c1;
            }
            else
            {
               c1.Connections[CUBEFACE.POSZ] = c2;
               c2.Connections[CUBEFACE.NEGZ] = c1;
            }
         }
      }


      //public static long P1( string[] inp )
      //{

      ////Create lava cubes.
      //   List<LavaCube> cubes = new List<LavaCube>( );
      //   foreach( string l in inp )
      //      cubes.Add( new LavaCube( l ) );

      //   for( int i = 0; i<cubes.Count; i++ )
      //      for( int j = i+1; j<cubes.Count; j++ )
      //         Connect( cubes[i], cubes[j] );


      //   HashSet<LavaCube> cubesLeft = cubes.ToHashSet( );

      //   long totalFreeSides = 0;
      //   while( cubesLeft.Count > 0 )
      //   {

      //   //FInd the next cube..
      //      LavaCube thisCube = cubesLeft.FirstOrDefault( );

      //   //Find all connected cubes..
      //      HashSet<LavaCube> cubesInGroup = new HashSet<LavaCube>( );
      //      thisCube.GetAllConnectedCubes( cubesInGroup );

      //   //Sum the free sides..
      //      long thisGroupFreeSides = 0;
      //      foreach( LavaCube cig in cubesInGroup )
      //         thisGroupFreeSides += cig.GetFreeSides( );

      //      totalFreeSides += thisGroupFreeSides;

      //   //Remove the cubes in this group from the cubes left..
      //      foreach( LavaCube cig in cubesInGroup )
      //         cubesLeft.Remove( cig );
      //   }

      //   return totalFreeSides;
      //}


      public static long P2( string[] inp )
      {

      //Find only external surfaces, not internal.

      //First, find macrocubes by the connection function
      //Then, for each macrocube, find an external point ( the most extreme X, and start on this side of the cube )
         //Start by propagating from this area and add all neighbouring areas to a que. Sum up all areas until no more are left.
         //Then we have the external area.

      //Create lava cubes.
         List<LavaCube> cubes = new List<LavaCube>( );
         foreach( string l in inp )
         {
            if( l.Length == 0 || l.IndexOf( '/' ) == 0 )
               continue;

            cubes.Add( new LavaCube( l ) );
         }

         for( int i = 0; i<cubes.Count; i++ )
            for( int j = i+1; j<cubes.Count; j++ )
               Connect( cubes[i], cubes[j] );


         HashSet<LavaCube> cubesLeft = cubes.ToHashSet( );

         //CODE DOES NOT CHECK IF CUBES ARE CONTAINED INSIDE OTHER CUBES!
         long totalExternalArea = 0;
         while( cubesLeft.Count > 0 )
         {

         //FInd the next cube..
            LavaCube thisCube = cubesLeft.FirstOrDefault( );

         //Find all connected cubes to this one.
            Dictionary<(int,int,int),LavaCube> cubesInGroup = new Dictionary<(int, int, int), LavaCube>( );
            thisCube.GetCubeGroup( cubesInGroup );

         //Get area of internal points..
            long thisExternalArea = FindExternalArea( cubesInGroup );
            totalExternalArea += thisExternalArea;


         //Remove the cubes in this group from the cubes left..
            foreach( KeyValuePair<(int, int, int), LavaCube> cig in cubesInGroup )
               cubesLeft.Remove( cig.Value);
         }

         return totalExternalArea;


      }

      public static long FindExternalArea( Dictionary<(int,int,int),LavaCube> cubeGroup )
      {

      //Find extreme negative x, and a cube that has that coordinate.
         long minX = long.MaxValue;
         LavaCube minXCube = null;
         foreach( KeyValuePair<(int, int, int), LavaCube> c in cubeGroup )
         {
            if( c.Value.X < minX ) 
            {
               minX = c.Value.X;
               minXCube = c.Value;
            }
         }

      //Declare the hashset of whats already checked..
         HashSet<string> checkedAreas = new HashSet<string>( );

         Queue<(LavaCube,CUBEFACE)> work = new Queue<(LavaCube, CUBEFACE)>( ); //A que of cubes with faces that should be propagated;
         work.Enqueue( ( minXCube, CUBEFACE.NEGX ) ) ;

         while( work.Count > 0 )
         {

         //Get next work item
            (LavaCube Cube, CUBEFACE Face) thisWork = work.Dequeue( );

         //Get the connected faces. Add to que if it havent already been checked..
            List<(LavaCube, CUBEFACE)> connections = GetConnectedAreas( thisWork.Cube, thisWork.Face, cubeGroup );


         //Add more work if not already checked..
            foreach( (LavaCube RetCube, CUBEFACE RetFace ) kvp in connections )
            {
               string hash = GetCubeAreaString( kvp.RetCube, kvp.RetFace ); //Hash will contains a list of all the external areas..
               if( !checkedAreas.Contains( hash ) )
               {
                  work.Enqueue( ( kvp.RetCube, kvp.RetFace ) );
                  checkedAreas.Add( hash );
               }
            }
         }

      //Return the unique areas that are connected..
         return checkedAreas.Count;

      }


      public static string GetCubeAreaString( LavaCube cube, CUBEFACE face )
      {
         return $"{cube.X},{cube.Y},{cube.Z},{face}";
      }

      #endregion

      /*METHODS*/
      #region

      public static List<(LavaCube, CUBEFACE)> GetConnectedAreas( LavaCube cube, CUBEFACE face, Dictionary<(int,int,int), LavaCube> cubesInGroup )
      {
         //Cubes that must be added to the que for further checking.
         List<(LavaCube,CUBEFACE)> retAreas = new List<(LavaCube, CUBEFACE)>( );

         List<(int,int,int,CUBEFACE )> firstLayer = new List<(int, int, int, CUBEFACE)>( );
         List<(int,int,int,CUBEFACE)> secondLayer = new List<(int, int, int, CUBEFACE)>( );
         List<(int,int,int,CUBEFACE)> thirdLayer = new List<(int, int, int, CUBEFACE)>( );

         if( face == CUBEFACE.NEGX || face == CUBEFACE.POSX )
         {
            int deltaX = 1;
            if( face == CUBEFACE.NEGX )
               deltaX = -1;

            firstLayer.Add( ( cube.X + deltaX, cube.Y + 1,  cube.Z      , CUBEFACE.NEGY ) );
            firstLayer.Add( ( cube.X + deltaX, cube.Y - 1,  cube.Z      , CUBEFACE.POSY ) );
            firstLayer.Add( ( cube.X + deltaX, cube.Y,      cube.Z + 1  , CUBEFACE.NEGZ ) );
            firstLayer.Add( ( cube.X + deltaX, cube.Y,      cube.Z - 1  , CUBEFACE.POSZ ) );

            secondLayer.Add( ( cube.X, cube.Y + 1,  cube.Z      , face ) );
            secondLayer.Add( ( cube.X, cube.Y - 1,  cube.Z      , face ) );
            secondLayer.Add( ( cube.X, cube.Y,      cube.Z + 1  , face ) );
            secondLayer.Add( ( cube.X, cube.Y,      cube.Z - 1  , face ) );

            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.POSY) );
            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.NEGY) );
            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.POSZ) );
            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.NEGZ) );

         }
         else if( face == CUBEFACE.POSY || face == CUBEFACE.NEGY )
         {
            int deltaY = 1;
            if( face == CUBEFACE.NEGY )
               deltaY = -1;

            firstLayer.Add( ( cube.X + 1 ,   cube.Y + deltaY,    cube.Z      , CUBEFACE.NEGX ) );
            firstLayer.Add( ( cube.X - 1 ,   cube.Y + deltaY,    cube.Z      , CUBEFACE.POSX ) );
            firstLayer.Add( ( cube.X,        cube.Y + deltaY,    cube.Z + 1  , CUBEFACE.NEGZ ) );
            firstLayer.Add( ( cube.X,        cube.Y + deltaY,    cube.Z - 1  , CUBEFACE.POSZ ) );

            secondLayer.Add( ( cube.X + 1,   cube.Y,     cube.Z      , face ) );
            secondLayer.Add( ( cube.X - 1,   cube.Y,     cube.Z      , face ) );
            secondLayer.Add( ( cube.X,       cube.Y,     cube.Z + 1  , face ) );
            secondLayer.Add( ( cube.X,       cube.Y,     cube.Z - 1  , face ) );

            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.POSX) );
            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.NEGX) );
            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.POSZ) );
            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.NEGZ) );
         }
         else if( face == CUBEFACE.POSZ || face == CUBEFACE.NEGZ )
         {
            int deltaZ = 1;
            if( face == CUBEFACE.NEGZ )
               deltaZ = -1;

            firstLayer.Add( ( cube.X + 1 ,   cube.Y,     cube.Z + deltaZ, CUBEFACE.NEGX ) );
            firstLayer.Add( ( cube.X - 1 ,   cube.Y,     cube.Z + deltaZ, CUBEFACE.POSX ) );
            firstLayer.Add( ( cube.X,        cube.Y + 1, cube.Z + deltaZ, CUBEFACE.NEGY ) );
            firstLayer.Add( ( cube.X,        cube.Y - 1, cube.Z + deltaZ, CUBEFACE.POSY ) );

            secondLayer.Add( ( cube.X + 1,   cube.Y,     cube.Z, face ) );
            secondLayer.Add( ( cube.X - 1,   cube.Y,     cube.Z, face ) );
            secondLayer.Add( ( cube.X,       cube.Y + 1, cube.Z, face ) );
            secondLayer.Add( ( cube.X,       cube.Y - 1, cube.Z, face ) );

            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.POSX) );
            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.NEGX) );
            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.POSY) );
            thirdLayer.Add( (cube.X, cube.Y, cube.Z, CUBEFACE.NEGY) );
         }

         for( int i = 0; i<4; i++ )
         {
            if( cubesInGroup.ContainsKey( (firstLayer[i].Item1, firstLayer[i].Item2, firstLayer[i].Item3 ) ) )
               retAreas.Add( ( cubesInGroup[( firstLayer[i].Item1, firstLayer[i].Item2, firstLayer[i].Item3 )], firstLayer[i].Item4 ) );
            else if( cubesInGroup.ContainsKey( (secondLayer[i].Item1, secondLayer[i].Item2, secondLayer[i].Item3 ) ) )
               retAreas.Add( ( cubesInGroup[( secondLayer[i].Item1, secondLayer[i].Item2, secondLayer[i].Item3 )], secondLayer[i].Item4 ) );
            else
               retAreas.Add( ( cubesInGroup[( thirdLayer[i].Item1, thirdLayer[i].Item2, thirdLayer[i].Item3 )], thirdLayer[i].Item4 ) );
         }
         
         return retAreas;
      }


      public void GetCubeGroup( Dictionary<(int,int,int), LavaCube> cubes ) //Get all cubes connected to this cube..
      {
      //First, add this cube to the set.
         if( cubes.ContainsKey( ( this.X, this.Y, this.Z ) ) )
            return;
         else
            cubes.Add( (this.X, this.Y, this.Z), this );
         foreach( KeyValuePair<CUBEFACE, LavaCube> c in this.Connections )
            c.Value.GetCubeGroup( cubes );
      }


      public int GetFreeSides( )
      {
         return 6 - this.Connections.Count;
      }




   #endregion
   }
}
