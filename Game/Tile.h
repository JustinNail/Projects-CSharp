#pragma once
#include "stdafx.h"

class Tile
{
public :
	void setTile(const sf::Image &Image, int x, int y);
	sf::Sprite& getSprite();
	void SetPosition(float x, float y);
	void SetX(float x);
	void SetY(float y);
	float GetX();
	float GetY();
private :
    sf::Sprite Sprite;
};