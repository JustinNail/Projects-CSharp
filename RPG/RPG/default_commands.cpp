#include "default_commands.h"
#include <iostream>

using namespace std;

Attack::Attack(){name="Attack";}
int Attack :: execute(RPGclass *source, RPGclass *target)
{
	source->attack(target);
	return 0;
}

Special_Ability::Special_Ability(string s){name=s;}
int Special_Ability::execute(RPGclass *source, RPGclass *target)
{
	source->specialAbility(target);
	return 0;
}

Status::Status(){name="Status";}
int Status :: execute(RPGclass *source, RPGclass *target)
{
	source->status();
	cout<<endl;
	target->status();
	return 1;
}

Flee::Flee(){name="Flee";}
int Flee :: execute(RPGclass *source, RPGclass *target)
{
	cout<<source->getName()<<" flees the combat, the coward!"<<endl;
	return 2;
}