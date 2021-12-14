#pragma once
#include <memory>
#include <unordered_map>

class PolymerElement
{
protected:
   PolymerElement* m_NextInLine;
   char m_Symbol;

public:
   PolymerElement( const char c );
   ~PolymerElement( );
   char GetSymbol( );
   void SetNextInLine( PolymerElement* next );
   PolymerElement* GetNextInLine( );

   static void ExpandPolymer( int *pMaxSteps, int stepCount, char const lhs, char const rhs, std::unordered_map<char, uint64_t> *pElementCounter, std::unordered_map<std::string, char> *pReactions );

};

