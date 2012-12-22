#include <SFML/Graphics.hpp>

class tile
{
public :
	void setTile(sf::Image &Image, int x, int y)
	{
		Sprite.SetImage(Image);
		Sprite.SetSubRect(sf::Rect<int>(x, y, x+16,y+16));
	}

	sf::Sprite getSprite()
	{
		return Sprite;
	}
	void SetPosition(float x, float y)
	{
		Sprite.SetPosition(x,y);
	}
	void SetX(float x)
	{
		Sprite.SetX(x);
	}
	void SetY(float y)
	{
		Sprite.SetY(y);
	}
	float GetX()
	{
		return Sprite.GetPosition().x;
	}
	float GetY()
	{
		return Sprite.GetPosition().y;
	}
private :
    sf::Sprite Sprite;
};

class RandomizedArea
{
public:
	RandomizedArea(int r, int c):row(r),col(c)
	{
		Area = new tile[r*c];
	}
	void fillArea(sf::Image &Image)
	{
		for(int i=0;i<row*col;i++)
		{
			Area[i].setTile(Image,sf::Randomizer::Random(0, 48),sf::Randomizer::Random(0, 48));
			if(i>0)
			{
				Area[i].SetX(Area[i-1].GetX()+16);
				Area[i].SetY(Area[i-1].GetY());
				if(i%col==0)
				{
					Area[i].SetX(0.f);
					Area[i].SetY(Area[i-1].GetY()+16);
				}
			}
		}
	}
	void DrawArea(sf::RenderWindow &window)
	{
		for(int i=0;i<row*col;i++)
		{
			window.Draw(Area[i].getSprite());
		}
	}
private:
	int row,col;
	tile* Area;
};

 int main()
 {
	 // Create the main window
     sf::RenderWindow App(sf::VideoMode(800, 600, 32), "SFML window");

	 sf::Image Image;
	 if (!Image.LoadFromFile("images/grassSet.bmp"))
	 {
		 system("pause");
         return EXIT_FAILURE;
	 }

	 RandomizedArea map(40,50);
	 map.fillArea(Image);

     // Load a sprite to display

	 while (App.IsOpened())
     {
         // Process events
         sf::Event Event;
         while (App.GetEvent(Event))
         {
             // Close window : exit
             if (Event.Type == sf::Event::Closed)
                 App.Close();
         }
	  // Clear screen
         App.Clear();
         // Draw the sprite
		 map.DrawArea(App);

         // Update the window
         App.Display();
     }
 
     return EXIT_SUCCESS;
 }