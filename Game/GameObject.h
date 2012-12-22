#pragma once
#include "stdafx.h"
class GameObject
{
public:
	GameObject();
	virtual ~GameObject();
	
	virtual void Load(std::string filename);
	virtual void Draw(sf::RenderWindow & window);
	virtual void Update(float elapsedTime);

	virtual bool IsLoaded() const;

protected:
	sf::Sprite  _sprite;
	sf::Image _image;
	std::string _filename;
	bool _isLoaded;
};

