#include "MovableObject.h"


MovableObject::MovableObject() :
GameObject()
{
}


MovableObject::~MovableObject()
{
	
}
void MovableObject::SetPosition(float x, float y)
{
	if(_isLoaded)
	{
		_sprite.SetPosition(x,y);
	}
}

sf::Vector2f MovableObject::GetPosition() const
{
	if(_isLoaded)
	{
		return _sprite.GetPosition();
	}
	return sf::Vector2f();
}