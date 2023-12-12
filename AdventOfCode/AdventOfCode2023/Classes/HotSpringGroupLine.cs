using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Classes
{
   public class HotSpringGroupLine
   {

   /*MEMBERS*/
   #region
      protected static Dictionary<string,long> m_Cache = new Dictionary<string,long>();
      protected static char[] m_DotSplit = { '.' };
      protected string m_Line = null;
      protected List<int> m_GroupPermutations = null;
   #endregion

   /*CONSTRUCTORS*/
   #region
      public HotSpringGroupLine( string inp )
      {
         m_GroupPermutations = new List<int>( );
      //Split by space to get numbers
         string[] inpS = inp.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
         string[] ns = inpS[1].Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
         foreach( string s in ns )
            m_GroupPermutations.Add( int.Parse( s ) );
         m_Line = inpS[0];

      }
   #endregion

   /*METHODS*/
   #region
      public long GetNumberOfPossiblePermutations( )
      {
         return Calculate( m_Line, m_GroupPermutations );
      }
   #endregion

   /*STATIC METHODS*/
   #region


      public static long Calculate( string s, List<int> groups )
      {
      //Create a key of this input, so we can get the numbers later and we dont have to do it for identical strings..
         string key = $"{s},{string.Join( ",", groups)}";

      //Try to see if it exists..
         if( m_Cache.TryGetValue( key, out long val ) )
            return val;

      //If the code reached this point, we didnt find it in the memo. Get the value and memo it so we dont have to do it more times than neccessary
         long value = GetPermutations( s, groups );

         m_Cache.Add( key, value );
         return value;
      }

      public static long GetPermutations( string spring, List<int> groups )
      {

         while( true )
         {

         //Fist, if there are no groups left..
            if( groups.Count == 0 )
            {
               if( spring.Contains( '#' ) ) //The leftover string contains #, but there are no more groups to match, this cannot be a match
                  return 0;
               else
                  return 1;
            }

         //If the code reached this point, we know there are more than one group. If the string has no length, this isnt a match.
            if( spring.Length == 0 )
               return 0;

         //If the first character is a dot, remove all the dots and continue.
            if( spring[0] == '.' )
            {
               spring = spring.Trim( '.' );
               continue;
            }
         //This can be either a . or a #, check both.
            if( spring[0] == '?' ) 
               return Calculate( "." + spring.Substring( 1, spring.Length - 1 ), groups ) + Calculate( "#" + spring.Substring( 1, spring.Length - 1 ), groups );

         //This is the start of a group.
            if( spring[0] == '#' )
            {
            //If groups are zero, this cannot be correct
               if( groups.Count == 0 )
                  return 0;

            //Check if there are enough characters left after this group
               if( spring.Length < groups[0] )
                  return 0;

            //Check if this block contains .  if so this isnt a match
               if( spring.Substring( 0, groups[0] ).Contains( "." ) )
                  return 0;

               if( groups.Count > 1 )
               {
               //Check if there are enough characters left after this group
                  if( spring.Length < groups[0] + 1 )
                     return 0;

               //Check if the character after this group is also a #, if so, this cannot be a match
                  if( spring[groups[0]] == '#' )
                     return 0;

               //If the code reachere here, this group is a match. Remove if from the string and groups and continue checking
                  spring = spring.Substring( groups[0] + 1 );
                  groups = new List<int>( groups );
                  groups.RemoveAt( 0 );
                  continue;
               }
            //If the code reached here, we know it is exactly 1 group left, and it is a match.
               spring = spring.Substring( groups[0] );
               groups = new List<int>( groups );
               groups.RemoveAt( 0 );
               continue;
            }
         }
      }

   #endregion

   }
}
