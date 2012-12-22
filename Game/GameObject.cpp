#include "GameObject.h"


GameObject::GameObject() :
_isLoaded(false)
{
}


GameObject::~GameObject()
{
	
}

void GameObject::Load(std::string filename)
{
	if(_image.LoadFromFile(filename) == false)
	{
		_filename =  "";
		_isLoaded = false;
	}
	else
	{
		_filename = filename;
		_sprite.SetImage(_image);
		_isLoaded = true;
	}
}

void GameObject::Draw(sf::RenderWindow & renderWindow)
{
	if(_isLoaded)
	{
		renderWindow.Draw(_sprite);
	}
}


void GameObject::Update(float elapsedTime)
{
}

bool GameObject::IsLoaded() const
{
	return _isLoaded;
}