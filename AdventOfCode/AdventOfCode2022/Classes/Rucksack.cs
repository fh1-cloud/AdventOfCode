using AdventOfCodeLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{

   public class Rucksack
   {
   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
      protected HashSet<char> m_Sack = new HashSet<char>( );
   #endregion

   /*CONSTRUCTORS*/
   #region
      public Rucksack( string inp )
      {
      //Split string in half..
         if( inp.Length%1 != 0 )
            throw new Exception( );

      //Loop over compartments and populate..
         for( int i = 0; i< inp.Length; i++ )
         {
            if( !m_Sack.Contains( inp[i] ) )
               m_Sack.Add( inp[i] );
         }


      }
   #endregion

   /*PROPERTIES*/
   #region
      public HashSet<char> Sack { get { return m_Sack; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   /*METHODS*/
   #region

      public long FindBadgeValue( Rucksack r1, Rucksack r2 )
      {
         HashSet<char> commonFirst = new HashSet<char>( );
         HashSet<char> commonSecond = new HashSet<char>( );

      //Check against this sack..
         foreach( char s in m_Sack )
         {
            if( r1.Sack.Contains( s ) )
               commonFirst.Add( s );
            if( r2.Sack.Contains( s ) )
               commonSecond.Add( s );
         }

      //Pair the last sack..
         List<char> commonItemsInThree = new List<char>( );
         foreach( char s in commonFirst )
         {
            if( commonSecond.Contains( s ) )
               commonItemsInThree.Add( s );
         }

      //Check fo occurence..
         if( commonItemsInThree.Count > 1 )
            throw new Exception( );


         char v = commonItemsInThree[0];
         long retVal = 0;

      //Both compartments contains the letter. Compute the score..
         if( v.IsUpper( ) )
            retVal += ( int ) v - 64 + 26;
         else
            retVal += ( int ) v - 96;

      //Return value..
         return retVal;


      }


   #endregion
   }
}
