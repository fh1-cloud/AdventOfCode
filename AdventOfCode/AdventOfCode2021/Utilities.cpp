#pragma once
#include <string>
#include <fstream>
#include <sstream>
#include <vector>
#include <iostream>
#include "Utilities.h"
#include <cstdlib>
#include <algorithm>
#include <cassert>
using namespace std;

/// <summary>
/// Creates a header for this years advent of code
/// </summary>
/// <param name="day"></param>
void GlobalMethods::Utilities::CreateHeader( int const& day )
{
   string dayString = to_string( day );

   cout << "*****************************************************************\n";
   cout << "*                                                               *\n";
   cout << "*                    ADVENT OF CODE 2021                        *\n";
   cout << "*                        December " + ( dayString.length( ) == 1 ? dayString + " " : dayString ) + "                            *\n";
   cout << "*                                                               *\n";
   cout << "*                                                               *\n";
   cout << "*****************************************************************\n";
   cout << "\n";
}


/// <summary>
/// Creates a vector containing std::strings for the input.
/// </summary>
/// <param name="filePath"></param>
/// <returns></returns>
vector<string> GlobalMethods::Utilities::CreateInputVectorString( std::string const& filePath )
{
//Try to open the file..
   std::ifstream inFile;
   inFile.open( filePath ); //open the input file

//Declare the returning vector
   vector<string> returnVec;

//declare the local dummy string that is replaced on each iteration
   string x;
   while( std::getline( inFile, x ) )
   {
      returnVec.push_back( x );
   }
   return returnVec;
}


/// <summary>
/// Creates an input vector containing integers
/// </summary>
/// <param name="filePath"></param>
/// <returns></returns>
std::vector<int> GlobalMethods::Utilities::CreateInputVectorInt( std::string const& filePath )
{
//Try to open the file..
   std::ifstream inFile;
   inFile.open( filePath ); //open the input file
   vector<int> returnVec;

//declare the local dummy string that is replaced on each iteration
   string thisLine;

//Loop over all lines and set to this line
   while( std::getline( inFile, thisLine ) )
      returnVec.push_back( stoi( thisLine ) );

   return returnVec;
}


/// <summary>
/// Creates a list of strings split by a delimiter for each line.
/// </summary>
/// <param name="filePath"></param>
/// <param name="del"></param>
/// <returns></returns>
std::vector<std::vector<std::string>> GlobalMethods::Utilities::CreateInputVectorStringVector( std::string const& filePath, char del )
{
//Try to open the file..
   std::ifstream inFile;
   inFile.open( filePath ); //open the input file
   vector<vector<string>> returnVec;

//declare the local dummy string that is replaced on each iteration
   string thisLine;

//Loop over all lines and set to this line
   while( getline( inFile, thisLine ) )
   {
   //Declare this vector
      vector<string> thisVec;

   //Split this line by the seperator
      int start = 0;
      int end = thisLine.find( del );
      if( end == -1 )
         thisVec.push_back( thisLine );
      else
      {
         while( end != -1 )
         {
         //Create a substring from start to end..
            string ss = thisLine.substr( start, end - start );
            thisVec.push_back( ss );

         //Set new starting index..
            start = end + 1;
            end = thisLine.find( del, start );
         
         //Add last part of line after no new delimiters was found.
            if( end == -1 && start != thisLine.length( ) - 2 )
               thisVec.push_back( thisLine.substr( start, thisLine.length( ) - 1 ) );
         }
      }

   //Add this string list to the vector.
      returnVec.push_back( thisVec );
   }

//Return complete vector of tokenized words..
   return returnVec;

}

std::string const digits = "0123456789abcdefghijklmnopqrstuvwxyz";
std::string GlobalMethods::Utilities::to_base( unsigned long num, int base )
{
   if (num == 0)
      return "0";

   std::string result;
   while (num > 0) 
   {
      std::ldiv_t temp = std::div(num, (long)base);
      result += digits[temp.rem];
      num = temp.quot;
   }
   std::reverse(result.begin(), result.end());
   return result;
}

unsigned long GlobalMethods::Utilities::from_base( std::string const& num_str, int base )
{
   unsigned long result = 0;
   for (std::string::size_type pos = 0; pos < num_str.length(); ++pos)
      result = result * base + digits.find(num_str[pos]);

   return result;
}
 
