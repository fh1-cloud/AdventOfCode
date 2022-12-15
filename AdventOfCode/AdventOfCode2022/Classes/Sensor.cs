using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   class Sensor
   {

   /*ENUMS*/
   #region

      /// <summary>
      /// An enum that represents the vertice placement..
      /// </summary>
      public enum VERTICEPLACEMENT
      {
         TOP,
         LEFT,
         RIGHT,
         BOTTOM
      }
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected Dictionary<VERTICEPLACEMENT, UVector2D> m_Vertices = new Dictionary<VERTICEPLACEMENT, UVector2D>( );//Vertices with coordinates..
      protected List<U2DLineStraight> m_Edges = new List<U2DLineStraight>( );
      protected int m_Radius = -1;
      protected UVector2D m_SensorPlacement = null;
      protected UVector2D m_BeaconPlacement = null;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Creates a Sensor with beacon from the input string..
      /// </summary>
      /// <param name="inpString"></param>
      public Sensor( string inpString )
      {
      //Split by space..
         string[] firstSplit = inpString.Split( new char[ ] { ' ' }, StringSplitOptions.RemoveEmptyEntries );

      //Parse the sensor placement..
         string sensorXString = firstSplit[2].Substring( 2, firstSplit[2].Length - 3 );
         string sensorYString = firstSplit[3].Substring( 2, firstSplit[3].Length - 3 );
         int sX = int.Parse( sensorXString );
         int sY = int.Parse( sensorYString );
         m_SensorPlacement = new UVector2D( sX, sY );
         
         string beaconXString = firstSplit[8].Substring( 2, firstSplit[8].Length - 3 );
         string beaconYString = firstSplit[9].Substring( 2, firstSplit[9].Length - 2 );
         int bX = int.Parse( beaconXString );
         int bY = int.Parse( beaconYString );
         m_BeaconPlacement = new UVector2D( bX, bY );

      //Calculate the vertices for this pair..
         UVector2D distVector = m_BeaconPlacement - m_SensorPlacement;
         m_Radius = ( int ) distVector.GetManhattanLength( );

      //Create the vertices for this sensor..
         UVector2D topVertex = m_SensorPlacement + new UVector2D( 0.0, m_Radius );
         UVector2D botVertex = m_SensorPlacement + new UVector2D( 0.0, -m_Radius );
         UVector2D leftVertex = m_SensorPlacement + new UVector2D( -m_Radius, 0.0 );
         UVector2D rightVertex = m_SensorPlacement + new UVector2D( m_Radius, 0.0 );
         m_Vertices.Add( VERTICEPLACEMENT.TOP, topVertex );
         m_Vertices.Add( VERTICEPLACEMENT.BOTTOM, botVertex );
         m_Vertices.Add( VERTICEPLACEMENT.LEFT, leftVertex );
         m_Vertices.Add( VERTICEPLACEMENT.RIGHT, rightVertex );

      //Create the 4 lines for this shape..
         U2DLineStraight l1 = new U2DLineStraight( topVertex, leftVertex );
         U2DLineStraight l2 = new U2DLineStraight( leftVertex, botVertex );
         U2DLineStraight l3 = new U2DLineStraight( botVertex, rightVertex );
         U2DLineStraight l4 = new U2DLineStraight( rightVertex, topVertex );
         m_Edges.AddRange( new List<U2DLineStraight>( ){ l1, l2, l3, l4 } );
      }
   #endregion

   /*PROPERTIES*/
   #region
      public UVector2D GetBeaconCoordinates { get { return m_BeaconPlacement; } }
      public int MinX { get { return (int) m_Vertices[VERTICEPLACEMENT.LEFT].X; } }
      public int MaxX { get { return (int) m_Vertices[VERTICEPLACEMENT.RIGHT].X; } }
      public int MaxY { get { return (int) m_Vertices[VERTICEPLACEMENT.BOTTOM].Y; } }
      public int MinY { get { return (int) m_Vertices[VERTICEPLACEMENT.TOP].Y; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Checks wheter or not the line intersects with the coverage area of the sensor..
      /// </summary>
      /// <param name="line"></param>
      /// <returns></returns>
      public void IntersectCoverageWithHeight( int ycor, int minX, int maxX, bool[] isOccupied )
      {
         if( Math.Abs( m_SensorPlacement.Y - ycor ) > m_Radius )
            return;

         int offset = Math.Abs( m_Radius - ycor );
         int intersectStartX = ( int ) m_SensorPlacement.X - offset;
         int intersectEndX = ( int ) m_SensorPlacement.X + offset;

         for( int i = 0; i<
         isOccupied[minX-intersectStartX] = 
         


         intersectionPoints.Add( new KeyValuePair<int, int>( intersectStartX, intersectEndX ) );
      }

   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   }
}
