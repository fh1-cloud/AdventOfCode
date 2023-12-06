using AdventOfCodeLib.Numerics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class SeedMapper
   {

   /*LOCAL CLASSES*/
   #region


      public class SingleMap
      {
         public SingleMap( string line )
         {
            string[] numSplit = line.Split( new char[] { ' ' } );
            this.DestinationRangeStart = long.Parse( numSplit[0] );
            this.SourceRangeStart = long.Parse( numSplit[1] );
            this.RangeLength = long.Parse( numSplit[2] );
            this.DestinationRange = new URangeSingle1D( this.DestinationRangeStart, this.DestinationRangeStart + this.RangeLength -1 );
            this.SourceRange = new URangeSingle1D( this.SourceRangeStart, this.SourceRangeStart + this.RangeLength - 1 );
         }

         public long DestinationRangeStart { get; set; }
         public long SourceRangeStart { get; set; }
         public long RangeLength { get; set; }
         public URangeSingle1D DestinationRange { get; protected set; }
         public URangeSingle1D SourceRange { get; protected set; }

         public bool AdjustForSingleMap( long inp, out long adjusted )
         {
            if( inp >= this.SourceRangeStart && ( inp <= this.SourceRangeStart - 1 + this.RangeLength ) )
            {
               adjusted = inp - this.SourceRangeStart + this.DestinationRangeStart;
               return true;
            }
            adjusted = inp;
            return false;
         }

         public URangeSingle1D AdjustRangeForMap( URangeSingle1D range )
         {
            this.AdjustForSingleMap( range.MinVal, out long val1 );
            this.AdjustForSingleMap( range.MaxVal, out long val2 );
            return new URangeSingle1D( val1, val2 );
         }

         public URange1D AdjustRangeForMap( URange1D range )
         {
            List<URangeSingle1D> newRanges = new List<URangeSingle1D>( );
            foreach( URangeSingle1D r in range.Ranges )
               newRanges.Add( AdjustRangeForMap( r ) );

            URange1D ret = new URange1D( newRanges );
            return ret;
         }

      }

      public class ClassMap
      {
         public ClassMap( List<string> arr, int id )
         {
            this.Maps = new List<SingleMap>( );
            this.Name = arr[0].Split( new char[] { ' ' } )[0];
            for( int i = 1; i< arr.Count; i++ )
            {
               string[] sp = arr[i].Split( new char[] { ' ' } );
               SingleMap map = new SingleMap( arr[i] );
               this.Maps.Add( map );
            }
            this.ID = id;
            this.ChildMap = null;
         }

         public int ID { get; protected set; }
         public string Name { get; set; }
         public List<SingleMap> Maps { get; set; }
         public ClassMap ChildMap { get; set; }

         public long AdjustForMap( long inpVal )
         {
            foreach( SingleMap map in this.Maps )
               if( map.AdjustForSingleMap( inpVal, out long adjVal ) )
                  return adjVal;

            return inpVal;
         }

         public URange1D AdjustRangesForThisClassMap( URange1D inputRange, URange1D endRanges )
         {
            List<URange1D> allAdjustedRanges = new List<URange1D>( );

            for( int i = 0; i<this.Maps.Count; i++ )
            {
            //Create a copy of the input range, and try to intersect..
               URange1D newRange = inputRange.DeepCopy( );
               newRange.Intersect( this.Maps[i].SourceRange );

            //If we found an intersection, we have to remove the intersection points from the input, and adjust the intersection points
               if( newRange.Count > 0 )
               {
                  inputRange.Subtract( newRange );
                  URange1D adjRange = this.Maps[i].AdjustRangeForMap( newRange );
                  allAdjustedRanges.Add( adjRange );
               }
            }
         //Create a range of all the adjusted ones..
            URange1D allAdjustedInOne = new URange1D( new List<URangeSingle1D>( ) );
            foreach( URange1D r in allAdjustedRanges )
               allAdjustedInOne.Add( r );

         //Add the adjusted ones back to the non adjusted ones..
            inputRange.Add( allAdjustedInOne );

         //If this rule has no child rule, we add the input range in to the end range object..
            if( this.ChildMap == null )
            {
               endRanges.Add( inputRange );
               return inputRange;
            }
            else //There are more children, adjust call this method by recursion to adjust at the next level.
               return this.ChildMap.AdjustRangesForThisClassMap( inputRange, endRanges );
         }
      }

   #endregion

   /*ENUMS*/
   #region
   #endregion

   /*MEMBERS*/
   #region
   #endregion

   /*CONSTRUCTORS*/
   #region
      public SeedMapper( string[] allText, out List<long> seedList )
      {
         this.ClassMaps = new List<ClassMap>( );

      //Create seeds..
         string[] seeds = allText[0].Split( new char[] { ' ' } );
         seedList = new List<long>( );
         for( int i = 1; i<seeds.Length; i++ )
            seedList.Add( long.Parse( seeds[i] ) );
         
      //Split by blocks
         int? blockStart = null;
         List<string[]> blocks = new List<string[]>( );
         for( int i = 1; i<allText.Length; i++ )
         {
            if( allText[i] == "" && blockStart == null )
               blockStart = i;
            else if( ( allText[i] == "" && blockStart != null ) || i == allText.Length - 1 )
            {
               List<string> currentBlock = new List<string>( );
               int endLim = i;
               if( i == allText.Length - 1 )
                  endLim++;
               for( int j = (int) blockStart+1; j<endLim; j++ )
                  currentBlock.Add( allText[j] );
               blockStart = i;
               blocks.Add( currentBlock.ToArray( ) );
            }
         }

      //Create all the class maps..
         foreach( string[] block in blocks )
            this.ClassMaps.Add( new ClassMap( block.ToList( ), blocks.IndexOf( block ) ) );

      //Set all the child objects..
         for( int i = 0; i< this.ClassMaps.Count-1; i++ )
            this.ClassMaps[i].ChildMap = this.ClassMaps[i+1];
      }
   #endregion

   /*PROPERTIES*/
   #region
      public List<ClassMap> ClassMaps { get; set; }

   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      public long GetSeedLocationNumberP1( long seedNumber )
      {
         foreach( ClassMap map in ClassMaps )
            seedNumber = map.AdjustForMap( seedNumber );

         return seedNumber;
      }

      public URange1D GetRangeFromSeedRange( URange1D thisSeedRange )
      {
         URange1D endRanges = new URange1D( new List<URangeSingle1D>( ) );

      //Call by recursion
         this.ClassMaps[0].AdjustRangesForThisClassMap( thisSeedRange, endRanges );
         return endRanges;

      }

   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   }
}
