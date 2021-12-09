#pragma once
#include <vector>
#include "ThermalVentMap.h"
#include "UIntPoint.h"

using namespace std;

class ThermalVentMapBasin
{

public:
   vector<UIntPoint> m_Points;
   UIntPoint m_LowPoint;
   ThermalVentMapBasin( ThermalVentMap map, int rowIdx, int colIdx );
   
   int GetSize( );
   bool Contains( UIntPoint p );

};

