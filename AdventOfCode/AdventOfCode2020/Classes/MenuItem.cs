using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class MenuItem
   {

   /*MEMBERS*/
   #region

      protected List<Ingredient> m_Ingredients = new List<Ingredient>( );
      protected List<string> m_Allergens = new List<string>( );

   #endregion

   /*CONSTRUCTORS*/
   #region
      
      /// <summary>
      /// Creates a menu item from a list of ingredients and a list of allergens
      /// </summary>
      /// <param name="ingredients"></param>
      /// <param name="allergens"></param>
      public MenuItem( List<Ingredient> ingredients, List<string> allergens )
      {
         m_Ingredients = ingredients;
         m_Allergens = allergens;
      }

   #endregion


   /*PROPERTIES*/
   #region

      public List<Ingredient> Ingredients
      {
         get
         {
            return m_Ingredients;
         }
         set
         {
            m_Ingredients = value;
         }
      }
      public List<string> Allergens
      {
         get
         {
            return m_Allergens;
         }
         set
         {
            m_Allergens = value;
         }
      }

   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Returns a bool indicating wheter this menu item contains the ingredient or not
      /// </summary>
      /// <param name="ing"></param>
      /// <returns></returns>
      public bool ContainsIngredient( Ingredient ing )
      {
         if( ing == null )
            return false;
         Ingredient i = m_Ingredients.Where( x => x.Name == ing.Name ).ToList( ).FirstOrDefault( );
         return i != null;
      }

      /// <summary>
      /// Returns a bool inidicating wheter this menu item contains this allergen or not
      /// </summary>
      /// <param name="allergen"></param>
      /// <returns></returns>
      public bool ContainsAllergen( string allergen )
      {
         if( allergen == null )
            return false;
         return m_Allergens.Contains( allergen );
      }


      /// <summary>
      /// A method that returns a list of the common allergens between this object and the input object
      /// </summary>
      /// <param name="allergensInOther"></param>
      /// <returns></returns>
      public List<string> CommonAllergens( List<string> allergensInOther )
      {
      //Loop over both lists..
         List<string> common = new List<string>( );
         foreach( string a in m_Allergens )
            foreach( string b in allergensInOther )
               if( a == b && !common.Contains( a ) )
                  common.Add( a );

      //Return the common list
         return common;
      }




   #endregion

   /*STATIC METHODS*/
   #region

      public static string ArrangeByAllergen( List<KeyValuePair<string, Ingredient>> allergenIngredientPairs )
      {
      //SOrt and print
         List<KeyValuePair<string, Ingredient>> orderedList = allergenIngredientPairs.OrderBy( x => x.Key ).ToList();
         StringBuilder sb = new StringBuilder( );
         foreach( KeyValuePair<string, Ingredient> t in orderedList )
         {
            sb.Append( t.Value.Name );
            if( orderedList.IndexOf(t) != orderedList.Count - 1 )
               sb.Append( "," );
         }
         return sb.ToString( );
      }


   #endregion



   }
}
