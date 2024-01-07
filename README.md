## Hello and welcome to my API project.

This program is a simple web API built using REST architecture where the goal is to showcase and store data within a local database themed around a School.

The functions include and aren't limited to the following;

* Store basic information such as First and Last names of students, but also their phone number.
* Store interests of each student. These interests typically has a title and a description.
* Store multiple interests per student that can be shared among other students. Creating a link.

  To use this API to it's full potential i would recommend downloading and installing an API development program such as [Postman](https://www.postman.com//). [Docker](https://www.docker.com/), [Swagger](https://swagger.io/), or my personal choice: [Insomnia](https://insomnia.rest/)


### The following commands are used in this project to pull and push data to and from the Database.

**POST** is used to push and store data into the database using the DTOs.

**GET** is used to pull data from the database in accordance with the ViewModels.

### **GET COMMANDS: **

/interests

/interests/{search?}

/people

/people/{search?}

/people/{personId}/interests

/people/{personId}/interests/links

### **PUSH COMMANDS:**

/interests

/people

/people/{personId}/interests/{interestId}

/people/{personId}/interests/{interestId}/links/





### The following ER Diagram and UML Diagram are meant to demonstrate how the program works in theory.

![Alt text](https://github.com/Manhattaa/MiniAPI/blob/master/MiniAPI/ER%20Diagram%20Fady%20Hatta.png "ER_Diagram")

![Alt text](https://github.com/Manhattaa/MiniAPI/blob/master/MiniAPI/UML%20Diagram%20Fady%20Hatta.png "UML Diagram")
