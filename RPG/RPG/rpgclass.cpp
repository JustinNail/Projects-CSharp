#include "rpgclass.h"
#include <iostream>

using namespace std;

RPGclass::RPGclass(string name)
{
/******************************/
/* Gets character name from   */
/* argument, initializes stats*/
/******************************/

	characterName=name;
	defense=1;
	hp=30;
	wounds=0;
	strength=1;
	agility=1;
	intelligence=1;
	level=0;

	//set flags to default
	PC=false;
	poisoned=false;
	blinking=false;
	isProtected=false;
	
}
void RPGclass::status()
{
	/****************************************/
	/*Prints out most of a character's stats*/
	/****************************************/
	cout<<endl<<characterName<<endl;
	cout<<"HP: "<<hp<<endl;
	cout<<"Wounds: "<<wounds<<endl;
	cout<<"Defense: "<<defense<<endl;
	cout<<"Strength: "<<strength<<endl;
	cout<<"Agility: "<<agility<<endl;
	cout<<"Intelligence: "<<intelligence<<endl;
	if(poisoned)
		cout<<"Poisoned!"<<endl;
}
int RPGclass::diceroll(int dice)
{
	/************************************************/
	/*argument determines what kind of die is rolled*/
	/************************************************/
	int roll = (rand() % dice + 1);
	//cout<<characterName<<" rolled:"<<endl<<"\t"<<roll<<" / "<<dice<<endl;//reveals dice rolls
	return roll;
}

int RPGclass :: doCommand(int choice, RPGclass *target)
{
	return commands[choice]->execute(this,target);
}
void RPGclass :: specialAbility(RPGclass *target){}

void RPGclass :: removedamage(int amt)
{
	wounds-=amt;
	//wounds can't be negative
	if (wounds<0)
		wounds=0;
}//method for healing damage
void RPGclass :: takedamage(int amt)
{
	/**************************************************************************/
	/*Blinking creatures randomly vanish, evading otherwise successful attacks*/
	/**************************************************************************/
	if(blinking&&(diceroll(100)>50))
	{
		cout<<characterName<<" Disappears at the last second, negating the attack!"<<endl;
		return;
	}
	/**************************************/
	/*Protected Creatures take half damage*/
	/**************************************/
	if(isProtected)
	{
		cout<<"A holy shield absorbs part of the attack!"<<endl;
		amt=amt/2;
	}
	wounds+=amt;
}

void RPGclass :: PrintCommands()
{
	for(int i=0;i<NPCcommands+2;i++)
	{
		cout<<"\t"<<i<<". ";
		commands[i]->displayName();
	}
}

bool RPGclass :: isAlive(){return wounds<hp;}//returns true if wounds < HP
int RPGclass :: getLevel(){return level;}//returns the creature's level
int RPGclass :: getDef(){return defense;}//returns the creature's defense
int RPGclass :: getStr(){return strength;}//returns the creature's strength
int RPGclass :: getAg(){return agility;}//returns the creature's agility
int RPGclass :: getInt(){return intelligence;}//returns the creature's intelligence
string RPGclass :: getName(){return characterName;}//returns the creature's name
int RPGclass :: getNPCcommands(){return NPCcommands;}

void RPGclass :: setPC(bool pc){PC=pc;}//sets if creature is a PC or NPC
void RPGclass :: setPoison(bool p){poisoned=p;}//sets poisoned variable
void RPGclass :: setBlink(bool p){blinking=p;}//sets blinking variable
void RPGclass :: setProtect(bool p){isProtected=p;}//sets isProtected variable 

bool RPGclass :: getPC(){return PC;}//gets if creature is a PC or NPC
bool RPGclass :: getPoison(){return poisoned;}//gets poisoned variable
bool RPGclass :: getBlink(){return blinking;}//gets blinking variable
bool RPGclass :: getProtect(){return isProtected;}//gets isProtected variable 