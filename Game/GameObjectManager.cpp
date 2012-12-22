#include "GameObjectManager.h"
#include "Game.h"

GameObjectManager::GameObjectManager()//default constructor
{
}

GameObjectManager::~GameObjectManager()//destructor
{
	//"Calls" GameObjectDeallocator for each object
	std::for_each(_gameObjects.begin(),_gameObjects.end(),GameObjectDeallocator());
}

void GameObjectManager::Add(std::string name, GameObject* gameObject)
{
	//add an object to the object map
	_gameObjects.insert(std::pair<std::string,GameObject*>(name,gameObject));
}

void GameObjectManager::Remove(std::string name)
{
	//searches by name
	std::map<std::string, GameObject*>::iterator results = _gameObjects.find(name);
	//if it finds it
	if(results != _gameObjects.end() )
	{
		//deletes both parts of map
		delete results->second;
		_gameObjects.erase(results);
	}
}

GameObject* GameObjectManager::Get(std::string name) const
{
	//searches by name
	std::map<std::string, GameObject*>::const_iterator results = _gameObjects.find(name);
	//returns NULL if doesn't exsist
	if(results == _gameObjects.end() )
		return NULL;
	//returns the object at name
	return results->second;
}

int GameObjectManager::GetObjectCount() const
{
	//returns number of objects
	return _gameObjects.size();
}


void GameObjectManager::DrawAll(sf::RenderWindow& renderWindow)
{
	//draws all the objects
	std::map<std::string,GameObject*>::const_iterator itr = _gameObjects.begin();
	while(itr != _gameObjects.end())
	{
		//calls each objects Draw function
		itr->second->Draw(renderWindow);
		itr++;
	}
}

void GameObjectManager::UpdateAll()
{
	//updates all the objects
	std::map<std::string,GameObject*>::const_iterator itr = _gameObjects.begin();
	//get time thats past
	float timeDelta = Game::GetWindow().GetFrameTime();

	while(itr != _gameObjects.end())
	{
		//calls each objects update function
		itr->second->Update(timeDelta);
		itr++;
	}
	
}