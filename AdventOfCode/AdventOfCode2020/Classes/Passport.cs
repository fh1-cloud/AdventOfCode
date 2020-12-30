using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class Passport
   {

   /*ENUMS*/
      #region

      /// <summary>
      /// Enum that represents what type of height that is input in the height field of the passport
      /// </summary>
      public enum PASSPORTHEIGHTUNIT
      {
         CM,
         IN
      }

      #endregion

   /*LOCAL CLASSES */
      #region
      /// <summary>
      /// A class that represents a raw data pair for the initialization of the passports
      /// </summary>
      protected class PassportRawDataPair
      {
         public string EntryType { get; set; }
         public object Value { get; set; }
      }

      /// <summary>
      /// Height with units for use in the passport class
      /// </summary>
      public class HeightWithUnits
      {
         public int Value { get; set; }
         public PASSPORTHEIGHTUNIT Unit { get; set; }
      }

      #endregion


      /*MEMBERS*/
      #region

      protected static char[] m_AcceptedHairColorSymbols = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f' };
      protected static string[] m_AcceptedEyeColorStrings = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

      #endregion

   /*CONSTRUCTORS*/
      #region

      public Passport( )
      {
         this.BirthYear = null;
         this.IssueYear = null;
         this.ExpirationYear = null;
         this.Height = null;
         this.HairColor = null;
         this.EyeColor = null;
         this.PassportID = null;
         this.CountryID = null;
      }

      #endregion


   /*PROPERTIES*/
      #region
      public int? BirthYear { get; set; }
      public int? IssueYear { get; set; }
      public int? ExpirationYear { get; set; }
      public HeightWithUnits Height { get; set; }
      public string HairColor { get; set; }
      public string EyeColor { get; set; }
      public string PassportID { get; set; }
      public long? CountryID { get; set; }

      #endregion


      /// <summary>
      /// Properties for the raw data strings. This is to validate the junk data from part 1
      /// </summary>
      public string Common_byr { get; set; }
      public string Common_iyr { get; set; }
      public string Common_eyr { get; set; }
      public string Common_hgt { get; set; }
      public string Common_hcl { get; set; }
      public string Common_ecl { get; set; }
      public string Common_pid { get; set; }
      public string Common_cid { get; set; }

   /*METHODS*/
      #region
      /// <summary>
      /// Validate if the password is valid or not..
      /// </summary>
      /// <param name="includeCountryValidation"></param>
      /// <returns></returns>
      public bool ValidatePassport( bool includeCountryValidation, bool validateJunkData )
      {
         if( !validateJunkData )
         {
            if( this.BirthYear == null || this.IssueYear == null || this.ExpirationYear == null || this.Height == null || this.HairColor == null || this.EyeColor == null || this.PassportID == null )
               return false;
            if( includeCountryValidation )
               if( this.CountryID == null )
                  return false;

         //Check the individual values here..

         //Birth date
            if( this.BirthYear < 1920 || this.BirthYear > 2002 )
               return false;
            if( this.IssueYear < 2010 || this.IssueYear > 2020 )
               return false;
            if( this.ExpirationYear < 2020 || this.ExpirationYear > 2030 )
               return false;
            if( this.Height.Unit == PASSPORTHEIGHTUNIT.CM )
            {
               if( this.Height.Value < 150 || this.Height.Value > 193 )
                  return false;
            }
            else if( this.Height.Unit == PASSPORTHEIGHTUNIT.IN )
            {
               if( this.Height.Value < 59 || this.Height.Value > 76 )
                  return false;
            }
            if( ValidateHairColorString( this.HairColor ) == false )
               return false;
            if( ValidateEyeColorString( this.EyeColor ) == false )
               return false;
            if( ValidatePassportIDString( this.PassportID ) == false )
               return false;
         }
         else
         {
            if( this.Common_byr == null || this.Common_ecl == null || this.Common_eyr == null || this.Common_hcl == null || this.Common_hgt == null || this.Common_iyr == null || this.Common_pid == null )
               return false;
            if( includeCountryValidation )
            {
               if( this.Common_cid == null )
               {
                  return false;
               }
            }
         }
         return true;
      }



      #endregion

      /*STATIC METHODS*/
      #region


      /// <summary>
      /// Validates the hexadecimal color string for hair color
      /// </summary>
      /// <param name="colorString"></param>
      /// <returns></returns>
      public static bool ValidateHairColorString( string colorString )
      {
      //Validation
         if( colorString == null )
            return false;

      //First, check the length of the string. It has to be exactly 7 characters long.
         if( colorString.Length != 7 )
            return false;

      //Then, check if the first character is a pound symbol
         if( colorString[0] != '#' )
            return false;

      //THen, check if any of the symbols are not correct.
         for( int i = 1; i < colorString.Length; i++ )
         {
            if( !m_AcceptedHairColorSymbols.Contains( colorString[i] ) )
               return false;
         }

      //If the code reached this point, the hair color is valid
         return true;
      }

      /// <summary>
      /// Validates the string that describes eye colors
      /// </summary>
      /// <param name="colorString"></param>
      /// <returns></returns>
      public static bool ValidateEyeColorString( string colorString )
      {
      //Validation
         if( colorString == null )
            return false;

      //First, check the length of the string. It has to be exactly 3 characters long.
         if( colorString.Length != 3 )
            return false;

      //Loop through the list and check..
         if( !m_AcceptedEyeColorStrings.Contains( colorString ) )
            return false;

      //If the code reached this point, the eye color is valid
         return true;
      }

      /// <summary>
      /// Validates the string for the passport ID.
      /// </summary>
      /// <param name="idString"></param>
      /// <returns></returns>
      public static bool ValidatePassportIDString( string idString )
      {
      //Validation
         if( idString == null )
            return false;

      //Check the length of the string. It has to be exactly 9 characters long.
         if( idString.Length != 9 )
            return false;

      //Try to parse the number to an int.
         int res;
         bool success = int.TryParse( idString, out res );

         if( !success )
            return false;

      //Check if the number is negative
         if( res <= 0 )
            return false;

      //If the code reached this point, it was successful
         return true;
      }

      /// <summary>
      /// Creates a passport from a string array
      /// </summary>
      /// <param name=""></param>
      /// <returns></returns>
      public static Passport CreateFromStringArray( string[] rawData, bool createTypes)
      {
      //Validation
         if( rawData == null )
            return null;

         List<PassportRawDataPair> rawDataPairs = new List<PassportRawDataPair>( );
         Passport retPassport = new Passport( );

      //First, loop through all the data in the array and order them into pairs
         foreach( string line in rawData )
         {

         //Split by space.
            string[] spaceSplit = line.Split( new char[] { ' ' } );

         //Loop through it all and save the value pairs..
            foreach( string spaceWord in spaceSplit )
            {
               string[] dotSplit = spaceWord.Split( new char[] { ':' } );
               PassportRawDataPair p = new PassportRawDataPair( );
               p.EntryType = dotSplit[0];
               p.Value = dotSplit[1];
               rawDataPairs.Add( p );
            }
         }
         if( createTypes )
         {
            //Loop through the datapairs and set the correct values
            foreach( PassportRawDataPair p in rawDataPairs )
            {
               if( p.EntryType == "byr" )
               {
                  int val = Convert.ToInt32( p.Value );
                  retPassport.BirthYear = val;
               }
               else if( p.EntryType == "iyr" )
               {
                  int val = Convert.ToInt32( p.Value );
                  retPassport.IssueYear = val;
               }
               else if( p.EntryType == "eyr" )
               {
                  int val = Convert.ToInt32( p.Value );
                  retPassport.ExpirationYear = val;
               }
               else if( p.EntryType == "hgt" )
               {
                  //Split the text where the units start
                  string val = ( string ) p.Value;
                  char[] data = val.ToCharArray( );

                  //Find the index of the start of the unit. Create substring.
                  int unitIdx = -1;
                  int heightStringLength = -1;
                  for( int i = 0; i < data.Length; i++ )
                  {
                     int parseVal;
                     bool parsedChar = int.TryParse( data[i].ToString( ), out parseVal );
                     if( parsedChar )
                     {
                        if( i != data.Length - 1 )
                        {
                           if( data[i + 1].Equals( '.' ) )
                           {
                              throw new Exception( );
                           }
                        }
                     }
                     else
                     {
                        heightStringLength = i;
                        unitIdx = i;
                        break;
                     }
                  }
                  HeightWithUnits height = new HeightWithUnits( );
                  //Check if units were given
                  if( unitIdx == -1 )
                  {
                     retPassport.Height = null;
                  }
                  else
                  {
                     string numberString;
                     if( unitIdx != -1 )
                        numberString = val.Substring( 0, heightStringLength);
                     else
                        numberString = val;

                  //Set the units..
                     string unitString = val.Substring( unitIdx, val.Length - heightStringLength );
                     if( unitString == "cm" )
                        height.Unit = PASSPORTHEIGHTUNIT.CM;
                     else if( unitString == "in" )
                        height.Unit = PASSPORTHEIGHTUNIT.IN;

                     long heightVal = long.Parse( numberString );
                     height.Value = ( int ) heightVal;
                     retPassport.Height = height;


                  }
                     
               }
               else if( p.EntryType == "hcl" )
                  retPassport.HairColor = ( string ) p.Value;
               else if( p.EntryType == "ecl" )
                  retPassport.EyeColor = ( string ) p.Value;
               else if( p.EntryType == "pid" )
                  retPassport.PassportID = ( string ) p.Value;
               else if( p.EntryType == "cid" )
               {
                  long val = Convert.ToInt64( p.Value );
                  retPassport.CountryID = val;
               }
               else //Invalid signature.
               {
                  throw new Exception( );
               }
            }

         }
         else
         {
            //Loop through the datapairs and set the correct values
            foreach( PassportRawDataPair p in rawDataPairs )
            {
               if( p.EntryType == "byr" )
                  retPassport.Common_byr = ( string ) p.Value;
               else if( p.EntryType == "iyr" )
                  retPassport.Common_iyr = ( string ) p.Value;
               else if( p.EntryType == "eyr" )
                  retPassport.Common_eyr = ( string ) p.Value;
               else if( p.EntryType == "hgt" )
                  retPassport.Common_hgt = ( string ) p.Value;
               else if( p.EntryType == "hcl" )
                  retPassport.Common_hcl = ( string ) p.Value;
               else if( p.EntryType == "ecl" )
                  retPassport.Common_ecl = ( string ) p.Value;
               else if( p.EntryType == "pid" )
                  retPassport.Common_pid = ( string ) p.Value;
               else if( p.EntryType == "cid" )
                  retPassport.Common_cid = ( string ) p.Value;
            }
         }

      //Return the created passport
         return retPassport;
      }





      #endregion


   }
}
