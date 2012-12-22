#include<iostream>
#include<ctime>
using namespace std;

class CoffeeMakerAPI 
{
public:
	enum WarmerPlateStatus { potNotEmpty, potEmpty, warmerEmpty };
	enum BoilerStatus { boilerEmpty, boilerNotEmpty };
	enum BrewButtonStatus { brewButtonPushed, brewButtonNotPushed };
	enum BoilerState { boilerOn, boilerOff };
	enum ValveState { valveOpen, valveClosed };
	enum WarmerState { warmerOn, warmerOff };
	enum IndicatorState { indicatorOn, indicatorOff, flashing };
	
	virtual WarmerPlateStatus GetWarmerPlateStatus () = 0;
	virtual BoilerStatus GetBoilerStatus () = 0;
	virtual BrewButtonStatus GetBrewButtonStatus () = 0;
	virtual BoilerState GetBoilerState ()= 0;
	virtual ValveState GetReliefValveState ()=0;
	virtual WarmerState GetWarmerState () = 0;
	virtual IndicatorState GetIndicatorState () = 0;

	virtual void SetWarmerPlateStatus (WarmerPlateStatus s) = 0;
	virtual void SetBoilerStatus (BoilerStatus s) = 0;
	virtual void SetBrewButtonStatus (BrewButtonStatus s) = 0;
	virtual void SetBoilerState (BoilerState s) = 0;
	virtual void SetReliefValveState (ValveState s)=0;
	virtual void SetWarmerState (WarmerState s) = 0;
	virtual void SetIndicatorState (IndicatorState s) = 0;
};
class Mk4API : public CoffeeMakerAPI
{
public:
	WarmerPlateStatus GetWarmerPlateStatus (){return plateSensor;}
	BoilerStatus GetBoilerStatus (){return waterSensor;}
	BrewButtonStatus GetBrewButtonStatus (){return button;}
	BoilerState GetBoilerState (){return boiler;}
	ValveState GetReliefValveState (){return valve;}
	WarmerState GetWarmerState (){return warmer;}
	IndicatorState GetIndicatorState (){return light;}

	void SetWarmerPlateStatus (WarmerPlateStatus s){plateSensor=s;}
	void SetBoilerStatus (BoilerStatus s){waterSensor=s;}
	void SetBrewButtonStatus (BrewButtonStatus s){button=s;}
	void SetBoilerState (BoilerState s){boiler=s;}
	void SetReliefValveState (ValveState s){valve=s;}
	void SetWarmerState (WarmerState s){warmer=s;}
	void SetIndicatorState (IndicatorState s){light=s;}

private:
	WarmerPlateStatus plateSensor;
	BoilerStatus waterSensor;
	BrewButtonStatus button;
	BoilerState boiler;
	ValveState valve;
	WarmerState warmer;
	IndicatorState light;
};

class HotWaterSource
{
public:
	HotWaterSource():isBrewing(false){}
	void start()
	{
		isBrewing=true;
		startBrewing();
	}
	void done()
	{
		isBrewing=false;
	}
	virtual bool ready()=0;
	virtual void startBrewing()=0;
	virtual void pause()=0;
	virtual void resume()=0;
protected:
	bool isBrewing;
};
class ContainerVessel
{
public:
	ContainerVessel():isBrewing(false),isComplete(true){}
	float volumeMax;
	float volumeCurrent;
	void start()
	{
		isBrewing = true;
		isComplete = false;
	}
	void done() 
	{
		isBrewing = false;
	}
	virtual bool ready()=0;
protected:
	bool isBrewing;
	bool isComplete;
};
class UserInterface
{
public:
	virtual void done()=0;
	virtual void completeCycle()=0;
	void complete()
	{
		isComplete=true;
		completeCycle();
	}
	void init(HotWaterSource* h, ContainerVessel* c)
	{
		hws=h;
		cv=c;
	}
protected:
	bool isComplete;
	void startBrewing()
	{
		if(hws->ready() && cv->ready())
		{
			cout<<"Brewing"<<endl;
			isComplete=false;
			hws->start();
			cv->start();
		}
	}
	HotWaterSource* hws;
	ContainerVessel* cv;
};


