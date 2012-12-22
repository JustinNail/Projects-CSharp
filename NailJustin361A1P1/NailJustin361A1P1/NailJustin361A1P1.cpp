#include <iostream>
using namespace std;
struct LinkedList
{
    int data; 
    LinkedList *next;
};

LinkedList* addToFront(LinkedList *first,int data)
{
	/********************/
	/*Add a new node to */
	/*the front of list */
	/********************/
	LinkedList *newnode;

	newnode=new LinkedList;//create and fill node
	newnode->data=data;

	newnode->next=first;//newnode points to old first node
	first=newnode;//newnode is now the first node
	return first;
}
LinkedList* removeFromFront(LinkedList *first)
{
	/*************************************/
	/*Remove the first node from the list*/
	/*************************************/
	LinkedList *temp=first;
	first=first->next;
	delete temp;
	return first;
}
void insertInbetween(LinkedList *first,int place,int data)
{
	/******************************************/
	/*Insert after an exsisting node inbetween*/
	/*nodes place and place +1				  */
	/******************************************/
	LinkedList *newnode;

	newnode=new LinkedList;//create and fill newnode
	newnode->data=data;

	LinkedList *temp=first;
	for(int i=0;i<place;i++)//move temp into position
	{
		temp=temp->next;
	}

	//insert newnode into list
	newnode->next=temp->next;
	temp->next=newnode;
}
void removeInbetween(LinkedList *first,int place)
{
	/************************************/
	/*Delets an exsisting node at place	*/
	/************************************/
	LinkedList *p,*q;
	p=first;
	for(int i=0;i<place;i++)//move p into position
	{
		p=p->next;
	}

	//remove node at place from the list
	q=p->next;
	p->next=q->next;
	delete q;//free up the memory

}
void print(LinkedList *first)
{
	/*****************/
	/*Prints the list*/
	/*****************/
	LinkedList *temp=first;
	while(temp!=NULL)//null means it's the last node
	{
		cout<< temp->data <<" ";
		temp=temp->next;
	}
}

int main()
{
	char c;

	/********************/
	/*make an empty list*/
	/********************/
	LinkedList *first, *last, *newnode;
	first=NULL;
	newnode=NULL;
	last=NULL;
	

	/********************/
	/*Populate the list */
	/*adding 3 to begin */
	/*adds new nodes to */
	/*back              */
	/********************/
	for(int i=0;i<3;i++)
	{
		newnode=new LinkedList;
		newnode->data=i;
		if (first == NULL)//Checks if list is empty (empty if first = null)
		{
			first=newnode;
			last=newnode;
		}
		else//does if list not empty
		{
			last->next = newnode;
			last = newnode;
			newnode->next=NULL;
		}
	}
	//print initial list
	print(first);
	cout<<endl;

	//add to front and print
	first=addToFront(first,12);
	print(first);
	cout<<endl;

	//insert in between to nodes and print
	insertInbetween(first,2,42);
	print(first);
	cout<<endl;

	//remove a node from middle of list and print
	removeInbetween(first,2);
	print(first);
	cout<<endl;

	//remove from front of list and print
	first=removeFromFront(first);
	print(first);
	cout<<endl;


	cout<<endl<<"Press any key to continue ";
	cin.get(c);
	return 0;
}