#include "cleric_spells.h"
#include <iostream>

using namespace std;

Heal::Heal(){name="Heal";}
void Heal :: cast(RPGclass *source, RPGclass *target)
{
	/***********************/
	/*Heal reduces wounds  */
	/*based on intelligence*/
	/***********************/
	int amt=(source->getInt()/2)+source->diceroll(10);
	cout<<source->getName()<<" is healed for "<<amt<<endl;
	source->removedamage(amt);
}

Protect::Protect(){name="Protect";}
void Protect :: cast(RPGclass *source, RPGclass *target)
{
	cout<<source->getName()
		<<" is surrounded by a divine shield!"<<endl;
	source->setProtect(true);
}

Holy::Holy(){name="Holy";}
void Holy :: cast(RPGclass *source, RPGclass *target)
{
	/***************************************/
	/*Holy is Intelligence vs. Intelligence*/
	/***************************************/
	bool hit = (source->getInt()+source->diceroll(20)) >= target->getInt() + target->diceroll(20);
	if(hit)
	{
		/******************************/
		/*Damage is Intelligence Based*/
		/******************************/
		int damage = (source->getInt()/10)+source->diceroll(10);
		cout<<source->getName()<<" blasts "<<target->getName()<<" for "<<damage<<endl;
		target->takedamage(damage);
	}
	else
	{
		cout<<target->getName()<<" resists "<<source->getName()<<"'s "<<name<<endl;
	}
}


