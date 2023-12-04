using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class ScratchCard
   {

      /// <summary>
      /// Creates a single scratch card.
      /// </summary>
      /// <param name="line"></param>
      public ScratchCard( string line )
      {
      //Declare hash sets..
         this.WinningNumbers = new HashSet<int>( );
         this.DrawnNumbers = new HashSet<int>( );
         this.Instances = 1;

      //Parse 
         string[] cSpl = line.Split( new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries );
         string[] numSplit = cSpl[0].Split( new char[]{ ' ' } );
         this.Number = int.Parse( numSplit[numSplit.Length - 1] );
         string[ ] nSpl = cSpl[cSpl.Length - 1].Split( new char[ ] { '|' }, StringSplitOptions.RemoveEmptyEntries );
         string[ ] wSpl = nSpl[0].Split( new char[ ] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
         string[ ] dSpl = nSpl[nSpl.Length - 1].Split( new char[ ] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
         foreach( string s in wSpl )
            this.WinningNumbers.Add( int.Parse( s ) );
         foreach( string s in dSpl )
            this.DrawnNumbers.Add( int.Parse( s ) );

      //Calculate score for p1 and calculate matches for p2
         foreach( var i in this.DrawnNumbers )
         {
            if( this.WinningNumbers.Contains( i ) )
            {
               if( this.Score == 0 )
                  this.Score = 1;
               else if( this.Score == 1 )
                  this.Score *= 2;

               this.Matches += 1; //p2
            }
         }
      }

      public int Number { get; set; }
      public HashSet<int> WinningNumbers { get; set; }
      public HashSet<int> DrawnNumbers { get; set; }
      public int Score { get; set; }
      public long Matches { get; set; }
      public long Instances { get; set; }

      public void PlayGame( List<ScratchCard> allCards )
      {
         if( this.Matches == 0 )
            return;
         else 
            for( int j = 0; j < this.Instances; j++ )
               for( int i = this.Number; i< this.Number + this.Matches; i++ )
                  allCards[i].Instances = allCards[i].Instances + 1;
      }


   }
}
