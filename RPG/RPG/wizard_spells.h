#ifndef GUARD_wizard_spells_h
#define GUARD_wizard_spells_h

#include "spell.h"

class Fireball : public Spell
{
public:
	Fireball();
	void cast(RPGclass *source, RPGclass *target);
};
class Blink : public Spell
{
public:
	Blink();
	void cast(RPGclass *source, RPGclass *target);
};
class Poison : public Spell
{
public:
	Poison();
	void cast(RPGclass *source, RPGclass *target);
};

#endif