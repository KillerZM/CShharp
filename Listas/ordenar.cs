using System;
using System.Collections.Generic;

public class Ordenar{
    public static void Main(string[] args){
        List<int> Lista = new List<int>();
        Lista.Add(10);
        Lista.Add(21);
        Lista.Add(7);
        Lista.Add(1);
        Lista.Add(12);
        Lista.Add(0);
        Lista.Add(25);
        Lista.Add(-15);
        Lista.Add(3);
        Lista.Add(7);
        int temp;
        for(int j=0; j< 10;j++){
        for(int i=0;i<9;i++){
            if (Lista[i] > Lista[i+1]){
                temp = Lista[i];
                Lista[i]=Lista[i+1];
                Lista[i+1]=temp;

            }
        }
        }

        foreach(int i in Lista){
            Console.WriteLine("["+i+"]");
        }
        Console.WriteLine("\n");
        Lista.RemoveRange(1, 2);
        foreach(int i in Lista){
            Console.WriteLine($"[{i}]");
        }
        Console.WriteLine("\n---------\n");
        Lista.Clear();
        foreach(int i in Lista){
            Console.WriteLine($"{i}");
        }

    }

}