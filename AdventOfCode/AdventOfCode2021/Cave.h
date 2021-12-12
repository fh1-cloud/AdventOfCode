#pragma once

#include <string>
#include <vector>
#include <unordered_map>
#include <unordered_set>

using namespace std;

class Cave
{

   enum Type { BIG, SMALL, START, END };

protected:
   unordered_set<Cave*> m_Connections;
   string m_ID;
   Type m_Type;

public:
   Cave( string );
   ~Cave( );

   void AddConnection( Cave* pCave );
   string GetID( );
   Type GetType( );
   unordered_set<Cave*> GetConnections( );

   static void CountPaths( Cave* cave, unordered_set<Cave*> visited, Cave* pSmallVisitedTwice, int* count, bool p2 );

};

