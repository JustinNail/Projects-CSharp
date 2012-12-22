#include<iostream>
#include<ctime>
using namespace std;

/*Abstract API, defines interface for interacting
	with the actual maker*/
class CoffeeMakerAPI 
{
public:
	/*Emunerated Types for various states and statuses*/
	enum WarmerPlateStatus { potNotEmpty, potEmpty, warmerEmpty };
	enum BoilerStatus { boilerEmpty, boilerNotEmpty };
	enum BrewButtonStatus { brewButtonPushed, brewButtonNotPushed };
	enum BoilerState { boilerOn, boilerOff };
	enum WarmerState { warmerOn, warmerOff };
	enum IndicatorState { indicatorOn, indicatorOff, flashing };
	
	/*Functions for getting what the state of status of 
		a machine component is*/
	virtual WarmerPlateStatus GetWarmerPlateStatus () = 0;
	virtual BoilerStatus GetBoilerStatus () = 0;
	virtual BrewButtonStatus GetBrewButtonStatus () = 0;
	virtual BoilerState GetBoilerState ()= 0;
	virtual WarmerState GetWarmerState () = 0;
	virtual IndicatorState GetIndicatorState () = 0;

	/*Functions for setting the state or status of a
		component*/
	virtual void SetWarmerPlateStatus (WarmerPlateStatus s) = 0;
	virtual void SetBoilerStatus (BoilerStatus s) = 0;
	virtual void SetBrewButtonStatus (BrewButtonStatus s) = 0;
	virtual void SetBoilerState (BoilerState s) = 0;
	virtual void SetWarmerState (WarmerState s) = 0;
	virtual void SetIndicatorState (IndicatorState s) = 0;
};

/*Specific API for the Mk4, with the functions all defined*/
class Mk4API : public CoffeeMakerAPI
{
public:
	Mk4API()/*Set initial state of machine*/
	{
		plateSensor=potEmpty;
		waterSensor=boilerEmpty;
		button=brewButtonNotPushed;
		boiler=boilerOff;
		warmer=warmerOff;
		light=indicatorOff;
		Verbose=false;
	}
	bool Verbose;/*Used to display state changes to screen*/

	/*Returns current state of machine parts*/
	WarmerPlateStatus GetWarmerPlateStatus (){return plateSensor;}
	BoilerStatus GetBoilerStatus (){return waterSensor;}
	BrewButtonStatus GetBrewButtonStatus (){return button;}
	BoilerState GetBoilerState (){return boiler;}
	WarmerState GetWarmerState (){return warmer;}
	IndicatorState GetIndicatorState (){return light;}

	/*Sets the states and prints to screen, if Verbose*/
	void SetWarmerPlateStatus (WarmerPlateStatus s)
	{
		plateSensor=s;
		if(Verbose)
		{
			cout<<"Warmer Plate Status: ";
			switch(plateSensor)
			{
			case potNotEmpty:
				cout<<"potNotEmpty"<<endl;
				break;
			case potEmpty: 
				cout<<"potEmpty"<<endl;
				break;
			case warmerEmpty:
				cout<<"warmerEmpty"<<endl;
				break;
			}
		}
	}
	void SetBoilerStatus (BoilerStatus s)
	{
		waterSensor=s;
		if(Verbose)
		{
			cout<<"Boiler Status: ";
			switch(waterSensor)
			{
			case boilerEmpty:
				cout<<"boilerEmpty"<<endl;
				break;
			case boilerNotEmpty: 
				cout<<"boilerNotEmpty"<<endl;
				break;
			}
		}
	}
	void SetBrewButtonStatus (BrewButtonStatus s)
	{
		button=s;
		if(Verbose)
		{
			cout<<"Brew Button Status: ";
			switch(button)
			{
			case brewButtonNotPushed:
				cout<<"brewButtonNotPushed"<<endl;
				break;
			case brewButtonPushed: 
				cout<<"brewButtonPushed"<<endl;
				break;
			}
		}
	}
	void SetBoilerState (BoilerState s)
	{
		boiler=s;
		if(Verbose)
		{
			cout<<"Boiler State: ";
			switch(boiler)
			{
			case boilerOn:
				cout<<"boilerOn"<<endl;
				break;
			case boilerOff: 
				cout<<"boilerOff"<<endl;
				break;
			}
		}
	}
	void SetWarmerState (WarmerState s)
	{
		warmer=s;
		if(Verbose)
		{
			cout<<"Warmer State: ";
			switch(warmer)
			{
			case warmerOn:
				cout<<"warmerOn"<<endl;
				break;
			case warmerOff: 
				cout<<"warmerOff"<<endl;
				break;
			}
		}
	}
	void SetIndicatorState (IndicatorState s)
	{
		light=s;

		if(Verbose)
		{
			cout<<"Indicator State: ";
			switch(light)
			{
			case indicatorOn:
				cout<<"indicatorOn"<<endl;
				break;
			case indicatorOff: 
				cout<<"indicatorOff"<<endl;
				break;
			case flashing:
				cout<<"flashing"<<endl;
				break;
			}
		}
	}

private:
	/*The various machine parts*/
	WarmerPlateStatus plateSensor;
	BoilerStatus waterSensor;
	BrewButtonStatus button;
	BoilerState boiler;
	WarmerState warmer;
	IndicatorState light;
};

