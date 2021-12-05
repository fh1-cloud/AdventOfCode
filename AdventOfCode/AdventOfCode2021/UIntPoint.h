#pragma once
class UIntPoint
{
protected:
   int m_X;
   int m_Y;

public:
   UIntPoint( int const x, int const y );
   ~UIntPoint( );

   int X( );
   int Y( );
};

