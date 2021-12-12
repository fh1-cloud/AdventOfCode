#pragma once
#include <fstream>
#include <vector>
#include <iostream>
#include <algorithm>
#include <cmath>
#include <unordered_map>
#include <stack>
#include "Days.h"
#include "Utilities.h"
#include "Submarine.h"
#include "BingoBoard.h";
#include "UIntVector2D.h"
#include "DIgitalDisplaySignal.h"
#include "ThermalVentMap.h"
#include "ThermalVentMapBasin.h"
#include "DumboOctopus.h"
#include "Cave.h"

using namespace std;
using namespace GlobalMethods;


void Days::Dec13( )
{
//Get the input and parse.
   vector<string> inp = Utilities::CreateInputVectorString( "Dec13.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp01.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp02.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp03.txt" );


}


void Days::Dec12( )
{
//Get the input and parse.
   vector<string> inp = Utilities::CreateInputVectorString( "Dec12.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp01.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp02.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp03.txt" );

   unordered_set<string> caveString;
   for( size_t i = 0; i < inp.size( ); i++ )
   {
   //Split line by -
      vector<string> spl = Utilities::split( inp[i], '-' );

   //Add if they arent in the list of string..
      for( size_t j = 0; j < 2; j++ )
         if( caveString.find( spl[j] ) == caveString.end( ) )
            caveString.insert( spl[j] );
   }

//Create the caves as objects..
   unordered_map<string, Cave*> caves;
   Cave* pStart = NULL;
   for( string s : caveString )
   {
      Cave* pC = new Cave( s );
      caves.insert( { s, pC } );
      if( s == "start" )
         pStart = pC;
   }

//Create the connections
   for( string s : inp )
   {
      vector<string> spl = Utilities::split( s, '-' );
      caves[spl[0]]->AddConnection( caves[spl[1]] );
      caves[spl[1]]->AddConnection( caves[spl[0]] );
   }

   int* pCounter = new int( 0 );
   unordered_set<Cave*> visited;
   bool p2 = true;
   Cave* pVIsitedTwice = nullptr;

   Cave::CountPaths( pStart, visited, pVIsitedTwice, pCounter, p2 );

//Start traversing from the cave called start.
   cout << ( *pCounter ) << endl;

//Delete the caves..
   delete pCounter;
   caves.clear( );

}



void Days::Dec11( )
{
//Get the input and parse.
   vector<string> inp = Utilities::CreateInputVectorString( "Dec11.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp01.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp02.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp03.txt" );

//Create all the octopi..
   unordered_map<int, unordered_map<int, DumboOctopus*>> octopi;
   for( size_t i = 0; i < inp.size( ); i++ )
   {
      unordered_map<int, DumboOctopus*> rowMap;
      for( size_t j = 0; j < inp[i].size( ); j++ )
      {
         int val = stoi( string(1, inp[i][j]) );
         DumboOctopus* c = new DumboOctopus( i, j, val );
         rowMap.insert( { j, c } );
      }
      octopi.insert( { i, rowMap } );
   }

//Create the number of flashes..
   uint64_t* nOfFlashes = new uint64_t( 0 );

//Declare the number of flashes this turn..
   uint64_t* nOfFlashesThisTurn = new uint64_t( 0 );

//Declare the sync time
   int syncTime = -1;

//Print state before..
   cout << "Initial state:\n";
   DumboOctopus::PrintState( octopi );

   int nSteps = 1000;
   for( int i = 0; i < nSteps; i++ )
   {
   //Declare the vector of all the octopi that should flash..
      vector<DumboOctopus*>* shouldFlashList = new vector<DumboOctopus*>( );

   //Reset counter for number of flashes this turn
      ( *nOfFlashesThisTurn ) = 0;

   //Reset all the octopi and increment..
      for( size_t i = 0; i < octopi.size( ); i++ )
      {
         for( size_t j = 0; j < octopi[i].size( ); j++ )
         {
         //Find this octopus
            DumboOctopus* thisOctopus = octopi[i][j];

         //Reset status
            thisOctopus->Reset( );

         //Increment.
            bool thisShouldFlash = thisOctopus->Increment( );

         //If this went over, it should flash. Collect it in the list.
            if( thisShouldFlash )
               shouldFlashList -> push_back( thisOctopus );
         }
      }

   //Start by flashing the octopi..
      while( shouldFlashList -> size( ) > 0 )
      {
         (*shouldFlashList)[0]->Flash( shouldFlashList, octopi, nOfFlashes, nOfFlashesThisTurn );
         ( *nOfFlashes )++;
         ( *nOfFlashesThisTurn )++;
      }

   //Print status after increment..
      cout << "Number of flashes:" << ( *nOfFlashes ) << endl;
      cout << "State after " << i+1 << +" steps:" << endl;
      DumboOctopus::PrintState( octopi );

   //Clean up the list allocated on the heap.
      delete shouldFlashList;

   //Break if number of flashes this turn is equal to 100;
      if( ( *nOfFlashesThisTurn ) == 100 )
      {
         syncTime = i+1;
         break;
      }
   }

//Print answer
   cout << "Time when they all sync: " << syncTime;

//Clean up all the octopi allocated on the heap..
   delete nOfFlashesThisTurn;
   delete nOfFlashes;
   for( size_t i = 0; i < octopi.size( ); i++ )
      for( size_t j = 0; j < octopi[i].size( ); j++ )
         delete octopi[i][j];
}





