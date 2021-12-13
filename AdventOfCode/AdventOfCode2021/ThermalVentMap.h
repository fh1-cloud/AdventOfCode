#pragma once
#include <vector>
#include <string>
#include <unordered_set>
#include "UIntPoint.h"

class ThermalVentMap
{

public:
   std::vector<std::vector<int>> m_Map;
   int m_NRows;
   int m_NCols;
   std::vector<UIntPoint> m_LowPoints;
   ThermalVentMap( std::vector<std::string> );
   ~ThermalVentMap( );

   std::vector<UIntPoint> GetNeighbourPoints( int rowIdx, int colIdx );
   bool IsLowPoint( int, int );
   int GetRows( );
   int GetCols( );
   int GetValue( int rowIdx, int colIdx );
   std::vector<UIntPoint> GetLowPoints( );

};

