#include "ButtonList.h"
#include "Menu.h"
#include "Game.h"

ExitButton::ExitButton(std::string s,std::string n):Button(s,n){}
void ExitButton::Action()
{
	Game::setGameState(Game::Exiting);
}

PlayButton::PlayButton(std::string s,std::string n):Button(s,n){}
void PlayButton :: Action()
{
	Game::setGameState(Game::Playing);
}