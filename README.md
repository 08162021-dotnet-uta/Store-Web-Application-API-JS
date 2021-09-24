# Store-Web-Application-API/JS
## Description
This is a store web application API that users can use to buy products from different stores. 

## Features
* Login/Register as a customer
* View past orders of the customer
* Select a store, each of which has different products
* View products, which have a description and a price
* Add products to cart

To-do
* Purchase products to cart which will create a new order.
* View more detailed orders of the customer
* View past orders of the stores

## Technologies used
* C#
* ADO.NET Entity Framework
* Testing Process / SDLC
* HTML5
* CSS3
* Defect Logging
* Microsoft SQL Server
* SQL
* XML
* JavaScript

## Getting Started
Install Git if you have not done so already. https://git-scm.com/downloads
After installing git, make a new folder and go to the folder.
Right click anywhere and select "Git Bash Here"
Find the URL of the project by clicking on the green Code button and copying the URL.
Run the command `git clone [URL you copied]`. Paste using the Shift + Insert key commands.
Install Visual Studio 2019 Community Edition https://visualstudio.microsoft.com/downloads/

## Usage
Run the scripts in the databaseScripts folder in this order: p0_DDL_Query.sql, p0_DML_Query.sql, and p1_Query.sql.
Using Visual Studio 2019, open the file `p1.StoreApplication.sln` and run the code. The webpage will show up.
Click on Register to add yourself as a customer.
Select a store to enter into a store.
Add products to cart by clicking on the "Add to cart" button.
View the cart, and make a purchase by clicking on the "Purchase" button.

## License
This project uses the MIT License.