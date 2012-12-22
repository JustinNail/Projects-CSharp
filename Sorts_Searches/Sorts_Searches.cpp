#include <iostream>
#include <iomanip>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
using namespace std;

struct node
{
	/*************************/
	/*doubly linked list node*/
	/*************************/
	node()//node default constructor
	{
		/******************/
		/*Initialize parts*/
		/******************/
		data=0;
		next=NULL;
		prev=NULL;
	}
	int data;
	node *next;//pointer to next node
	node *prev;//pointer to prev node
};

class LinkedList
{
	/***********************************/
	/*doubly Linked list data structure*/
	/***********************************/
public:
	LinkedList(int size);//constructor
	void BubbleSort();//Bubble sort method
	void SelectionSort();//Selection sort method
	void InsertionSort();//Insertion sort method
	void QuickSort();//Quick sort method
	node* BinarySearch(int value);//Binary search method, returns pointer to node

	void print();//prints the list
	void randomize();//randomize the list data

	void add();//add a new node to the end
	int getPos(node* n);//get the pos of a node
	//to tell you where the search found an element in a more meaningfull way than just what the pointer is
private:
	void swap(node* first, node* second);//swaps the data in two nodes
	node* partition(node* first, node* last);//partition method for Quick sort

	void recQuickSort(node* first, node* last);//recursive part of Quick sort
	node* recBinarySearch(int value,node* first,node* last);//recursive part of Binary search
	
	node* head;//points to head node
	node* tail;//points to tail node
	int size;//holds size of list
};

LinkedList::LinkedList(int s)
{
	//initialize data members
	head=NULL;
	tail=NULL;
	size=s;
	//add s number of nodes
	for(int i=0;i<s;i++)
	{
		add();
	}
}

void LinkedList::add()
{
	node* newnode=new node;//make newnode
	if (head == NULL)//Checks if list is empty (empty if first = null)
	{
		head=newnode;
		tail=newnode;
	}
	else//list isn't empty
	{
		tail->next = newnode;
		newnode->prev=tail;
		tail = newnode;
	}
}


void LinkedList::print()
{
	/*****************/
	/*Prints the list*/
	/*****************/
	node* temp=head;//start at head
	while(temp!=NULL)//null means it's the last node
	{
		cout<<setw(10)<< temp->data;//setw makes even columns
		temp=temp->next;//move to next node
	}
}
int LinkedList::getPos(node* n)
{
	node* temp=head;//start at head
	int i=0;//counter
	while(temp!=n)//go until it finds the passed node
	{
		i++;//increment
		temp=temp->next;//move to next
	}
	return i;//return position (integer)
}
void LinkedList::randomize()
{
	/*****************/
	/*Fills list with*/
	/*random data    */
	/*****************/
	node* temp=head;//start at head
	while(temp!=NULL)//null means it's the last node
	{
		temp->data=rand();//set data to a random int(0 to RAND_MAX)
		temp=temp->next;//move to next
	}
}
void LinkedList::swap(node* first, node* second)
{
	int temp;
	temp=first->data;//temp holds data at first
	first->data=second->data;//first gets second's data
	second->data=temp;//second gets temp
}

void LinkedList::BubbleSort()
{
	node* n=head;//start at head
	for(int i=0;i<size-1;i++)//goes for length of list-1
	{
		node* n=head;//reset to head
		for(int j=0;j<(size-i)-1;j++)//goes for length of list, ones at the end that have settled already
		{
			if(n->data > n->next->data)//if out of order
			{
				swap(n,n->next);//swap
			}
			n=n->next;//move to next
		}
	}
}
void LinkedList::SelectionSort()
{
	node* min=head;//assume head is smallest
	node* i;
	node* j;
	for(i=head;i!=NULL;i=i->next)//start at head, go to end
	{
		for(j=i->next;j!=NULL;j=j->next)//start at one past i, go to end
		{
			if(j->data < min->data)//if find one less than min
			{
				min=j;//have min point at it
			}
		}
		swap(i,min);//min's data switches with the data at the beginning of unsorted section
	}
}
void LinkedList::InsertionSort()
{
	node* FirstOutofOrder,*i;
	int temp;
	FirstOutofOrder=head->next;//start at the second node
	while(FirstOutofOrder!=NULL)//go until end
	{
		//if the First in the Out of Order section is not already in order
		if(FirstOutofOrder->data < FirstOutofOrder->prev->data)
		{
			temp=FirstOutofOrder->data;//store the data
			i=FirstOutofOrder;//start at the First in the Out of Order section
			//go until it hits the beginning or finds it's place
			while(i!=head && i->prev->data > temp)
			{
				i->data=i->prev->data;//shifts the data down
				i=i->prev;//steps backwards through list
			}
			i->data=temp;//puts the stored data in it's proper place
		}
		FirstOutofOrder=FirstOutofOrder->next;//move to next node in the Out of Order section
	}
}
void LinkedList::QuickSort()
{
	recQuickSort(head,tail);//start quick sort with the whole list
}
node* LinkedList::BinarySearch(int value)
{
	return recBinarySearch(value,head,tail);//start Binary search with the whole list
}

