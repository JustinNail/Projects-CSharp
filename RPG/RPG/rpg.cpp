#include <iostream>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#include "classes.h"

using namespace std;

int encounter(RPGclass *PC)
{
	/***************************************************/
	/*function for determining and resolving encounters*/
	/*Returns 0 if PC wins, 1 if PC dies			   */
	/***************************************************/

	/****************************************/
	/*encounter variable randomly determined*/
	/****************************************/
	int encounter = (rand()%10);

	/********************************************/
	/*randomly picks a name for the opponent	*/
	/*Names obtained using the name generator at*/
	/* http://www.rinkworks.com/namegen/        */
	/********************************************/
	string names[10]={"Burvor","Zamari","Trazrkel","Awyack","Tingis","Daviry","Soish","Naliri","Chriryn","Cert"};
	int i = rand()%10;

	/******************************************/
	/*Uses the encounter variable to determine*/
	/*the nature of the encounter             */
	/*	Monster = 50%                         */
	/*	PC class ~ 7% each					  */
	/*Level of opponent = level of PC         */
	/******************************************/
	RPGclass* opponent;
	if(encounter>4)
	{
		cout<<"You've encountered a Monster!"<<endl;
		opponent=new Monster(names[i]);
	}
	else
	{
		encounter=rand()%7;
		switch(encounter)
		{
		case 0:
			cout<<"You've engaged a hostile Fighter!"<<endl;
			opponent=new Fighter(names[i]);
			break;
		case 1:
			cout<<"You've caught an enemy Thief!"<<endl;
			opponent=new Thief(names[i]);
			break;
		case 2:
			cout<<"You've interrupted an evil Wizard!"<<endl;
			opponent=new Wizard(names[i]);
			break;
		case 3:
			cout<<"You've offended a dark Cleric!"<<endl;
			opponent=new Cleric(names[i]);
			break;
		case 4:
			cout<<"You've crossed a fallen Paladin!"<<endl;
			opponent=new Paladin(names[i]);
			break;
		case 5:
			cout<<"You've enraged a hostile Monk!"<<endl;
			opponent=new Monk(names[i]);
			break;
		case 6:
			cout<<"You've been ambushed by an evil Ninja!"<<endl;
			opponent=new Ninja(names[i]);
			break;
		}
	}
	opponent->levelup(PC->getLevel());

	/*************************/
	/*The combat loop		 */
	/*goes until someone dies*/
	/*or the PC flees		 */
	/*************************/
	bool end=false;
	while(!end)
	{
		/***********************************/
		/*Get and execute command from user*/
		/***********************************/
		int command;
//		int target;
		cout<<"What do you do?"<<endl<<endl;
		PC->PrintCommands();
		cout<<endl;	
		cin>>command;

		int result=PC->doCommand(command,opponent);
			
		switch(result)
		{
		case 0://result=0 means opponent gets to act back
			command=rand()%(opponent->getNPCcommands());
			opponent->doCommand(command,PC);
			break;
		case 2://result = 2 means combat should end
			end=true;
			break;
		}

		if(!(opponent->isAlive()))
		{
			cout<<endl<<opponent->getName()<<" has been slain!"<<endl;
			end=true;
		}

		if(!(PC->isAlive()))//Checks if PC is alive
		{
			cout<<endl<<PC->getName()<<" has been defeated . . ."<<endl;
			return 1;//Return 1 if PC is killed
		}

		/********************************/
		/*For some reason including     */
		/*the delete statement causes   */
		/*the program to crash with an  */
		/*assertion failure				*/
		/********************************/
		//Free up memory
		//delete opponent;
		//opponent=NULL;

	}
	PC->setPoison(false);//remove poison in between encounters
	return 0;//returns 0 if PC is not killed
}
int main()
{
	char c;//character for pause statements
	string name;//string for PC name
	int whatclass;//holds the PC's class
	int level;//holds the PC's level

	RPGclass * PC=NULL;//Make PC pointer

	srand(time(NULL));//seed = seconds at execution

	/********************/
	/*Reads in PC's name*/
	/********************/
	cout<<"What is your name? ";
	getline(cin,name);
	cout<<endl;

	/****************************************/
	/*Loop to ensure a valid type is entered*/
	/****************************************/
	while(true)
	{
		cout<<"What are you?"<<endl<<"\t1. a Fighter"<<endl<<"\t2. a Thief"
			<<endl<<"\t3. a Wizard"<<endl<<"\t4. a Cleric"<<endl<<"\t5. a Paladin"
			<<endl<<"\t6. a Monk"<<endl<<"\t7. a Ninja"<<endl;
		cin>>whatclass;

		if(whatclass > 0 && whatclass <= 7)//leave loop if selection is valid
			break;
		cout<<"Invalid selection"<<endl;
	}
	
	/******************************/
	/*Initialize PC depending on  */
	/*which class is chosen		  */
	/******************************/
	switch(whatclass)
	{
	case 1:
		PC = new Fighter(name);
		break;
	case 2:
		PC = new Thief(name);
		break;
	case 3:
		PC = new Wizard(name);
		break;
	case 4:
		PC = new Cleric(name);
		break;
	case 5:
		PC = new Paladin(name);
		break;
	case 6:
		PC = new Monk(name);
		break;
	case 7:
		PC = new Ninja(name);
		break;
	}

	/*****************************************/
	/*Loop to ensure a valid level is entered*/
	/*****************************************/
	while(true)
	{
		cout<<"What level are you? (Max 20)"<<endl;
		cin>>level;
		if (level<=20 && level>0)
			break;
		cout<<"Invalid Level"<<endl;
	}

	//levels up the PC
	PC->levelup(level);
	//Set PC flag to true
	PC->setPC(true);

	/*************************************/
	/*Goes through at least one encounter*/
	/*If PC dies, break out. Otherwise   */
	/*lets PC choose to continue or not  */
	/*************************************/
	while(true)
	{
		int cont = 1;
		int result = encounter(PC);
		if (result == 1)//1 means PC died
			break;
		else
		{
			cout<<"Go looking for another fight?"<<endl
				<<"	1. Yes, I want another fight"<<endl
				<<"	2. No, I'm done"<<endl;
			cin>>cont;
		}
		if(cont != 1)//anything entered besides 1 will break out
			break;
	}

	/********************************/
	/*For some reason including     */
	/*the delete statement causes   */
	/*the program to crash with an  */
	/*assertion failure				*/
	/********************************/
	//Free up memory
	//delete PC;
	//PC=NULL;

	cin.get(c);//soak up extra newline character
	cout<<endl<<"Press any key to quit ";
	cin.get(c);//wait for input
	return 0;
}