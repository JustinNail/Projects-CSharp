#include "wizard_spells.h"
#include <iostream>

using namespace std;

Fireball::Fireball(){name="Fireball";}
void Fireball :: cast(RPGclass *source, RPGclass *target)
{
	/**************************************/
	/*Fireball is Intelligence vs. Agility*/
	/**************************************/
	bool hit = (source->getInt()+ source->diceroll(20)) >= target->getAg() + target->diceroll(20);
	if(hit)
	{
		/******************************/
		/*Damage is intelligence based*/
		/******************************/
		int damage = (source->getInt()/10)+source->diceroll(10);
		cout<<source->getName()<<" blasts "<<target->getName()<<" for "<<damage<<endl;
		target->takedamage(damage);
	}
	else
	{
		cout<<target->getName()<<" evades "<<source->getName()<<"'s "<<name<<endl;
	}
}

Blink::Blink(){name="Blink";}
void Blink :: cast(RPGclass *source, RPGclass *target)
{
	cout<<source->getName()
		<<" begins blinking in and out of exsistance!"<<endl;
	source->setBlink(true);
}

Poison::Poison(){name="Poison";}
void Poison :: cast(RPGclass *source, RPGclass *target)
{
	/*************************************/
	/*Poison is Intelligence vs. Strength*/
	/*************************************/
	bool hit = (source->getInt()+source->diceroll(20)) >= target->getStr() + target->diceroll(20);
	if(hit)
	{
		/****************************************/
		/*Poison does damage based on targets HP*/
		/****************************************/
		cout<<"Poison permeates"<<target->getName()<<"'s body!"<<endl;
		target->setPoison(true);
	}
	else
	{
		cout<<target->getName()<<" resists "<<source->getName()<<"'s "<<name<<endl;
	}
}
