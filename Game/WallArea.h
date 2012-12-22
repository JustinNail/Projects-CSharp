#pragma once
#include "stdafx.h"
#include "Area.h"
#include "ImageManager.h"

class WallArea : public Area
{
public:
	WallArea(int x, int y, int length);
	void Load(std::string filename);
	void setNorthSouth();
	void setEastWest();
private:
	bool NorthSouth;
};