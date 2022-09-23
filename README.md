# Rebtel Library Project

## Table of Contents
* [Architecture overview ](#architecture)
* [Services](#services)
* [Domain Driven Design Consideration](#ddd)
* [Technologies and Libraries Used](#technologies-used)
* [Services Communication](#services_communication)
* [API documentation](#documentations)
* [Screenshots](#screenshots)



#architecture
Architecture overview :
Based on the requirement document analyzing, Microservice architecture has been chosen to create multiple cloud native backend services which might communicate together based on the purpose of project definitions.
Decomposition of the whole project based on DDD approach led to 3 different main Projects including 1-Book.API, 2-Borrwo.API, 3-User.API and one aggregator project named Library. Aggregator which is responsible for communicating with other 3 mentioned backend services using Grpc technology.Also clean architecture has been applied which means for example Borrow project itself including 4 projects: 
1- Borrwo.Core, the most inner layer containing entity objects (here BorrowedBook) with no any reference to outer projects.
2- Borrow.Application ,which include the business rules and level of abstractions and it just references to the Core project.
3-Borrow.Infrastructure, which contains the lower level dependencies such as EntityFramework and business logic implementations; this approach is protecting the higher levels from changing in case of thechnologies and other details changes.
4-Borrow.API, which is the most outer layer and references the 3 mentioned projects and expose the Grpc service to be consumed by the aggregator Api.

Although, in this project there are no modification commands such as insert and update based on the requirement document, the CQRS and Mediator pattern has been applied widely to segregate the query and command which is very useful when we decide to separate the write and read databases. There are also other benefits using Mediator patterns such as making thin and more testable controllers.

##services
Services:
1- Book.API : All required data about the books will be stored and retrieve independently to other services such as name, author, publication and etc. it exposes Grpc service which is responsible to get the data from repository and return back to the caller.
2- Borrwo.API : it is responsible to store and retrieve all data about the borrowed books, for example, book id, user id, borrowed date and etc. this service is also exposes Grpc service and has its own logic implementation to creating queries in order to fetch the right data.
3- User.API : This service maintain the required data for the client of the library (Borrowers) which include information like first name, phone no, address and etc.some DDD consideration also has been applied in its entities, for example, there is an aggregator entity named User which has some small business roles (constructor) and has some value objects like address.
4- Identity.API : security is one of the most important part of the each system and there is no exception for library too!, so this service provides JWT token for authenticating users among the aggregator api calls and it is separated api which can be leveraged by the other internal services or not (I have assumption about the running environment of this project in the cloud, which internal services can be hide in the isolated network and there is no need to be protected, however it can be done simply by using this service through add Authorization headers in each grpc call).

##ddd
Domain Driven Design Consideration :
As mentioned earlier this project need to be decomposed to multiple independent services and this could be done by leveraging DDD approach. the more precession and  experience in DDD, the more undependability and less coupled services will be gained, which is the success key in the microservices and cloud native architecture.
Also some entities in this project are avoided to be anemic, although there are no complex logics in the entitles to be considered.


##technologies-used
Technologies Used :

According to the task I have applied the following technologies:
ASP.Net Core 6.0
Microsoft Sql Server
Entityframework.Core 6.0
Grpc server and client communications
Protobuff-net.Grpc for code first Grpc
MediatR 10.0
Swashbuck Swagger
Xunit Test


##services_communication
Services Communication :
Communicating among the different services is the common challenge and nature of the Microservice architecture, for example, in this project there is an aggregator project applying back end for front end pattern which is the best practice to provide back services for different front-end consumers such as mobile and web. In this service there is a need to fetch data from the other services and aggregate and expose them to the consumer. so there is a need to communicate with other services which have two different approaches including synchronous and asynchronous ways.In this project I have used Grpc communication which is one of the most speedy way in the synchronous approaches.According to the requirements, there is no need to leverage asynchronous communications, but it is very common and useful and has many advantages in comparison to the synchronous which has its own challenges like chaining requests, increasing delays and failed response rate.For example, I think the asynchronous communication can be used when the borrower returns the book to the library by publishing the event to the message bus, which the Book service can consume to update the available copies of the returned book.In this project code first approach has been used which is very clean and maintainable in comparison to proto file creation, There is a Nuget package named Protobuff-net.Grpc which makes it super easy to create the contract and share between clients and server. (By using this approach you can make your own Nuget package from your contract and share it among the whole developer teams.)In every back service project there is a project whose name ends with "Contracts", that holds the types and classes required by the Grpc services.


##documentations
API documentation :
Rich API documentations are provided by leveraging and configuring the Swagger.
All methods input names, descriptions, types and the different returned status code are described and documented.
Authorization also can be done through Swagger interface.


##screenshots
Screenshots
![screenshot1](./documents/img/1.jpg)
![screenshot2](./documents/img/2.jpg)
![screenshot3](./documents/img/3.jpg)






