#ifndef GUARD_default_command_h
#define GUARD_default_command_h

#include "rpgclass.h"

class Attack : virtual public Command<RPGclass>
{
public:
	Attack();
	int execute(RPGclass *source, RPGclass *target);
};
class Special_Ability : virtual public Command<RPGclass>
{
public:
	Special_Ability(std::string s);
	int execute(RPGclass *source, RPGclass *target);
};
class Status : virtual public Command<RPGclass>
{
public:
	Status();
	int execute(RPGclass *source, RPGclass *target);
};
class Flee : virtual public Command<RPGclass>
{
public:
	Flee();
	int execute(RPGclass *source, RPGclass *target);
};

#endif