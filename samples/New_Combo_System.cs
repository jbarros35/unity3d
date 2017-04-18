public class New_Combo_System : MonoBehaviour
    {
 
        bool ActivateTimerToReset = false;
//The more bools, the less readibility, try to stick with the essentials.
//If you were to press 10 buttons in a row
//having 10 booleans to check for those would be confusing
 
        public float comboTimer = 0.5f;
        public int currentComboState = 0;
 
        // Update is called once per frame, yeah everybody knows this
 
        void Update()
        {
            NewComboSystem();
			//Initially set to false, so the method won't start
            ResetComboState(ActivateTimerToReset);  
        }
 
        void ResetComboState(bool ChooseYourWonderfulBoolName)
        {
            if (ChooseYourWonderfulBoolName)      
 //if the bool that you pass to the method is true
 // (aka if ActivateTimerToReset is true, then the timer start
            {
                comboTimer -= Time.Deltatime;  
//If the parameter bool is set to true, a timer start, when the timer
//runs out (because you don't press fast enought Z the second time)
//currentComboState is set again to zero, and you need to press it twice again
                if (comboTimer <= 0)
                {
                    currentComboState = 0;
                    ActivateTimerToReset = false;
                    comboTimer = 0.5f;
                }
            }
        }
 
        void NewComboSystem()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
 
				//Note that I'm to lazy to setup a switch statement
				//that would be WAY more readable than 3 if's in a row
 
                if (currentComboState == 0)
                {
                    currentComboState++;  
					//No need to create a comboStateUpdate()
					//function while you can directly
					//increment a variable using ++ operator
 
                    ActivateTimerToReset = true;    
					//Okay, you pressed Z once, so now the resetcombostate Function is
					//set to true, and the timer starts to reset the currcombostate
                    Debug.Log("1 hit");
                }
 
                if (currentComboState == 1)
                {
                    currentComboState++;
                    Debug.Log("2 hit, The combo Should Start");
					//Do your awesome stuff there and combokill the bitches
                }
 
                if (currentComboState >= 2)
                {
                    Debug.Log("Whooaaa 3 hits in half a second!");
					//I bet this will blast everthing off the screen
                }
 
 
            }
        }
 
    }