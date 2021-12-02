#pragma once
#include <vector>
#include <string>
#include "Submarine.h"

using namespace std;

/// <summary>
/// Constructor
/// </summary>
/// <param name="horPos">The starting horizontal position of the submarine</param>
/// <param name="vertPos">The starting vertical position of the submarine</param>
Submarine::Submarine(int horPos, int vertPos, int aim)
{
   m_HorizontalPosition = horPos;
   m_Depth = vertPos;
   m_Aim = aim;
}

/// <summary>
/// Destructor
/// </summary>
Submarine::~Submarine( )
{

}

/// <summary>
/// Carry out an instruction for the submarine
/// </summary>
/// <param name="ins"></param>
/// <param name="val"></param>
void Submarine::CarryOutInstruction( SUBMARINEINSTRUCTION const& ins, int const& val )
{
   if( ins == FORWARD )
   {
      m_HorizontalPosition += val;
      m_Depth += m_Aim*val;
   }
   else if( ins == DOWN )
      m_Aim += val;
   else if( ins == UP )
      m_Aim -= val;
}

/// <summary>
/// Gets the current position for this submarine
/// </summary>
/// <returns></returns>
vector<int> Submarine::GetPosition( )
{
   vector<int> retVec;
   retVec.push_back( m_HorizontalPosition );
   retVec.push_back( m_Depth );
   return retVec;
}
