#ifndef GUARD_monk_h
#define GUARD_monk_h

#include "default_commands.h"

class Monk : virtual public RPGclass
{
public:
	Monk(std::string name);
	void levelup(int levels);//level up for Monks
	void attack(RPGclass *target);//Monk's attack method
	void specialAbility(RPGclass *target);
protected:
	int ki;
	bool focused;
};

#endif