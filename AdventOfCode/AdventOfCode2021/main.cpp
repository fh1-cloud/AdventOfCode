#pragma once
#include <iostream>
#include <string>
#include "Days.h";
#include "Utilities.h"
using namespace std;

/// <summary>
/// Main entry point for application here.
/// </summary>
/// <returns></returns>
int main( )
{
   GlobalMethods::Utilities::CreateHeader( 14 );
   Days::Dec14( );

   cout << "\n";
   cout << "\n";
}


