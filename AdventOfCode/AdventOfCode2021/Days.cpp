#pragma once
#include <iostream>
#include <fstream>
#include <vector>
#include "Days.h"
#include "Utilities.h"
using namespace std;
using namespace GlobalMethods;



void Days::Dec02( )
{
   vector<int> inp = Utilities::CreateInputVectorInt( "Dec02.txt" );
   //vector<int> inp = Utilities::CreateInputVectorInt( "Temp01.txt" );


//Print answer..
   cout << 0;

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

