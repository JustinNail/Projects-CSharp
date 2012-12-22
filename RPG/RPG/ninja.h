#ifndef GUARD_ninja_h
#define GUARD_ninja_h

#include "monk.h"
#include "thief.h"

class Ninja:public Thief, public Monk
{
public:
	Ninja(std::string name);
	void levelup(int levels);//Level up for Ninjas
	void attack(RPGclass *target);//Ninja's attack method
	void specialAbility(RPGclass *target);//same as Monk
};

#endif