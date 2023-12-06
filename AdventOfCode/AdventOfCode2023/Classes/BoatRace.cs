using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class BoatRace
   {

   /*CONSTRUCTORS*/
   #region
      public BoatRace( long time, long distance )
      {
         this.Time = ( double ) time;
         this.Distance = ( double ) distance;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public double Time { get; protected set; }
      public double Distance { get; protected set; }
   #endregion

   /*METHODS*/
   #region
      public long GetNumberOfWaysToWin( )
      {
      //Calculate the limits of possible charge times
         double lowLim = this.Time * 0.5 - 0.5 * Math.Sqrt( Math.Pow( this.Time, 2.0 ) - 4.0 * this.Distance );
         double highLim = this.Time * 0.5 + 0.5 * Math.Sqrt( Math.Pow( this.Time, 2.0 ) - 4.0 * this.Distance );
         long upperLim = ( highLim % 1 == 0 ) ? ( long ) highLim - 1 : ( long ) highLim;
         return upperLim - ( long ) lowLim;
      }
   #endregion

   }
}
