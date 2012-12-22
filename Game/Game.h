#pragma once
#include "stdafx.h"
#include "Room.h"
#include "GameObjectManager.h"
#include "ImageManager.h"

class Game
{
public:
	static void Start();
	static sf::RenderWindow& GetWindow();
	const static sf::Input& GetInput();
	const static int SCREEN_WIDTH = 1024;
	const static int SCREEN_HEIGHT = 768;
	const static int TILE_WIDTH = 16;
	const static int TILE_HEIGHT = 16;

	enum GameState { Uninitialized, ShowingSplash, Paused, 
					ShowingMenu, Playing, Exiting };

	static void setGameState(GameState i);

private:
	static bool IsExiting();
	static void GameLoop();
	
	static void ShowSplashScreen();
	static void ShowMenu();

	static GameState _gameState;
	static sf::RenderWindow _mainWindow;
	
	static GameObjectManager _gameObjectManager;
};

