#pragma once
#include "stdafx.h"
#include "Button.h"

class Menu
{

public:	
	void SetPosition(float x, float y);
	void Update(sf::RenderWindow& window);
	void GetMenuResponse(sf::RenderWindow& window);
protected:
	std::vector<Button*> menuButtons;
	int spacing;
private:
	Button* getButton(int x,int y);
};

