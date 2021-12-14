#include "PolymerElement.h"

///// <summary>
///// Default constructor
///// </summary>
///// <param name="c">The symbol for this element.</param>
///// <param name="nextInLine">The next polymer in line</param>
//PolymerElement::PolymerElement( const char c )
//{
//   m_NextInLine = nullptr;
//   m_Symbol = c;
//}

PolymerElement::PolymerElement( char c )
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