class Mk4HWS : public HotWaterSource
{
public:
	Mk4HWS(CoffeeMakerAPI* a):api(a){}
	void init(UserInterface* u, ContainerVessel* c)
	{
		ui=u;
		cv=c;
	}
	bool ready()
	{
		int boilerStatus = api->GetBoilerStatus();
		return boilerStatus == CoffeeMakerAPI::boilerNotEmpty; 
	}
	void startBrewing()
	{
		api->SetBoilerState(CoffeeMakerAPI::boilerOn);
		api->SetReliefValveState(CoffeeMakerAPI::valveClosed);
	}
	void poll() 
	{
		int boilerStatus = api->GetBoilerStatus();
		if (isBrewing) 
		{
			if (boilerStatus == CoffeeMakerAPI::boilerEmpty) 
			{
				api->SetBoilerState(CoffeeMakerAPI::boilerOff);
				api->SetReliefValveState(CoffeeMakerAPI::valveClosed);
				declareDone();
			}
		}
	}
	void pause() 
	{
		api->SetBoilerState(CoffeeMakerAPI::boilerOff);
		api->SetReliefValveState(CoffeeMakerAPI::valveOpen);
	}
	void resume() 
	{
		api->SetBoilerState(CoffeeMakerAPI::boilerOn);
		api->SetReliefValveState(CoffeeMakerAPI::valveClosed);
	}
private:
	CoffeeMakerAPI* api;
	UserInterface* ui;
	ContainerVessel* cv;
	void declareDone()
	{
		ui->done();
		cv->done();
		isBrewing=false;
	}
};
class Mk4CV : public ContainerVessel
{
public:
	Mk4CV(CoffeeMakerAPI* a):api(a),lastPotStatus(CoffeeMakerAPI::potEmpty)
	{
		volumeCurrent=0.0;
		volumeMax=48.0;
	}
	void init(UserInterface* u, HotWaterSource* h)
	{
		ui=u;
		hws=h;
	}
	void poll() 
	{
		currentTime=time(NULL);
		cout<<"Current Time"<<currentTime<<endl;
		double dif=difftime(currentTime,startTime);
		cout<<"Time ellapsed: "<<dif<<endl;
		int potStatus = api->GetWarmerPlateStatus();
		if (potStatus != lastPotStatus) 
		{
			if (isBrewing) 
			{
		        handleBrewingEvent(potStatus);
			} 
			else if (isComplete == false) 
			{
				handleIncompleteEvent(potStatus);
			}
			lastPotStatus = potStatus;
		}
	}
	bool ready()
	{
		int warmerStatus = api->GetWarmerPlateStatus();
		return warmerStatus == CoffeeMakerAPI::potEmpty;
	}
	void start()
	{
		startTime=time(NULL);
		cout<<"startTime: "<<startTime<<endl;
		isBrewing=true;
	}
private:
	CoffeeMakerAPI* api;
	int lastPotStatus;
	UserInterface* ui;
	HotWaterSource* hws;
	time_t startTime, currentTime;

	void declareComplete() 
	{
		isComplete = true;
		ui->complete();
	}
	void containerAvailable() 
	{
		hws->resume();
	}
	void containerUnavailable() 
	{
		hws->pause();
	}
	void handleBrewingEvent(int potStatus) 
	{
		if (potStatus == CoffeeMakerAPI::potNotEmpty) 
		{
			containerAvailable();
			api->SetWarmerState(CoffeeMakerAPI::warmerOn);
		} 
		else if (potStatus == CoffeeMakerAPI::warmerEmpty) 
		{
			containerUnavailable();
			api->SetWarmerState(CoffeeMakerAPI::warmerOff);
		} 
		else 
		{ // potStatus == api.POT_EMPTY
			containerAvailable();
			api->SetWarmerState(CoffeeMakerAPI::warmerOff);
	    }
	}
	void handleIncompleteEvent(int potStatus) 
	{
		if (potStatus == CoffeeMakerAPI::potNotEmpty) 
		{
			api->SetWarmerState(CoffeeMakerAPI::warmerOn);
		} 
		else if (potStatus == CoffeeMakerAPI::warmerEmpty) 
		{
			api->SetWarmerState(CoffeeMakerAPI::warmerOff);
		} 
		else 
		{ // potStatus == api.POT_EMPTY
			api->SetWarmerState(CoffeeMakerAPI::warmerOff);
			declareComplete();
		}
	}
};
class Mk4UI : public UserInterface
{
public:
	Mk4UI(CoffeeMakerAPI* a):api(a){}
	void done() 
	{
		api->SetIndicatorState(CoffeeMakerAPI::indicatorOn);
	}
	void completeCycle() 
	{
		api->SetIndicatorState(CoffeeMakerAPI::indicatorOff);
	}
	void poll() 
	{
		checkButton();
	}
private:
	CoffeeMakerAPI* api;
	void checkButton()
	{
		int buttonStatus = api->GetBrewButtonStatus();
		if (buttonStatus == CoffeeMakerAPI::brewButtonPushed)
		{
			startBrewing();
		}
	}
};

