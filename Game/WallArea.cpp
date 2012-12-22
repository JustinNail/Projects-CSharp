#include "WallArea.h"

WallArea::WallArea(int x, int y, int length):Area(x,y,1,length),NorthSouth(true)
{
	passable=false;
}

void WallArea::setNorthSouth(){NorthSouth=true;}
void WallArea::setEastWest(){NorthSouth=false;}

void WallArea::Load(std::string filename)
{
	const sf::Image& image = ImageManager::Get(filename);
	int x=X;
	int y=Y;
	for(int i=0; i<Height; i++)
	{
		area[i].setTile(image,0,0);
		area[i].SetX(x);
		area[i].SetY(y);
		if(NorthSouth)
		{
			y+=16;
		}
		else
		{
			x+=16;
		}
	}
}