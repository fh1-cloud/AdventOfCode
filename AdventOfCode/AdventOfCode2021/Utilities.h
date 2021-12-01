#pragma once
#include <vector>
#include <fstream>
#include <iostream>
#include <string>
#include <sstream>

namespace GlobalMethods
{

   class Utilities
   {
   public:
      static void CreateHeader( int const& day );                              //Prints the advent of code header
      static std::vector<std::string> CreateInputVectorString( std::string const& filePath );
      static std::vector<int> CreateInputVectorInt( std::string const& filePath );
      static std::vector<std::vector<std::string>> CreateInputVectorStringVector( std:: string const& filePath, char del = ' ' );

   };

/*TEMPLATE IMPLEMENTATIONS*/

}
