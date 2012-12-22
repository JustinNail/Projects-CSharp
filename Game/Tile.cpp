#include "Tile.h"
void Tile::setTile(const sf::Image &Image, int x, int y)
{
	Sprite.SetImage(Image);
	Sprite.SetSubRect(sf::Rect<int>(x, y, x+16,y+16));
}
sf::Sprite& Tile::getSprite()
{
	return Sprite;
}
void Tile::SetPosition(float x, float y)
{
	Sprite.SetPosition(x,y);
}
void Tile::SetX(float x)
{
	Sprite.SetX(x);
}
void Tile::SetY(float y)
{
	Sprite.SetY(y);
}
float Tile::GetX()
{
	return Sprite.GetPosition().x;
}
float Tile::GetY()
{
	return Sprite.GetPosition().y;
}