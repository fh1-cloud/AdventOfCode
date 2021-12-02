#pragma once
#include <iostream>
#include <fstream>
#include <vector>
#include "Days.h"
#include "Utilities.h"
#include "Submarine.h"
using namespace std;
using namespace GlobalMethods;



void Days::Dec03( )
{
//Get the string array..
   vector<vector<string>> inp = Utilities::CreateInputVectorStringVector( "Dec03.txt" );



//Calculate the answer
   int ans = 0;

//Print answer..
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

