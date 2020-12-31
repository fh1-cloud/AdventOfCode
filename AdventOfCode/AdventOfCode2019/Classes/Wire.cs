using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2019.Numerics;

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
      public List<UVector2D> GetIntersectionPoints( Wire other )
      {
         List<UVector2D> intersectionPoints = new List<UVector2D>( );






      }



   #endregion

   /*STATIC METHODS*/
   #region

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
