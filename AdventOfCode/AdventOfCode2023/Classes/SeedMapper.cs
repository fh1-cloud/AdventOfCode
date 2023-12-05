using System;
using System.Collections.Generic;
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
         }

         public long DestinationRangeStart { get; set; }
         public long SourceRangeStart { get; set; }
         public long RangeLength { get; set; }
      }

      public class ClassMap
      {
         public ClassMap( List<string> arr )
         {
            this.Maps = new List<SingleMap>( );
            this.Name = arr[0].Split( new char[] { ' ' } )[0];
            for( int i = 1; i< arr.Count; i++ )
            {
               string[] sp = arr[i].Split( new char[] { ' ' } );
               SingleMap map = new SingleMap( arr[i] );
               this.Maps.Add( map );
            }
         }

         public string Name { get; set; }
         public List<SingleMap> Maps { get; set; }
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
      public SeedMapper( string[] allText )
      {
         this.InitialSeeds = new HashSet<long>( );
         this.ClassMaps = new List<ClassMap>( );

      //Create seeds..
         string[] seeds = allText[0].Split( new char[] { ' ' } );
         for( int i = 1; i<seeds.Length; i++ )
         {
            this.InitialSeeds.Add( long.Parse( seeds[i] ) );
         }
         
      //Split by blocks
         int? blockStart = null;
         List<string[]> blocks = new List<string[]>( );
         for( int i = 1; i<allText.Length; i++ )
         {
            if( allText[i] == "" && blockStart == null )
            {
               blockStart = i;
            }
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
            this.ClassMaps.Add( new ClassMap( block.ToList( ) ) );


      }
   #endregion

   /*PROPERTIES*/
   #region
      public HashSet<long> InitialSeeds { get; set; }
      public List<ClassMap> ClassMaps { get; set; }

   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region
      public long GetSeedLocationNumber( long seedNumber )
      {


      }

   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   }
}
