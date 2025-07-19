# Student API

This is my first ASP.NET Web API project, designed to manage student records using RESTful API principles. 
The project evolves through multiple versions, each adding new features such as filtering, posting, updating, deleting, and integrating a 3-tier architecture with database support.

---

## Features Covered (Step-by-Step)

Each version introduces a new feature or concept, explained with both server-side and client-side implementation.

---
## Get All Students
- **Server Side:** Implemented a basic GET endpoint to retrieve all student records  
- **Client Side:** Consumed the API to display all student data  

---

## Get Passed Students
- **Server Side:** Added filtering logic to return only students who passed  
- **Client Side:** Displayed only passed students in the UI  

---

## Get Average Grade
- **Server Side:** Added logic to calculate and return the average grade of all students  
- **Client Side:** Displayed the average grade dynamically

---

## Get Student by ID
- **Server Side:** Created an endpoint to retrieve a specific student by ID  
- **Client Side:** Added search functionality to show student details by ID

---

## Add New Student (POST)
- **Server Side:** Implemented a POST endpoint to add a new student to the list  
- **Client Side:** Created a form to collect and submit student data

---

## Delete Student
- **Server Side:** Added DELETE endpoint to remove a student by ID  
- **Client Side:** Integrated delete functionality with confirmation prompts

---

## Update Student
- **Server Side:** Developed a PUT endpoint to update student data  
- **Client Side:** Created an editable form to update student records

---

## DTO (Data Transfer Object)
- Implemented DTOs to transfer only required data between client and server.
- Benefits:
  - Prevent overfetching of data  
  - Improve response size and performance  
  - Add a layer of abstraction between internal models and exposed data  

---

## Student API – 3 Tier Architecture with Database
Refactored the solution to follow a 3-tier architecture:
- **Presentation Layer** – UI / client  
- **Business Logic Layer** – core processing logic  
- **Data Access Layer** – interacts with database using EF Core  

### Implemented Features:
- Get All Students  
- Get Passed Students  
- Get Average Grade  
- Get Student by ID  
- Add New Student  
- Update Student  
- Delete Student  

---

## Upload & Retrieve Images
Implemented basic image handling using Web API:
- Upload image to server folder  
- Retrieve image from server  


---

## Technologies Used

- ASP.NET Core Web API
- C#
- Entity Framework Core
- LINQ
- JSON
- REST
- Visual Studio
- Postman (for testing)
