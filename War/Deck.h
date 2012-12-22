/****************/
/*include Guards*/
/****************/
#ifndef GUARD_Deck_h
#define GUARD_Deck_h

#include <deque>

//Structure for Card's suit
struct card_suit
{
	//enumerative type for card shapes
	enum shape_type{Spades,Clubs,Hearts,Diamonds};
	//enumerative type for card colors
	enum color_type{Black,Red};

	//constructor, takes a shape
	card_suit(shape_type shape);

	//holds a card's shape and color
	shape_type Shape;
	color_type Color;
};

//Card class
class Card
{
public:
	//enumerative type for card Rank, start count at 1
	enum rank_type{Ace=1,Two,Three,Four,Five,Six,Seven,Eight,Nine,Ten,Jack,Queen,King};

	//constructor, takes a Rank and a Shape
	Card(rank_type rank, card_suit::shape_type shape);

	rank_type GetRank();//return Card's Rank
	card_suit* GetSuit();//return Card's suit struct
	void flip();//flip the card
	bool isFaceup();//returns if card is face up or not
	void print();//print the card
private:
	bool faceup;//hold is face up or not
	rank_type Rank;//holds Rank
	card_suit *Suit;//holds Suit
};

//Deck class
class Deck
{
public:
	//contructor
	Deck();

	void shuffle();//shuffles the deck
	Card* Draw();//removes card from top, returns pointer to that card

	void addBottom(Card* c);//add a card to the bottom of deck
	void print();//prints the cards in the deck, mostly for debugging
	bool isEmpty();//check if deck is empty
	int size();//returns size of the deck
private:
	std::deque<Card*> cards;//vector holding cards in the deck
};
#endif