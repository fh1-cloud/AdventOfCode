#pragma once
#include <string>
#include <vector>
#include <unordered_map>
#include <utility>

class DumboOctopus
{

protected:
   bool m_HasFlashed = false;
   int m_RowIdx = -1;
   int m_ColIdx = -1;
   int m_Value = -1;

public:
   DumboOctopus( int rowIdx, int colIdx, int val );
   ~DumboOctopus( );

   int GetRowIdx( );
   int GetColIdx( );
   int GetVal( );
   bool Increment( );
   void Flash( std::vector<DumboOctopus*>* theseShouldFlash, std::unordered_map<int, std::unordered_map<int, DumboOctopus*>> allOctopi, uint64_t* nOfFlashesTotal, uint64_t* nOfFlashesThisStep  );
   void Reset( );

   static std::vector<DumboOctopus*> GetNeighbours( DumboOctopus* center, std::unordered_map<int, std::unordered_map<int, DumboOctopus*>> allOctopi );
   static void PrintState( std::unordered_map<int, std::unordered_map<int, DumboOctopus*>> allOctopi );




};