void Days::Dec10( )
{
//Get the input and parse.
   vector<string> inp = Utilities::CreateInputVectorString( "Dec10.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp01.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp02.txt" );

//DEclare a map over the matchers that have to line up.
   unordered_map<char, char> matcherp1( { { '}', '{' }, { ')', '(' }, { ']', '[' }, { '>', '<' } } );
   unordered_map<char, char> matcherp2( { { '{', '}' }, { '(', ')' }, { '[', ']' }, { '<', '>' } } );
   //unordered_map<char, int> pointMapp1( { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } } );
   unordered_map<char, int> pointMapp2( { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } } );

//Initialize vector of scores for p2
   vector<uint64_t> scores;
   for( size_t i = 0; i < inp.size( ); i++ )
   {
   //Create a stack for this line. Insert starting elements in to the stack, and when a closing character occurs it has to be the last one inserted in the stack.
      stack<char> stack;
      bool isCorrupted = false;
      for( size_t j = 0; j < inp[i].size( ); j++ )
      {
      //Unpack this character
         char thisChar = inp[i][j];

      //If this character is a starting character, add it to the stack as last inserted
         if( thisChar == '(' || thisChar == '[' || thisChar == '{' || thisChar == '<' )
            stack.push( thisChar );
         else //The character is a closing character. Check if it matches the one previously entered and not popped. If it does not match the opposite, it cannot be a correctly parsed string
         {
            char p = stack.top( );
            stack.pop( );

         //The removed entry has to be a match with the last one pushed. If it does not match, the p is the corrupted value.
            if( p != matcherp1[thisChar] )
               isCorrupted = true;
         }
      //If it is corrupted, no need to continue parsing this string
         if( isCorrupted )
         {
         //Calculate points for part 1 here.
            break;
         }

      } //End line read

   //If the line is not corrupted so far, we have accumulated the characters that didnt have its pair removed in the stack.
   //loop through the stack and complete and complete with the opposite pair of what is in the stack.
      if( !isCorrupted )
      {
      //Initialize the score for this line.
         uint64_t thisScore = 0;

      //Calculate the score for the remaining entries in the list.
         while( stack.size( ) > 0 )
         {
         //Find the character that is missing.
            char opposite = matcherp2[stack.top( )]; 

         //Calculate the score for adding this missing character
            thisScore = thisScore * 5 + pointMapp2[opposite];

         //Remove this entry from the stack.
            stack.pop( );
         }

      //Add to the vector of scores..
         scores.push_back( thisScore );
      }
   }

//Sort the score vector..
   sort( scores.begin( ), scores.end( ) );

//Get the middle score..
   uint64_t middleScore = scores[( int ) ( scores.size( ) / 2 ) ];
   cout << middleScore << endl;

}



void Days::Dec09( )
{
//Get the input and parse.
   vector<string> inp = Utilities::CreateInputVectorString( "Dec09.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp01.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp02.txt" );

   ThermalVentMap map( inp );
   vector<UIntPoint> p = map.GetLowPoints( );
   vector<ThermalVentMapBasin> basins;
   for( size_t i = 0; i < p.size( ); i++ )
   {
      ThermalVentMapBasin bas( map, p[i].Y( ), p[i].X( ) );
      basins.push_back( bas );
   }

//Find the three larges basins.
   vector<ThermalVentMapBasin> threeLargest;
   for( int i = 0; i < 3; i++ )
   {
      int maxSize = 0;
      ThermalVentMapBasin* pLarge = NULL;
      int maxSizeIdx = 0;
      for( size_t j = 0; j < basins.size( ); j++ )
      {
         int thisSize = basins[j].GetSize( );

         if( maxSize < thisSize ) 
         {
            maxSize = thisSize;
            pLarge = &basins[j];
            maxSizeIdx = j;
         }
      }
      threeLargest.push_back( *pLarge );
      basins.erase( basins.begin( ) + maxSizeIdx );
   }

//Calculate the product of the sizes..
   uint64_t ans = threeLargest[0].GetSize( ) * threeLargest[1].GetSize( ) * threeLargest[2].GetSize( );
   cout << ans;
}


