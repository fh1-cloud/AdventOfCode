using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Numerics
{
   public class URangeSingle1D
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected long m_MinVal = -1;
      protected long m_MaxVal = -1;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor
      /// </summary>
      /// <param name="val1"></param>
      /// <param name="val2"></param>
      public URangeSingle1D( long val1, long val2 )
      {
         m_MinVal = Math.Min( val1, val2 );
         m_MaxVal = Math.Max( val1, val2 );
      }

      /// <summary>
      /// Copy constructor
      /// </summary>
      /// <param name="oldRange"></param>
      public URangeSingle1D( URangeSingle1D oldRange )
      {
         m_MinVal = oldRange.m_MinVal;
         m_MaxVal = oldRange.m_MaxVal;
      }


   #endregion

   /*PROPERTIES*/
   #region
      public long MinVal { get { return m_MinVal; } } //Gets the minimum value
      public long MaxVal { get { return m_MaxVal; } } //Gets the maximum value
   #endregion

   /*OPERATORS*/
   #region

   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// A method that tries to consolidate two ranges. Returns a new range if sucessful, if they dont intersect, null is returned
      /// </summary>
      /// <param name="range1"></param>
      /// <param name="range2"></param>
      /// <returns></returns>
      public static URangeSingle1D Consolidate( URangeSingle1D range1, URangeSingle1D range2 )
      {
         URangeSingle1D intersect = range1.Intersect( range2 );
         if( intersect == null )
         {
         //Even though the intersection is zero, one range may end right before another one starts. Check this..
            if( !( (range1.MaxVal == range2.MinVal - 1 ) || (range2.MaxVal == range1.MinVal - 1 ) ) )
               return null;
         }

      //If the code reached this point, they intersect, and can be turned in to a single Range.. The new minValue is the minimum of the two range points..
         long p1 = Math.Min( range1.MinVal, range2.MinVal );
         long p2 = Math.Max( range1.MaxVal, range2.MaxVal );
         return new URangeSingle1D( p1, p2 );
      }

   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Performs an intersection operation for two 1D ranges. Returns a new range if the intersection was sucessful. If no intersection was found, it returns null
      /// </summary>
      /// <param name="range1"></param>
      /// <param name="range2"></param>
      /// <returns></returns>
      public URangeSingle1D Intersect( URangeSingle1D range2 )
      {
         if( this.MinVal > range2.MaxVal || this.MaxVal < range2.MinVal ) //The range does not intersect at all.. return empty list
            return null;
         else if( this.MinVal >= range2.MinVal && this.MaxVal <= range2.MaxVal ) //Range 2 encompass range 1
            return new URangeSingle1D( this );
         else if( range2.MinVal >= this.MinVal && range2.MaxVal <= this.MaxVal ) //Range 1 encompass range 2
            return new URangeSingle1D( range2 );
         else if( this.MinVal <= range2.MaxVal && this.MaxVal >= range2.MaxVal ) //Ranges intersect.. 1
            return new URangeSingle1D( this.MinVal, range2.MaxVal );
         else if( this.MaxVal >=  range2.MinVal && range2.MaxVal >= this.MaxVal ) //Ranges intersec... 2
            return new URangeSingle1D( range2.MinVal, this.MaxVal );

      //If the code reached this point, something weird happened and nothing was calculated. Throw
         throw new Exception( );
      }

      /// <summary>
      /// SUbtracts a single range from another single range. Returns either 0, 1 or two ranges
      /// </summary>
      /// <param name="range2">The range that is subtracted from this range</param>
      /// <param name="didSomething">A flag that is set to true if something was subtracted</param>
      /// <param name="zeroLeft">A flag that is set to true if the resulting range is empty, but not null</param>
      /// <returns></returns>
      /// <exception cref="Exception"></exception>
      public ( URangeSingle1D, URangeSingle1D ) Subtract( URangeSingle1D range2, out bool didSomething, out bool zeroLeft )
      {
      //Find the intersection first..
         zeroLeft = false;
         didSomething = false;
         URangeSingle1D intersect = this.Intersect( range2 );
         if( intersect == null )
            return ( new URangeSingle1D( this ), null );
         else //The intersection is the part that should be removed from this range.
         {
            didSomething = true;
            //Check if they are exactly alike. Return nothing.
            if( this.MinVal == range2.MinVal && this.MaxVal == range2.MaxVal )
            {
               zeroLeft = true;
               return ( null, null );
            }
            //Check if range 2 is internal, then we must return two ranges..
            else if( this.MinVal< range2.MinVal && this.MaxVal > range2.MaxVal )
            {
               URangeSingle1D p1 = new URangeSingle1D( this.MinVal, range2.MinVal-1);
               URangeSingle1D p2 = new URangeSingle1D( range2.MaxVal+1, this.MaxVal );
               return ( p1, p2 );
            }
            //It is not internal, then we need to adjust one of the limits..
            else if( this.MinVal < range2.MinVal ) //THe minimum value is less, the upper limit must be adjusted..
            {
               URangeSingle1D p1 = new URangeSingle1D( this.MinVal, intersect.MinVal-1 );
               return ( p1, null );
            }
            else if( this.MaxVal > range2.MaxVal ) //THe maximum value is larger, the lower limit must be adjusted.
            {
               URangeSingle1D p1 = new URangeSingle1D( intersect.MaxVal+1, this.MaxVal );
               return ( p1, null );
            }
         }

      //The code should not reach this point. Throw
         throw new Exception( );
      }

      /// <summary>
      /// Writes the resulting range to a string
      /// </summary>
      /// <returns></returns>
      public override string ToString( )
      {
         return "[" + m_MinVal.ToString( ) + "," + m_MaxVal.ToString( ) + "]";
      }
      #endregion


   }
}
