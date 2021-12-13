#include "Cave.h"

using namespace std;
/// <summary>
/// Default constructor
/// </summary>
/// <param name=""></param>
Cave::Cave( string identifier )
{
   m_ID = identifier;
   if( identifier == "start" )
      m_Type = START;
   else if( identifier == "end" )
      m_Type = END;
   else if( isupper( identifier[0] ) )
      m_Type = BIG;
   else
      m_Type = SMALL;
}

/// <summary>
/// Delete the connection array set.
/// </summary>
Cave::~Cave( )
{

}

/// <summary>
/// Adds a connection for this cave. This should only be done in the constructor, and if it is already added, it will throw
/// </summary>
/// <param name="pCave"></param>
void Cave::AddConnection( Cave* pCave )
{
   if( m_Connections.find( pCave ) != m_Connections.end( ) )
      return;
   else
      m_Connections.insert( pCave );
}

/// <summary>
/// Returns the ID of this cave.
/// </summary>
/// <returns></returns>
string Cave::GetID( )
{
   return m_ID;
}

/// <summary>
/// REturns a bool indicating wheter or not this cave is major
/// </summary>
/// <returns></returns>
Cave::Type Cave::GetType( )
{
   return m_Type;
}

/// <summary>
/// Gets the unordered set of connections for this node.
/// </summary>
/// <returns></returns>
unordered_set<Cave*> Cave::GetConnections( )
{
   return m_Connections;
}


/// <summary>
/// Counts the unique paths through the cave system by recursion. Only increment the count if we managed to reach the end.
/// </summary>
/// <param name="cave">The current cave.</param>
/// <param name="smallCavesVisitedPreviously">An unordered set of all the small caves visited previously. We dont need to keep track of the large caves, because we can visit them unlimited number of times. It is important that this is passed as a copy and NOT as a reference, because it tracks the previously visited small caves on this arm</param>
/// <param name="pSmallVisitedTwice">A pointer to a small cave visited twice. This can only be set if the small cave is already in the previous visited list</param>
/// <param name="count">The number of times the code reached the end</param>
/// <param name="p2">A bool indicating wheter or not this is for part two and we can visit a small cave twice</param>
void Cave::CountPaths( Cave* cave, unordered_set<Cave*> smallCavesVisitedPreviously, Cave* pSmallVisitedTwice, int* count, bool p2 )
{
//Pass visited previously as a copy of the set. This is to make sure that each arm of the iteration actually have an unique path

//Check if this cave is the end cave. If so, add one to the count and return.
   if( cave->GetType( ) == Cave::END )
   {
   //Increment counter ONLY if we reached the end, because we then know we actually have an unique path
      (*count)++;
      return;
   }

//Check if this cave was already visited..

//If the cave is small and it was already visited. it should not count this path.
   if( cave->GetType( ) == Cave::SMALL )
   {
      if( smallCavesVisitedPreviously.find( cave ) == smallCavesVisitedPreviously.end( ) ) //Cave was not visited.. Add it to the list of visited caves.
         smallCavesVisitedPreviously.insert( cave );
      else if( p2 && pSmallVisitedTwice == NULL )
         pSmallVisitedTwice = cave;
      else //Cave was visited before, and is small. This is not a valid path. Return and to NOT increment the count. 
         //Also, IF this is part two, we are sure that the pointer for the small cavern visited twice is not null, so we have already visited a cavern twice here.
         return;
   }

//Loop through all the neighbours for this cave and count the path for the next one..
   unordered_set<Cave*> connections = cave->GetConnections( );

   for( auto i : connections )
   {
      if( i->GetType( ) != Cave::START )
      {
         CountPaths( i, smallCavesVisitedPreviously, pSmallVisitedTwice, count, p2 );
      }
   }

//After this previous loop, we have visited all the caves with THIS cave as being visited twice. Therefore, reset the pointer for the visited twice cave.
   if( cave->GetType( ) == Cave::SMALL )
   {
   //If this cave is small, and this cave is the one we have visited twice, set the pointer to nullpointer. We cannot visit this cave more than once on next runs.
      if( pSmallVisitedTwice == cave )
         pSmallVisitedTwice = nullptr;

   }


}

