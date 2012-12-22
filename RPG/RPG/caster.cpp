#include "caster.h"
#include <iostream>

using namespace std;

Caster::Caster(string name):RPGclass(name)
{
	/**********************/
	/*All casters have the*/
	/*same commands:	  */
	/*	Cast plus attack  */
	/*	and PC commands	  */
	/**********************/
	NPCcommands=2;
	commands[0]=new Attack;
	commands[1]=new Special_Ability("Cast");
	commands[2]=new Status;
	commands[3]=new Flee;
}
void Caster :: levelup(int levels)
{
	/************************************/
	/*Casters can Cast spells	        */
	/*their primary stat is intelligence*/
	/************************************/
	wounds=0;
	for(int i=0;i<levels;i++)
	{
		defense+=diceroll(6);
		hp+=diceroll(6);
		strength+=diceroll(4);
		agility+=diceroll(4);
		intelligence+=diceroll(8);
		level++;
	}
}
void Caster :: attack(RPGclass * target)
{
	/*********************************************/
	/*Caster's attack is Intelligence vs. Defense*/
	/*********************************************/
	bool hit = (intelligence+diceroll(20)) >= target->getDef() + target->diceroll(20);
	int damage;
	if(hit)
	{
		/******************************************/
		/*Damage is intelligence based as well    */ 
		/*Casters roll smaller die for base attack*/
		/******************************************/
		damage = (intelligence/10)+diceroll(4);
		cout<<characterName<<" hits "<<target->getName()<<" for "<<damage<<endl;
		target->takedamage(damage);
	}
	else
	{
		cout<<target->getName()<<" evades "<<characterName<<"'s attack"<<endl;
	}
}
void Caster :: specialAbility(RPGclass *target)
{
	int spell;
	/******************************/
	/*Gets spell from user if PC  */
	/*Otherwise randomly picks one*/
	/******************************/
	if(PC)//PC
	{
		cout<<"Which Spell?"<<endl;
		for(int i=0;i<3;i++)//prints spells
		{
			cout<<"\t"<<i<<". ";
			spells[i]->displayName();
			cout<<endl;
		}

		//Loop to ensure a valid selection
		while(true)
		{
			cin>>spell;
			if(spell<3 && spell>=0)
				break;//breaks out of loop if valid
			cout<<"Invalid Selection"<<endl;
		}
	}
	else//NPC
		spell=(rand()%3);//Randomly picks a spell

	cout<<characterName<<" casts ";
	spells[spell]->displayName();
	cout<<"!"<<endl;

	spells[spell]->cast(this,target);
}
