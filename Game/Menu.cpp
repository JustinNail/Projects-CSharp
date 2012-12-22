#include "Menu.h"


void Menu::SetPosition(float x, float y)
{
	for(unsigned int i=0;i<menuButtons.size();i++)
	{
		menuButtons[i]->GetSprite().SetPosition(sf::Vector2f(x,y));
		x+=spacing;
	}
}
void Menu::Update(sf::RenderWindow& window)
{
	for(unsigned int i=0;i<menuButtons.size();i++)
	{
		menuButtons[i]->Draw(window);
	}
}
Button* Menu::getButton(int x, int y)
{
	for(unsigned int i=0;i<menuButtons.size();i++)
	{
		if(
			(x>menuButtons[i]->GetSprite().GetPosition().x) && 
			(x<(menuButtons[i]->GetSprite().GetPosition().x) + (menuButtons[i]->GetSprite().GetSize().x)) &&
			((y>menuButtons[i]->GetSprite().GetPosition().y) && 
			(y<(menuButtons[i]->GetSprite().GetPosition().y) + (menuButtons[i]->GetSprite().GetSize().y)))
		  )
		{
			return menuButtons[i];
		}
	}
	return NULL;
}
void  Menu::GetMenuResponse(sf::RenderWindow& window)
{
	const sf::Input& input = window.GetInput();
	sf::Event currentEvent;
	window.GetEvent(currentEvent);
	Button* button = getButton(input.GetMouseX(),input.GetMouseY());
	while(input.IsMouseButtonDown(sf::Mouse::Left))
	{
		window.GetEvent(currentEvent);
		if(button!=NULL)
		{
			button->Press(true);
			if(!(input.IsMouseButtonDown(sf::Mouse::Left)) && button->Pressed())
			{
				button->Press(false);
				button->Action();
			}
		}
		window.Clear(sf::Color(0,0,0));
		Update(window);
		window.Display();
	}
	
	window.Clear(sf::Color(0,0,0));
	Update(window);
	window.Display();
}