using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class FoodMaker : MonoBehaviour
{
    //Goodluck y'all - Serhistorybuff

    [SerializeField] private Transform spawnPoint;//the location of the maker assign the maker to this Transform (it is needed to check the distance away from the player)
    [SerializeField] private string makertype; // what kind of food maker this is(note the name determines functionality so make sure it is named correctly)
    [SerializeField] private string rawfoodinput; //the rawfood needed to operate the foodmaker
    [SerializeField] private GameObject player; // Player object
    [SerializeField] private float workDistance = 5.0f;//how far away the player can access the maker

    //These variables track how many foods were made total and in specific categories, this is for debugging purposes 
    [SerializeField] private int preparedfood = 0;
    [SerializeField] public int burgersmade = 0;
    [SerializeField] public int friesmade = 0;
    [SerializeField] public int milkshakesmade = 0;

    [SerializeField] public int patties = 10;
    [SerializeField] public int icecream = 10;
    [SerializeField] public int potatoes = 10;



    //THIS LINE BELOW IS CRUICAL IT ALLOWS FOR ACCESS TO THE ITEMSPAWNER SCRIPT(which can be accessed by typing the scriptname(Itemspawner) before whatever you want to access
    public ItemSpawner ItemSpawner;
    

    private GameObject item;
    public void Restock()
    {
        patties = patties + 10;
        icecream = icecream + 10;
        potatoes = potatoes + 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        //On start there is checks which look at the makertype(the name we assigned to the maker) which from their decides what rawfood it needs 
        if (makertype == "grill")
        {
            rawfoodinput = "patties";
        }
        if (makertype == "mixer")
        {
            rawfoodinput = "icecream";
        }
        if (makertype == "fryer")
        {
            rawfoodinput = "potatoes";
        }
    }

    // Update is called once per frame
    void Update()
    {

        //this fuction checks if the player presses E near a food maker and then decides to make food based on if they are close enough(and if they have enough rawfood) 
        //It adds food by activating a function in ItemSpawner which increases the food by the amount made by the specificed maker
        if (Input.GetKeyDown(KeyCode.E))
        {
            var d = (player.transform.position - spawnPoint.transform.position).sqrMagnitude;
            if (d < workDistance)
            {
                if(makertype == "grill")
                {
                    if(patties > 0)
                    {
                        burgersmade = burgersmade + 1;
                        patties = patties - 1;
                        ItemSpawner.Burger();
                        preparedfood = preparedfood + 1;

                    }
                }
                if (makertype == "mixer")
                {
                    if (icecream > 0)
                    {
                        milkshakesmade = milkshakesmade + 1;
                        icecream = icecream - 1;
                        ItemSpawner.Milkshake();
                        preparedfood = preparedfood + 1;

                    }
                }
                if (makertype == "fryer")
                {
                    if (potatoes > 0)
                    {
                        friesmade = friesmade + 1;
                        potatoes = potatoes - 1;
                        ItemSpawner.Fries();
                        preparedfood = preparedfood + 1;

                    }
                }
                Debug.Log("Not Enough Raw Food Go Grab a box from outside");

            }

        }
    }
}