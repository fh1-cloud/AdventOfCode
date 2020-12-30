using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class Bag
   {

   /*MEMBERS*/
      #region
      protected string m_Color = null;
      protected Dictionary<string, int> m_OtherContainedBags = new Dictionary<string, int>( ); //The list of the bags contained in this bag. The key is the name of the bags below, and the int is the count of that type of children bag.
      #endregion

   /*CONSTRUCTORS*/
      #region

      /// <summary>
      /// Default constructor for a bag
      /// </summary>
      /// <param name="inputBagRule"></param>
      public Bag( string inputBagRule )
      {

      //Split the string and try to parse the contents.

         //Split by space, then the first two values will be the color of this bag
         string[] colorString = inputBagRule.Split( new char[] { ' ' } );
         string thisColor = colorString[0] + " " + colorString[1];
         m_Color = thisColor;

      //try to look for an integer in the string. This will signal the start of a new children bag.
         for( int i = 0; i<colorString.Length; i++ )
         {
         //Declare this string..
            string s = colorString[i];

         //Try to parse to an int..
            int nOfChildren;
            bool parsedThis = int.TryParse( s, out nOfChildren );
            if( parsedThis )
            {
               int startIdx = i;
               string thisChildColor = colorString[i + 1] + " " + colorString[i + 2];

               m_OtherContainedBags.Add( thisChildColor, nOfChildren );
            }
         }

      }

      #endregion

   /*PROPERTIES*/
      #region

      /// <summary>
      /// Gets the color of this bag
      /// </summary>
      public string Color
      {
         get
         {
            return m_Color;
         }
      }

      /// <summary>
      /// Get the children bags of this bag
      /// </summary>
      public Dictionary<string, int> ChildrenBags
      {
         get
         {
            return m_OtherContainedBags;
         }
      }


      #endregion

   /*METHODS*/
      #region

      /// <summary>
      /// A method that returns a list of parents for this bag. The input list is the list of all bags
      /// </summary>
      /// <param name="allBags"></param>
      /// <returns></returns>
      public List<Bag> GetImmediateParents( List<Bag> allBags )
      {
      //Declare the list of parent bags..
         List<Bag> parentBags = new List<Bag>( );

         foreach( Bag b in allBags )
         {
            if( b.ChildrenBags.ContainsKey( this.ToString( ) ) )
               parentBags.Add( b );
         }
      //Return the list of parent bags;
         return parentBags;

      }



      /// <summary>
      /// A method that returns the total number og children of the input bag.
      /// </summary>
      /// <returns></returns>
      public static int GetNumberOfBagsForAllChildrenInlcusive( Bag bag, List<Bag> allBags )
      {
      //Declare the number of bags for this bag
         int nOfBags = 0;

      //Count the bag itself..
         nOfBags++;

      //Declare list of children bags
         List<Bag> children = new List<Bag>( );

      //Loop through the dictionary on the bag and sum up the total number of children..
         foreach( KeyValuePair<string, int> kvp in bag.ChildrenBags )
         {
            Bag child = allBags.Where( x => x.Color == kvp.Key ).FirstOrDefault( );

         //If this is not null, the bag is a child of current
            if( child != null )
            {
               int thisNumberOfChildren = GetNumberOfBagsForAllChildrenInlcusive( child, allBags );
               nOfBags += thisNumberOfChildren*kvp.Value;
            }
         }
         return nOfBags;
      }

      /// <summary>
      /// A method that returns the color of the bag.
      /// </summary>
      /// <returns></returns>
      public override string ToString( )
      {
         return this.Color;
      }


      #endregion

   }
}
