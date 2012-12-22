#include "Area.h"

Area::Area(int x, int y, int w, int h):X(x),Y(y),Width(w),Height(h)
{
	area = new Tile[w*h];
}
void Area::Draw(sf::RenderWindow &window)
{
	for(int i=0;i<Width*Height;i++)
	{
		window.Draw(area[i].getSprite());
	}
}
bool Area::isPassable()
{
	return passable;
}
int Area::GetX()
{
	return X;
}
int Area::GetY()
{
	return Y;
}