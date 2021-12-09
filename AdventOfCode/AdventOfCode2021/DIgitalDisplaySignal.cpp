#include "DigitalDisplaySignal.h"
#include "Utilities.h";
#include <unordered_set>
#include <unordered_map>

using namespace std;
using namespace GlobalMethods;

DigitalDisplaySignal::DigitalDisplaySignal( string inp )
{
//SPlit by |
   vector<string> split = Utilities::split( inp, '|' );

//Store in member variables
   vector<string> firstPart = Utilities::splitWhitespace( split[0] );
   vector<string> secondPart = Utilities::splitWhitespace( split[1] );

   for( int i = 0; i < firstPart.size( ); i++ )
      m_SignalPatterns.push_back( firstPart[i] );
   for( int i = 0; i < secondPart.size( ); i++ )
      m_DigitOutputValue.push_back( secondPart[i] );


//First, find the one entry that is in 7, but not in 1. Thats the top part. Only if it contains 1 or 7..

   for( int i = 0; i < m_SignalPatterns.size( ); i++ )
      if( m_SignalPatterns[i].size( ) == 2 )
         m_Translator.insert( { 1, m_SignalPatterns[i] } );
      else if( m_SignalPatterns[i].size( ) == 3 )
         m_Translator.insert( { 7, m_SignalPatterns[i] } );
      else if( m_SignalPatterns[i].size( ) == 4 )
         m_Translator.insert( { 4, m_SignalPatterns[i] } );
      else if( m_SignalPatterns[i].size( ) == 7 )
         m_Translator.insert( { 8, m_SignalPatterns[i] } );

//The union between 4 and 7. The string with length 5 that has 7 entries after union with 4 and 7 is number 2.
   string fourSevenUnion = Utilities::get_union_string( m_Translator[4], m_Translator[7] );
   vector<string> candidates5;
   for( size_t i = 0; i < m_SignalPatterns.size( ); i++ )
      if( m_SignalPatterns[i].size( ) == 5 )
         candidates5.push_back( m_SignalPatterns[i] );
   //Check union for the number two, also, remove it from the list.
   for( size_t i = 0; i < candidates5.size( ); i++ )
   {
      if( Utilities::get_union_string( candidates5[i], fourSevenUnion ).size( ) == 7 )
      {
         m_Translator.insert( { 2, candidates5[i] } );
         candidates5.erase( candidates5.begin( ) + i );
      }
   }

//If the union between 7 and the remaining candidates with 5 entries is still 5, it has to be the number 3.
   for( size_t i = 0; i < candidates5.size( ); i++ )
   {
      if( Utilities::get_union_string( candidates5[i], m_Translator[7] ).size( ) == 5 )
      {
         m_Translator.insert( { 3, candidates5[i] } );
         candidates5.erase( candidates5.begin( ) + i );
      }
   }

//The remaining one with 5 entries have to be 5.
   m_Translator.insert( { 5, candidates5[0] } );

//Three candidates left with length 6.
   vector<string> candidates6;
   for( size_t i = 0; i < m_SignalPatterns.size( ); i++ )
      if( m_SignalPatterns[i].size( ) == 6 )
         candidates6.push_back( m_SignalPatterns[i] );


//The union between 1 and 6 should have length 7.
   for( size_t i = 0; i < candidates6.size( ); i++ )
   {
      if( Utilities::get_union_string( candidates6[i], m_Translator[1] ).size( ) == 7 )
      {
         m_Translator.insert( { 6, candidates6[i] } );
         candidates6.erase( candidates6.begin( ) + i );
      }
   }

//The union between 5 and 9 should have length 6
   for( size_t i = 0; i < candidates6.size( ); i++ )
   {
      if( Utilities::get_union_string( candidates6[i], m_Translator[5] ).size( ) == 6 )
      {
         m_Translator.insert( { 9, candidates6[i] } );
         candidates6.erase( candidates6.begin( ) + i );
      }
   }

//The remaining number is 0..
   m_Translator.insert( { 0, candidates6[0] } );
}

DigitalDisplaySignal::~DigitalDisplaySignal( )
{
}

vector<string> DigitalDisplaySignal::GetSignalPatterns( )
{
   return m_SignalPatterns;
}

vector<string> DigitalDisplaySignal::GetDigitOutputValue( )
{
   return m_DigitOutputValue;
}

/// <summary>
/// Uses the translator to return the map
/// </summary>
/// <returns></returns>
int DigitalDisplaySignal::GetOutput( )
{
   stringstream s;
   for( size_t i = 0; i < m_DigitOutputValue.size( ); i++ )
   {
      int ret = GetIntegerFromString( m_DigitOutputValue[i] );
      s << ret;
   }

//parse to integer and return.
   return stoi( s.str( ) );
}

/// <summary>
/// Gets the integer value from the passed string
/// </summary>
/// <param name=""></param>
/// <returns></returns>
int DigitalDisplaySignal::GetIntegerFromString( string s )
{
   for( int i = 0; i < m_Translator.size( ); i++ )
   {
      string comparer = m_Translator[i];
      string commonString = Utilities::get_common_string( s, m_Translator[i] );
      if( commonString.size( ) == s.size( ) && m_Translator[i].size( ) == s.size( ) )
      {
         return i;
      }
   }
//If the code reached this point, it should crash
   throw new exception( );
}
