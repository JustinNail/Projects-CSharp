#ifndef GUARD_caster_h
#define GUARD_caster_h

#include "default_commands.h"
#include "spell.h"

class Caster : virtual public RPGclass
{
	/****************************/
	/*base class for Casters*/
	/****************************/
public:
	Caster(std::string name);
	void levelup(int levels);//Casters all follow the same level up schemes
	void attack(RPGclass *target);//All caster have the same base attack 
	void specialAbility(RPGclass *target);//method for Casting spells

protected:
	Spell *spells[3];
};

#endif