/*Abstract UI class*/
class UserInterface
{
public:
	/*Starts brewing*/
	virtual void start()=0;
	/*Does what needs to be done when brewing
		is finished*/
	virtual void done()=0;
};
/*Abstract Hot Water Source class*/
class HotWaterSource
{
public:
	/*switch for whether or not the brewing
		process was interrupted*/
	bool interrupted;
	/*starts the HWS*/
	virtual void start()=0;
	/*returns if HWS is ready*/
	virtual bool isReady()=0;
	/*Does what needs to be done when brewing 
		is finished*/
	virtual void done()=0;
	/*Does what needs to be done when brewing 
		is interrupted*/
	virtual void pause()=0;
	/*resumes brewing*/
	virtual void resume()=0;
protected:
	/*Switch for if HWS is ready or not*/
	bool ready;
	/*Switch for whether or not coffee is brewing*/
	bool isBrewing;
};
/*Abstract Containment Vessel class*/
class ContainmentVessel
{
public:
	/*Does what needs to be done when brewing 
		starts*/
	virtual void start()=0;
	/*Returns if CV is ready*/
	virtual bool isReady()=0;
	/*Does what needs to be done when brewing 
		is finished*/
	virtual void done()=0;
	/*checks if CV is full*/
	bool isFull()
	{
		return((volumeMax-volumeCurrent) <= 0);
	}
protected:
	/*Switch for if CV is ready or not*/
	bool ready;
	/*Switch for whether or not coffee is brewing*/
	bool isBrewing;
	/*Variables for determing how much coffee is in CV*/
	float volumeCurrent;
	float volumeMax;
};

/*Specific UI class for Mk4*/
class Mk4UI : public UserInterface
{
public:
	/*passes the API to the class*/
	Mk4UI(CoffeeMakerAPI* a):api(a){}
	/*gets state changes and status important to the component*/
	void getEvents()
	{
		/*Checks if brew button was pressed*/
		if(api->GetBrewButtonStatus()==api->brewButtonPushed)
		{
			/*starts brewing*/
			start();
		}
	}
	/*Ties UI to a Hot Water Source and Containment Vessel*/
	void init(HotWaterSource* h, ContainmentVessel* c)
	{
		hws=h;
		cv=c;
	}

	/*Starts brewing*/
	void start()
	{
		/*resets button*/
		api->SetBrewButtonStatus(api->brewButtonNotPushed);
		/*If both other components are ready turns off light
			and tells them to start*/
		if(hws->isReady() && cv->isReady())
		{
			api->SetIndicatorState(api->indicatorOff);
			hws->start();
			cv->start();
		}
		/*else has light flash to indicate an error*/
		else
		{
			api->SetIndicatorState(api->flashing);
		}
	}
	/*Turns on light when brewing is done*/
	void done()
	{
		api->SetIndicatorState(api->indicatorOn);
	}
private:
	/*variables for referencing the other components and API*/
	CoffeeMakerAPI* api;
	HotWaterSource* hws;
	ContainmentVessel* cv;
};
/*Specific HotWaterSource for Mk4*/
class Mk4HWS : public HotWaterSource
{
public:
	/*passes the API to the class*/
	Mk4HWS(CoffeeMakerAPI* a):api(a){}

	/*gets state changes and status important to the component*/
	void getEvents()
	{
		/*if boiler is empty, it isn't ready*/
		if(api->GetBoilerStatus()==api->boilerEmpty)
		{
			ready=false;
		}
		else
		{
			ready=true;
		}
	}
	/*Ties it to the other components*/
	void init(UserInterface* u, ContainmentVessel* c)
	{
		ui=u;
		cv=c;
	}
	/*turns on boiler, sets switches*/
	void start()
	{
		isBrewing=true;
		interrupted=false;
		api->SetBoilerState(api->boilerOn);
	}
	
