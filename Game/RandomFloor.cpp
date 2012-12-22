#include "RandomFloor.h"
#include "ImageManager.h"

RandomFloor::RandomFloor(int x, int y, int width, int height):Floor(x,y,width,height){}
void RandomFloor::Load(std::string filename)
{
	const sf::Image& image=ImageManager::Get(filename);
	int x=X;
	int y=Y;
	for(int i=0;i<Width*Height;i++)
	{
		area[i].setTile(image,sf::Randomizer::Random(0, 48),sf::Randomizer::Random(0, 48));
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