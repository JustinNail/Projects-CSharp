#include "Room.h"

void Room::Add(GameObject* object)
{
	room.push_back(object);
}
void Room::Draw(sf::RenderWindow& window)
{
	for(int i=0;i<room.size();i++)
	{
		room[i]->Draw(window);
	}
}