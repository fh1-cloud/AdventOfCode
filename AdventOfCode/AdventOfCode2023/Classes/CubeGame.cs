using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class CubeGame
   {
      //Colors
      public enum DICECOLOR { BLUE, RED, GREEN }

      //Dice result
      public class CubeGameDiceResult
      {
         public CubeGameDiceResult( int number, DICECOLOR color )
         {
            this.Number = number;
            this.Color = color;
         }
         public int Number { get; set; } 
         public DICECOLOR Color { get; set; } 
      }

      //Single cube throw
      public class CubeGameSingle
      {
         public CubeGameSingle( string gameRes )
         {
            this.Results = new List<CubeGameDiceResult>( );
            string[] spl = gameRes.Split( new char[]{ ',' } );
            foreach( string s in spl )
            {
               string[] tS = s.Split( new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries );
               int num = int.Parse( tS[0] );

               DICECOLOR? col = null;
               if( tS[1].Equals( "blue" ) )
                  col = DICECOLOR.BLUE;
               else if( tS[1].Equals( "red" ) )
                  col = DICECOLOR.RED;
               else if( tS[1].Equals( "green" ) )
                  col = DICECOLOR.GREEN;

               this.Results.Add( new CubeGameDiceResult( num, (DICECOLOR) col ) );
            }
         }

         public List<CubeGameDiceResult> Results { get; set; }
         public bool IsValid( )
         {
            foreach( CubeGameDiceResult res in this.Results )
            {
               if( res.Color == DICECOLOR.RED && res.Number > 12 )
                  return false;
               else if( res.Color == DICECOLOR.GREEN && res.Number > 13 )
                  return false;
               else if( res.Color == DICECOLOR.BLUE && res.Number > 14 )
                  return false;
            }
            return true;
         }

         public int MaxDiceNum( DICECOLOR col )
         {
            int max = 0;
            foreach( CubeGameDiceResult g in this.Results )
               if( col != g.Color )
                  continue;
               else
                  max = Math.Max( max, g.Number );

            return max;
         }
      }

      public CubeGame( string inputLine )
      {
         this.Games = new List<CubeGameSingle>( );
         this.MinColorNumber = new Dictionary<DICECOLOR, int>( );
         this.MinColorNumber.Add( DICECOLOR.RED, 0 );
         this.MinColorNumber.Add( DICECOLOR.GREEN, 0 );
         this.MinColorNumber.Add( DICECOLOR.BLUE, 0 );

         string[] spl1 = inputLine.Split( new char[] { ':' } );
         this.Number = int.Parse( spl1[0].Split( new char[] { ' ' } )[1] );

         string[] gSpl = spl1[1].Split( new char[] { ';' } );
         foreach( string s in gSpl )
         {
            CubeGameSingle cgs = new CubeGameSingle( s );
            this.Games.Add( cgs );

            int redMax = cgs.MaxDiceNum( DICECOLOR.RED );
            int greenMax = cgs.MaxDiceNum( DICECOLOR.GREEN );
            int blueMax = cgs.MaxDiceNum( DICECOLOR.BLUE );

            this.MinColorNumber[DICECOLOR.RED] = Math.Max( redMax, this.MinColorNumber[DICECOLOR.RED] );
            this.MinColorNumber[DICECOLOR.GREEN] = Math.Max( greenMax, this.MinColorNumber[DICECOLOR.GREEN] );
            this.MinColorNumber[DICECOLOR.BLUE] = Math.Max( blueMax, this.MinColorNumber[DICECOLOR.BLUE] );
         }
      }

      public int Number { get; set; }
      public List<CubeGameSingle> Games {get; set; }
      public Dictionary<DICECOLOR,int> MinColorNumber { get; set; }

      public bool IsValid( )
      {
         foreach( CubeGameSingle s in Games )
            if( !s.IsValid( ) )
               return false;

         return true;
      }

      public long GetPower( )
      {
         long power = this.MinColorNumber[DICECOLOR.RED]*this.MinColorNumber[DICECOLOR.BLUE]*this.MinColorNumber[DICECOLOR.GREEN];
         return power;
      }


   }
}
