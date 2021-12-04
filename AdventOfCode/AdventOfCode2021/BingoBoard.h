#pragma once
#include <utility>
#include <string>
#include <vector>

using namespace std;

class BingoBoard
{
   /*MEMBERS*/
protected:
   static const int m_Dimension = 5;

   pair<int, bool> m_Values[m_Dimension][m_Dimension];     //The board. Marked if the number was drawn
   bool m_BingoRows[m_Dimension];                //The rows that have previously been marked as bingo. This is to make sure we can continue after it has gotten one bingo
   bool m_BingoCols[m_Dimension];                //The columns that have previously been marked as bingo.
   bool m_HasWon;

   /*CONSTRUCTORS*/
public:
   BingoBoard( vector<string> );
   ~BingoBoard( );


public:
   void MarkNumber( int );
   bool CheckForBingo( );
   void PrintBoard( );
   int GetScore( );
   bool HasWon( );

};
