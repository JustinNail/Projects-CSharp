#ifndef GUARD_cleric_h
#define GUARD_cleric_h

#include "caster.h"
#include "cleric_spells.h"

class Cleric : virtual public Caster
{
public:
	Cleric(std::string name);
};

#endif