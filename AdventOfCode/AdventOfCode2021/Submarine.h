#pragma once
#include <vector>
#include <string>

using namespace std;

/*ENUMS*/
enum SUBMARINEINSTRUCTION
{
   FORWARD,
   DOWN,
   UP
};

class Submarine
{


/*MEMBERS*/
protected:
   long m_HorizontalPosition;
   long m_Depth;
   long m_Aim;

/*CONSTRUCTORS*/
public:
   Submarine( int, int, int );
   ~Submarine( );

/*METHODS*/
   void CarryOutInstruction( SUBMARINEINSTRUCTION const& ins, int const& val );
   vector<int> GetPosition( );

};



