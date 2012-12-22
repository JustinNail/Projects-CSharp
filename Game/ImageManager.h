#pragma once
#include "stdafx.h"

class ImageManager
{
public:	
	ImageManager();
	~ImageManager();

	static void Add(std::string name, sf::Image image);
	static void Remove(std::string name);
	static int GetImageCount();
	static const sf::Image& Get(std::string name);

private:
	static std::map<std::string, sf::Image> images;
};