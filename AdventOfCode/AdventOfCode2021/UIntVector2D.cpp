#include "UIntVector2D.h"
#include <cmath>

using namespace std;


/// <summary>
/// Default constructor. 
/// </summary>
/// <param name="xs">The start x coordinate</param>
/// <param name="ys">The start y coordinate</param>
/// <param name="xe">The end x coordinage</param>
/// <param name="ye">The end y coordinate</param>
UIntVector2D::UIntVector2D( int const xs, int const ys, int const xe, int const ye )
{
   m_pStart = new UIntPoint( xs, ys );
   m_pEnd = new UIntPoint( xe, ye );
}

/// <summary>
/// Default destructor
/// </summary>
UIntVector2D::~UIntVector2D( )
{
   delete m_pStart;
   delete m_pEnd;
}


/// <summary>
/// Gets a pointer to the start point of this vector
/// </summary>
/// <returns></returns>
UIntPoint* UIntVector2D::GetStart( )
{
   return m_pStart;
}

/// <summary>
/// Gets a pointer to the endpoint of this vector
/// </summary>
/// <returns></returns>
UIntPoint* UIntVector2D::GetEnd( )
{
   return m_pEnd;
}

/// <summary>
/// Gets the length of this vector
/// </summary>
/// <returns></returns>
double UIntVector2D::GetLength( )
{
   return sqrt( pow( m_pEnd->X( ) - m_pStart->X( ), 2.0 ) + m_pEnd->Y( ) - m_pStart->Y( ) );
}

/// <summary>
/// Fills the canvas..
/// </summary>
/// <param name=""></param>
void UIntVector2D::FillCanvas( vector<vector<int>> &canvas )
{

//Check what direction the vector is pointing in..
   int xLength = abs( m_pStart->X( ) - m_pEnd->X( ) );
   int yLength = abs( m_pStart->Y( ) - m_pEnd->Y( ) );

//DIagonal line.
   if( xLength > 0 && yLength > 0 )
   {
   //right
      bool right = m_pStart->X( ) - m_pEnd->X( ) < 0;
      bool down = m_pStart->Y( ) - m_pEnd->Y( ) < 0;

      int xStardIdx = m_pStart->X( );
      int yStartIdx = m_pStart->Y( );
      if( right )
      {
         if( down )  //Down right
         {
            for( int i = 0; i<=xLength; i++ )
               canvas[yStartIdx++][xStardIdx++]++;
         }
         else //up right
         {
            for( int i = 0; i<=xLength; i++ )
               canvas[yStartIdx--][xStardIdx++]++;
         }
      }
      else //Left
      {
         if( down ) //down left
         {
            for( int i = 0; i<=xLength; i++ )
               canvas[yStartIdx++][xStardIdx--]++;
         }
         else //Up left
         {
            for( int i = 0; i<=xLength; i++ )
               canvas[yStartIdx--][xStardIdx--]++;
         }

      }
   }
   else if( xLength > 0 ) //Line is only in the x direction
   {
   //Columns
      int xStart;
      int xEnd;
      if( m_pStart->X( ) > m_pEnd->X( ) )
      {
         xStart = m_pEnd->X( );
         xEnd = m_pStart->X( );
      }
      else
      {
         xStart = m_pStart->X( );
         xEnd = m_pEnd->X( );
      }
      for( int i = xStart; i <= xEnd; i++ )
         canvas[m_pStart -> Y( )][i]++;
   }
   else if( yLength > 0 ) //Line is only in the y direction
   {
      int yStart;
      int yEnd;

      if( m_pStart->Y( ) > m_pEnd->Y( ) )
      {
         yStart = m_pEnd->Y( );
         yEnd = m_pStart->Y( );
      }
      else
      {
         yStart = m_pStart->Y( );
         yEnd = m_pEnd->Y( );
      }
      for( int i = yStart; i <= yEnd; i++ )
         canvas[i][m_pStart -> X( )]++;
   }


}