class CoffeeMakerMk4
{
public:
	CoffeeMakerAPI* api;
	Mk4HWS* hws;
	Mk4CV* cv;
	Mk4UI* ui;
	bool isPrepared;

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
	void poll()
	{
		ui->poll();
		cv->poll();
		hws->poll();
	}
	
};
class Menu
{
public:
	void displayCommands()
	{
		mk4.poll();
		cout<<"\t1.Prepare Mk4"<<endl
			<<"\t2.Put Coffee Pot on Plate"<<endl
			<<"\t3.Take Coffee Pot off Plate"<<endl
			<<"\t4.Pour Cup of Coffee"<<endl
			<<"\t5.Brew Coffee"<<endl
			<<"\t6.Coffee Status"<<endl
			<<"\t7.Toggle Verbose Mode"<<endl
			<<"\t8.Exit Coffee Maker"<<endl;

		cout<<"Indicator Light: ";
		if(mk4.api->GetIndicatorState() == CoffeeMakerAPI::flashing)
			cout<<"Flashing"<<endl;
		else if(mk4.api->GetIndicatorState() == CoffeeMakerAPI::indicatorOn)
			cout<<"On"<<endl;
		else
			cout<<"Off"<<endl;
	}
	int acceptCommand()
	{
		mk4.poll();
		int n=0;
		cout<<endl<<"What do you want to do? "<<endl;
		cin>>n;
		while(n<1 || n>8)
		{
			cout<<"Invalid command"<<endl;
			cin>>n;
		}
		return n;
	}
	bool doCommand(int command)
	{
		mk4.poll();
		switch(command)
		{
		case 1:
			if(!(mk4.isPrepared))
			{
				mk4.api->SetBoilerStatus(CoffeeMakerAPI::boilerNotEmpty);
				mk4.isPrepared=true;
				cout<<"Mark IV now prepared"<<endl;
			}
			else
			{
				cout<<"Mark IV already prepared"<<endl;
			}
			break;
		case 2:
			if(mk4.api->GetWarmerPlateStatus()!=CoffeeMakerAPI::warmerEmpty)
			{
				cout<<"Pot already on warmer"<<endl;
			}
			else if(mk4.cv->volumeCurrent>0)
			{
				mk4.api->SetWarmerPlateStatus(CoffeeMakerAPI::potNotEmpty);
			}
			else
			{
				mk4.api->SetWarmerPlateStatus(CoffeeMakerAPI::potEmpty);
			}
			break;
		case 3:
			if(mk4.api->GetWarmerPlateStatus()==CoffeeMakerAPI::warmerEmpty)
			{
				cout<<"Pot not on warmer"<<endl;
			}
			else
			{
				mk4.api->SetWarmerPlateStatus(CoffeeMakerAPI::warmerEmpty);
			}
			break;
		case 4:
			if(mk4.cv->volumeCurrent<8.0)
			{
				mk4.cv->volumeCurrent=0;
			}
			else
			{
				mk4.cv->volumeCurrent-=8.0;
			}
			break;
		case 5:
			mk4.api->SetBrewButtonStatus(CoffeeMakerAPI::brewButtonPushed);
			break;
		case 6:
			cout<<mk4.cv->volumeCurrent<<" fl. oz in pot"<<endl;
			break;
		case 7:
			if(verbose)
				verbose=false;
			else
				verbose=true;
			break;
		case 8:
			return true;
		}
		return false;
	}
private:
	CoffeeMakerMk4 mk4;
	bool verbose;
};
int main()
{
	bool exiting=false;
	Menu menu;

	while(!exiting)
	{
		menu.displayCommands();
		int command=menu.acceptCommand();
		exiting=menu.doCommand(command);
	}
}