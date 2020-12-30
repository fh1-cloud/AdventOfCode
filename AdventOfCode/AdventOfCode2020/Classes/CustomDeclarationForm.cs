using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class CustomDeclarationForm
   {
      protected Dictionary<char, bool> m_QuestionsWithAnswers = new Dictionary<char, bool>( );

      public CustomDeclarationForm( string[] groupsStringArr )
      {
      //initialize the dictionary with all the char values from 1 to 26';
         for( int i = 97; i < 123; i++ )
            m_QuestionsWithAnswers.Add( ( char ) i, false );


      //PART2
      //First, loop through all the lines in the group array and find the ones that everyone answered.
         Dictionary<char, bool> newDict = new Dictionary<char, bool>( );
         foreach( KeyValuePair<char, bool> kvp in m_QuestionsWithAnswers )
         {
            bool foundThisKey = true;
         //Check this key for all entries
            foreach( string p in groupsStringArr )
            {
               if( !p.ToCharArray( ).Contains( kvp.Key ) )
               {
                  foundThisKey = false;
                  break;
               }
            }
            newDict.Add( kvp.Key, foundThisKey );
         }
         m_QuestionsWithAnswers = newDict;

      ////PART1
      ////Loop through the different strings. Toggle look up the dictionary key for the question and toggle it.
      //   foreach( string personalAnswer in groupsStringArr )
      //      foreach( char c in personalAnswer.ToCharArray( ) )
      //         m_QuestionsWithAnswers[c] = true;
      }

      public int GetNumberOfPositivesFromForm( )
      {
         return m_QuestionsWithAnswers.Where( x => x.Value == true ).ToList( ).Count;
      }




   }
}
