#pragma once
#include "stdafx.h"
#include "Area.h"

class Floor : public Area
{
public:
	Floor(int x, int y, int width, int height);
	virtual void Load (std::string filename);
};