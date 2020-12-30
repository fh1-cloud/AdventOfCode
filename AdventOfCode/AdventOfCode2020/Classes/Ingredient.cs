using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   /// <summary>
   /// A class that contains information about a specific ingredient
   /// </summary>
   public class Ingredient
   {

   /*MEMBERS*/
   #region

      protected string m_Name = null;
      protected string m_Allergen = null;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Default constructor
      /// </summary>
      /// <param name="name"></param>
      public Ingredient( string name )
      {
         m_Name = name;
      }



   #endregion

   /*PROPERTIES*/
   #region
      public string Name => m_Name;

      public string Allergen
      {
         get
         {
            return m_Allergen;
         }
         set
         {
            m_Allergen = value;
         }
      }
   #endregion

   /*METHODS*/
   #region
   #endregion

   /*STATIC METHODS*/
   #region
   #endregion



   }
}
