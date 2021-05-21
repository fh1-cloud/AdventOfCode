using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{
   public class OrbitObject
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

      public OrbitObject( string name )
      {
         this.Name = name;
      }
   #endregion

   /*PROPERTIES*/
   #region


      public string Name { get; set; }
      public OrbitObject Parent { get; set; }

   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      public long CountOrbitsAbove( )
      {
         if( Parent == null )
            return 0;
         else
         {
            return Parent.CountOrbitsAbove( ) + 1;
         }
      }



   #endregion

   /*STATIC METHODS*/
   #region
   #endregion


   }
}