	/*returns ready*/
	bool isReady()
	{
		return ready;
	}

	/*turns of boiler and sets switches*/
	void done()
	{
		isBrewing=false;
		interrupted=false;
		api->SetBoilerState(api->boilerOff);
	}
	/*sets interrupted switch to true and turns off boiler*/
	void pause()
	{
		interrupted=true;
		api->SetBoilerState(api->boilerOff);
	}
	/*sets interrupted switch to false and turns on boiler*/
	void resume()
	{
		interrupted=false;
		api->SetBoilerState(api->boilerOn);
	}
private:
	/*variables for referencing the other components and API*/
	CoffeeMakerAPI* api;
	UserInterface* ui;
	ContainmentVessel* cv;
};
class Mk4CV : public ContainmentVessel
{
public:
	/*passes API to the class*/
	Mk4CV(CoffeeMakerAPI* a):api(a)
	{
		isBrewing=false;
		/*defines container size*/
		volumeCurrent=0;
		volumeMax=48;
	}

	/*gets state changes and status important to the component*/
	void getEvents()
	{
		/*if pot isn't present, turns off warmer, pauses if brewing, 
			and isn't ready*/
		if(api->GetWarmerPlateStatus()==api->warmerEmpty)
		{
			api->SetWarmerState(api->warmerOff);
			if(isBrewing)
			{
				hws->pause();
			}
			ready=false;
		}
		/*if pot is there, turns on warmer is there is coffee in pot,
			resumes if paused, and is ready*/
		else
		{
			if(api->GetWarmerPlateStatus()==api->potNotEmpty)
			{
				api->SetWarmerState(api->warmerOn);
			}
			if(isBrewing && hws->interrupted)
			{
				hws->resume();
			}
			ready=true;
		}
		
		/*if brewing coffee, add it to the container, after 
			the boiler has warmed up*/
		if(isBrewing)
		{
			currentTime=time(NULL);
			float dif=difftime(currentTime,startTime);
			/*5 seconds to warm up*/
			if(dif>5)
			{
				/*0.533 is the fl oz/second rate*/
				volumeCurrent+=dif*0.533;
			}
		}
		/*if container is full, stops brewing and makes sure it wasn't overfilled*/
		if(isFull())
		{
			volumeCurrent=48.0;
			done();
		}
	}

	/*Ties CV to other components*/
	void init(UserInterface* u, HotWaterSource* h)
	{
		ui=u;
		hws=h;
	}
	/*turns on warmer and get the start time*/
	void start()
	{
		isBrewing=true;
		startTime=time(NULL);
		api->SetWarmerState(api->warmerOn);
	}
	/*returns ready*/
	bool isReady()
	{
		return ready;
	}

	/*sets brewing switch and tells other components to stop*/
	void done()
	{
		isBrewing=false;
		ui->done();
		hws->done();
	}

	/*pours out coffee from container*/
	void pour()
	{
		if(volumeCurrent<8.0)
			volumeCurrent=0;
		else
			volumeCurrent-=8.0;
	}
	/*Places pot on warmer and sets appropriate status*/
	void placePot()
	{
		if(volumeCurrent==0)
			api->SetWarmerPlateStatus(api->potEmpty);
		else
			api->SetWarmerPlateStatus(api->potNotEmpty);
	}
	/*returns how much coffee is in pot*/
	float getVolume()
	{
		return volumeCurrent;
	}
private:
	/*variables for API and other components*/
	CoffeeMakerAPI* api;
	UserInterface* ui;
	HotWaterSource* hws;
	/*time variables for measuring flow*/
	time_t startTime,currentTime;
};

/*class for the entire Mk4*/
class CoffeeMakerMk4
{
public:
	/*variables for each component*/
	Mk4API* api;
	Mk4HWS* hws;
	Mk4CV* cv;
	Mk4UI* ui;
	/*switch for if user has prepared Mk4 or not*/
	bool isPrepared;

	/*Initializes the components*/
	CoffeeMakerMk4():isPrepared(false)
	{
		api = new Mk4API;
		ui = new Mk4UI(api);
		hws = new Mk4HWS(api);
		cv = new Mk4CV(api);
		ui->init(hws,cv);
		hws->init(ui,cv);
		cv->init(ui,hws);
	}

	/*polls each component for events*/
	void poll()
	{
		ui->getEvents();
		cv->getEvents();
		hws->getEvents();
	}
	
};

