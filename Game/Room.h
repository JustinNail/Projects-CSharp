#pragma once
#include "AreaList.h"
#include "GameObject.h"

class Room:public GameObject
{
public:
	void Add(GameObject* object);
	void Draw(sf::RenderWindow & window);
protected:
	std::vector<GameObject*> room;
};