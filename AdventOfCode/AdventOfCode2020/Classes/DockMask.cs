using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{

   /// <summary>
   /// Represents a bitmask for advent of code day14
   /// </summary>
   public class DockMask
   {

      /*MEMBERS*/
      #region

      protected static string m_ZeroString = "000000000000000000000000000000000000"; //The baseline string

      protected Dictionary<long, bool?> m_Mask = new Dictionary<long, bool?>( ); //The dictionary contains the indexes for the integers and their value as a bool. If the value is null, it is thought of as an X where the x can represent all the different adresses
      protected bool m_MaskAdress;
      #endregion

   /*CONSTRUCTORS*/
      #region

      /// <summary>
      /// Default constructor. Creates a mask from an input
      /// </summary>
      /// <param name="mask"></param>
      public DockMask( string inpMask, bool maskAdress )
      {
         m_MaskAdress = maskAdress;

      //Split the string by space to remove the mask part..
         string[ ] split = inpMask.Split( new char[ ] { ' ' } );
         if( split[0] != "mask" )
            throw new Exception( );

         string mask = split[2];

      //Create the mask from the string
         for( int i = mask.Length-1; i >= 0; i-- )
         {
            if( m_Mask.ContainsKey( i ) ) 
               throw new Exception( );
            if( mask[i] == '0' )
               m_Mask.Add( i, false );
            else if( mask[i] == '1' )
               m_Mask.Add( i, true );
            else if( mask[i] == 'X' )
               m_Mask.Add( i, null );
         }


      }
      #endregion



   /*METHODS*/
      #region


      /// <summary>
      /// Use this mask on the string..
      /// </summary>
      /// <param name="inp">Binary number as a string. If the masked adress bool is flagged, the method returns a string including Xs</param>
      /// <returns></returns>
      public string UseMaskOnString( string inp )
      {
      //Create the char array, but first. truncate the string with the necceccary zeros.
         char[ ] charArr = m_ZeroString.ToCharArray( );
         long idx = charArr.Length - 1;
         for( int i = inp.Length - 1; i >= 0; i-- )
            charArr[idx--] = inp[i];

      //Flag for part 1.
         if( !m_MaskAdress )
         {
            foreach( KeyValuePair<long, bool?> kvp in m_Mask )
            {
            //Skip nulls for this part..
               if( kvp.Value == null )
                  continue;

               charArr[kvp.Key] = kvp.Value == true ? '1' : '0';
            }
            return new string( charArr );
         }
         else //Flag for part 2
         {
         //Create the string.
            foreach( KeyValuePair<long, bool?> kvp in m_Mask )
            {
               if( kvp.Value == null )
                  charArr[kvp.Key] = 'X';
               else if( kvp.Value == true )
                  charArr[kvp.Key] = '1';
               else if( kvp.Value == false )
                  continue;
               else
                  throw new Exception( );
            }
            return new string( charArr );
         }
      }


      /// <summary>
      /// A method that returns all the adresses as integers with base 10 from a binary string representation of an original adress
      /// </summary>
      /// <param name="adress"></param>
      /// <returns></returns>
      public List<long> GetAllAdressesFromMask( string adress )
      {
      //Validation
         if( adress == null )
            throw new Exception( );

      //Get the masked string.
         string maskedAdressBinary = UseMaskOnString( adress );

      //Get all the possible permutations from that string.
         List<long> newAdresses = GetAllPossiblePermutationsForString( maskedAdressBinary );

      //Return this list of adresses..
         return newAdresses;
      }




      /// <summary>
      /// Gets a list of base 10 64 bit integers that is all the possible permutations of an adress containing the X as the abigious variable.
      /// </summary>
      /// <param name="inp"></param>
      /// <returns></returns>
      public static List<long> GetAllPossiblePermutationsForString( string inp )
      {
      //Create a list of permutations..
         List<string> permutations = new List<string>( );
         permutations.Add( inp );
         char[] cArr = inp.ToCharArray( );

      //Loop over all the indexes and create the permutations for each string
         for( int i = 0; i < cArr.Length; i++ )
            if( cArr[i] == 'X' )
            {
               permutations = GetPermutationsForOneIndex( permutations, i );
            }

      //COnvert the list of permutations to 64 bit integers..
         List<long> values = new List<long>( );
         foreach( string s in permutations )
         {
         //Convert back to an normal long
            long value = Convert.ToInt64( s, 2 );

         //Add values to the list..
            values.Add( value );
         }

      //Return the list of all the permutations..
         return values;
      }


      /// <summary>
      /// Gets the permutations of a string for one index. 
      /// </summary>
      /// <param name="previousStrings"></param>
      /// <param name="idx"></param>
      /// <returns></returns>
      public static List<string> GetPermutationsForOneIndex( List<string> previousStrings, int idx )
      {
         List<string> newStringsPermuated = new List<string>( );
         foreach( string s in previousStrings )
         {
            char[] tab = s.ToCharArray( );
            tab[idx] = '0';
            newStringsPermuated.Add( new string( tab ) );
            tab[idx] = '1';
            newStringsPermuated.Add( new string( tab ) );
         }

      //Update the adress of the previous strings to the permutated string.
         return newStringsPermuated;
      }

   #endregion


   }
}



