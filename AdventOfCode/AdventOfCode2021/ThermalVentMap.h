#pragma once
#include <vector>
#include <string>
#include <unordered_set>
#include "UIntPoint.h"

using namespace std;

class ThermalVentMap
{

public:
   vector<vector<int>> m_Map;
   int m_NRows;
   int m_NCols;
   vector<UIntPoint> m_LowPoints;
   ThermalVentMap( vector<string> );
   ~ThermalVentMap( );

   vector<UIntPoint> GetNeighbourPoints( int rowIdx, int colIdx );
   bool IsLowPoint( int, int );
   int GetRows( );
   int GetCols( );
   int GetValue( int rowIdx, int colIdx );
   vector<UIntPoint> GetLowPoints( );

};

