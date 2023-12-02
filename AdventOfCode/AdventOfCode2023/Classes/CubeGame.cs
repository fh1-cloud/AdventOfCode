using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class CubeGame
   {


   /*ENUMS*/
   #region

      /// <summary>
      /// REpresents a single dice color
      /// </summary>
      public enum DICECOLOR
      {
         BLUE,
         RED,
         GREEN
      }
   #endregion

   /*LOCAL CLASSES*/
   #region

      /// <summary>
      /// Represents a single dice results..
      /// </summary>
      public class CubeGameDiceResult
      {
         /// <summary>
         /// Creates a single dice throw resut
         /// </summary>
         /// <param name="number"></param>
         /// <param name="color"></param>
         public CubeGameDiceResult( int number, DICECOLOR color )
         {
            this.Number = number;
            this.Color = color;
         }

         public int Number { get; set; } //The number for this dice..
         public DICECOLOR Color { get; set; } //The color for this dice..
      }

      /// <summary>
      /// Represents a single cubegame within a game series..
      /// </summary>
      public class CubeGameSingle
      {

      /*CONSTRUCTORS*/
         public CubeGameSingle( string gameRes )
         {
            this.Results = new List<CubeGameDiceResult>( );

         //Split by comma..
            string[] spl = gameRes.Split( new char[]{ ',' } );
            foreach( string s in spl )
            {
            //Split by space..
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

      /*PROPERTIES*/
         public List<CubeGameDiceResult> Results { get; set; }

      /*METHODS*/

         /// <summary>
         /// Checks if the single game is valid..
         /// </summary>
         /// <returns></returns>
         public bool IsValid( )
         {
            bool isValid = true;
            foreach( CubeGameDiceResult res in this.Results )
            {
               if( res.Color == DICECOLOR.RED )
               {
                  if( res.Number > 12 )
                  {
                     return false;
                  }
               }
               else if( res.Color == DICECOLOR.GREEN )
               {
                  if( res.Number > 13 )
                  {
                     return false;
                  }
               }
               else if( res.Color == DICECOLOR.BLUE )
               {
                  if( res.Number > 14 )
                  {
                     return false;
                  }
               }
            }
            return isValid;
         }

         /// <summary>
         /// A method that returns the largest number for a dice of a given color..
         /// </summary>
         /// <param name="col"></param>
         /// <returns></returns>
         public int MaxDiceNum( DICECOLOR col )
         {
            int max = 0;
            foreach( CubeGameDiceResult g in this.Results )
            {
               if( col != g.Color )
                  continue;
               else
                  max = Math.Max( max, g.Number );
            }
            return max;
         }


      }

   #endregion

   /*MEMBERS*/
   #region
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Creates a single cube game..
      /// </summary>
      /// <param name="inputLine"></param>
      public CubeGame( string inputLine )
      {
      //Create list of games..
         this.Games = new List<CubeGameSingle>( );
         this.MinColorNumber = new Dictionary<DICECOLOR, int>( );
         this.MinColorNumber.Add( DICECOLOR.RED, 0 );
         this.MinColorNumber.Add( DICECOLOR.GREEN, 0 );
         this.MinColorNumber.Add( DICECOLOR.BLUE, 0 );

      //Split line by colon to get game number..
         string[] spl1 = inputLine.Split( new char[] { ':' } );
         this.Number = int.Parse( spl1[0].Split( new char[] { ' ' } )[1] );

      //Split by semicolon and create single games..
         string[] gSpl = spl1[1].Split( new char[] { ';' } );
         foreach( string s in gSpl )
         {
         //Create game
            CubeGameSingle cgs = new CubeGameSingle( s );
            this.Games.Add( cgs );

         //Find the maximum color number
            int redMax = cgs.MaxDiceNum( DICECOLOR.RED );
            int greenMax = cgs.MaxDiceNum( DICECOLOR.GREEN );
            int blueMax = cgs.MaxDiceNum( DICECOLOR.BLUE );

         //Check the maximum color number against the already max number..
            this.MinColorNumber[DICECOLOR.RED] = Math.Max( redMax, this.MinColorNumber[DICECOLOR.RED] );
            this.MinColorNumber[DICECOLOR.GREEN] = Math.Max( greenMax, this.MinColorNumber[DICECOLOR.GREEN] );
            this.MinColorNumber[DICECOLOR.BLUE] = Math.Max( blueMax, this.MinColorNumber[DICECOLOR.BLUE] );
         }

      }


   #endregion

   /*PROPERTIES*/
   #region
      public int Number { get; set; }
      public List<CubeGameSingle> Games {get; set; }

      public Dictionary<DICECOLOR,int> MinColorNumber { get; set; }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// A method that returns if this game is valid or not
      /// </summary>
      /// <returns></returns>
      public bool IsValid( )
      {
      //Return false if a single game is false
         foreach( CubeGameSingle s in Games )
            if( !s.IsValid( ) )
               return false;

      //If the code reached this point it is valid.
         return true;
      }

      /// <summary>
      /// Returns the power for this dice game..
      /// </summary>
      /// <returns></returns>
      public long GetPower( )
      {
         long power = this.MinColorNumber[DICECOLOR.RED]*this.MinColorNumber[DICECOLOR.BLUE]*this.MinColorNumber[DICECOLOR.GREEN];
         return power;
      }
   #endregion


   }
}
