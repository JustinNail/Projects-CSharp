#include <iostream>

using namespace std;

class fruit
{
public:
	//fruit(); //default constructer, same as not having anything, safer to include
	fruit(float,float);//new constructer, sets weight and price up front

	float cost();

	//gatekeeper methods
	void set_weight(float input);
	void set_price(float input);
private:
	float price_per_lb, weight;
};

fruit::fruit(float weightIn, float priceIn)
{
	weight=weightIn;
	price_per_lb=priceIn;
}

float fruit::cost()
{
	return (price_per_lb * weight);
}

void fruit::set_weight(float input)
{
	weight=input;
}

void fruit::set_price(float input)
{
	price_per_lb=input;
}

int main()
{
	//fruit apple;
	//apple.set_weight(.25);
	//apple.set_price(3.00);

	fruit orange(.25,3.00);
	cout<<"the price of the orange is $"<<orange.cost()<<endl;

	//cout<<"the price of the apple is $"<<apple.cost()<<endl;
	system("pause");

	return 0;
}