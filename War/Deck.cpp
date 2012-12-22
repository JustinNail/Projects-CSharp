#include "Deck.h"

#include <iostream>
#include <iomanip>

card_suit::card_suit(shape_type shape)
{
	Shape=shape;//initalize Shape
	switch(shape)//assign color based on shape
	{
	case Spades:
	case Clubs:
		Color=Black;
		break;
	case Hearts:
	case Diamonds:
		Color=Red;
		break;
	}

}

Card::Card(rank_type rank, card_suit::shape_type shape)
{
	faceup=false;//cards start off face down
	Rank=rank;//initialize Rank
	Suit=new card_suit(shape);//construct the Suit, passing it shape
}
void Card::flip()
{
	//inverse the faceup variable
	if(faceup)
	{
		faceup=false;
	}
	else
	{
		faceup=true;
	}
}
bool Card::isFaceup()
{
	//returns faceup variable
	return faceup;
}
Card::rank_type Card::GetRank()
{
	//returns Rank variable
	return Rank;
}
card_suit* Card::GetSuit()
{
	//returns pointer to Suit struct
	return Suit;
}
void Card::print()
{
	//only prints card info if face up
	if(faceup)
	{
		//print letter based on shape
		switch(Suit->Shape)
		{
		case card_suit::Spades:
			std::cout<<"S-";
			break;
		case card_suit::Clubs:
			std::cout<<"C-";
			break;
		case card_suit::Hearts:
			std::cout<<"H-";
			break;
		case card_suit::Diamonds:
			std::cout<<"D-";
			break;
		}
		//print rank letter or number
		switch(Rank)
		{
		case Ace:
			std::cout<<"A ";
			break;
		case Jack:
			std::cout<<"J ";
			break;
		case Queen:
			std::cout<<"Q ";
			break;
		case King:
			std::cout<<"K ";
			break;
		default:
			std::cout<<std::left<<std::setw(2)<<Rank;
		}
	}
	//if face down, print place holder
	else
	{
		std::cout<<"::: ";
	}
}

Deck::Deck()
{
	//makes all the spades and adds them to deck, then the clubs, etc
	for(int i=0;i<4;i++)
	{
		for(int j=0;j<13;j++)
		{
			cards.push_back(new Card((Card::rank_type)(j+1),(card_suit::shape_type)i));
		}
	}
}
void Deck::shuffle()
{
	//Fisher-Yates shuffle
	for(int i=cards.size()-1;i>=0;--i)
	{
		int j=rand()%(i+1);
		Card* temp;
		if(i!=j)
		{
			temp=cards[i];
			cards[i]=cards[j];
			cards[j]=temp;
		}
	}
}
Card* Deck::Draw()
{
	Card* c=cards.front();//hold pointer to top card
	cards.pop_front();//remove top card
	return c;//return pointer to top card
}
void Deck::addBottom(Card* c)
{
	cards.push_back(c);//add card to bottom
}
void Deck::print()
{
	for(int i=0;i<cards.size();i++)
	{
		if(!cards[i]->isFaceup())
		{
			cards[i]->flip();//if card is face down, flip it
		}
		cards[i]->print();//call card's print function
		std::cout<<std::endl;//newline
	}
}
bool Deck::isEmpty()
{
	return cards.empty();//returns if deque is empty
}
int Deck::size()
{
	return cards.size();//returns deque size
}
