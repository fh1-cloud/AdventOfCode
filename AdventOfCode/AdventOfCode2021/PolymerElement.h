#pragma once
#include <memory>

class PolymerElement
{
protected:
   PolymerElement* m_NextInLine;
   char m_Symbol;

public:
   PolymerElement( const char c );
   ~PolymerElement( ) = delete;
   char GetSymbol( );
   void SetNextInLine( PolymerElement* next );
   PolymerElement* GetNextInLine( );
};

