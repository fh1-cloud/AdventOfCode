#pragma once
#include <utility>
#include <string>
#include <sstream>
#include <vector>
#include "BingoBoard.h"
#include "Utilities.h"

//Default constructor.
BingoBoard::BingoBoard( vector<string> rows )
{
//Set the bingo status to false for this bingo board.
	for( int i = 0; i < m_Dimension ; i++ )
	{
		m_BingoRows[i] = false;
      m_BingoCols[i] = false;
	}
	m_HasWon = false;

//Initialize the bingo board
	for( int i = 0; i < m_Dimension ; i++ )
	{
		vector<string> test = GlobalMethods::Utilities::splitWhitespace( rows[i] );
		for( int j = 0; j < m_Dimension; j++ )
		{
			m_Values[i][j].first = stoi( test[j] );
			m_Values[i][j].second = false;
		}
	}
}

BingoBoard::~BingoBoard( )
{

	for( int i = 0; i < m_Dimension; i++ )
	{
		for( int j = 0; j < m_Dimension; j++ )
		{
		}

	}



}

/// <summary>
/// Marks the current number in this bingo board.
/// </summary>
/// <param name=""></param>
void BingoBoard::MarkNumber( int num )
{
	for( int i = 0; i < m_Dimension; i++ )
	{
		for( int j = 0; j < m_Dimension; j++ )
		{
			if( m_Values[i][j].first == num )
			{
				m_Values[i][j].second = true;
			}

		}
	}
}

/// <summary>
/// Checks this board for bingo. If it has bingo, the row, column or diagonal is flagged as bingo and is not checked further
/// </summary>
/// <returns></returns>
bool BingoBoard::CheckForBingo( )
{

//Check rows..
	for( int i = 0; i < m_Dimension; i++ )
	{
	//Skip if this row have gotten bingo before
		if( m_BingoRows[i] )
			continue;

	//Check remaining rows.
		bool bingo = true;
		for( int j = 0; j < m_Dimension; j++ )
			bingo &= m_Values[i][j].second;
		if( bingo )
		{
			m_BingoRows[i] = true;
			m_HasWon = true;
			return true;
		}
	}

//Check columns..
	for( int i = 0; i < m_Dimension; i++ )
	{
		if( m_BingoCols[i] )
			continue;

	//CHeck remaining columns
		bool bingo = true;
		for( int j = 0; j < m_Dimension; j++ )
			bingo &= m_Values[j][i].second;
		if( bingo )
		{
			m_BingoCols[i] = true;
			m_HasWon = true;
			return true;
		}
	}

//If the code reached this point, no bingo was reached this time.
   return false;
}


/// <summary>
/// Prints this bingo board.
/// </summary>
void BingoBoard::PrintBoard( )
{
	for( int i = 0; i < m_Dimension ; i++ )
	{
		for( int j = 0; j < m_Dimension; j++ )
		{
			if( m_Values[i][j].first )
				cout << "X";
			else
				cout << m_Values[i][j].first;

			cout << " ";
		}
		cout << "\n";
	}
}

/// <summary>
/// Calculates the score for this board. This is the sum of all the unmarked numbers on the board.
/// </summary>
/// <returns></returns>
int BingoBoard::GetScore( )
{
	int sum = 0;
	for( int i = 0; i < m_Dimension; i++ )
		for( int j = 0; j < m_Dimension; j++ )
			if( m_Values[i][j].second == false )
				sum += m_Values[i][j].first;

	return sum;
}

/// <summary>
/// Returns a bool indicating wheter or not this board has already won.
/// </summary>
/// <returns></returns>
bool BingoBoard::HasWon( )
{
	return m_HasWon;
}
