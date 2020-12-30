using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class TrainTicketValidater
   {


   /*MEMBERS*/
   #region
      public Dictionary<string, List<int[]>> m_Values = new Dictionary<string, List<int[]>>( ); //A dictionary that contains the keywords with the upper and lower bounds
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Creates a ticket validater
      /// </summary>
      /// <param name="inp"></param>
      public TrainTicketValidater( string[] inp )
      {
         for( int i = 0; i < inp.Length; i++ )
         {
            if( inp[i] == "" )
               break;
            string thisString = inp[i];

         //Collect the string with the numbers..
            string numberPart = null;
            for( int j = 0; j < thisString.Length; j++ )
            {
               char thisChar = thisString[j];
               int dummy;
               bool isInt = int.TryParse( thisChar.ToString( ), out dummy );
               if( isInt )
               {
                  numberPart = thisString.Substring( j );
                  break;
               }
            }

         //First, split numberstring by space..
            if( numberPart != null )
            {
               string name = thisString.Split( new char[] { ':' } )[0];
               string[] secondSplit = numberPart.Split( new char[] { ' ' } );

               List<int[]> bounds = new List<int[]>( );
               //Loop over this split and collect the upper and lower bound values
               for( int j = 0; j < secondSplit.Length; j++ )
               {
                  if( j%2 == 0 )
                  {
                     string[] thirdSplit = secondSplit[j].Split( new char[] { '-' } );
                     int lowBound;
                     int highBound;
                     bool lowParse = int.TryParse( thirdSplit[0], out lowBound );
                     bool highParse = int.TryParse( thirdSplit[1], out highBound );
                     if( !lowParse || !highParse )
                        throw new Exception( );

                  //Declare the new array with upper and lower bounds..
                     int[] thisBounds = new int[2] { lowBound, highBound };
                     bounds.Add( thisBounds );


                  }

               }
               m_Values.Add( name, bounds );

            }

         }

      }

   #endregion


   /*PROPERTIES*/
   #region

      /// <summary>
      /// Gets the total number of categories in the validater
      /// </summary>
      public int NumberOfCategories
      {
         get
         {
            return m_Values.Count;
         }
      }

   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Checks if the input number is between the bounds of a category. if the category is null, all the categories are checked and true is returned if it is between at least one of them
      /// </summary>
      /// <param name="number"></param>
      /// <param name="category"></param>
      /// <returns></returns>
      public bool IsBetweenBoundsOfCategory( int number, string category = null )
      {
         if( category == null )
         {
            foreach( KeyValuePair<string, List<int[]>> kvp in m_Values )
            {
               foreach( int[] bounds in kvp.Value )
               {
                  if( number > bounds[0] && number <= bounds[1] )
                  {
                     return true;
                  }
               }
            }
            return false;
         }
         else //Category is specific
         {
            if( !m_Values.ContainsKey( category ) )
               throw new Exception( );

            foreach( int[] bounds in m_Values[category] )
            {
               if( number >= bounds[0] && number <= bounds[1] )
               {
                  return true;
               }
            }
            return false;
         }

      }


      /// <summary>
      /// Gets the valid inddices for the train tickets for a specific categories. The ignored indices are skipped
      /// </summary>
      /// <param name="tickets"></param>
      /// <param name="category"></param>
      /// <param name="ignoreTheseIndices"></param>
      /// <returns></returns>
      public List<int> GetValidIndicesForCategory( List<TrainTicket> tickets, string category, List<int> ignoreTheseIndices )
      {
      //Check if category exists. If not, crash
         if( !m_Values.ContainsKey( category ) )
            throw new Exception( );

      //Check the valies for the train ticket for each one individually
         List<int> validIndicesForCategory = new List<int>( );
         int numberOfFields = tickets[0].Values.Count;
         for( int i = 0; i < numberOfFields; i++ )
         {
            if( ignoreTheseIndices.Contains( i ) )
               continue;

            bool isValidForAll = true;
            foreach( TrainTicket tt in tickets )
            {
               bool validForThis = IsBetweenBoundsOfCategory( tt.Values[i], category );
               if( !validForThis )
               {
                  isValidForAll = false;
                  break;
               }
            }
            if( isValidForAll )
               validIndicesForCategory.Add( i );
         }
      //Return the list of valid indices. All the tickets checked.
         return validIndicesForCategory;
      }
 



      /// <summary>
      /// A method that tries to find the next category that is only valid for exactly one of the categories. Certain categories can be ignored if they are already found.
      /// </summary>
      /// <param name="tickets"></param>
      /// <param name="ignoreThese"></param>
      /// <returns></returns>
      public Tuple<string,int> FindNextCategoryIndexPair( List<TrainTicket> tickets, List<Tuple<string,int>> ignoreThese )
      {

      //Declare the list of indices to ignore..
         List<int> ignoreTheseIndices = new List<int>( );
         foreach( Tuple<string,int> ign in ignoreThese )
            ignoreTheseIndices.Add( ign.Item2 );

      //Loop over all the categories
         foreach( KeyValuePair<string, List<int[]>> kvp in m_Values )
         {
         //First, check if the current category is in the ingore list. If so, cycle to next
            foreach( Tuple<string,int> ign in ignoreThese )
            {
               if( ign.Item1 == kvp.Key )
                  continue;
            }

         //Get the valid indices for this category.
            List<int> validIndices = GetValidIndicesForCategory( tickets, kvp.Key, ignoreTheseIndices );
            if( validIndices.Count == 1 )
               return new Tuple<string, int>( kvp.Key, validIndices[0] );
         }
         return null;
      }


   #endregion


   }
}
