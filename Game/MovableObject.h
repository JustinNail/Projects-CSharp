#pragma once
#include "stdafx.h"
#include "GameObject.h"

class MovableObject:public GameObject
{
public:
	MovableObject();
	virtual ~MovableObject();
	
	virtual void SetPosition(float x, float y);
	virtual sf::Vector2f GetPosition() const;
};