#include "wizard.h"
#include <iostream>

using namespace std;

Wizard::Wizard(string name):Caster(name),RPGclass(name)
{
	/*************************/
	/*Defines Wizard's Spells*/
	/*************************/
	spells[0]=new Fireball;
	spells[1]=new Blink;
	spells[2]=new Poison;
}