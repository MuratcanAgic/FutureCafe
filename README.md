# Future Cafe
* A .NET Core MVC project, School canteen management portal.
* School classes, students, products, stocks can be managed for a school canteen. Students can buy products with their student cards.
* Student cards and product barcodes can be scanned with barcode scanner. This will ease transaction and stock processes.
## Motivation
There are big and expensive softwares for supermarket stock management, but they aren't appropriate for school canteens. And they are not for managing students and not easy to use with student id cards. Why a canteen should need to manage students? Parents may need to control what and how much their child consume.  
## Goal
With Future Cafe, we can manage students, products, stocks and transaction with ease. Reports can be created for transactions, student credit, product price history etc. Products can be banned for students, and thats because students buy products with their cards, parents can control how much they spend.
## Documentation
1- First clone the project
```
git clone https://github.com/MuratcanAgic/FutureCafe.git
```
2- We need to add our connection string. In FutureCafe.Web, there is appsettings.json file, we need to add this line:
```json
"ConnectionStrings": {"DBConStr":"Server=YourSQLServer;Initial Catalog=FutureCafeBirey;User Id=YourUserId;Password=YourPassword;TrustServerCertificate=true;"}
```
or alternatively we can add it to our User Secrets, right-click FutureCafe.Web -> Manage User Secrets, we can add flat version of string like:
```json
"ConnectionStrings:DBConStr" : "Server=YourSQLServer;Initial Catalog=FutureCafeBirey;User Id=YourUserId;Password=YourPassword;TrustServerCertificate=true;"
```
3- Then we need to apply migration, that way database will be created. In FutureCafe.DataAccess there are already migrations to apply, so we open Package Manager Console, and select FutureCafe.DataAccess as default project, then run this:
```
Update-Database
```
We may need to create our migration if we change something in configurations, so we can run this:
```
add-migration OurMigrationName
```
4- After database is created, we can run FutureCafe.Web and use default users defined in UserConfiguration.cs to login.
## Contributing
I would love your help! Contribute by forking the repo and opening pull requests. All pull requests should be submitted to the "master" branch.
