#pragma once
#include "UIntPoint.h"
#include <vector>
class UIntVector2D
{
protected:
   UIntPoint* m_pStart;
   UIntPoint* m_pEnd;

public:
   UIntVector2D( int const, int const, int const, int const );
   ~UIntVector2D( );

   UIntPoint* GetStart( );
   UIntPoint* GetEnd( );
   double GetLength( );
   void FillCanvas( std::vector<std::vector<int>> &p );

};

