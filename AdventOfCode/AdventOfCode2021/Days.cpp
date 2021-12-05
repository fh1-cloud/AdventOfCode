#pragma once
#include <fstream>
#include <vector>
#include <sstream>
#include "Days.h"
#include "Utilities.h"
#include "Submarine.h"
#include "BingoBoard.h";
#include "UIntVector2D.h"
using namespace std;
using namespace GlobalMethods;


void Days::Dec06( )
{

//Get the input and parse.
   vector<vector<string>> inp = Utilities::CreateInputVectorStringVector( "Dec05.txt", ' ' );
   //vector<vector<string>> inp = Utilities::CreateInputVectorStringVector( "Temp01.txt", ' ' );
   //vector<vector<string>> inp = Utilities::CreateInputVectorStringVector( "Temp02.txt", ' ' );


   int ans = 0;
   cout << ans;

}

void Days::Dec05( )
{
//Get the string array..
   vector<vector<string>> inp = Utilities::CreateInputVectorStringVector( "Dec05.txt", ' ' );
   //vector<vector<string>> inp = Utilities::CreateInputVectorStringVector( "Temp01.txt", ' ' );
   //vector<vector<string>> inp = Utilities::CreateInputVectorStringVector( "Temp02.txt", ' ' );

//Create vectors of all the lines..
   vector<UIntVector2D*> pLines;
   for( int i = 0; i < inp.size( ); i++ )
   {
   //Unpack parts of string
      string startPointString = inp[i][0];
      string endPointString = inp[i][2];

   //Parse to integers..
      vector<string> startPointSplitString = Utilities::split( startPointString, ',' );
      int startX = stoi( startPointSplitString[0] );
      int startY = stoi( startPointSplitString[1] );
      vector<string> endPointSplitString = Utilities::split( endPointString, ',' );
      int endX = stoi( endPointSplitString[0] );
      int endY = stoi( endPointSplitString[1] );

   //Create the UVectors.
      //if( startX == endX || startY == endY )
      //{
         UIntVector2D* v = new UIntVector2D( startX, startY, endX, endY );
         pLines.push_back( v );
      //}
   }

//Try to go with the canvas method first..
   int dim = 990;
   //int dim = 10;
   vector<vector<int>> canvas;
   for( int i = 0; i < dim; i++ )
   {
      vector<int> thisRow;
      for( int j = 0; j < dim; j++ )
         thisRow.push_back( 0 );
      canvas.push_back( thisRow );
   }

//Fill canvas
   for( int i = 0; i < pLines.size( ); i++ )
      pLines[i]->FillCanvas( canvas );

//Print the canvas. Also count the number of multiple crossings.
   for( int i = 0; i < canvas.size( ); i++ )
   {
      stringstream s;
      for( int j = 0; j < canvas.size( ); j++ )
      {
         if( canvas[i][j] == 0 )
            s << '.';
         else
            s << canvas[i][j];

      }
      cout << s.str( ) + "\n";
   }

//Count multiples
   int multiples = 0;
   for( int i = 0; i < canvas.size( ); i++ )
      for( int j = 0; j < canvas.size( ); j++ )
         if( canvas[i][j] > 1 )
            multiples++;


   cout << "\n";
   cout << multiples;

}



void Days::Dec04 ( )
{
//Get the string array..
   vector<string> inp = Utilities::CreateInputVectorString( "Dec04.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp01.txt" );

//extract the winning sequence..
   vector<string> numberString = Utilities::split( inp[0], ',' );
   vector<int> winningNumbers;
   for( int i = 0; i < numberString.size( ); i++ )
      winningNumbers.push_back( stoi( numberString[i] ) );

//Create bingoboards
   vector<BingoBoard> bingoBoards;
   for( int i = 2; i < inp.size( ); i=i+6 )
   {
   //Extract this row and the 5 next rows.
      vector<string> thisBoard;
      thisBoard.push_back( inp[i] );
      thisBoard.push_back( inp[i+1] );
      thisBoard.push_back( inp[i+2] );
      thisBoard.push_back( inp[i+3] );
      thisBoard.push_back( inp[i+4] );

   //Create the board..
      BingoBoard b( thisBoard );
      bingoBoards.push_back( b );
   }

//Start to draw the winning numbers
   bool bingo = false;
   BingoBoard* pWinningBoard = NULL;
   int winningNumber = -1;
   int currIdx = 0;
   int nOfWinningBoards = 0;
   int nOfBoards = bingoBoards.size( );
   while( true )
   {
      int thisDraw = winningNumbers[currIdx];
      for( int i = 0; i < bingoBoards.size( ); i++ )
      {
      //If this board has already won, continue..
         if( bingoBoards[i].HasWon( ) )
            continue;

         bingoBoards[i].MarkNumber( thisDraw );
         bool thisBingo = bingoBoards[i].CheckForBingo( );

      //Check this for bingo. IF it reached bingo, remove it from the list..
         if( thisBingo )
         {
         //Increment the number of winning boards..
            nOfWinningBoards++;

         //Check if this is the last bingoboard..
            if( nOfWinningBoards == nOfBoards )
            {
               winningNumber = thisDraw;
               pWinningBoard = &bingoBoards[i];
               break;
            }
         }
      }
      if( pWinningBoard != NULL )
      {
         bingo = true;
         break;
      }
   //Increment the current index
      currIdx++;
   }

//Print the bingoboard that just won.
   int boardScore = pWinningBoard->GetScore( );
   int ans = boardScore * winningNumber;
   cout << ans;
}