node* LinkedList::partition(node* first, node* last)
{
	/*************************************************/
	/*Reorder the list so that everything with values*/ 
	/*less than the pivot come before the pivot,     */
	/*while everything with values greater than      */
	/*the pivot come after it					     */
	/*************************************************/
	int pivot;
    node *i, *j;
	bool done;


    pivot = first->data;//start pivot at beginning of partition
    done = false;//not done

    i = first; j = last;//start at both ends of partition
    while(true) //loop until done
	{
        while (j->data > pivot) //goes from end until it find data small than pivot 
		{
            j = j->prev;//steps backward
            if (i == j) //check if we meet other pointer
			{
				done = true;//if so, we're done
			}
        }
        if (done)//j is the new pivot
		{
			return j;
		}
        while (i->data < pivot) //goes from beginning until it finds data bigger than pivot
		{
            i = i->next;//step forward
            if (i == j)//check if we meet other pointer
			{
				done = true;//if so, we're done
			}
        }
        if (done) //i is the new pivot
		{
			return j;
		}

        swap(i,j);//if we get this far, swap the data at the two pointers

        // examine if next elements are the other pointers
        j = j->prev;
        if (i == j) 
		{
			done = true;
		}
        i = i->next;
        if (i == j) 
		{
			done = true;
		}
    }
}
void LinkedList::recQuickSort(node* first, node* last)
{
	node* pivotLoc;
	if(first!=last)//calls until the pointers meet
	{
		pivotLoc=partition(first,last);//get partition
		if(pivotLoc!=first)//if pivot is not beginning of partition
		{
			recQuickSort(first,pivotLoc);//quick sort right hand side
		}
		if(pivotLoc!=last)//if pivot is not end of partition
		{
			recQuickSort(pivotLoc->next,last);//quick sort left hand side
		}
	}
}
node* LinkedList::recBinarySearch(int value,node* first,node* last)
{
	if(first==last)//if pointers meet
	{
		return NULL;//not found
	}

	node* mid;
	node* j=first;

	//get partition size
	int partsize=0;
	while(j!=last)
	{
		j=j->next;
		partsize++;
	}
	
	/******************************/
	/*has mid point to middle node*/
	/******************************/
	//start at head
	mid=first;
	for(int x=0;x<partsize/2;x++)//go until you're half way through partition
	{
		mid=mid->next;//step through list
	}

	if(mid->data > value)//if middle bigger than search term
	{
		return recBinarySearch(value,first,mid);//search right
	}
	else if(mid->data < value)//if middle smaller than search term
	{
		return recBinarySearch(value,mid->next,last);//search left
	}
	else//must have found it
	{
		return mid;//returns the pointer
	}
}

class ArrayList
{
public:
	ArrayList(int s);//constructor
	void BubbleSort();//Bubble sort method
	void SelectionSort();//selection sort method
	void InsertionSort();//insertion sort method
	void QuickSort();//quick sort method
	int BinarySearch(int value);// Binary search, returns the index

	void print();//prints list
	void randomize();//randomize the list
private:
	void recQuickSort(int first, int last);//recursive part of quick sort
	int recBinarySearch(int value,int first,int last);//recursive part of binary search
	void swap(int first, int second);//swap two elements
	int partition(int first, int last);//partition method for quick sort
	int* data;//pointer for dynamically allocated array
	int size;//size of list
};

ArrayList::ArrayList(int s)
{
	data=new int[s]; //dynamically allocate array
	size=s;//sets the size
}

void ArrayList::print()
{
	/****************/
	/*Print the list*/
	/****************/
	for(int i=0;i<size;i++)
	{
		cout<<setw(10)<<data[i];//setw makes even columns
	}
}
void ArrayList::randomize()
{
	
	/********************/
	/*Randomize the list*/
	/********************/
	for(int i=0;i<size;i++)
	{
		data[i]=rand();//set element to a random number(0 to RAND_MAX)
	}
}
void ArrayList::swap(int first, int second)
{
	int temp;
	temp=data[first];//store first data
	data[first]=data[second];//first gets second's data
	data[second]=temp;//second gets stored data
}

