#ifndef GUARD_fighter_h
#define GUARD_fighter_h

#include "default_commands.h"

class Fighter : virtual public RPGclass
{
public:
	Fighter(std::string name);
	void levelup(int levels);//level up for Fighter
	void attack(RPGclass *target);//attack method for Fighter
};
#endif