#include "stdafx.h"
#include "ImageManager.h"
#include "Game.h"


ImageManager::ImageManager(){}
ImageManager::~ImageManager()
{
	images.clear();
}
void ImageManager::Add(std::string name, sf::Image image)
{
	//add an object to the object map
	images.insert(std::pair<std::string,sf::Image>(name,image));
}

void ImageManager::Remove(std::string name)
{
	//searches by name
	std::map<std::string, sf::Image>::iterator results = images.find(name);
	//if it finds it
	if(results != images.end() )
	{
		//erases it
		images.erase(results);
	}
}

const sf::Image& ImageManager::Get(std::string name)
{
	std::map<std::string, sf::Image>::const_iterator it;
	for( it = images.begin();it != images.end();++it)
	{
		if( name == it->first )
		{
			return it->second;
		}
	}
	// The image doesen't exists. Create it and save it.
	sf::Image image;

	// load it from filename
	if( image.LoadFromFile( name ) )
	{
		images[name] = image;
		return images[name];
	}
	else//can't find file
	{
		//return empty image
		images[name] = image;
		return images[name];
	}
}

int ImageManager::GetImageCount()
{
	//returns number of imagess
	return images.size();
}

std::map<std::string,sf::Image> ImageManager::images;