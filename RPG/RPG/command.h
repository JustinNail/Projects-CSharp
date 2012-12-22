#ifndef GUARD_RPGcommand_h
#define GUARD_RPGcommand_h

#include <string>
#include <iostream>

template <class T>
class Command
{
public:
	Command(){name="";}
	void displayName(){cout<<name<<endl;}
	
	/***********************************/
	/*Return Codes:					   */
	/*	0 means target gets an action  */
	/*	1 means target doesn't act     */
	/*	2 means combat end immediately */
	/***********************************/

	virtual int execute(T *source, T *target)=0;

protected:
	std::string name;
};

#endif