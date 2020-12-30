using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
   public class CardGamePlayer
   {

   /*MEMBERS*/
   #region
      protected List<int> m_Cards;
      protected int m_ID;
   #endregion

   /*CONSTRUCTORS*/
   #region

      /// <summary>
      /// Initializes a card game player from the input in the textfile
      /// </summary>
      /// <param name="input"></param>
      public CardGamePlayer( string[] input )
      {

      //Take the first line and parse the player id..
         string[] idSPlit = input[0].Split( new char[] { ' ' } );
         m_ID = int.Parse( idSPlit[1].Substring( 0, 1 ) );

      //Set starting card deck
         m_Cards = new List<int>( );
         for( int i = 1; i < input.Length; i++ )
         {
            int thisCardValue = int.Parse( input[i] );
            m_Cards.Add( thisCardValue );
         }

      }


      /// <summary>
      /// Creates a new card game player with the designated ID and a list of cards. Used to start a game of recursive combat
      /// </summary>
      /// <param name="id"></param>
      /// <param name="cards"></param>
      public CardGamePlayer( int id, List<int> cards )
      {
         m_ID = id;
         m_Cards = new List<int>( cards );
      }


   #endregion

   /*PROPERTIES*/
   #region
   #endregion

   /*METHODS*/
   #region

      /// <summary>
      /// Returns the score for this card player
      /// </summary>
      /// <returns></returns>
      public long GetScore()
      {
         long accumulator = 1;
         long scoreSoFar = 0;
         for( int i = m_Cards.Count - 1; i >= 0; i-- )
         {
            long thisPoint = accumulator * m_Cards[i];
            accumulator++;
            scoreSoFar += thisPoint;
         }
         return scoreSoFar;
      }

      /// <summary>
      /// Returns a string of the cards currently in the hand for indexing in a hash set
      /// </summary>
      /// <returns></returns>
      public string GetUniqueIdentifier()
      {
         string id = "";
         foreach( int card in m_Cards )
         {
            id += card.ToString( );
            id += ",";
         }
         return id;
      }

      /// <summary>
      /// Draws 
      /// </summary>
      /// <returns></returns>
      public int DrawTopCard( )
      {
         int retVal = m_Cards[0];
         m_Cards.RemoveAt( 0 );
         return retVal;
      }

      /// <summary>
      /// Inserts two cards at the back of the dictionary.
      /// </summary>
      /// <param name="cards"></param>
      public void WonCards( List<int> cards )
      {
         foreach( int card in cards )
            m_Cards.Add( card );
      }

   #endregion

   /*STATIC METHODS*/
   #region

      /// <summary>
      /// Plays a round of the card game. Returning a bool indicating if the game should continue or not
      /// </summary>
      /// <param name="p1"></param>
      /// <param name="p2"></param>
      /// <returns></returns>
      public static bool PlayOneRoundPART1( CardGamePlayer p1, CardGamePlayer p2 )
      {

      //Draw the two cards
         int c1 = p1.DrawTopCard( );
         int c2 = p2.DrawTopCard( );

      //Declare the winner and the follower
         int cFollower;
         int cWinner;
         CardGamePlayer winner;
         CardGamePlayer loser;
         if( c1 > c2 )
         {
            cFollower = c2;
            cWinner = c1;
            winner = p1;
            loser = p2;
         }
         else if( c2 > c1 )
         {
            cFollower = c1;
            cWinner = c2;
            winner = p2;
            loser = p1;
         }
         else //Cards cannot be equal.
            throw new Exception( );

      //Add the cards to the winners deck
         winner.WonCards( new List<int>( ) { cWinner, cFollower } );

      //Check if the non winner has any cards left.
         return loser.m_Cards.Count > 0;
      }


      /// <summary>
      /// Play a game of recursives with the crab.
      /// </summary>
      /// <param name="p1"></param>
      /// <param name="p2"></param>
      /// <returns></returns>
      public static CardGamePlayer PlayGameOfRecursives( CardGamePlayer p1, CardGamePlayer p2)
      {
      //Plays a game of recursive combat. Then returns the winner of the game with a list of the cards won.
      //Declare a hash set of unique scores. To prevent an infinite loop
         HashSet<string> p1PrevScore = new HashSet<string>( );
         HashSet<string> p2PrevScore = new HashSet<string>( );

      //Check if the current socre exist in the memo. If so, return player 1 as the champion and return.
         while( true )
         {
         //Check for previous state
            string p1Hash = p1.GetUniqueIdentifier( );
            string p2Hash = p2.GetUniqueIdentifier( );
            if( p1PrevScore.Contains( p1Hash ) )
               return p1;
            if( p2PrevScore.Contains( p2Hash ) )
               return p1;

         //Here, we are sure that the state have not existed before.
            p1PrevScore.Add( p1Hash );
            p2PrevScore.Add( p2Hash );

         //Draw the top card
            int c1 = p1.DrawTopCard( );
            int c2 = p2.DrawTopCard( );
            if( ( p1.m_Cards.Count >= c1 ) && ( p2.m_Cards.Count >= c2 ) )
            {
            //Create a copy of the players cards up to the number drawn.
               List<int> tempP1Cards = new List<int>( );
               List<int> tempP2Cards = new List<int>( );
               for( int i = 0; i < c1; i++ )
                  tempP1Cards.Add( p1.m_Cards[i] );
               for( int i = 0; i < c2; i++ )
                  tempP2Cards.Add( p2.m_Cards[i] );
               CardGamePlayer tempP1 = new CardGamePlayer( p1.m_ID, tempP1Cards );
               CardGamePlayer tempP2 = new CardGamePlayer( p2.m_ID, tempP2Cards );

            //Both players have enough to recurse. Start a new game of recursive combat..
               CardGamePlayer winner = PlayGameOfRecursives( tempP1, tempP2 );

            //Add the cards from who was the winner in the recursive combat.
               if( winner.m_ID == p1.m_ID )
                  p1.WonCards( new List<int>( ){ c1, c2 } );
               else
                  p2.WonCards( new List<int>( ){ c2, c1 } );
            }
            else //One of the players do not have enough cards to play a recursive game. THerefore the winner is the one with the higher value card
            {
               int cFollower;
               int cWinner;
               CardGamePlayer winner;
               CardGamePlayer loser;
               if( c1 > c2 )
               {
                  cFollower = c2;
                  cWinner = c1;
                  winner = p1;
                  loser = p2;
               }
               else if( c2 > c1 )
               {
                  cFollower = c1;
                  cWinner = c2;
                  winner = p2;
                  loser = p1;
               }
               else
                  throw new Exception( );

            //Add the cards to the winners deck
               winner.WonCards( new List<int>( ) { cWinner, cFollower } );
            }

         //CHeck if one of the players have zero cards in hand, if so, return the winner. There is no exit of this loop.
            if( p1.m_Cards.Count == 0 )
               return p2;
            if( p2.m_Cards.Count == 0 )
               return p1;
         }

      }

   #endregion



   }
}
