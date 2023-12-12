using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class HotSpringGroupLine
   {

   /*LOCAL CLASSES*/
   #region
   #endregion

   /*ENUMS*/
   #region
   #endregion

   /*MEMBERS*/
   #region

      protected static List<char> m_PermutationDigits = new List<char>( ) { '.', '#' };

      protected string m_Line = null;
      protected List<long> m_GroupPermutations = null;


   #endregion

   /*CONSTRUCTORS*/
   #region
      public HotSpringGroupLine( string inp )
      {
         m_GroupPermutations = new List<long>( );
      //Split by space to get numbers
         string[] inpS = inp.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
         string[] ns = inpS[1].Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
         foreach( string s in ns )
            m_GroupPermutations.Add( long.Parse( s ) );
         m_Line = inpS[0];
      }
   #endregion

   /*PROPERTIES*/
   #region
      public string Line { get { return m_Line; } }
   #endregion

   /*OPERATORS*/
   #region
   #endregion

   /*METHODS*/
   #region

      public long GetNumberOfPossiblePermutations( )
      {


      }




      protected bool IsValid( string line )
      {
      //Split by . and count number of groups
         string[] lineS = line.Split( new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries );

      //Validate groups
         if( lineS.Length != m_GroupPermutations.Count )
            return false;

         for( int i = 0; i<lineS.Length; i++ )
            if( lineS[i].Length != m_GroupPermutations[i] )
               return false;

         return true;
      }

      #endregion

      /*STATIC METHODS*/
      #region

      public static IEnumerable<string> GetNthEnumeration( IEnumerable<string> baseEnumeration, int n )
      {
         if( n == 0 ) //base case
         {
            foreach( var item in baseEnumeration ) { yield return item; }
         }
         else //build recursively
         {
            foreach( var pre in baseEnumeration )
            {
               foreach( var post in GetNthEnumeration( baseEnumeration, n - 1 ) )
               {
                  yield return pre + post;
               }
            }
         }
      }



      #endregion

   }
}
