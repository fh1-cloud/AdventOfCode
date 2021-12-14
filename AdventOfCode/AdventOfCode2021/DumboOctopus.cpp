#include "DumboOctopus.h"
#include <iostream>
using namespace std;

/// <summary>
/// Initialize a dumbo octopus with rowindex, colindex and value
/// </summary>
/// <param name="rowIdx">The row index of this octopus</param>
/// <param name="colIdx">THe column index of this octopus</param>
/// <param name="val">The current value of this octopus</param>
DumboOctopus::DumboOctopus( int rowIdx, int colIdx, int val )
{
   m_RowIdx = rowIdx;
   m_ColIdx = colIdx;
   m_Value = val;
}

/// <summary>
/// Default destructor.
/// </summary>
DumboOctopus::~DumboOctopus( )
{
   double test = 0.0;
   //Nothing is allocated on the heap?
}

/// <summary>
/// Gets the row index of this octopus
/// </summary>
/// <returns></returns>
int DumboOctopus::GetRowIdx( )
{
   return m_RowIdx;
}

/// <summary>
/// Gets the column index for this octopus
/// </summary>
/// <returns></returns>
int DumboOctopus::GetColIdx( )
{
   return m_ColIdx;
}

/// <summary>
/// Increments the value of the octopus. Returns true if the value is grater than nine after incremented
/// </summary>
/// <returns></returns>
bool DumboOctopus::Increment( )
{
   if( !m_HasFlashed )
      m_Value++;
   return m_Value > 9;
}


/// <summary>
/// Flash this octopus. Then get all its neighbours and increment them
/// </summary>
/// <param name="theseShouldFlash">A reference to a list of all the octopi that should flash</param>
/// <param name="allOctopi">A map of all the octopi. This is used to get the neighbours of the octopi</param>
/// <param name="nOfFlashesTotal">A pointer to an int that counts the total amount of flashes</param>
/// <param name="nOfFlashesThisTurn">A pointer to an int that counts the number of flashes this turn</param>
void DumboOctopus::Flash(vector<DumboOctopus*>* theseShouldFlash, unordered_map<int,unordered_map<int,DumboOctopus*>> allOctopi, uint64_t* nOfFlashesTotal, uint64_t* nOfFlashesThisTurn  )
{
//If this already has flashed, nothing should be done here. This should not happen?
   if( m_HasFlashed )
      return;

//Set flash status of this octopus
   m_HasFlashed = true;
   m_Value = 0;

//Remove this object from the vector..
   theseShouldFlash -> erase( std::remove( theseShouldFlash -> begin( ), theseShouldFlash -> end( ), this), theseShouldFlash -> end( ) );

//Get neighbours and increment..
   vector<DumboOctopus*> neighboursToIncrement = GetNeighbours( this, allOctopi );

//Find the ones that should flash..
   for( int i = 0; i < neighboursToIncrement.size( ); i++ )
   {
   //Increment the neighbours and check if they should flash
      bool thisShouldFlash = neighboursToIncrement[i]->Increment( );
      if( thisShouldFlash )
         theseShouldFlash -> push_back( neighboursToIncrement[i] );
   }

//Flash the ones in the list that was collected.
   while( theseShouldFlash -> size( ) > 0 )
   {
   //Flash the first one in the list.
      (*theseShouldFlash)[0]->Flash( theseShouldFlash, allOctopi, nOfFlashesTotal, nOfFlashesThisTurn );

   //Increment the number of flashes
      ( *nOfFlashesTotal )++;
      ( *nOfFlashesThisTurn )++;
   }

}

/// <summary>
/// Resets the flash status of this octopus. Typically done when a whole step is completed.
/// </summary>
void DumboOctopus::Reset( )
{
   m_HasFlashed = false;
}

/// <summary>
/// Gets a vector of all the neighbours of this center octopus that have not flashed this turn. If they flashed, they should not be incremented anyway.
/// </summary>
vector<DumboOctopus*> DumboOctopus::GetNeighbours( DumboOctopus* center, unordered_map<int, unordered_map<int, DumboOctopus*>> allOctopi )
{
//Declare the returning list.
   vector<DumboOctopus*> neigh;

   int thisRowIdx = center->GetRowIdx( );
   int thisColIdx = center->GetColIdx( );
   int mapDim = allOctopi[0].size( );

//TOp
   if( thisRowIdx != 0 )
   {
   //TopLeft
      if( thisColIdx != 0 )
      {
         DumboOctopus* d = allOctopi[thisRowIdx - 1][thisColIdx - 1];
         if( d -> m_HasFlashed == false )
            neigh.push_back( d );
      }

   //TopMid
      DumboOctopus* a = allOctopi[thisRowIdx - 1][thisColIdx];
      if( a -> m_HasFlashed == false )
         neigh.push_back( a );

   //TopRight
      if( thisColIdx != mapDim - 1 )
      {
         DumboOctopus* d = allOctopi[thisRowIdx - 1][thisColIdx + 1];
         if( d -> m_HasFlashed == false )
            neigh.push_back( d );
      }
   }

//Bottom..
   if( thisRowIdx != mapDim - 1 )
   {
   //BotLeft
      if( thisColIdx != 0 )
      {
         DumboOctopus* d = allOctopi[thisRowIdx + 1][thisColIdx - 1];
         if( d -> m_HasFlashed == false )
            neigh.push_back( d );
      }

   //BotMid
      DumboOctopus* a = allOctopi[thisRowIdx + 1][thisColIdx];
      if( a -> m_HasFlashed == false )
         neigh.push_back( a );

   //BotRight
      if( thisColIdx != mapDim - 1 )
      {
         DumboOctopus* d = allOctopi[thisRowIdx + 1][thisColIdx + 1];
         if( d -> m_HasFlashed == false )
            neigh.push_back( d );
      }
   }

//LeftLeft
   if( thisColIdx != 0 )
   {
      DumboOctopus* d = allOctopi[thisRowIdx][thisColIdx - 1];
      if( d -> m_HasFlashed == false )
         neigh.push_back( d );
   }

//BotRight
   if( thisColIdx != mapDim - 1 )
   {
      DumboOctopus* d = allOctopi[thisRowIdx][thisColIdx + 1];
      if( d -> m_HasFlashed == false )
         neigh.push_back( d );
   }

   return neigh;

}


/// <summary>
/// Static method. Prints the current state of all the octopi to the console screen
/// </summary>
/// <param name="allOctopi"></param>
void DumboOctopus::PrintState(unordered_map<int,unordered_map<int,DumboOctopus*>> allOctopi)
{
   for( size_t i = 0; i < allOctopi.size( ); i++ )
   {
      for( size_t j = 0; j < allOctopi[i].size( ); j++ )
         cout << allOctopi[i][j]->GetVal( );

      cout << endl;
   }
   cout << "\n";
}

/// <summary>
/// GEts the current value of this octopus
/// </summary>
/// <returns></returns>
int DumboOctopus::GetVal()
{
   return m_Value;
}
