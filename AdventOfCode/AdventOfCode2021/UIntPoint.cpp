#include "UIntPoint.h"

/// <summary>
/// Default constructor. Initializes a point 
/// </summary>
/// <param name="x">XCoordinate</param>
/// <param name="y">YCoordinate</param>
UIntPoint::UIntPoint( int const x, int const y )
{
   m_X = x;
   m_Y = y;
}

/// <summary>
/// Default destructor
/// </summary>
UIntPoint::~UIntPoint( )
{
}

/// <summary>
/// Returns the x coordinate of this point
/// </summary>
/// <returns></returns>
int UIntPoint::X( )
{
   return m_X;
}

/// <summary>
/// Gets the Y-coordinate of this point.
/// </summary>
/// <returns></returns>
int UIntPoint::Y( )
{
   return m_Y;
}
