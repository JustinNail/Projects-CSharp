#ifndef GUARD_thief_h
#define GUARD_thief_h

#include "default_commands.h"

class Thief : virtual public RPGclass
{
public:
	Thief(std::string name);
	void levelup(int levels);//level up for Thieves
	void attack(RPGclass *target);//Thieves attack method
protected:
	int sneakAttack;
};


#endif