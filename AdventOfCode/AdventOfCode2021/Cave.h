#pragma once
#include <string>
#include <vector>
#include <unordered_map>
#include <unordered_set>

class Cave
{

   enum Type { BIG, SMALL, START, END };

protected:
   std::unordered_set<Cave*> m_Connections;
   std::string m_ID;
   Type m_Type;

public:
   Cave( std::string );
   ~Cave( );

   void AddConnection( Cave* pCave );
   std::string GetID( );
   Type GetType( );
   std::unordered_set<Cave*> GetConnections( );

   static void CountPaths( Cave* cave, std::unordered_set<Cave*> visited, Cave* pSmallVisitedTwice, int* count, bool p2 );

};

