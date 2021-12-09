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
#include <iterator>
#include <algorithm>
#include <unordered_set>

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
/// <summary>
/// Converts a number from base 10 to a different base. Returns it as a string
/// </summary>
/// <param name="num>The number that should be converted</param>
/// <param name="base">The new base that should be used</param>
/// <returns></returns>
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

/// <summary>
/// Converts a number from a different base to base 10.
/// </summary>
/// <param name="num_str">The number represented as a string</param>
/// <param name="base">The base it should be converted from</param>
/// <returns></returns>
unsigned long GlobalMethods::Utilities::from_base( std::string const& num_str, int base )
{
   unsigned long result = 0;
   for (std::string::size_type pos = 0; pos < num_str.length(); ++pos)
      result = result * base + digits.find(num_str[pos]);

   return result;
}

/// <summary>
/// Split a string by a chosen delimiter. Returns a vector of strings..
/// </summary>
/// <param name="str">The string that is passed</param>
/// <param name="del">The delimiter the string should be pass ed as.</param>
/// <returns></returns>
std::vector<std::string> GlobalMethods::Utilities::split( std::string str, char del )
{
   std::string buf;                 // Have a buffer string
   std::stringstream ss(str);       // Insert the string into a stream
   std::vector<std::string> tokens; // Create vector to hold our words

//Loop over all the strings
   while( std::getline( ss, buf, del ) )
      tokens.push_back( buf );

//Return the tokens for this string.
   return tokens;
}

/// <summary>
/// Splits a string by whitespaces. Regardless of how many spaces there is
/// </summary>
/// <param name=""></param>
/// <returns></returns>
std::vector<std::string> GlobalMethods::Utilities::splitWhitespace( std::string str)
{
   std::string buf;                 // Have a buffer string
   std::stringstream ss( str );       // Insert the string into a stream
   std::vector<std::string> tokens; // Create vector to hold our words

   while( ss >> buf )
      tokens.push_back( buf );

   return tokens;
}

/// <summary>
/// Gets the union of characters in the string
/// </summary>
/// <param name="s1"></param>
/// <param name="s2"></param>
/// <returns></returns>
std::string GlobalMethods::Utilities::get_union_string( std::string s1, std::string s2 )
{
//Get the unordered set
   unordered_set<char> set = Utilities::get_union_set( s1, s2 );
   std::stringstream ss;
   for( const auto& elem : set )
      ss << elem;

//return string
   return ss.str( );
}


/// <summary>
/// Gets the common characters in a string
/// </summary>
/// <param name="s1"></param>
/// <param name="s2"></param>
/// <returns></returns>
std::string GlobalMethods::Utilities::get_common_string( std::string s1, std::string s2 )
{
//Get the unordered set
   unordered_set<char> set = Utilities::get_common_set( s1, s2 );
   std::stringstream ss;
   for( const auto& elem : set )
      ss << elem;

//return string
   return ss.str( );
}


/// <summary>
/// Gets the union of characters between the two strings.
/// </summary>
/// <param name=""></param>
/// <param name=""></param>
/// <returns></returns>
std::unordered_set<char> GlobalMethods::Utilities::get_union_set( std::string s1, std::string s2 )
{
   unordered_set<char> set;
   for( size_t i = 0; i < s1.size( ); i++ )
      if( set.count( s1[i] ) < 1 )
         set.insert( s1[i] );
   for( size_t i = 0; i < s2.size( ); i++ )
      if( set.count( s2[i] ) < 1 )
         set.insert( s2[i] );

//Return completed string
   return set;
}

/// <summary>
/// Gets the common characters between the two strings
/// </summary>
/// <param name="s1"></param>
/// <param name="s2"></param>
/// <returns></returns>
std::unordered_set<char> GlobalMethods::Utilities::get_common_set( std::string s1, std::string s2 )
{
//Create the unordered set of the common characters
   unordered_set<char> set;
   for( size_t i = 0; i < s1.size( ); i++ )
      if( s2.find( s1[i] ) > 0 )
         set.insert( s1[i] );

   return set;
}




