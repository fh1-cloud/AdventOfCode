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
   unordered_map<int, string> m_Translator;

   DigitalDisplaySignal( string );
   ~DigitalDisplaySignal( );

   vector<string> GetSignalPatterns( );
   vector<string> GetDigitOutputValue( );
   int GetOutput( );

   int GetIntegerFromString( string );
};

