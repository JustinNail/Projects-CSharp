#include "cleric.h"
#include <iostream>

using namespace std;

Cleric::Cleric(string name):Caster(name),RPGclass(name)
{
	/*************************/
	/*Defines Cleric's Spells*/
	/*************************/
	spells[0]=new Heal;
	spells[1]=new Protect;
	spells[2]=new Holy;
}