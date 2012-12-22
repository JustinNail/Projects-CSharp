#ifndef GUARD_cleirc_spells_h
#define GUARD_cleric_spells_h

#include "spell.h"

class Heal : public Spell
{
public:
	Heal();
	void cast(RPGclass *source, RPGclass *target);
};
class Protect : public Spell
{
public:
	Protect();
	void cast(RPGclass *source, RPGclass *target);
};
class Holy : public Spell
{
public:
	Holy();
	void cast(RPGclass *source, RPGclass *target);
};

#endif