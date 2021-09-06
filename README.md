# Car Rental
A car rental with multiple renting offices, cars and an inbuilt calendar.

## Introduction and Components
The application provides an interface for both the employees and the customers of the car rental via the UI class.  
An internal calender where 1 day is approximately equal to 2 seconds from the real world starts at the same time as the application keeps working in the background.  
The calender is used to a number of variables such as the day of rental, to update the days remaining until a car is available again, etc.   
The calender also powers the LiveTicker which as the name suggests, shows and keeps refreshing the days left until the car is available again, and eventually the availability.  
  
An Employee has the option to add new offices, cars, see the revenue of the office or to view the LiveTicker.  
A customer has the option to rent one or more cars and to view the LiveTicker.  

## Learnings
The application initially used a design where the UI class was split into three classes, CustomerUI and EmployeeUI classes inheriting from the UI class.  
The design was deemed optimal considering while the application didn't include a calender and a LiveTicker.  
While the design facilitated for a better structure, readability and easier moving between the UIs, it was not suitable for the added functionality.  
There were issues calling and assgning values to UIs since there were multiple instance variable with the same name (owing to the inheritance)  
Having a single UI class resolved most of the issues although making the class dense on methods.  

## Known Bugs
- A ReadLine() remains open when UI.RestartUI() is called from LiveTicker  
