#include "StdAfx.h"
#include "SplashScreen.h"
#include "ImageManager.h"

void SplashScreen::Show(sf::RenderWindow & renderWindow)
{

	sf::Sprite sprite(ImageManager::Get("Images/SplashScreen.png"));
	
	renderWindow.Draw(sprite);
	renderWindow.Display();

	sf::Event currentevent;
	while(true)
	{
		renderWindow.GetEvent(currentevent);
		if(currentevent.Type == sf::Event::KeyPressed 
			|| currentevent.Type == sf::Event::MouseButtonPressed
			|| currentevent.Type == sf::Event::Closed )
		{
			return;
		}
	}
}