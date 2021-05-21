using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeLib.Numerics;

namespace AdventOfCode2019.Classes
{
   
   /// <summary>
   /// Represents a single wire for day 3
   /// </summary>
   public class Wire
   {


   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region


      protected List<UVector2D> m_Points = new List<UVector2D>( );



   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Initializes a wire from the string input
      /// </summary>
      /// <param name="inp"></param>
      public Wire( string inp )
      {
      //Split the string by comma
         string[] split = inp.Split( new char[] { ',' } );

      //Add the start point for this wire
         UVector2D prevVec = new UVector2D( 0.0, 0.0 );
         m_Points.Add( prevVec );

      //Add the rest of the end points.
         for( int i = 0; i < split.Length; i++ )
         {
            UVector2D thisVec = GetPointFromStartingPointAndDisplacement( prevVec, split[i] );
            m_Points.Add( thisVec );
            prevVec = thisVec;
         }

      }


   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Returns the list of all the intersection between this wire and the input wire
      /// </summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public List<UVector2D> GetIntersectionPoints( Wire other, out List<double> signalDelays )
      {
      //Declare returning list
         List<UVector2D> intersectionPoints = new List<UVector2D>( );

      //Declare the list of delays..
         signalDelays = new List<double>( );

      //Declare list of accumulated wire lengths..
         List<UVector2D> wireAPointsSoFar = new List<UVector2D>( );
         List<UVector2D> wireBPointsSoFar = new List<UVector2D>( );

         wireAPointsSoFar.Add( m_Points[0] );

      //Loop over all the point in the main wire.
         for( int i = 0; i<m_Points.Count - 1; i++ )
         {
         //Declare points for this wire.
            UVector2D w1Start = m_Points[i];
            UVector2D w1End = m_Points[i+1];

         //Add the endpoint..
            wireAPointsSoFar.Add( w1End );

            wireBPointsSoFar = new List<UVector2D>( );
            wireBPointsSoFar.Add( other.m_Points[0] );

            for( int j = 0; j<other.m_Points.Count-1; j++ )
            {

            //Declare endpoints for this wire.
               UVector2D w2Start = other.m_Points[j];
               UVector2D w2End = other.m_Points[j+1];

               wireBPointsSoFar.Add( w2End );

            //CHeck for intersection points.
               bool doesIntersect = UVector2D.DoesLinesIntersect( w1Start, w1End, w2Start, w2End, false, out UVector2D intPoint );
               if( doesIntersect && intPoint != null )
               {
               //Check if it is already in the list..
                  bool doesExist = intersectionPoints.Where( x => x.X == intPoint.X ).ToList( ).Where( y => y.Y == intPoint.Y ).ToList( ).Count > 0;

               //Add if it doesnt.
                  if( !doesExist )
                  {
                  //Add the intersection point
                     intersectionPoints.Add( intPoint );

                  //Calculate the signal delay here.
                     signalDelays.Add( GetLengthUpToPoint( intPoint, wireAPointsSoFar, wireBPointsSoFar ) );

                  }
               }
            }
         }

      //Retrun completed list.
         return intersectionPoints;

      }

      



   #endregion

   /*STATIC METHODS*/
   #region


      /// <summary>
      /// Calculate the length of the wire up to this point
      /// </summary>
      /// <param name="point">The intersection point</param>
      /// <param name="wireAPoints">The points for wire A. Including the endpoint for where is passed the intersection</param>
      /// <param name="wireBPoints">See A.</param>
      /// <returns></returns>
      public static double GetLengthUpToPoint( UVector2D point, List<UVector2D> wireAPoints, List<UVector2D> wireBPoints )
      {
      //First, create a copy of the list to prevent errors..
         List<UVector2D> localAList = new List<UVector2D>( wireAPoints );
         List<UVector2D> localBList = new List<UVector2D>( wireBPoints );

      //Remove the last item in the list.
         localAList.RemoveAt( wireAPoints.Count - 1 );
         localBList.RemoveAt( wireBPoints.Count - 1 );

      //Add the new point as the endpoint for both wires.
         localAList.Add( point );
         localBList.Add( point );

      //Calculate the minimum resistance..
         double aLength = 0.0;
         double bLength = 0.0;
         for( int i = 0; i < localAList.Count - 1; i++ )
         {
            UVector2D wireSegmentA = new UVector2D( localAList[i], localAList[i + 1] );
            aLength += wireSegmentA.GetLength( );
         }
         for( int i = 0; i < localBList.Count - 1; i++ )
         {
            UVector2D wireSegmentB = new UVector2D( localBList[i], localBList[i + 1] );
            bLength += wireSegmentB.GetLength( );
         }
         return aLength + bLength;
      }



      /// <summary>
      /// Gets the endpoint from a starting point and a string that indentifies the displacement from that point
      /// </summary>
      /// <param name="start"></param>
      /// <param name="disp"></param>
      /// <returns></returns>
      public static UVector2D GetPointFromStartingPointAndDisplacement( UVector2D start, string disp )
      {
      //Find the displacement value
         double dispVal = double.Parse( disp.Substring( 1, disp.Length - 1 ) );

      //Find thedisplacement direction
         UVector2D dir = null;
         if( disp[0] == 'R' )
            dir = UVector2D.UnitX( );
         else if( disp[0] == 'L' )
            dir = -UVector2D.UnitX( );
         else if( disp[0] == 'U' )
            dir = UVector2D.UnitY( );
         else if( disp[0] == 'D' )
            dir = -UVector2D.UnitY( );

      //Calculate end point and return.
         UVector2D dispVec = dispVal * dir;
         UVector2D retVec = start + dispVec;
         return retVec;

      }


   #endregion


   }
}
