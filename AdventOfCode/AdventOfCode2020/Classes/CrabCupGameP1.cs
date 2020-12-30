using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{

   /// <summary>
   /// A class that represents the crab cup game for DEC23
   /// </summary>
   public class CrabCupGameP1
   {

   /*MEMBERS*/
   #region

      protected List<int> m_CupState = new List<int>( ); //The current state of the cups. Order with values
      protected int m_CurrentCupIdx;
      protected int m_NumberOfCups;
      protected List<int> m_SortedCups;
      

   #endregion

   /*CONSTRUCTORS*/
   #region



      /// <summary>
      /// Default constructor.
      /// </summary>
      public CrabCupGameP1( string inp )
      {
         m_CurrentCupIdx = 0;
         m_NumberOfCups = inp.Length;
         for( int i = 0; i < inp.Length; i++ )
         {
            char thisNum = inp[i];
            m_CupState.Add( int.Parse( thisNum.ToString( ) ) );
         }
         m_SortedCups = new List<int>( m_CupState );
         m_SortedCups.Sort( );
      }
   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*METHODS*/
   #region
      
      /// <summary>
      /// Plays one round of the crab cup game. THen increments the current cup.
      /// </summary>
      public void PlayRound( )
      {
      //First, pick up three cups counter clockwise of the current cup
         List<int> remIdx = new List<int>( );
         remIdx.Add( ( m_CurrentCupIdx + 1 ) % m_NumberOfCups );
         remIdx.Add( ( m_CurrentCupIdx + 2 ) % m_NumberOfCups );
         remIdx.Add( ( m_CurrentCupIdx + 3 ) % m_NumberOfCups );

         List<int> remVals = new List<int>( );
         remVals.Add( m_CupState[remIdx[0]] );
         remVals.Add( m_CupState[remIdx[1]] );
         remVals.Add( m_CupState[remIdx[2]] );

         int currentVal = m_CupState[m_CurrentCupIdx];

         List<int> cupStateAccumulated = new List<int>( m_CupState );
         cupStateAccumulated.Remove( remVals[0] );
         cupStateAccumulated.Remove( remVals[1] );
         cupStateAccumulated.Remove( remVals[2] );

      //Find the one with a value one less than the current cup.
         List<int> predicateList = new List<int>( m_SortedCups );
         predicateList.Remove( remVals[0] );
         predicateList.Remove( remVals[1] );
         predicateList.Remove( remVals[2] );

      //Find the index of the destination cup
         int predicateIdx = predicateList.IndexOf( currentVal ) - 1;
         if( predicateIdx < 0 )
            predicateIdx = predicateList.Count - 1;
         int destVal = predicateList[predicateIdx];
         int destIdx = cupStateAccumulated.IndexOf( destVal );

      //Rewrite to a dictionary with placements such that the current cup is always index zero. This should make adding to the list easier.

      //Insert into list using the removed indices. This will have them in the correct order, but if something was inserted before the current index, they need to be pushed. THerefore, loop through the accumulated list and shift them back
         for( int i = 2; i >= 0; i-- )
            cupStateAccumulated.Insert( destIdx + 1, remVals[i] );

         int startIdx = cupStateAccumulated.IndexOf( currentVal );
         int[] newArr = new int[m_NumberOfCups];
         int arrCounter = m_CurrentCupIdx;
         for( int i = 0; i<m_NumberOfCups; i++ )
         {
            newArr[arrCounter % m_NumberOfCups] = cupStateAccumulated[startIdx % m_NumberOfCups];
            arrCounter++;
            startIdx++;
         }

      //Get new list.
         m_CupState = newArr.ToList( );

      //Increment the current cup index.
         m_CurrentCupIdx = ( m_CurrentCupIdx + 1 ) % m_NumberOfCups;

      }

      /// <summary>
      /// Prints the current cupstate to the console
      /// </summary>
      public void PrintState( )
      {
         string ret = "";
         for( int i = 0; i < m_CupState.Count; i++ )
         {
            string thisText = "";
            if( i == m_CurrentCupIdx )
               thisText += "(" + m_CupState[i].ToString( ) + ")";
            else
               thisText += " " + m_CupState[i].ToString( ) + " ";

            ret += thisText + " ";
         }
         Console.WriteLine( ret );
      }

   #endregion

   /*STATIC METHODS*/
   #region



   #endregion

   }
}
