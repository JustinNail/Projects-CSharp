#pragma once
#include "stdafx.h"
#include "Floor.h"

class RandomFloor: public Floor
{
public:
	RandomFloor(int x, int y, int width, int height);
	virtual void Load(std::string filename);
};