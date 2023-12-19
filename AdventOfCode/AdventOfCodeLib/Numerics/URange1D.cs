using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib.Numerics
{
   public class URange1D
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected List<URangeSingle1D> m_Ranges = null;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Creates a 1D range that can consist of many sub ranges
      /// </summary>
      /// <param name="ranges"></param>
      public URange1D( List<URangeSingle1D> ranges )
      {
         m_Ranges = new List<URangeSingle1D>( );
         if( ranges != null && ranges.Count > 0 )
            m_Ranges.AddRange( ranges );

      //Consolidate the ranges..
         Consolidate( );
      }
   #endregion

   /*PROPERTIES*/
   #region
      public int Count { get { return m_Ranges.Count; } }
      public List<URangeSingle1D> Ranges { get { return m_Ranges; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region


      public static URange1D CreateSingleRange( long minVal, long maxVal )
      {
         return new URange1D( new List<URangeSingle1D>( ) { new URangeSingle1D( minVal, maxVal ) } );
      }

   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Creates a deep copy of this range.
      /// </summary>
      /// <returns></returns>
      public URange1D DeepCopy( )
      {
         List<URangeSingle1D> copiedRanges = new List<URangeSingle1D>( );
         foreach( URangeSingle1D range in m_Ranges )
            copiedRanges.Add( new URangeSingle1D( range ) );

         return new URange1D( copiedRanges );
      }

      /// <summary>
      /// Adds a new range to the existing range.
      /// </summary>
      /// <param name="range"></param>
      public void Add( URangeSingle1D range )
      {
         m_Ranges.Add( new URangeSingle1D( range ) );
         Consolidate( );
      }

      /// <summary>
      /// Add a series of ranges to this series of ranges
      /// </summary>
      /// <param name="range"></param>
      public void Add( URange1D range )
      {
         foreach( URangeSingle1D r in range.Ranges )
            m_Ranges.Add( new URangeSingle1D( r ) );

         Consolidate( );
      }

      /// <summary>
      /// Intersects a single range on the top of this collection of ranges.
      /// </summary>
      /// <param name="range"></param>
      /// <returns></returns>
      public void Intersect( URangeSingle1D range )
      {
      //LOop over all ranges in this collection and intersect with range..
         List<URangeSingle1D> intersections = new List<URangeSingle1D>( );
         for( int i = 0; i<m_Ranges.Count; i++ )
         {
            URangeSingle1D inters = m_Ranges[i].Intersect( range );  
            if( inters != null )
               intersections.Add( inters );
         }
         m_Ranges = intersections;
         Consolidate( );
      }

      /// <summary>
      /// Subtracts the URange1D from this range.
      /// </summary>
      /// <param name="rangeCollection"></param>
      public void Subtract( URange1D rangeCollection )
      {
         bool didSomething = false;
         do
         {
            didSomething = false;
         //Need to collect trash and additional so we dont modify the collection when we iterate over it
            List<URangeSingle1D> trash = null;
            List<URangeSingle1D> additional = null;
            foreach( URangeSingle1D range in rangeCollection.Ranges )
            {
               this.Subtract( range, out trash, out additional );
               if( trash.Count > 0 || additional.Count > 0 )
               {
                  didSomething = true;
                  break;
               }
            }
            if( didSomething )
            {
               foreach( URangeSingle1D t in trash )
                  m_Ranges.Remove( t );
               foreach( URangeSingle1D a in additional )
                  m_Ranges.Add( a );
            }
         } while( didSomething );
      }
      
      /// <summary>
      /// Subtracts a range from the collection of ranges. Consolidation should not be needed after since it should be consolidated..
      /// </summary>
      /// <param name="range"></param>
      protected void Subtract( URangeSingle1D range, out List<URangeSingle1D> trash, out List<URangeSingle1D> additional )
      {
         bool didSomething = false;
         trash = new List<URangeSingle1D>( );
         additional = new List<URangeSingle1D>( );
         HashSet<URangeSingle1D> skipped = new HashSet<URangeSingle1D>( );
         do
         {
            didSomething = false;
            bool zeroLeft = false;
            List<URangeSingle1D> newRangesAfterSubtraction = new List<URangeSingle1D>( );
            URangeSingle1D rangeBeforeSubtraction = null;
            for( int i = 0; i < m_Ranges.Count; i++ )
            {
            //Skip if already collected..
               if( skipped.Contains( m_Ranges[i] ) )
                  continue;

               ( URangeSingle1D r1, URangeSingle1D r2 ) = m_Ranges[i].Subtract( range, out bool subtractedThis, out zeroLeft );
               if( subtractedThis )
               {
                  rangeBeforeSubtraction = m_Ranges[i];
                  if( r1 != null )
                     newRangesAfterSubtraction.Add( r1 );
                  if( r2 != null )
                     newRangesAfterSubtraction.Add( r2 );

               //Break for if something was done. start over.
                  break;
               }
            }

         //Remove the subtracted range
            if( zeroLeft || newRangesAfterSubtraction.Count > 0 )
            {
               trash.Add( rangeBeforeSubtraction );
               skipped.Add( rangeBeforeSubtraction );
               additional.AddRange( newRangesAfterSubtraction );
               foreach( URangeSingle1D r in additional )
                  skipped.Add( r );
               didSomething = true;
            }

         } while( didSomething );
      }


      /// <summary>
      /// A method that consolidates all the ranges that are in the list by creating new ranges from the internal points.. Usually called when a new range is added in to the list..
      /// </summary>
      public void Consolidate( )
      {
         bool didSomething = false;
         do
         {
            didSomething = false;
            URangeSingle1D consolidatedRange = null;
            URangeSingle1D consolidated1 = null;
            URangeSingle1D consolidated2 = null;
            for( int i = 0; i < m_Ranges.Count; i++ )
            {
               for( int j = 0; j < m_Ranges.Count; j++ )
               {
                  if( i == j )
                     continue;
                  consolidatedRange = URangeSingle1D.Consolidate( m_Ranges[i], m_Ranges[j] );
                  if( consolidatedRange != null )
                  {
                     consolidated1 = m_Ranges[i];
                     consolidated2 = m_Ranges[j];
                     break;
                  }
               }
               if( consolidatedRange != null )
                  break;
            }

         //Remove the two that was added together..
            if( consolidatedRange != null )
            {
               m_Ranges.Remove( consolidated1 );
               m_Ranges.Remove( consolidated2 );
               m_Ranges.Add( consolidatedRange );
               didSomething = true;
            }

         } while( didSomething );

      }

      /// <summary>
      /// Returns the highest value of all the ranges in this range
      /// </summary>
      /// <returns></returns>
      public long GetMinValue( )
      {
         long minValue = long.MaxValue;
         foreach( URangeSingle1D range in m_Ranges )
            minValue = Math.Min( minValue, range.MinVal );
         return minValue;
      }

      /// <summary>
      /// Returns the lowest value of all the ranges in this range
      /// </summary>
      /// <returns></returns>
      public long GetMaxValue( )
      {
         long maxValue = long.MinValue;
         foreach( URangeSingle1D range in m_Ranges )
            maxValue = Math.Max( maxValue, range.MaxVal );
         return maxValue;

      }

      /// <summary>
      /// Prints the range as a series of ranges..
      /// </summary>
      /// <returns></returns>
      public override string ToString( )
      {
         StringBuilder sb = new StringBuilder( );
         foreach( URangeSingle1D range in m_Ranges )
            sb.Append( range.ToString( ) + " " );
         return sb.ToString( );
      }

      #endregion

   }
}
