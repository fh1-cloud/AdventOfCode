#pragma once
#include <string>
#include <vector>
#include <unordered_map>

class DigitalDisplaySignal
{

public:
   std::vector<std::string> m_SignalPatterns;
   std::vector<std::string> m_DigitOutputValue;
   std::unordered_map<int, std::string> m_Translator;

   DigitalDisplaySignal( std::string );
   ~DigitalDisplaySignal( );

   std::vector<std::string> GetSignalPatterns( );
   std::vector<std::string> GetDigitOutputValue( );
   int GetOutput( );

   int GetIntegerFromString( std::string );
};

