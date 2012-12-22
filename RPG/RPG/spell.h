#ifndef GUARD_spell_h
#define GUARD_spell_h

#include "rpgclass.h"

class Spell
{
public:
	Spell();
	void displayName();
	virtual void cast(RPGclass *source, RPGclass *target)=0;

protected:
	std::string name;
};

#endif