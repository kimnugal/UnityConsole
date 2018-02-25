using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class Adapter : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text text;
    [SerializeField]
    Executor executor;

    StringBuffer buffer;

    void Start()
    {
        buffer = new StringBuffer();
        UConsole.Executor = executor;
        UConsole.Buffer = buffer;
        Thread main = new Thread(() =>
        {
            try
            {
                Program.Main();
            }
            catch (System.Exception e)
            {
                UConsole.WriteLine("Exception:\n" + e.Message);
            }
        });
        main.Start();
    }
    void Print()
    {
        text.text += buffer.String;
    }
    void Update()
    {
        if (buffer.Ready)
            Print();
    }
}

public static class UConsole
{
    public static Executor Executor { get; set; }
    public static StringBuffer Buffer { get; set; }

    public static string ReadLine()
    {
        string res;
        while (!Executor.InputReady)
            Thread.Sleep(100);
        res = Executor.String;
        return res;
    }
    public static void WriteLine(string text)
    {
        Buffer.String = text;
    }
    public static void WriteLine(object obj)
    {
        WriteLine(obj.ToString());
    }
    public static void WriteLine()
    {
        WriteLine("");
    }
}

public class StringBuffer
{
    List<string> strings;
    public StringBuffer()
    {
        strings = new List<string>();
    }
    public string String
    {
        get
        {
            string res = "";
            while(strings.Count != 0)
            {
                string current = strings[0];
                res += current + "\n";
                strings.Remove(current);
            }
            return res;
        }
        set
        {
            strings.Add(value);
            Ready = true;
        }
    }
    public bool Ready { get; private set; }
}