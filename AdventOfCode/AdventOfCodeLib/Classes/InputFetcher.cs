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

   //Session cookie 2024
      public static string m_SessionCookie = "53616c7465645f5f410cc68893c37175208aaf93955641731096d74cb0fd38afa0ce5b6ccff9c638b930b9ee6fa73b10c00a301b2eea18e5cc5206241fdd59fe";

      public static async Task GetInput( int day, int year, string filename, string sessionCookie )
      {
         if( !File.Exists( filename ) )
         {
            var uri = new Uri( "https://adventofcode.com" );
            var cookies = new CookieContainer( );
            cookies.Add( uri, new System.Net.Cookie( "session", sessionCookie ) );
            using var file = new FileStream( filename, FileMode.Create, FileAccess.Write, FileShare.None );
            using var handler = new HttpClientHandler( ) { CookieContainer = cookies };
            using var client = new HttpClient( handler ) { BaseAddress = uri };
            using var response = await client.GetAsync( $"/{year}/day/{day}/input" );
            using var stream = await response.Content.ReadAsStreamAsync( );
            await stream.CopyToAsync( file );
            file.Close( );
         }
      }

      public static async Task GetInput( int day, int year, string filename )
      {
         GetInput( day, year, filename, m_SessionCookie );
      }

   }



}
