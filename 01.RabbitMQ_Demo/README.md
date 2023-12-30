# RabbitMQ Demo

## Create RabbitMQ With Docker
```bash
docker run -d --hostname myrabbit --name RabbitMQ -p 15672:15672 -p 5672:5672  rabbitmq:3-management
```

## Create Project 

```bash
dotnet new sln
```

```bash
dotnet new gitignore
```
### Sender
#### Create a Sender Project
```bash
dotnet new console --name Sender
dotnet sln 01.RabbitMQ_Demo.sln add Sender/Sender.csproj
```
#### Add RabbitMQ Package
[package](https://www.nuget.org/packages/RabbitMQ.Client)
```bash
cd Sender
dotnet add package RabbitMQ.Client --version 6.8.1
```

### Consumer
We can create muiltiple project to simulate multiple server listen to the same queue.
#### Create a Consumer1 Project
```bash
dotnet new console --name Consumer1
dotnet sln 01.RabbitMQ_Demo.sln add Consumer1/Consumer1.csproj
```
#### Create a Consumer2 Project
```bash
dotnet new console --name Consumer2
dotnet sln 01.RabbitMQ_Demo.sln add Consumer2/Consumer2.csproj
```




## Source
[Youtube](https://www.youtube.com/watch?v=bfVddTJNiAw&t=410s)