/*class for the option menu*/
class Menu
{
public:
	/*passes coffee maker to class*/
	Menu(CoffeeMakerMk4* m):mk4(m){}
	/*Displays the menu*/
	void displayCommands()
	{
		cout<<"\t1.Prepare Mk4"<<endl
			<<"\t2.Put Coffee Pot on Plate"<<endl
			<<"\t3.Take Coffee Pot off Plate"<<endl
			<<"\t4.Pour Cup of Coffee"<<endl
			<<"\t5.Brew Coffee"<<endl
			<<"\t6.Coffee Status"<<endl
			<<"\t7.Toggle Verbose Mode"<<endl
			<<"\t8.Exit Coffee Maker"<<endl;
		
		/*Displays appropriate light state*/
		cout<<"Indicator Light: ";
		if(mk4->api->GetIndicatorState() == mk4->api->flashing)
			cout<<"Flashing"<<endl;
		else if(mk4->api->GetIndicatorState() == mk4->api->indicatorOn)
			cout<<"On"<<endl;
		else
			cout<<"Off"<<endl;
	}
	/*recieves action from user*/
	int acceptCommand()
	{
		int n=0;
		cout<<endl<<"What do you want to do? "<<endl;
		cin>>n;
		/*loops until valid command*/
		while(n<1 || n>8)
		{
			cout<<"Invalid command"<<endl;
			cin>>n;
		}
		return n;
	}

	/*carries out the user's action, 
	returns true when exit is command*/
	bool doCommand(int command)
	{
		switch(command)
		{
		case 1:/*Prepares the Mk4 if not prepared*/
			/*checks if it needs prepared again, if so
				allows user to prepare it*/
			if(mk4->isPrepared && 
			   (mk4->api->GetIndicatorState()==mk4->api->indicatorOn)&&
			   (mk4->cv->getVolume()==0))
			{
				mk4->isPrepared=false;
			}
			/*Fills boiler and "adds ingredients"*/
			if(!(mk4->isPrepared))
			{
				mk4->api->SetBoilerStatus(mk4->api->boilerNotEmpty);
				mk4->isPrepared=true;
				cout<<"Mark IV now prepared"<<endl;
			}
			/*Can't prepare if already prepared*/
			else
			{
				cout<<"Mark IV already prepared"<<endl;
			}
			mk4->poll();
			break;
		case 2:/*Put's the Pot on the warmer*/
			if(mk4->api->GetWarmerPlateStatus()!=mk4->api->warmerEmpty)
			{
				cout<<"Pot already on warmer"<<endl;
			}
			else
				mk4->cv->placePot();
			mk4->poll();
			break;
		case 3:/*Takes the pot off the warmer*/
			if(mk4->api->GetWarmerPlateStatus()==mk4->api->warmerEmpty)
			{
				cout<<"Pot not on warmer"<<endl;
			}
			else
			{
				mk4->api->SetWarmerPlateStatus(mk4->api->warmerEmpty);
			}
			mk4->poll();
			break;
		case 4:/*pours coffee, takes off of warmer if necessary*/
			if(mk4->api->GetWarmerPlateStatus()!=mk4->api->warmerEmpty)
			{
				mk4->api->SetWarmerPlateStatus(mk4->api->warmerEmpty);
			}
			if(mk4->cv->getVolume()<=0)
			{
				cout<<"No more coffee..."<<endl;
				break;
			}
			mk4->cv->pour();
			mk4->poll();
			break;
		case 5:/*presses brew button*/
			mk4->api->SetBrewButtonStatus(mk4->api->brewButtonPushed);
			mk4->poll();
			break;
		case 6:/*prints out how much coffee is in the pot*/
			mk4->poll();
			cout<<mk4->cv->getVolume()<<" fl. oz in pot"<<endl;
			break;
		case 7:/*toggles verbose mode*/
			if(mk4->api->Verbose)
				mk4->api->Verbose=false;
			else
				mk4->api->Verbose=true;
			break;
		case 8:/*exit*/
			return true;
		}
		return false;
	}
private:
	CoffeeMakerMk4* mk4;
};
int main()
{
	/*Initialize Mk4 and Menu*/
	bool exiting=false;
	CoffeeMakerMk4* mk4=new CoffeeMakerMk4;
	Menu menu(mk4);

	/*The Coffee Making Loop*/
	while(!exiting)
	{
		menu.displayCommands();
		int command=menu.acceptCommand();
		exiting=menu.doCommand(command);
		mk4->poll();
	}
}