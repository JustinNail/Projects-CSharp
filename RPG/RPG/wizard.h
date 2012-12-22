#ifndef GUARD_wizard_h
#define GUARD_wizard_h

#include "caster.h"
#include "wizard_spells.h"

class Wizard : virtual public Caster
{
public:
	Wizard(std::string name);
};

#endif