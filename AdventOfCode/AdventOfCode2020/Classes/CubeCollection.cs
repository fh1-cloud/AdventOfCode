using AdventOfCode2020.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{


   /// <summary>
   /// A cube collection for AoC DEC17
   /// </summary>
   public class CubeCollection 
   {




   /*MEMBERS*/
   #region

      protected Dictionary<string, IntVector4D> m_Cubes = new Dictionary<string, IntVector4D>( );
      protected long m_NumberOfCycles = 0;

   #endregion

   /*CONSTRUCTORS*/
   #region
   

      /// <summary>
      /// Default constructor. initializes a cube collection from the input in the file
      /// </summary>
      /// <param name="inp"></param>
      public CubeCollection( string[ ] inp )
      {
         //Create all the cubes from the first input..
         for( long i = 0; i < inp.Length; i++ )
         {
            string thisLine = inp[i];
            for( long j = 0; j < thisLine.Length; j++ )
            {
               char c = thisLine[ (int) j];
               if( c == '#' )
               {
                  IntVector4D v = new IntVector4D( i, j, 0, 0 );
                  this.Add( v );
               }
            }
         }
      }

      /// <summary>
      /// Copy constructor. Creates a deep copy of all the cubes in the old dictionary
      /// </summary>
      /// <param name="oldCollection"></param>
      public CubeCollection( CubeCollection oldCollection )
      {
         m_Cubes = oldCollection.GetCopyOfCubes( );
         m_NumberOfCycles = oldCollection.m_NumberOfCycles;
      }

      /// <summary>
      /// Initializes a cube collection with no cubes
      /// </summary>
      public CubeCollection()
      {


      }





   #endregion



   /*PROPERTIES*/
   #region

      /// <summary>
      /// Gets the number of active cubes in the dictionary
      /// </summary>
      public long ActiveCubesInCollection
      {
         get
         {
            return m_Cubes.Count;
         }
      }

   #endregion


   /*OPERATORS*/
   #region

   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// A method that returns a copy of the cubes for use in the copy constructor or the cycle method
      /// </summary>
      /// <returns></returns>
      protected Dictionary<string, IntVector4D> GetCopyOfCubes()
      {
      //Create a deep copy of all the cubes..
         Dictionary<string, IntVector4D> copy = new Dictionary<string, IntVector4D>( );
         foreach( KeyValuePair<string, IntVector4D> cube in m_Cubes )
            copy.Add( cube.Value.ID, new IntVector4D( cube.Value ) );

         return copy;
      }


      /// <summary>
      /// Cycles the cube collection to the next state.
      /// </summary>
      public void Cycle( )
      {
      //Increment number of cycles
         m_NumberOfCycles++;

      //Get the state of the old activated cubes..
         Dictionary<string, IntVector4D> oldActivatedCubes = this.GetCopyOfCubes( );

      //Initialize a list of neibours, this dictionary needs to be checked in the old state, since only neighbours can be activated in a following turn..
         Dictionary<string, IntVector4D> neighbourOfActivatedCubes = new Dictionary<string, IntVector4D>( );

      //Initialize a new collection..
         Dictionary<string, IntVector4D> newCollection = new Dictionary<string, IntVector4D>( );

      //First, check the current active cubes and populate the list of neigbours..
         foreach( KeyValuePair<string,IntVector4D> oldCube in oldActivatedCubes )
         {
         //Populate neighbour list..
            IntVector4D[] neighboursOfActivated = GetNeighbours( oldCube.Value );
            for( int i = 0; i < neighboursOfActivated.Length; i++ )
               if( !neighbourOfActivatedCubes.ContainsKey( neighboursOfActivated[i].ID ) && !oldActivatedCubes.ContainsKey( neighboursOfActivated[i].ID ) )
                  neighbourOfActivatedCubes.Add( neighboursOfActivated[i].ID, neighboursOfActivated[i]);

         }

      //Loop through the existing cubes in the old set. Only add if they dont already 
         foreach( KeyValuePair<string, IntVector4D> oldCube in oldActivatedCubes )
         {
            int nOfActiveNeighbours = GetActiveNeighbours( oldActivatedCubes, oldCube.Value );
            if( nOfActiveNeighbours == 2 || nOfActiveNeighbours == 3 )
               newCollection.Add( oldCube.Value.ID, oldCube.Value );
         }

      //Then, check the neighbours if they should be activated..
         foreach( KeyValuePair<string, IntVector4D> neighbour in neighbourOfActivatedCubes )
         {
            int nOfActiveNeighbours = GetActiveNeighbours( oldActivatedCubes, neighbour.Value );
            if( nOfActiveNeighbours == 3 ) //Exactly three neighbours is active. Activate this cube.
            {
               if( !newCollection.ContainsKey( neighbour.Key ) )
                  newCollection.Add( neighbour.Value.ID, neighbour.Value );
            }
            else //The cube should be inactive. 
            {
               if( newCollection.ContainsKey( neighbour.Key ) )
                  newCollection.Remove( neighbour.Value.ID );
            }
         }

      //Set the cube collection to the new collection..
         m_Cubes = newCollection;


      }



      /// <summary>
      /// Checks if the cube collection contains this vector
      /// </summary>
      /// <param name="v"></param>
      /// <returns></returns>
      public bool Contains( IntVector4D v )
      {
         return m_Cubes.ContainsKey( v.ID );
      }

      /// <summary>
      /// Removes a cuve with the specific position
      /// </summary>
      /// <param name="v"></param>
      public void Add( IntVector4D v )
      {
         m_Cubes.Add( v.ID, v );
      }

      /// <summary>
      /// Adds a cube with the specific position
      /// </summary>
      /// <param name="v"></param>
      public void Remove( IntVector4D v )
      {
         m_Cubes.Remove( v.ID );
      }



   #endregion



   /*STATIC METHODS*/
   #region

      /// <summary>
      /// Gets the number of active neighbours to this position
      /// </summary>
      /// <param name="v"></param>
      /// <returns></returns>
      public static int GetActiveNeighbours( Dictionary<string,IntVector4D> cc, IntVector4D v )
      {
         IntVector4D[] neigh = GetNeighbours( v );
         int nOfActive = 0;
         for( int i = 0; i < neigh.Length; i++ )
         {
            IntVector4D thisNeigh = neigh[i];
            if( cc.ContainsKey( thisNeigh.ID ) )
               nOfActive++;
         }

         return nOfActive;

      }

      /// <summary>
      /// Gets the neighbouring
      /// </summary>
      /// <param name="v"></param>
      /// <returns></returns>
      public static IntVector4D[] GetNeighbours( IntVector4D v )
      {
      //Create an array with all the neighbouring positions from this vector..
         IntVector4D[] retVec = new IntVector4D[80];
         int arrCount = 0;
         for( long i = v.X - 1; i <= v.X+1; i++ )
            for( long j = v.Y - 1; j <= v.Y+1; j++ )
               for( long k = v.Z - 1; k <= v.Z+1; k++ )
                  for( long l = v.W - 1; l <= v.W+1; l++ )
                     if( !( i == v.X && j == v.Y && k == v.Z && l == v.W ) )
                        retVec[arrCount++] = new IntVector4D( i, j, k, l);

         return retVec;
      }




      /// <summary>
      /// Prints the cube state to the console..
      /// </summary>
      public void PrintCubeState()
      {
         //long minX = m_Cubes.Select( x => x.Value.X ).Min( );
         //long maxX = m_Cubes.Select( x => x.Value.X ).Max( );
         //long minY = m_Cubes.Select( x => x.Value.Y ).Min( );
         //long maxY = m_Cubes.Select( x => x.Value.Y ).Max( );
         //long minZ = m_Cubes.Select( x => x.Value.Z ).Min( );
         //long maxZ = m_Cubes.Select( x => x.Value.Z ).Max( );

         long minX = -1;
         long maxX = 5;
         long minY = -1;
         long maxY = 5;
         long minZ = -1;
         long maxZ = 1;

         List<string[]> planes = new List<string[]>( );

         for( long z = minZ; z <= maxZ; z++ )
         {
            string[] thisPlane = new string[maxY - minY + 1];
            int stringArrCount = 0;
            for( long x = minX; x <= maxX; x++ )
            {
               char[] thisLine = new char[maxY - minY + 1];
               int charArrCount = 0;
               for( long y = minY; y <= maxY; y++ )
               {
                  IntVector3D v = new IntVector3D( x, y, z );
                  if( m_Cubes.ContainsKey( v.ID ) )
                     thisLine[charArrCount++] = '#';
                  else
                     thisLine[charArrCount++] = '.';
               }
               thisPlane[stringArrCount++] = new string( thisLine );
            }
            planes.Add( thisPlane );
         }

         Console.WriteLine( "-------------------------------------------" );
         Console.WriteLine( "After " + m_NumberOfCycles + " cycle:" );
         Console.WriteLine( " " );
         foreach( string[] plane in planes )
         {
            Console.WriteLine( "z=" + minZ++ );
            foreach( string line in plane )
               Console.WriteLine( line );

            Console.WriteLine( " " );
         }


      }


   #endregion


   }
}
