#include "fighter.h"
#include <iostream>

using namespace std;


Fighter::Fighter(string name):RPGclass(name)
{
	/***************************************/
	/*Defines Fighter Commands:	           */
	/*Can only attack					   */
	/*plus PC only commands Status and Flee*/
	/***************************************/
	NPCcommands=1;
	commands[0]=new Attack;
	commands[1]=new Status;
	commands[2]=new Flee;
}

void Fighter :: levelup(int levels)
{
	/********************************/
	/*Fighters have the most HP     */
	/*their primary stat is strength*/
	/********************************/
	wounds=0;
	for(int i=0;i<levels;i++)
	{
		defense+=diceroll(6);
		hp+=diceroll(12);
		strength+=diceroll(8);
		agility+=diceroll(4);
		intelligence+=diceroll(4);
		level++;
	}
}
void Fighter :: attack(RPGclass * target)
{
	/******************************************/
	/*Fighter's attack is Strength vs. Defense*/
	/******************************************/
	bool hit = (strength+diceroll(20)) >= target->getDef() + target->diceroll(20);
	int damage; 
	if(hit)
	{
		/*******************************/
		/*Damage also based on Strength*/
		/*******************************/
		damage = (strength/10)+diceroll(10);
		cout<<characterName<<" hits "<<target->getName()<<" for "<<damage<<endl;
		target->takedamage(damage);
	}
	else
	{
		cout<<target->getName()<<" evades "<<characterName<<"'s attack"<<endl;
	}
}