using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Classes
{
   public class PageSorter
   {
      public enum PAGESORTERRESULT
      {
         CORRECT,
         INCORRECT,
         NOTAPPLICABLE
      }

      public class PageSorterRule
      {
         public int m_PrePage = -1;
         public int m_PostPage = -1;

         public PageSorterRule( string thisLine )
         {
            string[] pages = thisLine.Split( '|', StringSplitOptions.RemoveEmptyEntries );
            m_PrePage = int.Parse( pages[0] );
            m_PostPage = int.Parse( pages[1] );
         }

         public PAGESORTERRESULT ApplyRule( int currentPage, int subsequentPage )
         {
            if( currentPage == m_PrePage && subsequentPage == m_PostPage )
               return PAGESORTERRESULT.CORRECT;
            else if( currentPage == m_PostPage && subsequentPage == m_PrePage )
               return PAGESORTERRESULT.INCORRECT;
            else
               return PAGESORTERRESULT.NOTAPPLICABLE;
         }

      }

      public List<PageSorterRule> m_Rules = null;
      public List<int[]> m_Updates = null;

      public PageSorter( string[] inp )
      {
         List<string[]> newLineSplit = GlobalMethods.SplitStringArrayByEmptyLine( inp );

      //Create rules
         m_Rules = new List<PageSorterRule>( );
         foreach( var s in newLineSplit[0] )
            m_Rules.Add( new PageSorterRule( s ) );

      //Create updates
         m_Updates = GlobalMethods.GetInputIntArrayListFromStringArray( newLineSplit[1] );
      }

      public int P1( )
      {

      //Collection of incorrect updates..
         int middlePageSum = 0;
         foreach( int[] thisUpdate in m_Updates )
         {
            bool isPrinted = true;
            for( int i = 0; i < thisUpdate.Length - 1; i++ )
               for( int j = i + 1; j < thisUpdate.Length; j++ )
                  foreach( PageSorterRule rule in m_Rules )
                     if( rule.ApplyRule( thisUpdate[i], thisUpdate[j] ) == PAGESORTERRESULT.INCORRECT )
                        isPrinted = false;

         //Check if this update is printed, if so add middle page number
            if( isPrinted )
               middlePageSum += thisUpdate[thisUpdate.Length / 2];
         }
         return middlePageSum;
      }


      public int P2( )
      {
      //Collect incorrect updates..
         List<int[]> incorrectUpdates = new List<int[]>( );
         foreach( int[] thisUpdate in m_Updates )
         {
            bool isPrinted = true;
            for( int i = 0; i < thisUpdate.Length - 1; i++ )
               for( int j = i + 1; j < thisUpdate.Length; j++ )
                  foreach( PageSorterRule rule in m_Rules )
                     if( rule.ApplyRule( thisUpdate[i], thisUpdate[j] ) == PAGESORTERRESULT.INCORRECT )
                        isPrinted = false;

         //Check if this update is printed, if so add middle page number
            if( !isPrinted )
               incorrectUpdates.Add( thisUpdate );
         }

      //Loop over all incorrect updates. Find the first that violates the page rules, When it does, shift it to the left one spot and try again.
         int middlePageSum = 0;
         foreach( int[ ] thisUpdate in incorrectUpdates )
         {
            bool run = true;
            while( run )
            {
               bool shiftedSomething = false;
               for( int i = 0; i < thisUpdate.Length - 1; i++ )
               {
                  for( int j = i + 1; j < thisUpdate.Length; j++ )
                  {
                     foreach( PageSorterRule rule in m_Rules )
                     {
                        if( rule.ApplyRule( thisUpdate[i], thisUpdate[j] ) == PAGESORTERRESULT.INCORRECT )
                        {
                           ( thisUpdate[j-1], thisUpdate[j]) = (thisUpdate[j], thisUpdate[j - 1]);
                           shiftedSomething = true;
                           break;
                        }
                     }
                     if( shiftedSomething )
                        break;
                  }
                  if( shiftedSomething )
                     break;
               }
            //If the code reached this point, nothing was shiften while looping over the elements. This means that the current update is correct.
               if( !shiftedSomething )
                  run = false;
            }

         //When the code reached here, everything is correct. Add the middle page number to the sum.
            middlePageSum += thisUpdate[thisUpdate.Length / 2];
         }

         return middlePageSum;
      }

   }


}
