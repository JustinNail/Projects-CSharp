#include "thief.h"
#include <iostream>

using namespace std;

Thief::Thief(string name):RPGclass(name)
{
	/*************************/
	/*Defines Thief Commands */
	/*and initializes Thief  */
	/*Special stat			 */
	/*************************/
	sneakAttack = 25;
	/**********************/
	/*Thieves have Attack */
	/*plus PC commands	  */
	/**********************/
	NPCcommands=1;
	commands[0]=new Attack;
	commands[1]=new Status;
	commands[2]=new Flee;
}

void Thief :: levelup(int levels)
{
	/********************************/
	/*Thieves can sneak attack      */
	/*their primary stat is agility */
	/********************************/
	wounds=0;
	for(int i=0;i<levels;i++)
	{
		defense+=diceroll(6);
		hp+=diceroll(6);
		strength+=diceroll(4);
		agility+=diceroll(8);
		intelligence+=diceroll(4);
		level++;
		sneakAttack++;
	}
}
void Thief :: attack(RPGclass * target)
{
	/*Thief's attack is Agility vs. Defense*/
	bool hit = (agility+diceroll(20)) >= target->getDef() + target->diceroll(20);
	int damage;
	if(hit)
	{
		/******************************/
		/*Damage is also Agility Based*/
		/******************************/
		damage = (agility/10)+diceroll(10);
		/*********************************************************/
		/*Thieves have a chance to sneak attack for double damage*/
		/*********************************************************/
		if(((rand()%100)+1) < sneakAttack)
		{
			cout<<characterName<<" hits with a sneak attack!"<<endl;
			damage*=2;
		}
		cout<<characterName<<" hits "<<target->getName()<<" for "<<damage<<endl;
		target->takedamage(damage);
	}
	else
	{
		cout<<target->getName()<<" evades "<<characterName<<"'s attack"<<endl;
	}
}