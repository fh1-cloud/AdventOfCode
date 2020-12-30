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
   public class CrabCupGameP2
   {




   /*LOCAL CLASSES*/
   #region

      /// <summary>
      /// Create a cup class
      /// </summary>
      public class Cup
      {
         public int Value { get; set; }       //The value of this cup
         public Cup Next { get; set; } //The next cup in line after this cup. THis is the cup just to the right of this cup
      }
   #endregion


   /*MEMBERS*/
   #region

      protected int m_NumberOfCups;
      protected Cup m_CurrentCup;
      protected Dictionary<long, Cup> m_Cups = new Dictionary<long, Cup>( );        //A dictionary with the cup values and the underlying cup. The next cup in line can be gotten this way


   #endregion

   /*CONSTRUCTORS*/
   #region



      /// <summary>
      /// Default constructor.
      /// </summary>
      public CrabCupGameP2( string inp, int extraCups )
      {
      //Set the total number of cups..
         m_NumberOfCups = inp.Length + extraCups;

      //Create the linked list for these values
         int thisVal = int.Parse( inp[0].ToString( ) );
         int firstVal = thisVal;
         Cup prevCup = new Cup { Value = thisVal };
         m_Cups.Add( thisVal, prevCup );

      //Add the input cups
         for( int i = 1; i < inp.Length; i++ )
         {
            char thisNum = inp[i];
            thisVal = int.Parse( inp[i].ToString( ) );
            Cup thisCup = new Cup { Value = thisVal };
            m_Cups.Add( thisVal , thisCup );
            prevCup.Next = thisCup;
            prevCup = thisCup;
         }

      //Add extra cups
         for( int i = inp.Length+1; i<extraCups+inp.Length +1; i++ )
         {
            Cup thisCup = new Cup { Value = i };
            m_Cups.Add( i, thisCup );
            prevCup.Next = thisCup;
            prevCup = thisCup;

         }
      //Set the link for the last cup in the set.

         m_Cups[prevCup.Value].Next = m_Cups[firstVal];

      //Set the current cup to the first one in the dictionary
         m_CurrentCup = m_Cups[firstVal];
      }
   #endregion

   /*PROPERTIES*/
   #region
      public Dictionary<long, Cup> Cups
      {
         get
         {
            return m_Cups;
         }
      }



   #endregion

   /*METHODS*/
   #region
      
      /// <summary>
      /// Plays one round of the crab cup game. THen increments the current cup.
      /// </summary>
      public void PlayRound( )
      {

      //Pick up three cups
         Cup pickup1 = m_CurrentCup.Next;
         Cup pickup2 = pickup1.Next;
         Cup pickup3 = pickup2.Next;

      //Set the next cup in line after the current cup to the next cup after pickup 3
         m_CurrentCup.Next = pickup3.Next;

      //The three cups are now out of the loop. find the destination value. Add the picked up values to a hash set, and find the next cup with a value that is not in this hash set.
         HashSet<int> pickedUpValues = new HashSet<int>( );
         pickedUpValues.Add( pickup1.Value );
         pickedUpValues.Add( pickup2.Value );
         pickedUpValues.Add( pickup3.Value );

         int currentVal = m_CurrentCup.Value;
         
         while( true )
         {
            currentVal--;
            if( currentVal == 0 )
               currentVal = m_NumberOfCups;
            if( pickedUpValues.Contains( currentVal ) )
               continue;
            else
            {
               break;
            }
         }
         Cup valueCup = m_Cups[currentVal];

      //Insert the picked up cups. 
      //First, store what the value cup was previously linked to because the third cup picked up needs to be linked to this cup
         Cup lostLink = valueCup.Next;
         valueCup.Next = pickup1;
         pickup3.Next = lostLink;

      //Increment the current cup to the next in the link..
         m_CurrentCup = m_CurrentCup.Next;

      }

      /// <summary>
      /// Prints the current cupstate to the console
      /// </summary>
      public void PrintState( )
      {
         string ret = "";
         //for( int i = 0; i < m_CupState.Count; i++ )
         //{
         //   string thisText = "";
         //   if( i == m_CurrentCupIdx )
         //      thisText += "(" + m_CupState[i].ToString( ) + ")";
         //   else
         //      thisText += " " + m_CupState[i].ToString( ) + " ";

         //   ret += thisText + " ";
         //}
         Console.WriteLine( ret );
      }

   #endregion

   /*STATIC METHODS*/
   #region



   #endregion

   }
}