void ArrayList::BubbleSort()
{
	for(int i=0;i<size-1;i++)//goes for length of list-1
	{
		for(int j=0;j<(size-i)-1;j++)//goes for length of list, ones at the end that have settled already
		{
			if(data[j]>data[j+1])//if out of order
			{
				swap(j,j+1);//swap
			}
		}
	}
}
void ArrayList::SelectionSort()
{
	int min=0;//assume first is smallest
	for(int i=0;i<size-1;i++)//go through list
	{
		for(int j=i+1;j<size-1;j++)//start at one past i, to end of list
		{
			if(data[j]<data[min])//if find one less than min
			{
				min=j;//set min to the new smallest
			}
		}
		swap(i,min);//move min to the end of sorted section
	}
}
void ArrayList::InsertionSort()
{
	int FirstOutofOrder,i,temp;
	for(FirstOutofOrder=1;FirstOutofOrder<size;FirstOutofOrder++)//start at the second second element, goes till end
	{
		//if the First in the Out of Order section is not already in order
		if(data[FirstOutofOrder] < data[FirstOutofOrder-1])
		{
			temp=data[FirstOutofOrder];//store data
			i=FirstOutofOrder;//start at the First in the Out of Order section
			//go until it hits the beginning or finds it's place
			do
			{
				data[i]=data[i-1];//shift elements down
				i--;//step backwards
			}while(i>0 && data[i-1]>temp);
			data[i]=temp;//puts data in proper place
		}
	}
}
void ArrayList::QuickSort()
{
	recQuickSort(0,size-1);//start Quick sort with whole list
}
int ArrayList::BinarySearch(int value)
{
	return recBinarySearch(value,0,size-1);//start Binary search with whole list
}

int ArrayList::partition(int first, int last)
{
	/*************************************************/
	/*Reorder the list so that everything with values*/ 
	/*less than the pivot come before the pivot,     */
	/*while everything with values greater than      */
	/*the pivot come after it					     */
	/*************************************************/
	int pivot,i,j;
	swap(first,(first+last)/2);//swap first and middle element

	pivot=data[first];//pivot gets first element (old middle element)
	j=first;//start at beginning
		
	for(i=first+1; i<=last;i++)//go from second to end
	{
		if(data[i]<pivot)//if i's data is less than pivot
		{
			j++;//step j forward
			swap(j,i);//swap i and j's elements
		}
	}
	swap(first,j);//move pivot(at first) to it's correct spot
	return j;//return pivot location
}
void ArrayList::recQuickSort(int first, int last)
{
	int pivotLoc;
	if(first<last)//checks if the ends cross (means we're done)
	{
		pivotLoc=partition(first,last);//get pivot location and partition the list
		recQuickSort(first,pivotLoc-1);//sort left
		recQuickSort(pivotLoc+1,last);//sort right
	}
}
int ArrayList::recBinarySearch(int value,int low,int high)
{
	int mid=(low+high)/2;//get middle
	if(high<low)//if ends cross
	{
		return -1;//not found
	}
	if(data[mid]>value)//if middle greater than search term
	{
		return recBinarySearch(value,low,mid-1);//search left
	}
	else if(data[mid]<value)//if middle less than search term
	{
		return recBinarySearch(value,mid+1,high);//search right
	}
	else//must have found it
	{
		return mid;//returns the position
	}
}