void Days::Dec08( )
{

//Get the input and parse.
   vector<string> inp = Utilities::CreateInputVectorString( "Dec08.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp01.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp02.txt" );

//Parse into two different strings per line..
   vector<DigitalDisplaySignal> signals;

   for( int i = 0; i < inp.size( ); i++ )
      signals.push_back( DigitalDisplaySignal( inp[i] ) );

//Count the number of 1,4,7,8
   int count = 0;
   for( size_t i = 0; i < signals.size( ); i++ )
   {
      vector<string> digitOutputValue = signals[i].GetDigitOutputValue( );
      for( size_t j = 0; j < digitOutputValue.size( ); j++ )
      {
         if( digitOutputValue[j].size( ) == 2 || digitOutputValue[j].size( ) == 4 || digitOutputValue[j].size( ) == 3 || digitOutputValue[j].size( ) == 7 )
            count++;
      }
   }

//PART 2
   int64_t sum = 0;
   vector<int> ans;
   for( size_t i = 0; i < signals.size( ); i++ )
   {
      ans.push_back( signals[i].GetOutput( ) );
      sum += signals[i].GetOutput( );
   }
   cout << sum;

}



void Days::Dec07( )
{

//Get the input and parse.
   vector<string> inp = Utilities::CreateInputVectorString( "Dec07.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp01.txt" );

//Parse the input string fish
   vector<string> split = Utilities::split( inp[0], ',' );
   vector<int> positions;

//Create the vector ofr starting fish
   for( int i = 0; i < split.size( ); i++ )
      positions.push_back( stoi( split[i] ) );


   vector<int> sortedPositions = positions;
   sort( sortedPositions.begin( ), sortedPositions.end( ) );

//Calculate median
   double median = -1;
   if( sortedPositions.size( ) % 2 == 1 )
      median = sortedPositions[sortedPositions.size( ) / 2];
   else
      median = (sortedPositions[(int)(sortedPositions.size( )/2)-1]+sortedPositions[(int)(sortedPositions.size( )/2)] )/2;

//Calculate fuel
   long fuel = 0;
   for( int i = 0; i < positions.size( ); i++ )
      fuel += abs( (positions[i]-median ) );

   //cout << fuel;

//Calculate mean. (move all crabs a small amount because fuel increases exponentially
   long sum = 0;
   for( int i = 0; i < positions.size( ); i++ )
      sum += positions[i];
   double mean = ( double ) ( ( double ) sum / ( double ) positions.size( ) );

//Dont know why it needs to be floored. Depends on the number of values below or above.
   mean = (int)(mean);

//Loop over all the positions and calculate fueld to the mean..
   long fuel2 = 0;
   for( int i = 0; i < positions.size( ); i++ )
   {
      double dist = abs( positions[i]-mean);
      fuel2 += dist*(dist+1.0)/2.0;
   }

   cout << fuel2;



}



void Days::Dec06( )
{
//Get the input and parse.
   vector<string> inp = Utilities::CreateInputVectorString( "Dec06.txt" );
   //vector<string> inp = Utilities::CreateInputVectorString( "Temp01.txt" );

//Parse the input string fish
   vector<string> split = Utilities::split( inp[0], ',' );
   vector<int> startingFish;

//Create the vector ofr starting fish
   for( int i = 0; i < split.size( ); i++ )
      startingFish.push_back( stoi( split[i] ) );

//Initialize the reproducation table. This is a table that tracks days until as the key, and the number of fish as the value
   unordered_map<long,int64_t> fishes;
   for( int j = 0; j < 9; j++ )
   {
      int dayCount = 0;
      for( int i = 0; i < startingFish.size( ); i++ )
         if( startingFish[i] == j )
            dayCount++;
      fishes.insert( { j, dayCount } );
   }

//Declare the number of days to simulate
   int days = 256;
   for( int i = 0; i < days; i++ )
   {
      int64_t nOfNewFish = fishes[0];
      fishes[0] = fishes[1];
      fishes[1] = fishes[2];
      fishes[2] = fishes[3];
      fishes[3] = fishes[4];
      fishes[4] = fishes[5];
      fishes[5] = fishes[6];
      fishes[6] = fishes[7] + nOfNewFish;
      fishes[7] = fishes[8];
      fishes[8] = nOfNewFish;
   }

//Sum up total number of fishes
   int64_t sum = 0;
   for( int i = 0; i < 9; i++ )
      sum += fishes[i];
   cout << sum;

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

