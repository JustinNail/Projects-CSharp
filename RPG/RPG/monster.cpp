#include "monster.h"
#include <iostream>

using namespace std;

Monster::Monster(string name):RPGclass(name)
{
	/******************************/
	/*Constructor defines commands*/
	/*Monsters can only attack,   */
	/*is never a PC so no Status  */
	/*or flee commands			  */
	/******************************/
	NPCcommands=1;
	commands[0] = new Attack();
}
void Monster :: levelup(int levels)
{
	/********************************/
	/*Monsters have nothing special */
	/*their primary stat is strength*/
	/********************************/
	wounds=0;
	for(int i=0;i<levels;i++)
	{
		defense+=diceroll(6);
		hp+=diceroll(4);
		strength+=diceroll(6);
		agility+=diceroll(4);
		intelligence+=diceroll(4);
		level++;
	}
}
void Monster :: attack(RPGclass * target)
{
	/******************************************/
	/*Monster's attack is Strength vs. Defense*/
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