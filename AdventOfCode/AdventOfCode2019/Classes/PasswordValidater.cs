using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Classes
{
   public class PasswordValidater
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

      public static bool Validate( int inputNum )
      {

      //Convert to string..
         string password = inputNum.ToString( );

      //Check if digits increase or not first..
         for( int i = 0; i< password.Length - 1; i++ )
            if( int.Parse( password[i + 1].ToString( ) ) < int.Parse( password[i].ToString( ) ) )
               return false;

      //Check if two values are the same..
         bool twoAdjacent = false;
         for( int i = 0; i < password.Length - 1; i++ )
         {
            if( password[i + 1] == password[i] )
               twoAdjacent = true;
         }
         if( !twoAdjacent )
            return false;

      //Check for repeating matching values larger than two. Since the code reached this point, we are sure that there are some repeating numbers.
         List<string> parts = new List<string>( );
         string newString = password;
         int length = 1;
         for( int i = 0; i <= password.Length - 1; i++ )
         {
            if( i != password.Length - 1 && password[i] == password[i + 1])
            {
               length++;
               continue;
            }
            else
            {
               parts.Add( newString.Substring( 0, length ) );
               newString = newString.Substring( length , newString.Length - length );
               length = 1;
            }
         }
      //Check the individual parts. If there are any that have length of 3 or more, it is invalid
         bool hasAtLeastOne = false;
         foreach( string s in parts )
            if( s.Length == 2 )
               hasAtLeastOne = true;

         if( !hasAtLeastOne )
            return false;
            

      //If the code reached this point, it is valid.
         return true;


      }


      private static int CountNeighboursWithSameChar( string s, int idx )
      {
         int nOfEqualNeighbours = 0;
         for( int i = idx + 1; i<s.Length-1; i++ )
         {
            if( s[idx] == s[i] )
               nOfEqualNeighbours++;
            else
               break;
         }
         return nOfEqualNeighbours;
      }

   #endregion





   }
}
