#include <string>
#include <iostream>
#include <iomanip>

using namespace std;

#define READY 'y'
#define RUNNING 'r'
#define BLOCKED 'b'
#define SYSTEM true
#define APPLICATION false
struct PCB
{
    string Name;
	bool Class;//true=system, false=Application
	int Priority;
	char State;//y=Ready state, r=running state, b=blocked state
	bool Suspended;
	int Memory;
	PCB* next;
	PCB* prev;
};
struct Queue
{
	int size;
	PCB *head;
	PCB *tail;
};

Queue ReadyQueue;
Queue BlockedQueue;

PCB* AllocatePCB();
void FreePCB(PCB *node);
PCB* SetupPCB(string Name, int Priority, bool Class);
PCB* FindPCB(string Name);
void InsertPCB(PCB *node);
void RemovePCB(PCB *node);
void showPCB(PCB *node);
int main()
{
	ReadyQueue.head=NULL;
	ReadyQueue.tail=NULL;
	ReadyQueue.size=0;

	BlockedQueue.head=NULL;
	BlockedQueue.tail=NULL;
	BlockedQueue.size=0;
	
	int command;
	PCB *node;
	string name;
	char c;
	int Priority;
	bool Class;
	bool end=false;
	int linecount=0;

	while(!end)
	{
		cout<<"\t1.Create PCB"<<endl<<"\t2.Delete PCB"<<endl<<"\t3.Block"<<endl<<"\t4.Unblock"<<endl
			<<"\t5.Suspend"<<endl<<"\t6.Resume"<<endl<<"\t7.Set Priority"<<endl<<"\t8.Show PCB"<<endl
			<<"\t9.Show All"<<endl<<"\t10.Show Ready"<<endl<<"\t11.Show Blocked"<<endl<<"\t12.exit"<<endl;

		cin>>command;
	
		switch(command)
		{
		case 1://Create
			cout<<"Input Process Name: "<<endl;
			cin>>name;
					
			cout<<"Input Process Priority: "<<endl;
			cin>>Priority;
			
			cout<<"System Process?(y/n) "<<endl;
			cin>>c;
			if(c=='y' || c=='Y')
				Class=true;
			else
				Class=false;
	
			node=SetupPCB(name,Priority,Class);
			if(node==NULL)
				cout<<"Invalid Parameters, Process not created"<<endl;
			else
				InsertPCB(node);
			break;
		case 2://Delete
			cout<<"Input Process Name: ";
			cin>>name;
			node=FindPCB(name);
			if(node==NULL)
				cout<<"Process does not exsist"<<endl;
			else
			{
				RemovePCB(node);
				FreePCB(node);
				cout<<"Process "<<name<<" deleted"<<endl;
			}
			break;
		case 3://Block
			cout<<"Input Process Name: ";
			cin>>name;
			node=FindPCB(name);
			if(node==NULL)
				cout<<"Process does not exsist"<<endl;
			else
			{
				if(node->State==BLOCKED)
					cout<<"Process already Blocked"<<endl;
				else
				{
					RemovePCB(node);
					node->State=BLOCKED;
					InsertPCB(node);
				}
			}
			break;
		case 4://Unblock
			cout<<"Input Process Name: ";
			cin>>name;
			node=FindPCB(name);
			if(node==NULL)
				cout<<"Process does not exsist"<<endl;
			else
			{	
				if(node->State!=BLOCKED)
					cout<<"Process not Blocked"<<endl;
				else
				{
					RemovePCB(node);
					node->State=READY;
					InsertPCB(node);
				}	
			}
			break;
		case 5://Suspend
			cout<<"Input Process Name: ";
			cin>>name;
			node=FindPCB(name);
			if(node==NULL)
				cout<<"Process does not exsist"<<endl;
			else	
			{
				if(node->Suspended)
					cout<<"Process already Suspended"<<endl;
				else
				{
					node->Suspended=true;
				}
			}
			break;
		case 6://Resume
			cout<<"Input Process Name: ";
			cin>>name;
			node=FindPCB(name);
			if(node==NULL)
				cout<<"Process does not exsist"<<endl;
			else
			{
				if(!(node->Suspended))
					cout<<"Process not Suspended"<<endl;
				else
				{
					node->Suspended=false;
				}
			}
			break;
		case 7://Set Priority
			cout<<"Input Process Name: "<<endl;
			cin>>name;
			node=FindPCB(name);
			if(node==NULL)
				cout<<"Process does not exsist"<<endl;
			else
			{
				cout<<"Input Process Priority: "<<endl;
				cin>>Priority;
				if(Priority<-127 || Priority > 127)
					cout<<"Invalid Priority"<<endl;
				else
					node->Priority=Priority;
			}
			break;
		case 8://Show
			cout<<"Input Process Name: "<<endl;
			cin>>name;
			node=FindPCB(name);
			if(node==NULL)
				cout<<"Process does not exsist"<<endl;
			else
				showPCB(node);
			break;
		case 9://Show all
			node=ReadyQueue.head;
			while(node!=NULL)
			{
				cout<<setw(10)<<"Name"<<setw(10)<<"Class"<<setw(10)<<"Priority"
					<<setw(10)<<"State"<<setw(10)<<"Suspended"<<setw(10)<<"Memory"<<endl;
				for(linecount=0;linecount<21;linecount++)
				{
					showPCB(node);
					node=node->next;
					if(node==NULL)
						break;
				}
				system("pause");
			}
			node=BlockedQueue.head;
			while(node!=NULL)
			{
				cout<<setw(10)<<"Name"<<setw(10)<<"Class"<<setw(10)<<"Priority"
					<<setw(10)<<"State"<<setw(10)<<"Suspended"<<setw(10)<<"Memory"<<endl;
				for(linecount=0;linecount<21;linecount++)
				{
					showPCB(node);
					node=node->next;
					if(node==NULL)
						break;
				}
				system("pause");
			}
			break;
		case 10://Show Ready
			node=ReadyQueue.head;
			while(node!=NULL)
			{
				cout<<setw(10)<<"Name"<<setw(10)<<"Class"<<setw(10)<<"Priority"
					<<setw(10)<<"State"<<setw(10)<<"Suspended"<<setw(10)<<"Memory"<<endl;
				for(linecount=0;linecount<21;linecount++)
				{
					showPCB(node);
					node=node->next;
					if(node==NULL)
						break;
				}
				system("pause");
			}
			break;
		case 11://Show Blocked
			node=BlockedQueue.head;
			while(node!=NULL)
			{
				cout<<setw(10)<<"Name"<<setw(10)<<"Class"<<setw(10)<<"Priority"
					<<setw(10)<<"State"<<setw(10)<<"Suspended"<<setw(10)<<"Memory"<<endl;
				for(linecount=0;linecount<21;linecount++)
				{
					showPCB(node);
					node=node->next;
					if(node==NULL)
						break;
				}
				system("pause");
			}
			break;
		case 12:
			end=true;
			break;
		default:
			cout<<"Invalid choice"<<endl;
		}
	}
	system("pause");
	return 0;
}
PCB* AllocatePCB()
{
	PCB *newNode;
	try
	{
		newNode=new PCB;
	}
	catch (bad_alloc&)
	{
		return NULL;
	}
	return newNode;
}
void FreePCB(PCB *node)
{
	delete node;
}
PCB* SetupPCB(string Name, int Priority, bool Class)
{
	if(FindPCB(Name)!=NULL)
		return NULL;
	if(Priority<-127 || Priority > 127)
		return NULL;
	PCB *newNode=AllocatePCB();
	
	newNode->Name=Name;
	newNode->Priority=Priority;
	newNode->Class=Class;
	newNode->Memory=1;
	newNode->State=READY;
	newNode->Suspended=false;
	newNode->next=NULL;
	newNode->prev=NULL;

	return newNode;
}
PCB* FindPCB(string Name)
{
	PCB *p;
	p=ReadyQueue.head;
	while(p!=NULL)
	{
		if(p->Name==Name)
			return p;
		p=p->next;
	}
	p=BlockedQueue.head;
	while(p!=NULL)
	{
		if(p->Name==Name)
			return p;
		p=p->next;
	}
	return NULL;
}
void InsertPCB(PCB *node)
{
	if(node->State==READY)
	{
		if(ReadyQueue.tail==NULL)//queue is empty
		{
			ReadyQueue.head=node;
			ReadyQueue.tail=node;
			ReadyQueue.size=1;
		}
		else
		{
			ReadyQueue.tail->next=node;
			node->prev=ReadyQueue.tail;
			ReadyQueue.tail=node;
			ReadyQueue.size++;
		}
	}
	else if(node->State==BLOCKED)
	{
		if(BlockedQueue.tail==NULL)//queue is empty
		{
			BlockedQueue.head=node;
			BlockedQueue.tail=node;
			BlockedQueue.size=1;
		}
		else
		{
			BlockedQueue.tail->next=node;
			node->prev=BlockedQueue.tail;
			BlockedQueue.tail=node;
			BlockedQueue.size++;
		}
	}
}
void RemovePCB(PCB *node)
{
	PCB *p=FindPCB(node->Name);
	PCB *q;

	if(p==ReadyQueue.head)
		ReadyQueue.head=p->next;
	else if(p==ReadyQueue.tail)
		ReadyQueue.tail=p->prev;

	else if(p==BlockedQueue.head)
		BlockedQueue.head=p->next;
	else if(p==BlockedQueue.tail)
		BlockedQueue.tail=p->prev;
	else
	{
		q=p->prev;
		q->next=p->next;
		q->next->prev=p->prev;
	}

	if(p->State==READY)
		ReadyQueue.size--;
	else if(p->State==BLOCKED)
		BlockedQueue.size--;

	p->next=NULL;
	p->prev=NULL;
}

void showPCB(PCB *node)
{
	cout<<setw(10)<<node->Name;
	if(node->Class==SYSTEM)
		cout<<setw(10)<<"Sys";
	if(node->Class==APPLICATION)
		cout<<setw(10)<<"App";
	cout<<setw(10)<<node->Priority;
	if(node->State==READY)
		cout<<setw(10)<<"Rdy";
	if(node->State==RUNNING)
		cout<<setw(10)<<"Run";
	if(node->State==BLOCKED)
		cout<<setw(10)<<"Blk";

	if(node->Suspended)
		cout<<setw(10)<<"Yes";
	else
		cout<<setw(10)<<"No";
	cout<<setw(10)<<node->Memory<<endl;

}