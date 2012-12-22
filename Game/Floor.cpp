#include "Floor.h"
#include "ImageManager.h"

Floor::Floor(int x, int y, int width, int height):Area(x,y,width,height)
{
	passable=true;
}

void Floor::Load(std::string filename)
{
	const sf::Image& image=ImageManager::Get(filename);
	int x=X;
	int y=Y;
	for(int i=0;i<Width*Height;i++)
	{
		area[i].setTile(image,0,0);
		if(i>0)
		{
			if(i % Width==0)
			{
				x=X;
				y+=16;
			}
			else
			{
				x+=16;
			}
		}
		area[i].SetX(x);
		area[i].SetY(y);
	}
}