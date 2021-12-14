#include "PolymerElement.h"

/// <summary>
/// Default constructor
/// </summary>
/// <param name="c">The symbol for this element.</param>
/// <param name="nextInLine">The next polymer in line</param>
PolymerElement::PolymerElement( const char c )
{
   m_NextInLine = nullptr;
   m_Symbol = c;
}

PolymerElement::~PolymerElement( )
{
   //if( m_NextInLine != nullptr )
   //   delete m_NextInLine;
}

char PolymerElement::GetSymbol( )
{
   return m_Symbol;
}

void PolymerElement::SetNextInLine( PolymerElement* next )
{
   m_NextInLine = next;
}


PolymerElement* PolymerElement::GetNextInLine( )
{
   return m_NextInLine;
}

void PolymerElement::ExpandPolymer( int *pMaxSteps, int stepCount, char const lhs, char const rhs, std::unordered_map<char, uint64_t>* pElementCounter, std::unordered_map<std::string, char>* pReactions )
{

//Return if we reached the maximum number of steps
   if( stepCount >= (*pMaxSteps ) )
      return;

//Creates the new element between the two.
   std::string key = std::string( 1, lhs ) + rhs;

//Found it in the dictionary
   //if( pReactions->find( key ) != pReactions->end( ) )
   //{
      char newElem = (*pReactions)[key];

   //Increment the element counter and the step counter.
      ( *pElementCounter )[newElem]++;
      stepCount += 1;

   ////Do the reaction for the lhs and the new element, and rhs and new element
   //   std::string key1 = std::string( 1, lhs ) + newElem;
   //   std::string key2 = std::string( 1, newElem ) + rhs;

   //Look for the keys in the rections. If they exist, continue with the reactions.
      //if( pReactions->find( key1 ) != pReactions->end( ) )
      PolymerElement::ExpandPolymer( pMaxSteps, stepCount, lhs, newElem, pElementCounter, pReactions );
      //if( pReactions->find( key2 ) != pReactions->end( ) )
      PolymerElement::ExpandPolymer( pMaxSteps, stepCount, newElem, rhs, pElementCounter, pReactions );
   //}


}



