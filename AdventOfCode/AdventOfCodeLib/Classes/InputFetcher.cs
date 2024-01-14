using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeLib
{
   public static class InputFetcher
   {

   //Get the session cookie..
      public static string m_SessionCookie = "53616c7465645f5f095c544346869dbc2eb86c692c4de40a97374ad0ecaf5116ced2f713d807002dbd6ae92493fa46429461f5357aadd5ec72faf0b28e461ecb";
      //public static string m_SessionCookie = "53616c7465645f5f70553a538d57be792c298688c2668baf2f56f4a5adc81cff822744455b8c26c63936effbce4fdb1c69a7f91c9341782e3842700864e5877a";

      public static async Task GetInput( int day, int year, string filename )
      {
         if( !File.Exists( filename ) )
         {
            var uri = new Uri( "https://adventofcode.com" );
            var cookies = new CookieContainer( );
            cookies.Add( uri, new System.Net.Cookie( "session", m_SessionCookie ) );
            using var file = new FileStream( filename, FileMode.Create, FileAccess.Write, FileShare.None );
            using var handler = new HttpClientHandler( ) { CookieContainer = cookies };
            using var client = new HttpClient( handler ) { BaseAddress = uri };
            using var response = await client.GetAsync( $"/{year}/day/{day}/input" );
            using var stream = await response.Content.ReadAsStreamAsync( );
            await stream.CopyToAsync( file );
            file.Close( );
         }
      }

   }


}
