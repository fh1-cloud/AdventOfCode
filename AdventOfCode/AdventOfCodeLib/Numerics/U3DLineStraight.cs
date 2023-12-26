using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Numerics
{
   public class U3DLineStraight
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected UVector3D m_Vertex1 = null;
      protected UVector3D m_Vertex2 = null;
   #endregion

   /*CONSTRUCTORS*/
   #region

      public U3DLineStraight( ) { }

      public U3DLineStraight( UVector3D startPoint, UVector3D endPoint )
      {
         m_Vertex1 = new UVector3D( startPoint );
         m_Vertex2 = new UVector3D( endPoint );
      }

      public U3DLineStraight( U3DLineStraight oldLine )
      {
         m_Vertex1 = new UVector3D( oldLine.m_Vertex1 );
         m_Vertex2 = new UVector3D( oldLine.m_Vertex2 );
      }

   #endregion

   /*PROPERTIES*/
   #region
      public UVector3D StartPoint => m_Vertex1;
      public UVector3D EndPoint => m_Vertex2;

   #endregion


   /*OPERATORS*/
   #region

      public static U3DLineStraight operator * ( double num, U3DLineStraight rhs )
      {
         return new U3DLineStraight( rhs.StartPoint * num, rhs.EndPoint * num );
      }

      public static U3DLineStraight operator * ( U3DLineStraight lhs, double num )
      {
         return num * lhs;
      }

   #endregion


   /*METHODS*/
   #region

      public double GetLength( )
      {
         return Math.Sqrt( Math.Pow( m_Vertex2.X - m_Vertex1.X, 2 ) + Math.Pow( m_Vertex2.Y - m_Vertex1.Y, 2 ) + Math.Pow( m_Vertex2.Z - m_Vertex1.Z, 2 ) );
      }

      public UVector3D IntersectWithLine( U3DLineStraight line )
      {
         List<UVector3D> intPts = IntersectStraightStraight( this, line );
         if( intPts == null || intPts.Count == 0 )
            return null;
         return intPts[0];
      }

   #endregion

   /*STATIC METHODS*/
   #region

      public UVector3D GetDirectionVector( )
      {
         UVector3D dirVec = m_Vertex2 - m_Vertex1;
         dirVec.Normalize( );
         return dirVec;
      }

      protected static List<UVector3D> IntersectStraightStraight( U3DLineStraight line1, U3DLineStraight line2 )
      {
      /* Local variables */
         UVector3D dirVec1, dirVec2, xyz1, xyz2, dvec;
         bool doBreak;
         int i, j, idx1 = 0, idx2 = 0, idx3 = 0;
         double s, t, x1, x2;
         List<UVector3D> list = null;

      /* Unpack */
         dirVec1 = line1.GetDirectionVector( );
         dirVec2 = line2.GetDirectionVector( );
         xyz1  = line1.m_Vertex1;
         xyz2  = line2.m_Vertex2;
         
      /* If the two lines are parallel, an intersection is not possible, so we'll just return */
         if( dirVec1.IsParallel( dirVec2 ) )
            return null;
      
      /* We need to check for intersection in a plane. Find the indices of the plane to check in */
         doBreak = false;
         for( i = 0 ; i < 3 ; i++ )
         {
            for( j = 0 ; j < 3 ; j++ )
            {
               if( i != j )
               {
                  if( !GS.IsZero( dirVec1[j] * dirVec2[i] - dirVec1[i] * dirVec2[j] ) )
                  {
                     idx1 = i;
                     idx2 = j;
                     doBreak = true;
                     break;
                  }
               }
            }
            if( doBreak ) break;
         }
      /* Get the parametric values for the intersection point */
         dvec = xyz2 - xyz1;
         s = ( dirVec1[idx1] * dvec[idx2] - dirVec1[idx2] * dvec[idx1] )
            / ( dirVec1[idx2] * dirVec2[idx1] - dirVec1[idx1] * dirVec2[idx2] );
         t = ( dirVec2[idx1] * dvec[idx2] - dirVec2[idx2] * dvec[idx1] )
            / ( dirVec1[idx2] * dirVec2[idx1] - dirVec1[idx1] * dirVec2[idx2] );
         
      /* Find out-of-plane coordinate for each line and compare */
         if( idx1 == 0 )
            idx3 = idx2==1?2:1;
         else if( idx1 == 1 )
            idx3 = idx2==0?2:0;
      
         x1 = xyz1[idx3] + dirVec1[idx3] * t;
         x2 = xyz2[idx3] + dirVec2[idx3] * s;
         
         if( GS.IsZero( x1 - x2, 1.0e-5 ) )
         {
            list = new List<UVector3D>( );
            list.Add( xyz1 + dirVec1 * t );
         }
         return list;
      }

   #endregion

   }
}
