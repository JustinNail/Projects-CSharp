#pragma once
#include "Button.h"

class ExitButton : public Button
{
public:
	ExitButton(std::string s,std::string n);
	virtual void Action();
};
class PlayButton : public Button
{
public:
	PlayButton(std::string s,std::string n);
	virtual void Action();
};
/*class AttackButton : public Button
{
public:
	ExitButton(std::string s);
	virtual void Action();
};
class FleeButton : public Button
{
public:
	ExitButton(std::string s);
	virtual void Action();
};
class StatusButton : public Button
{
public:
	ExitButton(std::string s);
	virtual void Action();
};*/