#pragma once
#include <unordered_map>
#include <vector>

class DjikstraNode
{

protected:
   bool m_Visited;
   int m_RowIdx;
   int m_ColIdx;
   int m_Weight;
   uint64_t m_CurrentValue;

public:
   DjikstraNode( int row, int col, int weight, int val );
   bool GetVisited( );
   uint64_t GetValue( );
   void SetValue( uint64_t val );
   int GetWeight( );
   std::vector<DjikstraNode*> GetNeighbours( std::vector<std::vector<DjikstraNode*>>* nodes, bool onlyUnvisited );

   void Run( std::vector<std::vector<DjikstraNode*>>* nodes );

   static DjikstraNode* GetLowestUnvisitedNode( std::vector<std::vector<DjikstraNode*>>* nodes );


};

