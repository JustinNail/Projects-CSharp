#include <iostream>
#include <iomanip>
#include <stdlib.h>
#include <time.h>
#include <vector>
#include <stack>
#include <algorithm>
#include <stdexcept>

//has Deck, Card, and Suit objects
#include "Deck.h"

using namespace std;

//class for the Solitaire playing field
class playing_field
{
public:
	//constructor
	playing_field();

	void Deal();//deals out the initial set up
	void show();//show field state
	void CheckMoveToF(Card* c, int f);//checks if a move to a foundation is valid
	void CheckMoveToT(Card* c, int t);//checks if a move to a tableau is valid
	void DtoW();//Deck to waste
	void WtoT(int t);//waste to Tableau
	void WtoF(int f);//waste to Foundation
	void TtoF(int t, int f);//Tableau to Foundation
	void FtoT(int f, int t);//Foundation to Tableau
	void TtoT(int t1,int t1Start,int t2);//Tableau to Tableau

private:
	Deck d;//the deck
	stack<Card*> Foundations[4];//array for the foundation stacks
	vector<Card*> Tableau[7];//array for the tableau vectors
	stack<Card*> Waste;//the waste stack

	int score;//player's score
};

playing_field::playing_field()
{
	score=0;//score starts at 0
}
void playing_field::Deal()
{
	Card* c;//temp Card pointer
	d.shuffle();//shuffle the deck

	//deal out the Tableau cards
	for(int i=0;i<7;i++)//7 tableau
	{
		c=d.Draw();//draw a card
		for(int j=0;j<i;j++)//1st gets 1, 2nd gets 2, etc
		{
			Tableau[i].push_back(c);//add to tableau
			c=d.Draw();//draw next card
		}
		c->flip();//flip the last card in tableau
		Tableau[i].push_back(c);
	}
	//draw first card to waste
	c=d.Draw();
	c->flip();
	Waste.push(c);
}
void playing_field::show()
{
	//headers for deck, waste, and foundations
	cout<<"  DECK       WASTE                 FOUNDATION          SCORE: "<<score<<endl;
	cout<<"                             1      2      3      4  "<<endl;
	cout<<"---------  ---------       -----  -----  -----  -----"<<endl;
	
	if(d.isEmpty())//if deck is empty, say so
	{
		cout<<"  Empty  ";
	}
	else//otherwise, print deck size
	{
		cout<<"   "<<setw(2)<<d.size()<<"    ";
	}
	cout<<"  ";

	if(!Waste.empty())//if waste not empty
	{
		cout<<"   ";
		Waste.top()->print();//show the top card
		cout<<"  ";
	}
	else//else, say it's empty
	{
		cout<<"  Empty  ";
	}
	cout<<"       ";

	for(int i=0;i<4;i++)//for each foundation
	{
		if(!Foundations[i].empty())//if not empty
		{
			cout<<" ";
			Foundations[i].top()->print();//show top card
			cout<<"  ";
		}
		else//else, say it's empty
		{
			cout<<"Empty  ";
		}
	}
	cout<<endl<<endl;

	//header for tableau
	cout<<"                    TABLEAU                   "<<endl;
	cout<<"     1     2     3     4     5     6     7    "<<endl;
	cout<<"   ----- ----- ----- ----- ----- ----- -----  ";

	//find the size of longest tableau
	int max=0;
	for(int j=0;j<7;j++)
	{
		if(Tableau[j].size() > Tableau[max].size())
		{
			max=j;
		}
	}

	//go for length of longest tableau
	for(int j=0;j<Tableau[max].size();j++)
	{
		cout<<endl<<left<<setw(2)<<j+1<<":";//print row labels

		for(int i=0;i<7;i++)//for each tableau
		{
			if(!Tableau[i].empty())//if tableau isn't empty
			{
				if(j<Tableau[i].size())//if j isn't past the end of a tableau
				{
					cout<<" ";
					Tableau[i].at(j)->print();//print the card
					cout<<" ";
				}
				else//otherwise print spaces
				{
					cout<<"      ";
				}
			}
			else//print spaces if empty
			{
				cout<<"      ";
			}
		}
	}
	cout<<endl<<endl;
	
}
void playing_field::CheckMoveToF(Card* c, int f)
{
	if(!Foundations[f].empty())//if not empty
	{
		//if suits don't match
		if(c->GetSuit()->Shape != Foundations[f].top()->GetSuit()->Shape)
		{
			throw domain_error ("Must have same Suit\n");//throw error
			return;//exit
		}

		int cRank=(int)(c->GetRank());//hold source rank
		int fRank=(int)(Foundations[f].top()->GetRank());//hold rank of top card

		//if not the next card in order
		if(cRank <= fRank-1)
		{
			throw domain_error ("Must be the next card in order\n");//throw error
			return;//exit
		}
	}
	else if(c->GetRank()!=Card::Ace)//if is empty, and source isn't an Ace
	{
		throw domain_error ("Only Aces can go on Empty Foundations\n");//throw error
		return;//exit
	}
}
void playing_field::CheckMoveToT(Card* c, int t)
{
	//if target empty and source isn't a king
	if((c->GetRank()!=Card::King)&&(Tableau[t].empty()))
	{
		throw domain_error ("Only Kings can move to empty Tableau\n");//throw error
		return;//exit
	}
	//if source is a king, but target isn't empty
	if((c->GetRank()==Card::King)&&(!Tableau[t].empty()))
	{
		throw domain_error ("Cannot Move There\n");//throw error
		return;//exit
	}

	//if target not empty
	if((!Tableau[t].empty()))
	{
		//colors are the same
		if(c->GetSuit()->Color == Tableau[t].back()->GetSuit()->Color)
		{
			throw domain_error ("Must be the opposite color\n");//throw error
			return;//exit
		}
		//not the next in order
		if(c->GetRank() != Tableau[t].back()->GetRank()-1)
		{
			throw domain_error ("Must be the next card in order\n");//throw error
			return;//exit
		}
	}
}
void playing_field::DtoW()
{
	Card* c;
	if(!d.isEmpty())//deck isn't empty
	{
		//move a card to the waste from the deck
		c=d.Draw();
		//flip if face down
		if(!c->isFaceup())
		{
			c->flip();
		}
		Waste.push(c);//push on to top of waste
	}
	else//if deck is empty
	{
		//turn the waste into the deck
		while(!Waste.empty())
		{
			c=Waste.top();
			Waste.pop();
			d.addBottom(c);
		}
	}
}
void playing_field::WtoT(int t)
{
	if(Waste.empty())//no card to move
	{
		throw domain_error ("Waste Empty\n");//throw error
		return;//exit
	}

	Card* c=Waste.top();//hold top card
	
	//check of move is valid
	try
	{
		CheckMoveToT(c,t);
	}
	catch(domain_error e)
	{
		throw domain_error (e.what());//pass on the error
		return;//exit
	}

	//remove top waste card
	Waste.pop();
	//push it on end
	Tableau[t].push_back(c);

	score+=5;
}
void playing_field::WtoF(int f)
{
	Card* c=Waste.top();
	try
	{
		CheckMoveToF(c,f);
	}
	catch(domain_error e)
	{
		throw domain_error (e.what());
		return;
	}
	Waste.pop();
	Foundations[f].push(c);

	score+=10;
}
void playing_field::TtoF(int t, int f)
{
	if(Tableau[t].empty())
	{
		throw domain_error ("Tableau Empty\n");
		return;
	}
	Card* c = Tableau[t].back();

	try
	{
		CheckMoveToF(c,f);
	}
	catch(domain_error e)
	{
		throw domain_error (e.what());
		return;
	}
	
	Tableau[t].pop_back();

	Foundations[f].push(c);

	score+=10;

	if(!(Tableau[t].empty()) && !(Tableau[t].back()->isFaceup()))
	{
		Tableau[t].back()->flip();
		score+=5;
	}
}
void playing_field::FtoT(int f, int t)
{
	if(Foundations[f].empty())
	{
		throw domain_error ("Foundation Empty\n");
		return;
	}
	Card* c = Foundations[f].top();
	try
	{
		CheckMoveToT(c,t);
	}
	catch(domain_error e)
	{
		throw domain_error (e.what());
		return;
	}
	Foundations[f].pop();
	Tableau[t].push_back(c);

	score -= 15;
}
void playing_field::TtoT(int t1,int t1Start,int t2)
{
	vector<Card*> temp;
	if(Tableau[t1].empty())
	{
		throw domain_error ("Tableau Empty\n");
		return;
	}
	if(t1Start > Tableau[t1].size()-1)
	{
		throw domain_error ("No Card There\n");
		return;
	}
	if(!Tableau[t1].at(t1Start)->isFaceup())
	{
		throw domain_error ("Cannot Move Face Down Cards\n");
		return;
	}

	try
	{
		CheckMoveToT(Tableau[t1].at(t1Start),t2);
	}
	catch(domain_error e)
	{
		throw domain_error (e.what());
		return;
	}

	for(int i=t1Start;i<Tableau[t1].size();i++)
	{
		temp.push_back(Tableau[t1].at(i));
	}
	for(int i=Tableau[t1].size();i>t1Start;i--)
	{
		Tableau[t1].pop_back();
	}
	for(int i=0;i<temp.size();i++)
	{
		Tableau[t2].push_back(temp[i]);
	}
	if(!(Tableau[t1].empty()) && !Tableau[t1].back()->isFaceup())
	{
		Tableau[t1].back()->flip();
		score+=5;
	}
}