void menu()
{
	char inc,inc2;//input variables
	int inx,inx2;//integer versions of input
	while(true)//loops until valid input
	{
		cout<<"What do you want to use?"<<endl
			<<"\t1. Linked List"<<endl
			<<"\t2. Array"<<endl;
		cin>>inc;//get as a character
		if(inc=='1'||inc=='2')//check input
		{
			break;//leaves loop if valid
		}
		cout<<"Invalid Selection"<<endl;
	}
	inx=inc-48;//convert input to integer
	if(inx==1)//1 means it's linked list
	{
		LinkedList Llist(100);//make list
		node* n;
		Llist.randomize();//randomize initial list
		cout<<"Initial List:"<<endl;
		Llist.print();//print initial list
		while(true)//loops until quit command
		{
			while(true)//loops until valid input
			{
				cout<<endl
					<<"What do you want to do?"<<endl
					<<"\t1. Bubble Sort"<<endl
					<<"\t2. Selection Sort"<<endl
					<<"\t3. Insertion Sort"<<endl
					<<"\t4. Quick Sort"<<endl
					<<"\t5. Binary Search"<<endl
					<<"\t6. Print List"<<endl
					<<"\t7. New List"<<endl
					<<"\t8. Quit"<<endl;
				cin>>inc2;//gets as a character
				//check input
				if(inc2=='1'||inc2=='2'||inc2=='3'||inc2=='4'||inc2=='5'||inc2=='6'||inc2=='7'||inc2=='8')
				{
					break;//leave loop if valid
				}
				cout<<"Invalid Selection"<<endl;
			}
			inx2=inc2-48;//convert to int
			switch(inx2)
			{
			case 1://Bubble sort
				//Sort, then print new list
				Llist.BubbleSort();
				cout<<"New List:"<<endl;
				Llist.print();
				break;
			case 2://Selection Sort
				//Sort, then print new list
				Llist.SelectionSort();
				cout<<"New List:"<<endl;
				Llist.print();
				break;
			case 3://Insertion Sort
				//Sort, then print new list
				Llist.InsertionSort();
				cout<<"New List:"<<endl;
				Llist.print();
				break;
			case 4://Quick Sort
				//Sort, then print new list
				Llist.QuickSort();
				cout<<"New List:"<<endl;
				Llist.print();
				break;
			case 5://Binary Search
				//Search, if found, prints position and pointer value
				//if not says not found
				int value;
				cout<<"Search for what?"<<endl;
				cin>>value;
				n= Llist.BinarySearch(value);
				if(n==NULL)
				{
					cout<<"Not Found"<<endl;
				}
				else
				{
					cout<<"Found at position "<<Llist.getPos(n)<<"(Pointer = "<<n<<")"<<endl;	
				}
				break;
			case 6://Print
				//prints the list
				Llist.print();
				break;
			case 7://New List
				Llist.randomize();//randomizes the elements again
				cout<<"New List:"<<endl;
				Llist.print();//prints the new list
				break;
			case 8://Quit
				//leaves the menu
				return;
				break;
			}
		}
	}
	else//must be an array
	{
		ArrayList Alist(100);//make the list
		int x=0;
		Alist.randomize();//randomize initial list
		cout<<"Initial List:"<<endl;
		Alist.print();//print initial list
		while(true)//loop until quit command
		{
			while(true)//loop until valid input
			{
				cout<<endl
					<<"What do you want to do?"<<endl
					<<"\t1. Bubble Sort"<<endl
					<<"\t2. Selection Sort"<<endl
					<<"\t3. Insertion Sort"<<endl
					<<"\t4. Quick Sort"<<endl
					<<"\t5. Binary Search"<<endl
					<<"\t6. Print List"<<endl
					<<"\t7. New List"<<endl
					<<"\t8. Quit"<<endl;
				cin>>inc2;//get as character
				//check input
				if(inc2=='1'||inc2=='2'||inc2=='3'||inc2=='4'||inc2=='5'||inc2=='6'||inc2=='7'||inc2=='8')
				{
					break;//leave loop if valid
				}
				cout<<"Invalid Selection"<<endl;
			}
			inx2=inc2-48;//convert to integer
			switch(inx2)
			{
			case 1://Bubble sort
				//Sort, then print new list
				Alist.BubbleSort();
				cout<<"New List:"<<endl;
				Alist.print();
				break;
			case 2://Selection Sort
				//Sort, then print new list
				Alist.SelectionSort();
				cout<<"New List:"<<endl;
				Alist.print();
				break;
			case 3://Insertion Sort
				//Sort, then print new list
				Alist.InsertionSort();
				cout<<"New List:"<<endl;
				Alist.print();
				break;
			case 4://Quick sort
				//Sort, then print new list
				Alist.QuickSort();
				cout<<"New List:"<<endl;
				Alist.print();
				break;
			case 5://Binary Search
				//Searches for value, if found prints position
				//if not, says not found
				int value;
				cout<<"Search for what?"<<endl;
				cin>>value;
				x= Alist.BinarySearch(value);
				if(x==-1)
				{
					cout<<"Not Found"<<endl;
				}
				else
				{
					cout<<"Found at position "<<x<<endl;	
				}
				break;
			case 6://Print
				Alist.print();
				break;
			case 7://New List
				//rerandomize the list
				Alist.randomize();
				cout<<"New List:"<<endl;
				Alist.print();//print new list
				break;
			case 8://Quit
				//leaves menu
				return;
				break;
			}
		}
	}
}

int main()
{
	char c;//soak variable
	srand(time(NULL));//seeds with time

	menu();//calls menu

	cin.get(c);//soak up any extra character
	cout<<endl<<"Press any key to continue ";
	cin.get(c);//pause until input
}