#pragma once
#include "ThermalVentMapBasin.h"
#include "UIntPoint.h"
#include "ThermalVentMap.h";
using namespace std;

/// <summary>
/// Default constructor. Creates a basin from an input lowpoint
/// </summary>
/// <param name="map">The map of all the points</param>
/// <param name="rowIdx">The row index of the starting lowpoint</param>
/// <param name="colIdx">The column index of the starting lowpoint</param>
ThermalVentMapBasin::ThermalVentMapBasin( ThermalVentMap map , int lowPointRowIdx, int lowPointColIdx )
{
//Start from the lowpoint. 
//Get neighbours of point. 
//If it is not in the basin list, add it to the list
//Restart iteration with new list, looping through all the points.
//When all points were looped through, and nothing was added, the basin is complete.

//Add the lowPoint.
   m_LowPoint = UIntPoint( lowPointColIdx, lowPointRowIdx );
   m_Points.push_back( m_LowPoint );

   bool addedSomething = true;
   while( addedSomething )
   {
      addedSomething = false;
      for( int i = 0; i < m_Points.size( ); i++ )
      {
      //Get neighbours of this point.
         vector<UIntPoint> neigh = map.GetNeighbourPoints( m_Points[i].Y( ), m_Points[i].X( ) );
         bool addedOneNeighbour = false;
      //Loop through neighbours, check if it should be added to the list of points..
         for( int j = 0; j < neigh.size( ); j++ )
         {
         //Check the value. If it is 9, it is not a part of the basin
            int val = map.GetValue( neigh[j].Y( ), neigh[j].X( ) );
            bool isInList = Contains( neigh[j] );
            if( val == 9 || isInList )
            {
               continue;
            }
            else
            {
            //If the code reached this point, it is int the basin because it is impossible to cross over with the neighbour function
               m_Points.push_back( neigh[j] );
               addedSomething = true;
            }
         }
      //If something was added, restart because we changed the size of m_Points..
         if( addedSomething )
            break;
      }
   }

}

/// <summary>
/// Gets the size of this basin
/// </summary>
/// <returns></returns>
int ThermalVentMapBasin::GetSize( )
{
   return m_Points.size( );
}

/// <summary>
/// CHecks if the basin contains a point.
/// </summary>
/// <param name="p"></param>
/// <returns></returns>
bool ThermalVentMapBasin::Contains( UIntPoint p )
{
//Checks if the basin contains this point
   for( int i = 0; i < m_Points.size( ); i++ )
      if( m_Points[i].X( ) == p.X( ) && m_Points[i].Y( ) == p.Y( ) )
         return true;

//Return false if the point was not found..
   return false;
}
