# Raven
Learning to build a database and a UI


Create a database that holds the information for logs instead of using multiple excel files. 

Process:

Material Log:

When material is recieved it must be processed and sampled. To process the materail it needs to be entered into an excel spreadsheet and then entered into a sample submit log. When multiple drums are recieved each drum must be entered seperately. After the sample has been submitted the sample id must then be entered into the spreadsheet. For material that only one drum is sampled the remaining drums must be entered as they are used. The spreadsheet also contains material drums that are reclaimed from the distillation and packaging process. This makes keeping track of the drum ids prone to user error.

Usage:

Information must be writen down on a paper during the distillation process, then transfered to a second excel spreadsheet. When the distillation process is completed the sample submit must be completed and the same information must be entered. Once the process is complete the Information obtain from this process must be added to 3 additional spreadsheets. 

Decreases the time taken to obtain a drum number as well as decrease the chance that a drum number is assigned to multiple drums. 
The system would verify the information entered for a drum, such as the vendor, vendor lot number and product, then output the next drum number in sequence. 
For new vendor lots the ability to enter in the number of drums and have the system output the appropriate number of drum numbers for those products that require all drums to be sampled. For products that use a mother drum sample for the lot it will allow you to label that specific drum and keep track of how many drums are of the same lot.

Information would only need to be entered once.
The database will collect information from separate processes, it will then be available to other processes, such as the usage log, customer requirements, and the sample submit.
After a drum is input into the system during the sampling process the information is transferred to the sample submit. When an approval status is received the drum is then available to the usage log. Upon the completion of a product lot the information for the sample submit and the customer requirements (In-process data) can then be uploaded to the appropriate location.  
The process of creating this database is a time constraint. The database and the user interface must be created as well as the logic that links the two systems together.  Work on the system must be completed during down time. This project would need to be done in stages and may require more than a year. 

Software requirements - the software required to build the system is only available through IT some may require a license to use. We are trying to work with free to use software whenever possible.
