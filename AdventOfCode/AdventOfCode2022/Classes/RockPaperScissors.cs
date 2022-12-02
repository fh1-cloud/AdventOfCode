using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class RockPaperScissors
   {

   /*ENUMS*/
   #region
   #endregion

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*MEMBERS*/
   #region
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// A game of rock paper scissors..
      /// </summary>
      /// <param name="c1"></param>
      /// <param name="c2"></param>
      public RockPaperScissors( char c1, char c2 )
      {
         if( c1 == 'A' ) //Opponent chooses rock
         {
            if( c2 == 'X' ) //Round should end in a loss, need to choose scissors
               this.Score = 3 + 0;
            else if( c2 == 'Y' ) //Round should end in a draw. Choose rock
               this.Score = 1 + 3;
            else if( c2 == 'Z' ) //Round should end in a win, choose paper
               this.Score = 2 + 6;
         }
         else if( c1 == 'B' ) //Opponent chooses paper
         {
            if( c2 == 'X' ) //Round should end in a loss. Choose rock
               this.Score = 1 + 0;
            else if( c2 == 'Y' ) //Draw. Choose paper
               this.Score = 2 + 3;
            else if( c2 == 'Z' ) //Win. Choose scissors
               this.Score = 3 + 6;
         }
         else if( c1 == 'C' ) //Opponent choose scissors
         {
            if( c2 == 'X' ) //Loss. Paper
               this.Score = 2 + 0;
            else if( c2 == 'Y' )
               this.Score = 3 + 3; //Draw. Scissors
            else if( c2 == 'Z' ) //Win. Rock
               this.Score = 1 + 6;
         }


         //if( c1 == 'A' )
         //{
         //   if( c2 == 'X' )
         //      this.Score = 1 + 3;
         //   else if( c2 == 'Y' )
         //      this.Score = 2 + 6;
         //   else if( c2 == 'Z' )
         //      this.Score = 3 + 0;
         //}
         //else if( c1 == 'B' )
         //{
         //   if( c2 == 'X' )
         //      this.Score = 1 + 0;
         //   else if( c2 == 'Y' )
         //      this.Score = 2 + 3;
         //   else if( c2 == 'Z' )
         //      this.Score = 3 + 6;
         //}
         //else if( c1 == 'C' )
         //{
         //   if( c2 == 'X' )
         //      this.Score = 1 + 6;
         //   else if( c2 == 'Y' )
         //      this.Score = 2 + 0;
         //   else if( c2 == 'Z' )
         //      this.Score = 3 + 3;
         //}
      }
   #endregion

   /*PROPERTIES*/
   #region
      public long Score { get; set; }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion

   /*METHODS*/
   #region
   #endregion



   }
}
