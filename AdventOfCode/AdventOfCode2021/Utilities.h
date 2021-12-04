#pragma once
#include <vector>
#include <fstream>
#include <iostream>
#include <string>
#include <sstream>
#include <cstdlib>
#include <algorithm>
#include <cassert>

namespace GlobalMethods
{

   class Utilities
   {
   public:
      static void CreateHeader( int const& day );                              //Prints the advent of code header
      static std::vector<std::string> CreateInputVectorString( std::string const& filePath );
      static std::vector<int> CreateInputVectorInt( std::string const& filePath );
      static std::vector<std::vector<std::string>> CreateInputVectorStringVector( std:: string const& filePath, char del = ' ' );
      static std::string to_base(unsigned long num, int base);
      static unsigned long from_base(std::string const& num_str, int base);
      static std::vector<std::string> split( std::string, char );
      static std::vector<std::string> splitWhitespace( std::string );

   };

/*TEMPLATE IMPLEMENTATIONS*/
 
}
