#pragma once
#include <vector>

namespace GlobalMethods
{

   class Utilities
   {
   public:
      static void CreateHeader( int const& day );                              //Prints the advent of code header
      static std::vector<std::string> CreateInputVectorString( std::string const& filePath );
      static std::vector<int> CreateInputVectorInt( std::string const& filePath );
   };

}
