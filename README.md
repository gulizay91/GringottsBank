# GringottsBank
In this backend development assignment, we would like you to create a simple online banking
API app. Gringotts Bank is a bank that has an online branch for wizards to do some account
transactions. Gringotts Bank is known for its goblin-made, secure, and consistent accounting
structure so account transaction consistency is the first priority for their operations.


## Tech
[.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)
[NServiceBus](https://particular.net/nservicebus)
[RabbitMQ](https://www.rabbitmq.com/)

KeyWords: 
  * NServicebus, RabbitMQ
  * Restful api, Swagger, FluentValidation, Automapper
  * Cqrs Mediatr
  * Domain-Driven Design, Repository Pattern and UnitOfWork
  * ORM: EF Core
  * SQL
  * XUnit : UnitTest, IntegrationTest


Each endpoint is written according to restful api best practice on API. And each endpoint has a description on swagger (api documentation)
CQRS using the mediator pattern for splitting commands (saves) and queries (reads) into different models.
NServiceBus and Rabbitmq used for Microservices communication.
Data concurrency is important when update account balance on db. so handled with optimistic lock. 

### Startup Projects
* Bank.API
* Bank.Core.WorkerServiceHost

## Docker
if you are using VS, you can run with docker
if you're not, open a terminal in your root directory which is 'GringottsBank/src' and run the following commands:
	
```cmd
cd src
docker-compose build
docker-compose up
```

if you want to change ports on docker-compose.yaml
```cmd
docker-compose down
docker-compose build
docker-compose up
```
Once done, run the Docker image and map the port to whatever you wish on
your host.

You can check data on sql management studio

```sh
Server Name: localhost,1445
Sql Server Authentication
    Login: sa
    Password: p@ssw0rd
```


### 

### SQL scripts for local environment
You can find SQL scripts in etc/scripts folder
If you need migration, u can use package manager console on VS. be sure Default Project selected Bank.Core.Infrastructure
```sh
Add-Migration Initial -StartupProject src\Bank.API
Remove-Migration -StartupProject src\Bank.API
Update-database Initial -StartupProject src\Bank.API
```

### MUST-HAVE REQUIREMENTS:

| Endpoints | Url | Http Verb |
| ------ | ------ | ------ |
| 1. New Customer Endpoint | api/customers | POST
| 2. Will persist brief information about customers | api/customers/{id} | GET
| 3. New Account Endpoint. | api/accounts | POST
| 4. Will persist new account information and current balance of the account. | api/accounts/{id} | GET
| 5. New Account Transaction Endpoint for adding and withdrawing money. This action needs to update the balance of the account. | api/accounts | PUT
| 6. List transactions of an account | api/accounts/{id}/transactions | GET
| 7. Will query all accounts of the customer | api/customers/{id}/accounts | GET
| 8. View the account details | api/accounts/{id} | GET
| 9. View all transactions of the customer between a time period | none | GET

| Requirements | Status | Description |
| ------ | ------ | ------ |
| 10. Validations - Please be sure your system is error-proof. | done | fluentvalidation and Guards
| 11. Responses - Please define success and error response models and use them | done | swagger options
| 12. Authentication - Please secure endpoints (for ex. bearer token) | none | i didnt implement


## License
[MIT](https://choosealicense.com/licenses/mit/)
