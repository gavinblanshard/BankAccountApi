# BankAccountApi

This project provides a simple API for managing bank accounts and transactions, based on the requirements listed below. It is written in .Net 8.0 using Visual Studio 2022.

## Requirements

1. Support for different account types with the possibility of future expansion. For now, Current and Savings accounts are the only required types.
2. Ability to create, edit, delete, and retrieve accounts.
3. Ability to debit and credit accounts.
4. Ability to transfer funds between accounts.
5. Ability to freeze (disable) an account so that no transactions can occur on the account.
6. Written in C# (.NET 6 and onwards).
7. Data storage is up to you. It is recommended to use in-memory storage, but you can use any database if you prefer.

## Notes

The API is based around a database that has three entities/tables: AccountTypes, Accounts, and Transactions. It uses the .NET Core In-Memory database for simplicity, but this would not be recommended for anything more than a simple demo such as this. However, it does make the project self-contained and easy to run.

The data operations are separated out using the Repository pattern, and data is manipulated using Linq and Entity Framework (EF). A simple service has been added to initialise the database with the required Account Types (Current, and Savings), but it would be straightforward to add another controller to manage these.

As this is a demo project, and the requirements state that it is for internal use, no security (eg. OAuth) has been added.

No unit testing or end-to-end testing has been included as I believe the focus of the task is on the implementation, and I did not have adequate spare time to add any testing.

Please contact me if you have any questions.
