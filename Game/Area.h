#pragma once
#include "stdafx.h"
#include "Tile.h"
#include "GameObject.h"

class Area:public GameObject
{
public:
	Area(int x, int y, int w, int h);
	virtual void Load(std::string filename)=0;
	void Draw(sf::RenderWindow &window);
	bool isPassable();
	int GetX();
	int GetY();
protected:
	int Width, Height, X, Y;
	Tile* area;
	bool passable;
};