void menu(playing_field& pf)
{
	char inc,inc1,inc2,inc3;
	int inx,inx1,inx2,inx3;

	while(true)//loops until quit command
	{
		while(true)//loops until valid input
		{
			cout<<endl
				<<"What do you want to do?"<<endl
				<<"\t1. Move Waste to Tableau"
				<<"\t2. Move Waste to Foundation"<<endl
				<<"\t3. Move Tableau to Foundation"
				<<"\t4. Move Foundation to Tableau"<<endl
				<<"\t5. Move Tableau to Tableau"
				<<"\t6. Draw to Waste"<<endl
				<<"\t7. Quit"<<endl;
			cin>>inc;//gets as a character
			//check input
			if(inc=='1'||inc=='2'||inc=='3'||inc=='4'||inc=='5'||inc=='6'||inc=='7')
			{
				break;//leave loop if valid
			}
			cout<<"Invalid Selection"<<endl;
		}
		inx=inc-48;//convert to int
		switch(inx)
		{
		case 1://waste to Tableau
			while(true)
			{
				cout<<"Which Tableau? ";
				cin>>inc1;
				if(inc1=='1'||inc1=='2'||inc1=='3'||inc1=='4'||inc1=='5'||inc1=='6'||inc1=='7')
				{
					break;//leave loop if valid
				}
				pf.show();
				cout<<"Invalid Selection"<<endl;
			}
			inx1=(inc1-48)-1;
			try
			{
				pf.WtoT(inx1);
			}
			catch(domain_error e)
			{
				cout<<e.what();
			}
			pf.show();
			break;
		case 2://Waste to Foundation
			while(true)
			{
				cout<<"Which Foundation? ";
				cin>>inc1;
				if(inc1=='1'||inc1=='2'||inc1=='3'||inc1=='4')
				{
					break;//leave loop if valid
				}
				pf.show();
				cout<<"Invalid Selection"<<endl;
			}
			inx1=(inc1-48)-1;
			try
			{
				pf.WtoF(inx1);
			}
			catch(domain_error e)
			{
				cout<<e.what();
			}
			pf.show();
			break;
		case 3://Tableau to Foundation
			while(true)
			{
				cout<<"Which Tableau? ";
				cin>>inc1;
				if(inc1=='1'||inc1=='2'||inc1=='3'||inc1=='4'||inc1=='5'||inc1=='6'||inc1=='7')
				{
					break;//leave loop if valid
				}
				pf.show();
				cout<<"Invalid Selection"<<endl;
			}
			while(true)
			{
				cout<<"Which Foundation? ";
				cin>>inc2;
				if(inc2=='1'||inc2=='2'||inc2=='3'||inc2=='4')
				{
					break;//leave loop if valid
				}
				pf.show();
				cout<<"Invalid Selection"<<endl;
			}
			inx1=(inc1-48)-1;
			inx2=(inc2-48)-1;
			try
			{
				pf.TtoF(inx1,inx2);
			}
			catch(domain_error e)
			{
				cout<<e.what();
			}
			pf.show();
			break;
		case 4://Foundation to Tableau
			while(true)
			{
				cout<<"Which Foundation? ";
				cin>>inc2;
				if(inc2=='1'||inc2=='2'||inc2=='3'||inc2=='4')
				{
					break;//leave loop if valid
				}
				pf.show();
				cout<<"Invalid Selection"<<endl;
			}
			while(true)
			{
				cout<<"Which Tableau? ";
				cin>>inc1;
				if(inc1=='1'||inc1=='2'||inc1=='3'||inc1=='4'||inc1=='5'||inc1=='6'||inc1=='7')
				{
					break;//leave loop if valid
				}
				pf.show();
				cout<<"Invalid Selection"<<endl;
			}
			inx1=(inc1-48)-1;
			inx2=(inc2-48)-1;
			try
			{
				pf.FtoT(inx2,inx1);
			}
			catch(domain_error e)
			{
				cout<<e.what();
			}
			pf.show();
			break;
		case 5://Tableau to Tableau
			while(true)
			{
				cout<<"Source Tableau? ";
				cin>>inc1;
				if(inc1=='1'||inc1=='2'||inc1=='3'||inc1=='4'||inc1=='5'||inc1=='6'||inc1=='7')
				{
					break;//leave loop if valid
				}
				pf.show();
				cout<<"Invalid Selection"<<endl;
			}
			while(true)
			{
				cout<<"Which row? ";
				cin>>inc2;
				if(inc2>='1')
				{
					break;//leave loop if valid
				}
				pf.show();
				cout<<"Invalid Selection"<<endl;
			}
			while(true)
			{
				cout<<"Destination Tableau? ";
				cin>>inc3;
				if(inc3=='1'||inc3=='2'||inc3=='3'||inc3=='4'||inc3=='5'||inc3=='6'||inc3=='7')
				{
					break;//leave loop if valid
				}
				pf.show();
				cout<<"Invalid Selection"<<endl;
			}
			inx1=(inc1-48)-1;
			inx2=(inc2-48)-1;
			inx3=(inc3-48)-1;
			try
			{
				pf.TtoT(inx1,inx2,inx3);
			}
			catch(domain_error e)
			{
				cout<<e.what();
			}
			pf.show();
			break;
		case 6:
			pf.DtoW();
			pf.show();
			break;
		case 7:
			cout<<"Thank you for playing"<<endl;
			return;
			break;
		}
	}
}

int main()
{
	char c;
	srand(time(NULL));
	playing_field pf;
	/*Deck d;
	d.print();
	cout<<endl;
	d.shuffle();
	d.print();*/
	pf.Deal();
	pf.show();
	menu(pf);
	system("Pause");
}