using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DelegateExamples : MonoBehaviour
{
    public delegate void ExampleDelegate(); //defines the delegate
    public ExampleDelegate exampleDelegate; //instance of the delegate

    private void OnEnable()
    {
        exampleDelegate += MyFunction; //add a function
        exampleDelegate += MyOtherFunction; //add another function
    }

    private void OnDisable()
    {
        exampleDelegate -= MyFunction; //remove a functoion
        exampleDelegate -= MyOtherFunction;//remove another function
    }

    private void MyFunction()
    {
        Debug.Log("This will get annoying!");
    }

    private void MyOtherFunction()
    {
        //shh! I don't do anything.
    }

    public void Start()
    {
        exampleDelegate = MyFunction; //subscribes function to the delegate
    }

    private void Update()
    {
        exampleDelegate?.Invoke(); //check if there is a subscriber, if there is we invoke
    }


    public delegate int IReturnAnInt();

    public delegate void IHaveInputs(int number, string text);

    private int FunctionReturnInt()
    {
        return 42;
    }

    private void FunctionWithTwoInputs(int number, string text)
    {
        Debug.Log("I got a number " + number + " And a string " + text);
    }


}

public class StaticDelegateClass : MonoBehaviour
{
    public delegate void MyStaticDelegate(); //normal delegate definition
    public static MyStaticDelegate myStaticDelegate; //static instance!!

    private void Update()
    {
        myStaticDelegate?.Invoke();
    }
}

public class StaticDelegateSubscriber : MonoBehaviour
{
    private void OnEnable()
    {
        StaticDelegateClass.myStaticDelegate += MyFunction; //subscribe to static delegate
    }

    private void OnDisable()
    {
        StaticDelegateClass.myStaticDelegate -= MyFunction; //unsubscribe to static delegate
    }

    private void MyFunction()
    {
        Debug.Log("I was called!");
    }
}

public class StaticEventClass : MonoBehaviour
{
    public delegate void MyStaticEvent(); //normal delegate definition
    public static event MyStaticEvent myStaticDelegate; //static instance!!

    private void Update()
    {
        myStaticDelegate?.Invoke();
    }
}

public class StaticEventSubscriber : MonoBehaviour
{
    private void OnEnable()
    {
        StaticEventClass.myStaticDelegate += MyFunction; //subscribe to static delegate

        //StaticEventClass.myStaticDelegate = MyFunction; //this throws an error!
    }

    private void OnDisable()
    {
        StaticEventClass.myStaticDelegate -= MyFunction; //unsubscribe to static delegate
    }

    private void MyFunction()
    {

    }
}

public class StaticEventClassWithAction : MonoBehaviour
{
    public static event Action myStaticEvent; //static instance!!
    public static event Action<int> myStaticEventWithInt;

    private void Update()
    {
        myStaticEvent?.Invoke();
        myStaticEventWithInt?.Invoke(12);
    }
}

public class StaticEventWithActionSubscriber : MonoBehaviour
{
    private void OnEnable()
    {
        StaticEventClassWithAction.myStaticEvent += MyFunction; //subscribe to static delegate
        StaticEventClassWithAction.myStaticEventWithInt += MyFunctionWithAnInt;
    }

    private void OnDisable()
    {
        StaticEventClassWithAction.myStaticEvent -= MyFunction; //unsubscribe to static delegate
        StaticEventClassWithAction.myStaticEventWithInt -= MyFunctionWithAnInt;
    }

    private void MyFunction()
    {

    }

    private void MyFunctionWithAnInt(int number)
    {
        Debug.Log("I got a number: " + number);
    }
}
