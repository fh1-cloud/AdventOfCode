#include "DjikstraNode.h"

using namespace std;
DjikstraNode::DjikstraNode( int row, int col, int weight, int val )
{
   m_Visited = false;
   m_RowIdx = row;
   m_ColIdx = col;
   m_Weight = weight;
   m_CurrentValue = val;
}

/// <summary>
/// Gets the visited status
/// </summary>
/// <returns></returns>
bool DjikstraNode::GetVisited( )
{
   return m_Visited;
}

/// <summary>
/// Gets the current travel value for this node
/// </summary>
/// <returns></returns>
uint64_t DjikstraNode::GetValue( )
{
   return m_CurrentValue;
}


/// <summary>
/// Gets a list of all the neighbouring nodes of this node.
/// </summary>
/// <param name="nodes">All the nodes in the array</param>
/// <returns></returns>
vector<DjikstraNode*> DjikstraNode::GetNeighbours( vector<vector<DjikstraNode*>>* nodes, bool onlyUnvisited )
{
//Declare list of returning pointers
   vector<DjikstraNode*> neigh;

//Top
   if( m_RowIdx != 0 )
   {
      auto n = ( *nodes )[m_RowIdx - 1][m_ColIdx];
      if( onlyUnvisited )
      {
         if( n->GetVisited( ) == false )
            neigh.push_back( n );
      }
      else
         neigh.push_back( n );
   }
//Bot
   if( m_RowIdx != nodes->size( ) - 1 )
   {
      auto n = ( *nodes )[m_RowIdx + 1][m_ColIdx];
      if( onlyUnvisited )
      {
         if( n->GetVisited( ) == false )
            neigh.push_back( n );
      }
      else
         neigh.push_back( n );
   }
//Left
   if( m_ColIdx != 0 )
   {
      auto n = ( *nodes )[m_RowIdx][m_ColIdx - 1];
      if( onlyUnvisited )
      {
         if( n->GetVisited( ) == false )
            neigh.push_back( n );
      }
      else
         neigh.push_back( n );
   }
//Right
   if( m_ColIdx != ( *nodes )[0].size( ) - 1 )
   {
      auto n = ( *nodes )[m_RowIdx][m_ColIdx + 1];
      if( onlyUnvisited )
      {
         if( n->GetVisited( ) == false )
            neigh.push_back( n );
      }
      else
         neigh.push_back( n );
   }

   return neigh;
}

/// <summary>
/// Gets the weight for this node.
/// </summary>
/// <returns></returns>
int DjikstraNode::GetWeight( )
{
   return m_Weight;
}

/// <summary>
/// Sets the value for this node.
/// </summary>
/// <param name="val"></param>
void DjikstraNode::SetValue( uint64_t val )
{
   m_CurrentValue = val;
}



/// <summary>
/// Runs Djikstras algorithm on the input set
/// </summary>
/// <param name="nodes"></param>
void DjikstraNode::Run( std::vector<std::vector<DjikstraNode*>> *pNodes )
{
//Calculate the value for this node.
   vector<DjikstraNode*> unvisitedNeighbours = GetNeighbours( pNodes, true );

//Calculate the weights of the neighbour nodes and set it on the neighbour nodes
   for( auto i : unvisitedNeighbours )
   {
      if( m_RowIdx == 0 && m_ColIdx == 0 )
      {
         i->SetValue( i->GetWeight( ) );
      }
      else
      {
      //Calculate the value
         uint64_t currentValue = m_CurrentValue + i->GetWeight( );
         if( i->GetValue( ) > currentValue )
            i->SetValue( currentValue );
      }
   }

//Mark this node as visited..
   m_Visited = true;

}

/// <summary>
/// Gets a pointer to the unvisited node with the lowest value
/// </summary>
/// <param name="nodes"></param>
/// <returns></returns>
DjikstraNode* DjikstraNode::GetLowestUnvisitedNode( std::vector<std::vector<DjikstraNode*>>* nodes )
{

   DjikstraNode* pLowest = nullptr;
   uint64_t lowVal = 1.0e6;

   for( int i = 0; i < ( *nodes ).size( ); i++ )
   {
      for( int j = 0; j < ( *nodes )[i].size( ); j++ )
      {
         DjikstraNode* pThisNode = ( *nodes )[i][j];

         if( pThisNode->GetVisited( ) )
            continue;
         else
         {
            if( pLowest == nullptr )
            {
               pLowest = pThisNode;
               lowVal = pThisNode->GetValue( );
            }
            else
            {
               uint64_t thisVal = pThisNode->GetValue( );
               if( thisVal < lowVal )
               {
                  pLowest = pThisNode;
                  lowVal = thisVal;
               }
            }
         }
      }
   }

//Return a pointer to the one node with the lowest value;
   return pLowest;
}
