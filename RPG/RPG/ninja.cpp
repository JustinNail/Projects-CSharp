#include "ninja.h"
#include <iostream>

using namespace std;

Ninja::Ninja(string name):Thief(name),Monk(name),RPGclass(name)
{
	/***************************/
	/*Defines Ninja Commands   */
	/*Same as Monk  		   */
	/***************************/
	/***************************/
	/*Inherits Stats and Flags */
	/*From Thief and Monk	   */
	/***************************/
	NPCcommands=2;
	commands[0]=new Attack;
	commands[1]=new Special_Ability("Focus");
	commands[2]=new Status;
	commands[3]=new Flee;
}
void Ninja :: levelup(int levels)
{
	/**************************************/
	/*Ninja are a mix of Thieves and Monks*/
	/*their primary stat is agility		  */
	/**************************************/
	wounds=0;
	for(int i=0;i<levels;i++)
	{
		defense+=diceroll(6);
		hp+=diceroll(6);
		strength+=diceroll(4);
		agility+=diceroll(8);
		intelligence+=diceroll(4);
		level++;
		ki++;
		sneakAttack++;
	}
}
void Ninja :: attack(RPGclass * target)
{
	/*************************************/
	/*Ninja attack is Agility vs. Defense*/
	/*************************************/
	bool hit = (agility+diceroll(20)) >= target->getDef() + target->diceroll(20);
	int damage;
	if(hit)
	{
		/*************************/
		/*Damage is Agility based*/
		/*************************/
		damage = (agility/10)+diceroll(10);

		/*********************************************************/
		/*Thieves have a chance to sneak attack for double damage*/
		/*********************************************************/
		if(((rand()%100)+1) < sneakAttack)
		{
			cout<<characterName<<" hits with a sneak attack!"<<endl;
			damage*=2;
		}
		/***************************************/
		/*Monks focus their Ki for extra damage*/
		/***************************************/
		if(focused)
		{
			cout<<characterName<<" unleashes their focused Ki!"<<endl;
			damage+=ki+diceroll(6);
			focused=false;
		}
		cout<<characterName<<" hits "<<target->getName()<<" for "<<damage<<endl;
		target->takedamage(damage);
	}
	else
	{
		cout<<target->getName()<<" evades "<<characterName<<"'s attack"<<endl;
	}
}
void Ninja :: specialAbility(RPGclass *target){Monk::specialAbility(target);}//same as Monk