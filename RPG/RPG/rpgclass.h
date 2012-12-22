#ifndef GUARD_RPGclass_h
#define GUARD_RPGclass_h

#include "command.h"

class RPGclass
{
public:
	RPGclass(std::string name);//constructor
	void status();//prints out the stats for the creature
	int diceroll(int dice);//the creature roll a particular die

	bool isAlive();//returns true if wounds < HP
	int getLevel();//returns the creature's level
	int getDef();//returns the creature's defense
	int getStr();//returns the creature's strength
	int getAg();//returns the creature's agility
	int getInt();//returns the creature's intelligence
	std::string getName();//returns the creature's name
	int getNPCcommands();

	void setPC(bool pc);//sets if creature is a PC or NPC
	void setPoison(bool p);//sets poisoned variable
	void setBlink(bool p);//sets blinking variable
	void setProtect(bool p);//sets isProtected variable 

	bool getPC();//gets if creature is a PC or NPC
	bool getPoison();//gets poisoned variable
	bool getBlink();//gets blinking variable
	bool getProtect();//gets isProtected variable 

	
	void removedamage(int amt);//method for healing damage
	virtual void takedamage(int amt);//method for applying damage
	virtual int doCommand(int choice,RPGclass *target);//executes a command
	virtual void specialAbility(RPGclass *target);

	virtual void levelup(int levels) = 0;//method for leveling up
	virtual void attack(RPGclass *target) = 0;//attack method
	void PrintCommands();//prints what PC can do


protected:
	//character's Statistics
	std::string characterName;//the character's name, set with constructor
	int defense;
	int wounds;
	int hp;
	int strength;
	int agility;
	int intelligence;
	int level;

	int NPCcommands;//number of commands an NPC could do

	//command array
	Command<RPGclass> *commands[4];

	//character flags
	bool PC;//true if object is a PC
	bool poisoned;//true if creature is poisoned
	bool blinking;//true if character affected by Blink Spell
	bool isProtected;//true if character affected by Protect Spell
};
#endif