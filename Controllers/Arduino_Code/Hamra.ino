#include "Keyboard.h"

const int P1destroyPin = 7; 
const int P1killPin = 6;    
const int P1upPin = 5;
const int P1downPin  = 4;
const int P1leftPin = 2;
const int P1rightPin = 3;

const int ResetButton = 1; 

const int P2destroyPin = 13; 
const int P2killPin = 12;    
const int P2upPin = 11;
const int P2downPin = 10;
const int P2leftPin = 9;
const int P2rightPin = 8;

void setup() {
  // put your setup code here, to run once:
   pinMode(P1destroyPin, INPUT_PULLUP);
   pinMode(P1killPin, INPUT_PULLUP);
   pinMode(P1upPin, INPUT_PULLUP);
   pinMode(P1downPin, INPUT_PULLUP);
   pinMode(P1leftPin, INPUT_PULLUP);
   pinMode(P1rightPin, INPUT_PULLUP);

   pinMode(P2destroyPin, INPUT_PULLUP);
   pinMode(P2killPin, INPUT_PULLUP);
   pinMode(P2upPin, INPUT_PULLUP);
   pinMode(P2downPin, INPUT_PULLUP);
   pinMode(P2leftPin, INPUT_PULLUP);
   pinMode(P2rightPin, INPUT_PULLUP);

   pinMode(ResetButton, INPUT_PULLUP);

  // initialize control over the keyboard:
  Keyboard.begin();
}

void loop() {
  
  // aragoz
  if (digitalRead(P1destroyPin) == LOW)
  {
    Keyboard.press('n');
    Keyboard.release('n');
    delay(500);
  }
  if (digitalRead(P1killPin) == LOW)
  {
    Keyboard.press('m');
    Keyboard.release('m');
    delay(500);
  }  
  if (digitalRead(P1upPin) == LOW)
  {
    Keyboard.press('w');
    Keyboard.releaseAll();
    delay(100);
  } 
  if (digitalRead(P1downPin) == LOW)
  {
    Keyboard.press('s');
    Keyboard.releaseAll();
    delay(100);
  }
  if (digitalRead(P1leftPin) == LOW)
  {
    Keyboard.press('a');
    Keyboard.releaseAll();
    delay(100);
  } 
  if (digitalRead(P1rightPin) == LOW)
  {
    Keyboard.press('d');
    Keyboard.releaseAll();
    delay(100);
  }

  //Hamra
  if (digitalRead(P2destroyPin) == LOW)
  {
    Keyboard.press('x');
    Keyboard.releaseAll();
    delay(500);
  }
  if (digitalRead(P2killPin) == LOW)
  {
    Keyboard.press('z');
    Keyboard.releaseAll();
    delay(500);
  }
  if (digitalRead(P2upPin) == LOW)
  {
    Keyboard.press(KEY_UP_ARROW);
    Keyboard.releaseAll();
    delay(100);
  }
  
  if (digitalRead(P2downPin) == LOW)
  {
    Keyboard.press(KEY_DOWN_ARROW);
    Keyboard.releaseAll();
    delay(100);
  }

  if (digitalRead(P2leftPin) == LOW)
  {
    Keyboard.press(KEY_LEFT_ARROW);
    Keyboard.releaseAll();
    delay(100);
  }
 
  if (digitalRead(P2rightPin) == LOW)
  {
    Keyboard.press(KEY_RIGHT_ARROW);
    Keyboard.releaseAll();
    delay(100);
  }
  
// reset
if (digitalRead(ResetButton) == LOW)
  {
    Keyboard.press(KEY_TAB);
    Keyboard.releaseAll();
    delay(500);
  }

  //delay to slow things down a little on screen readingSS
  
}
