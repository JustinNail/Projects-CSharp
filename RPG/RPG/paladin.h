#ifndef GUARD_paladin_h
#define GUARD_paladin_h

#include "cleric.h"
#include "fighter.h"

class Paladin : public Fighter, public Cleric
{
public:
	Paladin(std::string name);
	void levelup(int levels);//level up for Paladin
	void attack(RPGclass *target);//Same attack method as Fighter
	void specialAbility(RPGclass *target);//same as cleric
};

#endif