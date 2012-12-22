#include <iostream>
#include <iomanip>
#include <time.h>
#include <string>
#include <queue>
#include <stack>

//has Deck, Card, Suit object
#include "Deck.h"

using namespace std;

//class for a player
class Player
{
public:
	//constructor, takes name string
	Player(string s);

	string name();//returns player name

	Card* draw();//draws a card from player's pile
	void addToPile(Card* c);//add a card to player's pile

	int pileSize();//returns the size of player's pile
	bool OutOfCards();//return if player is out of cards

	stack<Card*> WarStack;//stack of cards fielded during a WAR
private:
	string Name;//holds name
	queue<Card*> pile;//player's pile of cards

};
Player::Player(string s)
{
	Name=s;//initalizes Name
}
string Player::name()
{
	return Name;//returns Name
}
Card* Player::draw()
{
	Card* c=pile.front();//hold pointer to top card
	pile.pop();//remove top card
	return c;//return pointer
}
void Player::addToPile(Card* c)
{
	pile.push(c);//add card to bottom of pile
}
int Player::pileSize()
{
	return pile.size();//returns pile size
}
bool Player::OutOfCards()
{
	return pile.empty();//returns if pile is empty or not
}

//class for the game itself
class WarGame
{
public:
	void Start();//set up method
	void Battle();//Battle method
	void War(Card* c1, Card* c2);//WAR method
	void Show();//shows game state
	void Play();//playing loop
private:
	void recWar();//recursive section of WAR method

	Player* p1,* p2;//pointers for two players
	Deck d;//deck of cards
	bool playing;//true until someone wins
};

