using System;
using System.Collections.Generic;

public class RandomizedList<T>
{
    private List<T> stuff = new List<T>();
    private Random random = new Random();

    
    public void Add(T element) 
    {
        int r=random.Next(100);
        r=r%2;
        if (r == 0) {
            stuff.Insert(0, element); 
        }
        else {
            stuff.Add(element); 
        }
    }

    
    public T Get(int index)
    {
        
        int randomIndex = random.Next(0, index + 1);
        return stuff[randomIndex];
    }

    
    public bool IsEmpty(){
        bool verif = false;
        if ( stuff.Count == 0){
            verif = true;
        }
        return verif;
    } 

    public void PrintAll()
    {
        Console.WriteLine($"Stuff: [{string.Join(", ", stuff)}]");
    }
}
