using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{

   
   /// <summary>
   /// A class that represents a train ticket for AoC Dec 16
   /// </summary>
   public class TrainTicket
   {

   /*MEMBERS*/
   #region
      protected List<int> m_Values = new List<int>( );
      protected Dictionary<string, int> m_CategoriesWithValues = new Dictionary<string, int>( );
   #endregion

   /*CONSTRUCTORS*/
   #region
      /// <summary>
      /// Default constructor. Creates a tickets from a series of numbers
      /// </summary>
      /// <param name="inp"></param>
      public TrainTicket( string inp )
      {
      //SPlit string by comma
         string[] split = inp.Split( new char[] { ',' } );

      //Add values for this tickets
         for( int i = 0; i < split.Length; i++ )
            m_Values.Add( int.Parse( split[i] ) );
      }

   #endregion



   /*PROPERTIES*/
   #region
      public List<int> Values => m_Values;
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Adds a category with a value to the dictionary
      /// </summary>
      /// <param name="cat"></param>
      /// <param name="val"></param>
      public void AddCategoryWithValue( string cat, int val )
      {
         m_CategoriesWithValues.Add( cat, val );
      }


      /// <summary>
      /// Gets the value for a specific category
      /// </summary>
      /// <param name="cat"></param>
      /// <returns></returns>
      public int GetValueForCategory( string cat )
      {
         return m_CategoriesWithValues[cat];
      }


      /// <summary>
      /// Gets the number of values currently in the dictionary
      /// </summary>
      /// <returns></returns>
      public int GetNumberOfDecidedCategories()
      {
         return m_CategoriesWithValues.Count;
      }

      /// <summary>
      /// Gets the invalid numbers for this train ticket
      /// </summary>
      /// <param name="validater"></param>
      /// <returns></returns>
      public List<int> GetInvalidNumbers( TrainTicketValidater validater )
      {
      //Check all the numbers on this ticket
         List<int> invalidNumbers = new List<int>( );
         foreach( int i in m_Values )
            if( !validater.IsBetweenBoundsOfCategory( i, null ) )
               invalidNumbers.Add( i );

      //Return completed list of invalid numbers
         return invalidNumbers;
      }


      /// <summary>
      /// Gets the values for the categories starting with departure for my ticket. Probably wont work anywhere else
      /// </summary>
      /// <returns></returns>
      public long GetAnswerForPartTwoForMyTicked()
      {
         List<int> departureValues = new List<int>( );
         foreach( KeyValuePair<string, int> kvp in m_CategoriesWithValues )
         {
            string[] splitter = kvp.Key.Split( new char[] { ' ' } );
            if( splitter[0] == "departure" )
               departureValues.Add( kvp.Value );
         }

         long ans = 1;
         foreach( int a in departureValues )
            ans *= a;

         return ans;
      }




   #endregion



   }
}
