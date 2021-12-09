#pragma once
#include <string>
#include <vector>
#include <unordered_map>

using namespace std;

class DigitalDisplaySignal
{

public:
   vector<string> m_SignalPatterns;
   vector<string> m_DigitOutputValue;
   unordered_map<int, char> m_SignalMap;

   /*
   *     0
   *   1   2
   *     3
   *   4   5
   *     6
   */ 




   DigitalDisplaySignal( string );
   ~DigitalDisplaySignal( );

   vector<string> GetSignalPatterns( );
   vector<string> GetDigitOutputValue( );
   int GetOutput( );


};