void Days::Dec03( )
{
//Get the string array..
   vector<string> inp = Utilities::CreateInputVectorString( "Dec03.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp01.txt" );

   int bitLength = inp[0].size( );

//PART1
   ostringstream epsilon;
   ostringstream gamma;

   for( int i = 0; i < bitLength; i++ )
   {
      int n0 = 0;
      int n1 = 0;

      for( int j = 0; j < inp.size( ); j++ )
      {
         if( inp[j][i] == '0' )
            n0++;
         else if( inp[j][i] == '1' )
            n1++;
      }
      if( n0 == n1 )
         throw new exception( );
      if( n0 > n1 )
      {
         gamma << '0';
         epsilon << '1';
      }
      else
      {
         gamma << '1';
         epsilon << '0';
      }
   }

//Convert from binary to regular numbers..
   unsigned long gammaNum = Utilities::from_base( gamma.str( ), 2 );
   unsigned long epsilonNum = Utilities::from_base( epsilon.str( ), 2 );

//Calculate the answer
   //unsigned long ans = gammaNum*epsilonNum;

//Print answer..
   //cout << ans;

//PART2
   //Oxygen generator rating..

   //Create a copy of the list..
   vector<string> oxyCandidates = inp;       //Copies by value?
   for( int i = 0; i < bitLength; i++ )
   {
   //First, check how many numbers are left in the list..
      if( oxyCandidates.size( ) == 1 )
         break;

   //Reset bit counters..
      int n0 = 0;
      int n1 = 0;

      for( int j = 0; j < oxyCandidates.size( ); j++ )
      {
         if( oxyCandidates[j][i] == '0' )
            n0++;
         else if( oxyCandidates[j][i] == '1' )
            n1++;
      }

   //1 larger than 0. Remove all the zeroes.
      vector<string> newCandidates;
      if( n1 >= n0 )
      {
         for( int j = 0; j < oxyCandidates.size( ); j++ )
            if( oxyCandidates[j][i] == '1' )
               newCandidates.push_back( oxyCandidates[j] );
      }
      else
      {
         for( int j = 0; j < oxyCandidates.size( ); j++ )
            if( oxyCandidates[j][i] == '0' )
               newCandidates.push_back( oxyCandidates[j] );
      }

   //Set new oxy candidates..
      oxyCandidates = newCandidates;
   }
   if( oxyCandidates.size( ) != 1 )
      throw new exception( );

//Create a copy of the list..
   vector<string> scrubberCandidates = inp;       //Copies by value?
   for( int i = 0; i < bitLength; i++ )
   {
   //First, check how many numbers are left in the list..
      if( scrubberCandidates.size( ) == 1 )
         break;

   //Reset bit counters..
      int n0 = 0;
      int n1 = 0;

      for( int j = 0; j < scrubberCandidates.size( ); j++ )
      {
         if( scrubberCandidates[j][i] == '0' )
            n0++;
         else if( scrubberCandidates[j][i] == '1' )
            n1++;
      }

   //1 larger than 0. Remove all the zeroes.
      vector<string> newCandidates;
      if( n1 < n0 )
      {
         for( int j = 0; j < scrubberCandidates.size( ); j++ )
            if( scrubberCandidates[j][i] == '1' )
               newCandidates.push_back( scrubberCandidates[j] );
      }
      else
      {
         for( int j = 0; j < scrubberCandidates.size( ); j++ )
            if( scrubberCandidates[j][i] == '0' )
               newCandidates.push_back( scrubberCandidates[j] );
      }

   //Set new oxy candidates..
      scrubberCandidates = newCandidates;
   }
   if( scrubberCandidates.size( ) != 1 )
      throw new exception( );

//Convert from binary to regular numbers..
   unsigned long oxyNum = Utilities::from_base( oxyCandidates[0], 2 );
   unsigned long scrubberNum = Utilities::from_base( scrubberCandidates[0], 2 );
   unsigned long ans = oxyNum*scrubberNum;
   cout << ans;




}

void Days::Dec02( )
{
//Get the string array..
   vector<vector<string>> inp = Utilities::CreateInputVectorStringVector( "Dec02.txt" );

//Declare submarine
   Submarine sub( 0,0,0 );

//Loop over all the strings and carry out instructions..
   for( int i = 0; i < inp.size( ); i++ )
   {
   //Find instruction
      SUBMARINEINSTRUCTION ins;
      if( inp[i][0] == "forward" )
         ins = FORWARD;
      else if( inp[i][0] == "down" )
         ins = DOWN;
      else if( inp[i][0] == "up" )
         ins = UP;
   
   //Find value
      int val = stoi( inp[i][1] );

   //Carry out submarine
      sub.CarryOutInstruction( ins, val );
   }

//Find the position
   vector<int> currentPos = sub.GetPosition( );
   int hos = currentPos[0];
   int dep = currentPos[1];

//Calculate the answer
   int ans = hos*dep;

//Print answer..
   cout << ans;

}


/// <summary>
/// Function definition of Dec01. 
/// </summary>
void Days::Dec01( )
{
   vector<int> inp = Utilities::CreateInputVectorInt( "Dec01.txt" );
   //vector<int> inp = Utilities::CreateInputVectorInt( "Temp01.txt" );

   int nOfIncreases = 0;
   int prevSum = inp[0] + inp[1] + inp[2];
   for( int i = 3; i < inp.size( ); i++ )
   {
      int thisSum = inp[i-2] + inp[i-1] + inp[i];
      if( thisSum > prevSum )
         nOfIncreases++;

      prevSum = thisSum;
   }

//Print answer..
   cout << nOfIncreases;

}

