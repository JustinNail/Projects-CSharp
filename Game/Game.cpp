#include "stdafx.h"
#include "Game.h"
#include "MainMenu.h"
#include "SplashScreen.h"

void Game::Start(void)
{
	if(_gameState != Uninitialized)
		return;
	
	_mainWindow.Create(sf::VideoMode(SCREEN_WIDTH,SCREEN_HEIGHT,32),"RPG");
	
	_mainWindow.SetFramerateLimit(60);

	Room* test = new Room;

	Floor* floor = new Floor(0,0,33,25);
	floor->Load("Images/StoneTile.png");

	WallArea* Nwall = new WallArea(floor->GetX()+16,floor->GetY(),32);
	Nwall->setEastWest();
	Nwall->Load("Images/WallTile.png");
	
	WallArea* Swall = new WallArea(floor->GetX(),floor->GetY()+(24*TILE_HEIGHT),32);
	Swall->setEastWest();
	Swall->Load("Images/WallTile.png");

	WallArea* Ewall = new WallArea(floor->GetX()+(32*TILE_WIDTH),floor->GetY()+16,24);
	Ewall->Load("Images/WallTile.png");

	WallArea* Wwall = new WallArea(floor->GetX(),floor->GetY(),24);
	Wwall->Load("Images/WallTile.png");

	test->Add(floor);
	test->Add(Nwall);
	test->Add(Swall);
	test->Add(Ewall);
	test->Add(Wwall);

	_gameObjectManager.Add("test",test);
	_gameState= Game::ShowingSplash;


	while(!IsExiting())
	{
		GameLoop();
	}

	_mainWindow.Close();
}

bool Game::IsExiting()
{
	if(_gameState == Game::Exiting) 
		return true;
	else 
		return false;
}

sf::RenderWindow& Game::GetWindow()
{
	return _mainWindow;
}

const sf::Input& Game::GetInput() 
{
	return _mainWindow.GetInput();
}

void Game::GameLoop()
{
	sf::Event currentEvent;
	switch(_gameState)
	{
		case Game::ShowingMenu:
			{
				ShowMenu();
				break;
			}
		case Game::ShowingSplash:
			{
				ShowSplashScreen();
				break;
			}
		case Game::Playing:
			{
				_mainWindow.GetEvent(currentEvent);
				_mainWindow.Clear(sf::Color(0,0,0));

				GameObject* test=_gameObjectManager.Get("test");


				_gameObjectManager.UpdateAll();
				//_gameObjectManager.DrawAll(_mainWindow);
				test->Draw(_mainWindow);

				_mainWindow.Display();
				if(currentEvent.Type == sf::Event::Closed) _gameState = Game::Exiting;

				if(currentEvent.Type == sf::Event::KeyPressed)
					{
						if(currentEvent.Key.Code == sf::Key::Escape) ShowMenu();
					}

				break;
			}
	}
}

void Game::ShowSplashScreen()
{
	SplashScreen splashScreen;
	splashScreen.Show(_mainWindow);
	_mainWindow.Clear(sf::Color(0,0,0));
	_gameState = Game::ShowingMenu;
}

void Game::ShowMenu()
{
	MainMenu mainMenu;
	mainMenu.SetPosition(296,384);
	mainMenu.GetMenuResponse(_mainWindow);
}

void Game::setGameState(GameState i)
{
	_gameState=i;
}

Game::GameState Game::_gameState = Uninitialized;
sf::RenderWindow Game::_mainWindow;
GameObjectManager Game::_gameObjectManager;
