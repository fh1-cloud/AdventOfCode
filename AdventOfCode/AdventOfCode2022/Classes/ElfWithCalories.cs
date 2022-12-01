using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Classes
{
   internal class ElfWithCalories
   {

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*ENUMS*/
   #region
   #endregion

   /*MEMBERS*/
   #region
   #endregion

   /*CONSTRUCTORS*/
   #region
      public ElfWithCalories( )
      {
         this.Food = new List<long>( );
      }
   #endregion

   /*PROPERTIES*/
   #region
      public List<long> Food { get; set; }
      public long TotalCalories
      {
         get
         {
            return this.Food.Sum( );
         }
      }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion


   }
}
