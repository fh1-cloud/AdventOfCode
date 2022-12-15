using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Numerics
{
   public class U2DLineStraight
   {
   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected UVector2D m_StartPoint = null;
      protected UVector2D m_EndPoint = null;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public U2DLineStraight( UVector2D startPoint, UVector2D endPoint )
      {
         m_StartPoint = startPoint;
         m_EndPoint = endPoint;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public UVector2D Start { get { return m_StartPoint; } }
      public UVector2D End { get { return m_EndPoint; } }

   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region
      public bool DoesIntersectWith( U2DLineStraight otherLine )
      {
      //A method that checks if the input vector intersects with this line..
         UVector2D p1 = m_StartPoint;
         UVector2D q1 = m_EndPoint;
         UVector2D p2 = otherLine.m_StartPoint;
         UVector2D q2 = otherLine.m_EndPoint;


         // Find the four orientations needed for general and
         // special cases
         int o1 = Orientation( p1, q1, p2 );
         int o2 = Orientation( p1, q1, q2 );
         int o3 = Orientation( p2, q2, p1 );
         int o4 = Orientation( p2, q2, q1 );

         // General case
         if( o1 != o2 && o3 != o4 )
            return true;

         // Special Cases
         // p1, q1 and p2 are collinear and p2 lies on segment p1q1
         if( o1 == 0 && OnSegment( p1, p2, q1 ) ) 
            return true;

         // p1, q1 and q2 are collinear and q2 lies on segment p1q1
         if( o2 == 0 && OnSegment( p1, q2, q1 ) ) 
            return true;

         // p2, q2 and p1 are collinear and p1 lies on segment p2q2
         if( o3 == 0 && OnSegment( p2, p1, q2 ) ) 
            return true;

         // p2, q2 and q1 are collinear and q1 lies on segment p2q2
         if( o4 == 0 && OnSegment( p2, q1, q2 ) ) 
            return true;

         return false; // Doesn't fall in any of the above cases

      }
      #endregion

      /*STATIC METHODS*/
      #region

      // Given three collinear points p, q, r, the function checks if
      // point q lies on line segment 'pr'
      private static bool OnSegment( UVector2D p, UVector2D q, UVector2D r )
      {
         if( q.X <= Math.Max( p.X, r.X ) && q.X >= Math.Min( p.X, r.X ) && q.Y <= Math.Max( p.Y, r.Y ) && q.Y >= Math.Min( p.Y, r.Y ) )
            return true;

         return false;
      }


      // To find orientation of ordered triplet (p, q, r).
      // The function returns following values
      // 0 --> p, q and r are collinear
      // 1 --> Clockwise
      // 2 --> Counterclockwise
      static int Orientation( UVector2D p, UVector2D q, UVector2D r )
      {
         // See https://www.geeksforgeeks.org/orientation-3-ordered-points/
         // for details of below formula.
         double val = ( q.Y - p.Y ) * ( r.X - q.X ) - ( q.X - p.X ) * ( r.Y - q.Y );

         if( val == 0 ) 
            return 0; // collinear

         return ( val > 0 ) ? 1 : 2; // clock or counterclock wise
      }


      #endregion

   }
}
