using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{
   public class FuelCalculator
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
   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// Calculates the required mass for this input value by recursion
      /// </summary>
      /// <param name="mass"></param>
      /// <returns></returns>
      public static long GetFuel( long mass )
      {
      //Calculate the mass
         long thisMass = mass / 3 - 2;
         if( thisMass <= 0 )
            return 0;

      //Return recursive call.
         return thisMass + GetFuel( thisMass );
      }

   #endregion




   }
}
