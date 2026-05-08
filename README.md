*Dynamic Entity Framework Application

**How to Run
**Prerequisites
- Visual Studio 2019/2022
- .Net 9 SDK
- SQL Server LocalDB (included with visual studio)

**Steps
- Clone the repo
- Open 'DEF.sln' in visual studio
- Update the "ConnectionStrings" in 'appsettings.json' if needed:
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DEF;Trusted_Connection=True;"
   }
- Open the package manager and run: "Update-Database" (package manager located under View -> Other Windows -> Package Manager Console)
- Press 'F5' to run the application
- If not brought to the home screen navigate to "https://localhost:7024"

**Data Storage Approach
By using JSON strings in a column both for the client and job the following are accomplished:

-No additional tables are required and the data stays with it's respective object. This makes reads and writes straightforward

-Adding a new field to a form requires no new database migrations

-Since I mapped it to a dictionary the domain stays clean and allows for easy serialization for the JSON data
