#include <iostream>
#include <string>

using namespace std;

class animal{
public:
	animal(const string& name) : name(name){}
	virtual string talk() = 0;
	const string name;
};

class cat : public animal{
public:
	cat(const string& name) : animal(name){}
	virtual string talk(){return "meow";}
};

class dog : public animal{
public:
	dog(const string& name) : animal(name){}
	virtual string talk(){return "woof woof";}
};

int main()
{
	cat fiesty("Fiesty");
	cout<<fiesty.talk()<<endl;
	dog fido("Fido");
	cout<<fido.talk()<<endl;

	system("pause");
	
	return 0;
}