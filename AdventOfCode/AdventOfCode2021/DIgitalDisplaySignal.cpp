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
   unordered_map<int, string> stringMap; //A map of the strings with corresponding number.

   for( int i = 0; i < m_SignalPatterns.size( ); i++ )
      if( m_SignalPatterns[i].size( ) == 2 )
         stringMap.insert( { 1, m_SignalPatterns[i] } );
      else if( m_SignalPatterns[i].size( ) == 3 )
         stringMap.insert( { 7, m_SignalPatterns[i] } );
      else if( m_SignalPatterns[i].size( ) == 4 )
         stringMap.insert( { 4, m_SignalPatterns[i] } );
      else if( m_SignalPatterns[i].size( ) == 7 )
         stringMap.insert( { 8, m_SignalPatterns[i] } );

//The union between 4 and 7. The string with length 5 that has 7 entries after union with 4 and 7 is number 2.
   string fourSevenUnion = Utilities::get_union_string( stringMap[4], stringMap[7] );
   vector<string> candidates5;
   for( size_t i = 0; i < m_SignalPatterns.size( ); i++ )
      if( m_SignalPatterns[i].size( ) == 5 )
         candidates5.push_back( m_SignalPatterns[i] );
   //Check union for the number two, also, remove it from the list.
   for( size_t i = 0; i < candidates5.size( ); i++ )
   {
      if( Utilities::get_union_string( candidates5[i], fourSevenUnion ).size( ) == 7 )
      {
         stringMap.insert( { 2, candidates5[i] } );
         candidates5.erase( candidates5.begin( ) + i );
      }
   }

//If the union between 7 and the remaining candidates with 5 entries is still 5, it has to be the number 3.
   for( size_t i = 0; i < candidates5.size( ); i++ )
   {
      if( Utilities::get_union_string( candidates5[i], stringMap[7] ).size( ) == 5 )
      {
         stringMap.insert( { 3, candidates5[i] } );
         candidates5.erase( candidates5.begin( ) + i );
      }
   }

//The remaining one with 5 entries have to be 5.
   stringMap.insert( { 5, candidates5[0] } );

//Three candidates left with length 6.
   vector<string> candidates6;
   for( size_t i = 0; i < m_SignalPatterns.size( ); i++ )
      if( m_SignalPatterns[i].size( ) == 6 )
         candidates6.push_back( m_SignalPatterns[i] );


//The union between 1 and 6 should have length 7.
   for( size_t i = 0; i < candidates6.size( ); i++ )
   {
      if( Utilities::get_union_string( candidates6[i], stringMap[1] ).size( ) == 7 )
      {
         stringMap.insert( { 6, candidates6[i] } );
         candidates6.erase( candidates6.begin( ) + i );
      }
   }

//The union between 5 and 9 should have length 6
   for( size_t i = 0; i < candidates6.size( ); i++ )
   {
      if( Utilities::get_union_string( candidates6[i], stringMap[5] ).size( ) == 6 )
      {
         stringMap.insert( { 9, candidates6[i] } );
         candidates6.erase( candidates6.begin( ) + i );
      }
   }

//The remaining number is 0..
   stringMap.insert( { 0, candidates6[0] } );


   double test1 = 0.0;

//Deduce whats what.. indexes
   /*
   *     0
   *   1   2
   *     3
   *   4   5
   *     6
   */ 








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

int DigitalDisplaySignal::GetOutput( )
{
   return 0;


}
