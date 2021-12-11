#pragma once

#include <string>
#include <vector>
#include <unordered_map>
#include <utility>

using namespace std;

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
   void Flash( vector<DumboOctopus*>* theseShouldFlash, unordered_map<int, unordered_map<int, DumboOctopus*>> allOctopi, uint64_t* nOfFlashesTotal, uint64_t* nOfFlashesThisStep  );
   void Reset( );

   static vector<DumboOctopus*> GetNeighbours( DumboOctopus* center, unordered_map<int, unordered_map<int, DumboOctopus*>> allOctopi );
   static void PrintState( unordered_map<int, unordered_map<int, DumboOctopus*>> allOctopi );




};

