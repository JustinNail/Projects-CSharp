#include "paladin.h"
#include <iostream>

using namespace std;

Paladin::Paladin(string name):Fighter(name),Cleric(name),Caster(name),RPGclass(name)
{
	NPCcommands=2;
	commands[0]=new Attack;
	commands[1]=new Special_Ability("Cast");
	commands[2]=new Status;
	commands[3]=new Flee;
}
void Paladin :: levelup(int levels)
{
	/********************************************/
	/*Paladins are a mix of Fighters and Clerics*/
	/*their primary stat is strength			*/
	/********************************************/
	wounds=0;
	for(int i=0;i<levels;i++)
	{
		defense+=diceroll(6);
		hp+=diceroll(6);
		strength+=diceroll(8);
		agility+=diceroll(4);
		intelligence+=diceroll(4);
		level++;
	}
}
void Paladin::attack(RPGclass *target){Fighter::attack(target);}//Same attack method as Fighter
void Paladin::specialAbility(RPGclass *target){Cleric::specialAbility(target);}//Same as Cleric