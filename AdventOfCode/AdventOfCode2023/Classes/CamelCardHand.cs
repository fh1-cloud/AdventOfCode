using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdventOfCode2023.Classes
{
   public class CamelCardHand : IComparable<CamelCardHand>
   {

   /*ENUMS*/
   #region
      public enum HANDTYPE
      {
         HIGHCARD = 0,
         ONEPAIR = 1,
         TWOPAIR = 2,
         THREEOFAKIND = 3,
         FULLHOUSE = 4,
         FOUROFAKIND = 5,
         FIVEOFAKIND = 6
      }
   #endregion

   /*MEMBERS*/
   #region
      protected List<CamelCard> m_Cards = new List<CamelCard>( );
   #endregion

   /*CONSTRUCTORS*/
   #region
      public CamelCardHand( string line, bool p1 = true )
      {
         string[] ls = line.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
         this.Bid = long.Parse( ls[1] );
         for( int i = 0; i<ls[0].Length; i++ )
            m_Cards.Add( new CamelCard( ls[0][i] ) );
         if( p1 )
            this.HandType = GetHandtypeP1( this );
         else
            this.HandType = GetHandtypeP2( this );
      }
   #endregion

   /*PROPERTIES*/
   #region
      public long Bid { get; protected set; }
      public HANDTYPE HandType { get; protected set; }
   #endregion

   /*OPERATORS*/
   #region

      public CamelCard this[ int x ] { get { return m_Cards[ x ]; } }
      public static bool operator > ( CamelCardHand h1, CamelCardHand h2 )
      {
         if( h1.HandType > h2.HandType )
            return true;
         else if( h1.HandType == h2.HandType )
         {
            for( int i = 0; i < 5; i++ ) 
            {
               if( h1[i] == h2[i] )
                  continue;
               else if( h1[i] > h2[i] )
                  return true;
               else
                  return false;
            }
            return false;
         }
         else 
            return false;
      }

      public static bool operator >= ( CamelCardHand h1, CamelCardHand h2 )
      {
         if( h1.HandType > h2.HandType )
            return true;
         else if( h1.HandType == h2.HandType )
         {
            for( int i = 0; i < 5; i++ ) 
            {
               if( h1[i] == h2[i] )
                  continue;
               else if( h1[i] > h2[i] )
                  return true;
               else
                  return false;
            }
            return true;
         }
         else 
            return false;
      }

      public static bool operator < ( CamelCardHand h1, CamelCardHand h2 )
      {
         if( h1.HandType < h2.HandType )
            return true;
         else if( h1.HandType == h2.HandType ) 
         {
            for( int i = 0; i < 5; i++ ) 
            {
               if( h1[i] == h2[i] )
                  continue;
               else if( h1[i] < h2[i] )
                  return true;
               else
                  return false;
            }
            return false;
         }
         else 
            return false;
      }

      public static bool operator <= ( CamelCardHand h1, CamelCardHand h2 )
      {
         if( h1.HandType < h2.HandType )
            return true;
         else if( h1.HandType == h2.HandType ) 
         {
            for( int i = 0; i < 5; i++ ) 
            {
               if( h1[i] == h2[i] )
                  continue;
               else if( h1[i] < h2[i] )
                  return true;
               else
                  return false;
            }
            return true;
         }
         else 
            return false;
      }

      public static bool operator == ( CamelCardHand h1, CamelCardHand h2 )
      {
         if( h1.HandType != h2.HandType )
            return false;
         else 
         {
            for( int i = 0; i<5; i++ )
            {
               if( h1[i] == h2[i] )
                  continue;
               else
                  return false;
            }
            return true;
         }
      }

      public static bool operator != ( CamelCardHand h1, CamelCardHand h2 ) { return ! ( h1 == h2 ); }

   #endregion

   /*STATIC METHODS*/
   #region

      protected static HANDTYPE GetTypeFromUniqueOccurences( Dictionary<char,int> uniqueCards, int? nOfJokers = null )
      {
         if( nOfJokers == null ) //Part1 or no joker
         {
            long largestOccurence = uniqueCards.Select( x => x.Value ).Max( );
            if( uniqueCards.Count == 1 )
               return HANDTYPE.FIVEOFAKIND;
            else if( uniqueCards.Count == 2 ) 
            {
               if( largestOccurence == 4 )
                  return HANDTYPE.FOUROFAKIND;
               else
                  return HANDTYPE.FULLHOUSE;
            }
            else if ( uniqueCards.Count == 3 )
            {
               if( largestOccurence == 2 )
                  return HANDTYPE.TWOPAIR;
               else
                  return HANDTYPE.THREEOFAKIND;
            }
            else if( uniqueCards.Count == 4 )
               return HANDTYPE.ONEPAIR;
            else if( uniqueCards.Count == 5 )
               return HANDTYPE.HIGHCARD;
         }
         else //At least one joker. (part 2)
         {
            if( uniqueCards.Count == 0 || uniqueCards.Count == 1 )
               return HANDTYPE.FIVEOFAKIND;
            else if( uniqueCards.Count == 2 )
            {
               if( nOfJokers >= 2 )
                  return HANDTYPE.FOUROFAKIND;
               else 
               {
                  if( uniqueCards.FirstOrDefault( ).Value == 2 )
                     return HANDTYPE.FULLHOUSE;
                  else
                     return HANDTYPE.FOUROFAKIND;
               }
            }
            else if( uniqueCards.Count == 3 )
               return HANDTYPE.THREEOFAKIND;
            else 
               return HANDTYPE.ONEPAIR;
         }

      //Cannot reach here.
         throw new Exception( );
      }

      public static long GetTotalWinnings( List<CamelCardHand> allHandUnordered )
      {
         SortedSet<CamelCardHand> ss = new SortedSet<CamelCardHand>( );
         foreach( CamelCardHand h in allHandUnordered )
            ss.Add( h );

      //Cdlculate total winnings.
         long totalWinnings = 0;
         int rank = 0;
         foreach( CamelCardHand h in ss )
         {
            rank++;
            long thisTotalWinnings = rank*h.Bid;
            Console.WriteLine( h.ToString( ) );
            totalWinnings += thisTotalWinnings;
         }
         return totalWinnings;
      }

      protected static HANDTYPE GetHandtypeP1( CamelCardHand hand )
      {
         Dictionary<char,int> cards = new Dictionary<char, int>( );
         for( int i = 0; i<5; i++ )
            if( !cards.ContainsKey( hand[i].C ) )
               cards.Add( hand[i].C, 1 );
            else
               cards[hand[i].C]++;
         return GetTypeFromUniqueOccurences( cards );
      }

      protected static HANDTYPE GetHandtypeP2( CamelCardHand hand )
      {
         Dictionary<char,int> cards = new Dictionary<char, int>( );
         int? nOfJokers = null;
         for( int i = 0; i<5; i++ )
         {
            if( hand[i].C == 'J' )
            {
               if( nOfJokers == null )
                  nOfJokers = 1;
               else
                  nOfJokers++;
               continue;
            }
            else if( !cards.ContainsKey( hand[i].C ) )
               cards.Add( hand[i].C, 1 );
            else
               cards[hand[i].C]++;
         }
         return GetTypeFromUniqueOccurences( cards, nOfJokers );
      }
   #endregion

   /*METHODS*/
   #region
      public override string ToString( )
      {
         StringBuilder sb = new StringBuilder( );
         for( int i = 0; i < 5; i++ )
            sb.Append( m_Cards[i].ToString( ) );
         sb.Append( " " + this.Bid );
         return sb.ToString( );
      }
   #endregion

   /*INTERFACE IMPLEMENTATIONS*/
   #region

      public int CompareTo( CamelCardHand other ) 
      {
         if( this < other )
            return -1;
         else if( this == other )
            return 0;
         else
            return 1;
      }
      #endregion

   }
}
