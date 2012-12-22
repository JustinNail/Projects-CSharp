#include "Button.h"
#include "ImageManager.h"

Button::Button(std::string label, std::string name) : Label(label), isPressed(false)
{
	Sprite.SetImage(ImageManager::Get(name));
}

sf::Sprite& Button::GetSprite()
{
	return Sprite;
}
std::string Button::getLabel()
{
	return Label;
}
bool Button::Pressed()
{
	return isPressed;
}
void Button::Press(bool p)
{
	isPressed=p;
}
void Button::Draw(sf::RenderWindow& window)
{
	if(isPressed)
	{
		Sprite.SetSubRect(sf::Rect<int>(0,108,216,216));
	}
	else
	{
		Sprite.SetSubRect(sf::Rect<int>(0,0,216,108));
	}
	sf::String text(Label);
	text.SetPosition(Sprite.GetPosition().x,Sprite.GetPosition().y);
	window.Draw(Sprite);
	window.Draw(text);
}