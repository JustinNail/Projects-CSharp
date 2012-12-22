#ifndef GUARD_monster_h
#define GUARD_monster_h

#include "default_commands.h"

class Monster : virtual public RPGclass
{
public:
	Monster(std::string name);
	void levelup(int levels);//level up the Monster
	void attack(RPGclass *target);//method for attacking
private:
};

#endif