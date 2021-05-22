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

      /// <summary>
      /// Counts the number of orbits above this object.
      /// </summary>
      /// <returns></returns>
      public long CountOrbitsAbove( )
      {
         if( Parent == null )
            return 0;
         else
            return Parent.CountOrbitsAbove( ) + 1;
      }

      /// <summary>
      /// Counts the number of orbits above this object up to the passed orbit object. IF the name was not encountered in the chain, it will throw an exception
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public long CountOrbitsAbove( OrbitObject o )
      {
         if( Parent == null )
         {
            if( o.Name != this.Name )
               throw new Exception( );
            else
               return 0;
         }
         else
         {
            if( this.Name == o.Name )
               return 1;
            else
               return Parent.CountOrbitsAbove( o ) + 1;
         }
      }

      /// <summary>
      /// Gets the complete chain of orbits above this object.
      /// </summary>
      /// <param name="above"></param>
      public void GetChainAbove( ref List<OrbitObject> above )
      {
         if( this.Parent == null )
         {
            above.Add( this );
            return;
         }
         else
         {
            above.Add( this );
            Parent.GetChainAbove( ref above );
         }
      }



   #endregion

   /*STATIC METHODS*/
   #region


   #endregion


   }
}
