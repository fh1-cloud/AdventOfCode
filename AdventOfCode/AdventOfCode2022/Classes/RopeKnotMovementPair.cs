using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   public class RopeKnotMovementPair
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
      public RopeKnotMovementPair( RopeKnot.MOVEDIRECTION moveDir, int nOfTimes )
      {
         this.Direction = moveDir;
         this.Repeats = nOfTimes;
      }
   #endregion

   /*PROPERTIES*/
   #region
      public RopeKnot.MOVEDIRECTION Direction { get; set; }
      public int Repeats { get; set; }
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
