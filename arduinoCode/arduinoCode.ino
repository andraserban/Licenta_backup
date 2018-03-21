#include <LiquidCrystal.h>
#include "DHT.h"

#define DHTPIN 22

#define DHTTYPE DHT11

DHT dht(DHTPIN, DHTTYPE);


String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
String commandString = "";

int led1Pin = 19;
int led2Pin = 20;
int led3Pin = 21;

boolean isConnected = false;
  /*The circuit:
 * LCD RS pin to digital pin 12
 * LCD Enable pin to digital pin 11
 * LCD D4 pin to digital pin 5
 * LCD D5 pin to digital pin 4
 * LCD D6 pin to digital pin 3
 * LCD D7 pin to digital pin 2
 * LCD R/W pin to ground
 * LCD VSS pin to ground
 * LCD VCC pin to 5V*/

const int rs = 12, en = 11, d4 = 5, d5 = 4, d6 = 3, d7 = 2;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7); 


void setup() {
  
  Serial.begin(9600);
 pinMode(led1Pin,OUTPUT);

//lcdReadSensor();
//delay(5000);
//getTemperatureAndHumidity();
//delay(5000);

  //initDisplay();
  lcd.begin(16, 2);
  lcd.print("Ready to connect");
}

void loop() {

//getTemperatureAndHumidity();

if(stringComplete)
{
  stringComplete = false;
  getCommand();
  
  if(commandString.equals("STAR"))
  {
    lcd.clear();
  }
  if(commandString.equals("STOP"))
  {
    turnLedOff(led1Pin);
     turnLedOff(led2Pin);
     turnLedOff(led3Pin);
    lcd.clear();
    lcd.print("Ready to connect");    
  }
  else if(commandString.equals("TEXT"))
  {
    String text = getTextToPrint();
    printText(text);
    delay(1000);
  }
  else if(commandString.equals("LED1"))
  {
    boolean LedState = getLedState();
    if(LedState == true)
    {
      turnLedOn(led1Pin);
    }else
    {
      turnLedOff(led1Pin);
    }   
  }
    else if(commandString.equals("LED2"))
  {
    boolean LedState = getLedState();
    if(LedState == true)
    {
      turnLedOn(led2Pin);
    }else
    {
      turnLedOff(led2Pin);
    }   
  }
    else if(commandString.equals("LED3"))
  {
    boolean LedState = getLedState();
    if(LedState == true)
    {
      turnLedOn(led3Pin);
    }else
    {
      turnLedOff(led3Pin);
    }   
  }
  
  inputString = "";
}

}

void initDisplay()
{
  lcd.begin(16, 2);
  lcd.print("Ready to connect");
}
void lcdReadSensor()
{
    lcd.begin(16,2);
    lcd.print("Reading sensor");
    dht.begin();
}

/*void getTemperatureAndHumidity()
{
  float temperature, humidity;

  humidity = dht.readHumidity();
  temperature = dht.readTemperature();
  delay(2000);

  lcd.clear();

  char tempF[6];
  char humF[6];
  dtostrf(temperature, 5, 1, tempF);
  dtostrf(humidity, 2, 0, humF);

  lcd.print("T:");
  lcd.print(tempF);
  lcd.print((char)223);
  lcd.print("C ");
  lcd.print("H: ");
  lcd.print(humF);
  lcd.print("%");
}*/
boolean getLedState()
{
  boolean state = false;
  if(inputString.substring(5,7).equals("ON"))
  {
    state = true;
  }else
  {
    state = false;
  }
  return state;
}

void getCommand()
{
  if(inputString.length()>0)
  {
     commandString = inputString.substring(1,5);
  }
}

void turnLedOn(int pin)
{
  digitalWrite(pin,HIGH);
}

void turnLedOff(int pin)
{
  digitalWrite(pin,LOW);
}


String getTextToPrint()
{
  String value = inputString.substring(5,inputString.length()-2);
  return value;
}

void printText(String text)
{
  lcd.clear();
  lcd.setCursor(0,0);
    if(text.length()<16)
    {
      lcd.print(text);
    }else
    {
      lcd.print(text.substring(0,16));
      lcd.setCursor(0,1);
      lcd.print(text.substring(16,32));
    }
}

void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read();
    // add it to the inputString:
    inputString += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '\n') {
      stringComplete = true;
    }
  }
}


