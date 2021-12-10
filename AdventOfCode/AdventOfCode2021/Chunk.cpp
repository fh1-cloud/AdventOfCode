#include "Chunk.h"
#include <unordered_map>
#include <iostream>

Chunk::Chunk( string expression )
{

//CHeck if an end of this chunk can be found.
   m_EnclosingChar = expression[0];
   int endOfThisIdx = 0;
   bool endOfThis = FindEndOfChunk( expression, endOfThisIdx );

//Validate inside. If it isnt valid, this end has to be the erronous one..
   char unbalanced = '0';
   m_IsValid = CheckForValidity( expression.substr( 0, endOfThisIdx + 1 ), unbalanced );

   if( !m_IsValid )
   {
      string s( 1, GetOpposite( m_EnclosingChar ) );
      cout << "Chunk is invalid. Got " + s;
      return;
   }

   if( endOfThis )
   {
   //Set the length of this chunk
      m_Length = endOfThisIdx + 1;

   //Find inner chunks..
      int endIdx = 0;
      bool endOfChunk = true;
      int currentStartIdx = 1;

   //Return if there are no inner chuncks
      if( endOfThisIdx <= 1 )
         return;
   //There can be multiple inner chunks. Split them up.
      while( endOfChunk )
      {
         bool foundEnd = FindEndOfChunk( expression.substr( currentStartIdx, expression.size( ) - 2 ), endIdx );
         int stringLength = -1;
         if( foundEnd )
         {
            int stringLength = endIdx - currentStartIdx + 2;
            Chunk* c = new Chunk( expression.substr( currentStartIdx, stringLength ) );
            m_InnerChunks.push_back( c );
            currentStartIdx = endIdx + 1;
         }

         if( endIdx == -1 || !foundEnd || stringLength == 2 )
            break;

      }

   }


}

/// <summary>
/// Default destructor
/// </summary>
Chunk::~Chunk( )
{
   for( int i = 0; i < m_InnerChunks.size( ); i++ )
      delete m_InnerChunks[i];
}


/// <summary>
/// Find the end of the chunk. If it could not locate the end, it returns false
/// </summary>
/// <param name="expression"></param>
/// <param name="endIdx"></param>
/// <returns></returns>
bool Chunk::FindEndOfChunk( string expression, int& endIdx )
{
//StartingChar
   char s = expression[0];
   char e = GetOpposite( s );

   int startCount = 1;
   endIdx = -1;
   for( int i = 1; i < expression.size( ); i++ )
   {
   //Add 1 to the indexer if we found a new starting character
      if( expression[i] == s )
         startCount++;
      else if( expression[i] == e ) //Subtract if we found and opposite enclosing character
         startCount--;

   //CHeck if we reached the end of the chunk.
      if( startCount == 0 )
      {
         endIdx = i;
         return true;
      }
   }

//Return false if the code reached this point, it did not find an end of the string.
   return false;
}


/// <summary>
/// Gets the opposite chunk enclosing character if the input character c.
/// </summary>
/// <param name="c"></param>
/// <returns></returns>
char Chunk::GetOpposite( char c )
{
   if( c == '(' )
      return ')';
   else if( c == '[' )
      return ']';
   else if( c == '{' )
      return '}';
   else if( c == '<' )
      return '>';
   else if( c == ')' )
      return '(';
   else if( c == ']' )
      return '[';
   else if( c == '}' )
      return '{';
   else if( c == '>' )
      return '<';
   
//If the code reached this point, something went wrong.
   throw new exception( );
}

/// <summary>
/// Gets the length of this chunk
/// </summary>
/// <returns></returns>
int Chunk::GetLength( )
{
   return m_Length;
}

/// <summary>
/// Checks this and all the inner chunks for validity
/// </summary>
/// <returns></returns>
bool Chunk::IsValid( )
{
   bool validity = true;
   for( int i = 0; i < m_InnerChunks.size( ); i++ )
      validity &= m_InnerChunks[i]->m_IsValid;

   return validity;
}


/// <summary>
/// Gets a bool indicating wheter or not this expression is valid. For this to be valid, the parantheses have to be balanced..
/// </summary>
/// <param name="str"></param>
/// <returns></returns>
bool Chunk::CheckForValidity( string str, char& unbalanced )
{
//Create the map of parantheses
   unordered_map<char, int> map;
   map.insert( { '(', 0 } );
   map.insert( { ')', 0 } );
   map.insert( { '[', 0 } );
   map.insert( { ']', 0 } );
   map.insert( { '{', 0 } );
   map.insert( { '}', 0 } );
   map.insert( { '<', 0 } );
   map.insert( { '>', 0 } );

   for( int i = 0; i < str.size( ); i++ )
      map[str[i]] = map[str[i]] + 1;

   bool isValid = true;
   isValid &= ( map['{'] == map['}'] );
   isValid &= ( map['('] == map[')'] );
   isValid &= ( map['['] == map[']'] );
   isValid &= ( map['<'] == map['>'] );
   return isValid;
}

//Tries to validate this string backwards..
bool Chunk::ValidateBackwards( string str )
{

   char startChar = str[str.size( ) - 1];

   for( int i = str.size( ) - 2; i >= 0; i-- )
   {


   }

   return false;
}


