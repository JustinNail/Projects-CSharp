#include "monk.h"
#include <iostream>

using namespace std;

Monk::Monk(string name):RPGclass(name)
{
	/*************************/
	/*Defines Monk Commands  */
	/*and initializes Monk   */
	/*Special stats and flags*/
	/*************************/
	ki=1;
	focused=false;
	/************************/
	/*Monks get Focus plus  */
	/*Attack and PC commands*/
	/************************/
	NPCcommands=2;
	commands[0]=new Attack;
	commands[1]=new Special_Ability("Focus");
	commands[2]=new Status;
	commands[3]=new Flee;
}
void Monk :: levelup(int levels)
{
	/************************************/
	/*Monks have the can Focus their Ki */
	/*their primary stat is strength    */
	/************************************/
	wounds=0;
	for(int i=0;i<levels;i++)
	{
		defense+=diceroll(6);
		hp+=diceroll(8);
		strength+=diceroll(8);
		agility+=diceroll(4);
		intelligence+=diceroll(4);
		level++;
		ki++;
	}
}
void Monk :: attack(RPGclass * target)
{
	/***************************************/
	/*Monk's attack is Strength vs. Defense*/
	/***************************************/
	bool hit = (strength+diceroll(20)) >= target->getDef() + target->diceroll(20);
	int damage;
	
	if(hit)
	{
		/*******************************/
		/*Damage also based on Strength*/
		/*******************************/
		damage = (strength/10)+diceroll(10);

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
void Monk :: specialAbility(RPGclass *target)
{
	cout<<characterName<<" focuses their Ki!"<<endl;
	focused=true;
}