void WarGame::Start()
{
	//get players' names
	//then construct the player objects with them
	string s1,s2;
	cout<<"Player 1, what is your name?"<<endl;
	cin>>s1;
	p1 = new Player(s1);
	cout<<"Player 2, what is your name?"<<endl;
	cin>>s2;
	p2 = new Player(s2);

	//shuffle the deck
	d.shuffle();

	//deal out all the cards to the player piles
	while(!d.isEmpty())
	{
		p1->addToPile(d.Draw());
		p2->addToPile(d.Draw());
	}

}
void WarGame::Battle()
{
	//pointers for the cards
	//involved in the battle
	Card* c1,* c2;

	//draw a card from each player's pile
	c1=p1->draw();
	c2=p2->draw();

	cout<<endl<<"BATTLE!"<<endl;
	
	//flip both cards
	c1->flip();
	c2->flip();
	
	//display the results
	cout<<p1->name()<<" draws ";
	c1->print();
	cout<<endl;
	cout<<p2->name()<<" draws ";
	c2->print();
	cout<<endl<<endl;

	//turn the cards back over
	c1->flip();
	c2->flip();

	//special cases for Aces
	if(c1->GetRank() == Card::Ace)
	{
		if(c2->GetRank() == Card::Ace)//if both drew an Ace
		{
			//Draw, calls War method
			cout<<"DRAW"<<endl<<endl;
			War(c1,c2);
		}
		else//otherwise, Ace beats all
		{
			cout<<p1->name()<<" Wins the Battle!"<<endl
				<<"They get "<<p2->name()<<"'s Card!"<<endl<<endl;

			//winner gets the spoils
			p1->addToPile(c1);
			p1->addToPile(c2);
		}
	}
	else if(c2->GetRank() == Card::Ace)
	{
		if(c1->GetRank() == Card::Ace)//if both drew Ace
		{
			//Draw, calls War method
			cout<<"DRAW"<<endl<<endl;
			War(c1,c2);
		}
		else//otherwise Ace beats all
		{
			cout<<p2->name()<<" Wins the Battle!"<<endl
				<<"They get "<<p1->name()<<"'s Card!"<<endl<<endl;

			//winner gets the spoils
			p2->addToPile(c2);
			p2->addToPile(c1);
		}
	}
	else//no one drew an ace
	{
		if(c1->GetRank() > c2->GetRank())//if player 1 wins
		{
			cout<<p1->name()<<" Wins the Battle!"<<endl
				<<"They get "<<p2->name()<<"'s Card!"<<endl<<endl;

			//winner gets the spoils
			p1->addToPile(c1);
			p1->addToPile(c2);
		}
		else if(c2->GetRank() > c1->GetRank())//if player 2 wins
		{
			cout<<p2->name()<<" Wins the Battle!"<<endl
				<<"They get "<<p1->name()<<"'s Card!"<<endl<<endl;

			//winner gets the spoils
			p2->addToPile(c2);
			p2->addToPile(c1);
		}
		else//else it's a draw
		{
			//calls War method
			cout<<"DRAW"<<endl<<endl;
			War(c1,c2);
		}
	}

	//check if anyone has won
	if(p1->OutOfCards())
	{
		//player 1 out of cards, so player 2 wins
		cout<<p2->name()<<" Wins the game!"<<endl;
		playing=false;
	}
	else if(p2->OutOfCards())
	{
		//player 2 out of cards, so player 1 wins
		cout<<p1->name()<<" Wins the game!"<<endl;
		playing=false;
	}
	else
	{
		//no one has won yet
		cout<<"Prepare for next Battle!"<<endl;
		system("pause");//pause so the players can see results of battle
	}
}
void WarGame::War(Card* c1, Card* c2)
{
	//add the battle cards to the stack of cards to win
	p1->WarStack.push(c1);
	p2->WarStack.push(c2);

	//start recursive part of War method
	recWar();
}
void WarGame::recWar()
{
	//variables for size of the array for each player's War cards
	int x1,x2;
	//variables for which card they wish to play from the array
	int p1Choice,p2Choice;
	
	cout<<"WAR"<<endl;

	if(p1->pileSize()<3)//player 1 has less than 3 cards
	{
		x1=p1->pileSize();//use all cards available
	}
	else//has at least 3 cards
	{
		x1=3;//use just 3 cards
	}
	if(p2->pileSize()<3)//player 2 has less than 3 cards
	{
		x2=p2->pileSize(); //use all cards available
	}
	else//has at least 3 cards
	{
		x2=3;//use just 3 cards
	}
	//make the dynamically allocated arrays
	Card** p1War=new Card*[x1];
	Card** p2War=new Card*[x2];
	
	//fill the "War array" with cards from the player's pile
	for(int i=0;i<x1;i++)
	{
		p1War[i]=p1->draw();
	}

	//display face down cards and has player pick one
	cout<<p1->name()<<" pick your Card"<<endl;
	for(int i=0;i<x1;i++)
	{
		cout<<" "<<i+1<<"   ";
	}
	cout<<endl;
	for(int i=0;i<x1;i++)
	{
		p1War[i]->print();
		cout<<' ';
	}
	cout<<endl;
	cin>>p1Choice;
	p1Choice--;//subtract 1 so it conforms to a zero-based array index

	//fill the "War array" with cards from the player's pile
	for(int i=0;i<x2;i++)
	{
		p2War[i]=p2->draw();
	}
	
	//display face down cards and has player pick one
	cout<<p2->name()<<" pick your Card"<<endl;
	for(int i=0;i<x2;i++)
	{
		cout<<" "<<i+1<<"   ";
	}
	cout<<endl;
	for(int i=0;i<x2;i++)
	{
		p2War[i]->print();
		cout<<' ';
	}
	cout<<endl;
	cin>>p2Choice;
	p2Choice--;//subtract 1 so it conforms to a zero-based array index

	//reveal the picked cards
	cout<<endl<<p1->name()<<" picked:";
	p1War[p1Choice]->flip();
	p1War[p1Choice]->print();
	p1War[p1Choice]->flip();

	cout<<endl<<p2->name()<<" picked:";
	p2War[p2Choice]->flip();
	p2War[p2Choice]->print();
	p2War[p2Choice]->flip();

	//add entire 'war array' for each player to the stacks of cards to win
	for(int i=0;i<x1;i++)
	{
		p1->WarStack.push(p1War[i]);
	}

	for(int i=0;i<x2;i++)
	{
		p2->WarStack.push(p2War[i]);
	}


	if(p1War[p1Choice]->GetRank() > p2War[p2Choice]->GetRank())//if player 1 wins
	{
		cout<<endl<<p1->name()<<" Wins the War!"<<endl
			<<"They get "<<p2->name()<<"'s Cards!"<<endl;
		
		//add their own stack back to their pile
		while(!p1->WarStack.empty())
		{
			Card* c=p1->WarStack.top();
			p1->WarStack.pop();
			p1->addToPile(c);
		}
		
		//show what they won, then add them to their deck
		cout<<p1->name()<<"gets:"<<endl;
		while(!p2->WarStack.empty())
		{
			Card* c=p2->WarStack.top();
			p2->WarStack.pop();
			c->flip();
			c->print();
			c->flip();
			p1->addToPile(c);
		}
	}
	else if(p2War[p2Choice]->GetRank() > p1War[p1Choice]->GetRank())//if player 2 wins
	{
		cout<<endl<<p2->name()<<" Wins the War!"<<endl
			<<"They get "<<p1->name()<<"'s Cards!"<<endl;

		//add their stack back to their pile
		while(!p2->WarStack.empty())
		{
			Card* c=p2->WarStack.top();
			p2->WarStack.pop();
			p2->addToPile(c);
		}

		//show what they won, then add them to their deck
		cout<<p2->name()<<" gets:"<<endl;
		while(!p1->WarStack.empty())
		{
			Card* c=p1->WarStack.top();
			p1->WarStack.pop();
			c->flip();
			c->print();
			c->flip();
			p2->addToPile(c);
		}
	}
	else//draw
	{
		//calls this method again
		cout<<"DRAW"<<endl;
		recWar();
	}
}

void WarGame::Play()
{
	Start();//set up game
	while(playing)//loop until someone wins
	{
		Show();//display game state
		Battle();//start a battle
	}
}


void WarGame::Show()
{
	//shows each player's name and how many cards they have left
	cout<<endl<<p1->name()<<endl;
	cout<<"Pile: "<<p1->pileSize()<<endl;
	cout<<endl<<p2->name()<<endl;
	cout<<"Pile: "<<p2->pileSize()<<endl;
}

int main()
{
	srand(time(NULL));//seed the RNG
	WarGame wg;//make game obj
	wg.Play();//start playing

	system("pause");
	return 0;
}