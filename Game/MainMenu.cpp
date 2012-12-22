#include "MainMenu.h"
#include "ButtonList.h"

MainMenu::MainMenu()
{
	spacing=216;
	menuButtons.push_back(new PlayButton("Play Game","Images/Button.png"));
	menuButtons.push_back(new ExitButton("Exit Game","Images/Button.png"));
}