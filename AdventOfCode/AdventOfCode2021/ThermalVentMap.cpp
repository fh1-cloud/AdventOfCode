#include "ThermalVentMap.h"
#include "UIntPoint.h"

/// <summary>
/// Default constructor
/// </summary>
/// <param name=""></param>
ThermalVentMap::ThermalVentMap( vector<string> inpString )
{
//Loop over the input strings and create the map.
	for( size_t i = 0; i < inpString.size( ); i++ )
	{
		vector<int> thisRow;
		string thisString = inpString[i];
		for( size_t j = 0; j < thisString.size( ); j++ )
		{
			string p( 1, thisString[j] );
			thisRow.push_back( stoi( p ) );
		}
		m_Map.push_back( thisRow );
	}
	m_NRows = m_Map.size( );
	m_NCols = m_Map[0].size( );

//Create low points
   for( size_t i = 0; i < m_NRows; i++ )
   {
      for( size_t j = 0; j < m_NCols; j++ )
      {
         if( IsLowPoint( i, j ) )
         {
				m_LowPoints.push_back( UIntPoint( j, i ) );
         }
      }
   }


}

/// <summary>
/// Default destructor
/// </summary>
ThermalVentMap::~ThermalVentMap( )
{
}

/// <summary>
/// Gets the values of the neighbours of the input index.
/// </summary>
/// <param name=""></param>
/// <param name=""></param>
/// <returns></returns>
vector<UIntPoint> ThermalVentMap::GetNeighbourPoints( int rowIdx, int colIdx )
{
	vector<UIntPoint> neigh;

	if( rowIdx != 0 )
		neigh.push_back( UIntPoint(colIdx, rowIdx - 1 ) );
	if( rowIdx != m_NRows - 1 )
		neigh.push_back( UIntPoint(colIdx, rowIdx + 1 ) );
	if( colIdx != 0 )
		neigh.push_back( UIntPoint(colIdx-1, rowIdx ) );
	if( colIdx != m_NCols - 1 )
		neigh.push_back( UIntPoint(colIdx + 1, rowIdx ) );

	return neigh;
}

/// <summary>
/// CHecks if this point is a low point.
/// </summary>
/// <param name=""></param>
/// <param name=""></param>
/// <returns></returns>
bool ThermalVentMap::IsLowPoint( int rowIdx, int colIdx )
{
//Get the value of the neighbours
	vector<UIntPoint> neigh = GetNeighbourPoints( rowIdx, colIdx );
	
//Loop over members in set. 
	bool isLow = true;
	int thisValue = m_Map[rowIdx][colIdx];
//Get the value of the neighbours
	for( size_t i = 0; i < neigh.size( ); i++ )
	{
		if( GetValue( neigh[i].Y( ), neigh[i].X( ) ) <= thisValue )
			isLow = false;
	}

	return isLow;
}

/// <summary>
/// Gets the number of rows in the map
/// </summary>
/// <returns></returns>
int ThermalVentMap::GetRows( )
{
	return m_NRows;
}

/// <summary>
/// Get the number of columns in the map
/// </summary>
/// <returns></returns>
int ThermalVentMap::GetCols( )
{
	return m_NCols;
}

/// <summary>
/// Gets the value at position
/// </summary>
/// <param name=""></param>
/// <param name=""></param>
/// <returns></returns>
int ThermalVentMap::GetValue( int rowIdx, int colIdx )
{
	return m_Map[rowIdx][colIdx];
}

/// <summary>
/// Gets the low points for this thermal vent map
/// </summary>
/// <returns></returns>
vector<UIntPoint> ThermalVentMap::GetLowPoints( )
{
	return m_LowPoints;
}
