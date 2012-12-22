#pragma once
#include "stdafx.h"

class Button
{
public:
	Button(std::string label,std::string name);
	virtual void Action()=0;
	sf::Sprite& GetSprite();
	std::string getLabel();
	bool Pressed();
	void Press(bool p);
	void Draw(sf::RenderWindow& window);
private :
	std::string Label;
	bool isPressed;
    sf::Sprite Sprite;
};