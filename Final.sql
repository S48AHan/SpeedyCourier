create database SpeedyCouriers;
use SpeedyCouriers;

drop table DeliveryInfo;
drop table TransactionInfo;
drop table OrderInfo;
drop table userInformation;
drop table receiverInfo;
drop table DeliveryMan;

/*User table*/
select * from userInformation;

create table userInformation(
userID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
Name varchar(20),
Userpassword varchar(20),
Email varchar(25),
UserRole varchar(20),
Constraint uc_PersonID UNIQUE (Userpassword,Email)  
);

insert into userInformation values ('Saber','1234','saber@gmail.com','Admin');
insert into userInformation values ('Sean','1234','sean@gmail.com','User');
insert into userInformation values ('Samir','1234','samir@gmail.com','User');

/*Receiver Table*/
create table receiverInfo(
receiverID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
receiverName varchar(25),
receiverAddress varchar(40),
receiveDate DATE,
productType varchar(30),
productPrice int,
phoneNumber varchar(11),
Constraint uc_ReceiverPhoneNo UNIQUE (phoneNumber)
);

insert into receiverInfo values ('Raju','Malibag','2022-08-03','Frame',500,'01234567891');
insert into receiverInfo values ('Rasel','Mirpur','2022-08-04','Phone Case',200,'01234567892');
select * from receiverInfo;

/*Delivery man table*/
create table DeliveryMan(
delmanID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
delmanName varchar(20),
delmanEmail varchar(30),
delmanPass varchar(30),
delmanPhone varchar(11),
delmanAddress varchar(40),
delmanStatus varchar(15) NOT NULL,
Constraint uc_DeliveryManID UNIQUE (delmanEmail,delmanPass,delmanPhone)  
);
insert into DeliveryMan values('Raju Rastogi','raj@gmail.com','123','01234567893','Badda','Available');
insert into DeliveryMan values('Arnob','arnob@gmail.com','123','01234567891','Green Road','Busy');
select  * from DeliveryMan;

/*Order Table*/
create table OrderInfo(
orderID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
Order_userID int FOREIGN KEY REFERENCES userInformation(userID),
Order_receiverID int FOREIGN KEY REFERENCES receiverInfo(receiverID),
totalCost int,
orderStatus varchar(20) NOT NULL
); 
insert into OrderInfo values(2,1,570,'Processing');
insert into OrderInfo values(3,2,270,'Done');
select  * from OrderInfo;

/*Transaction Info*/

create table TransactionInfo(
transactionID int NOT NULL IDENTITY(101,1) PRIMARY KEY,
tranOrderID int FOREIGN KEY REFERENCES OrderInfo(orderID),
tranUserID int FOREIGN KEY REFERENCES DeliveryMan(delmanID)
); 
insert into TransactionInfo values(1,2);
insert into TransactionInfo values(2,2);
select * from TransactionInfo;

/*Delivery info*/

create table DeliveryInfo(
DeliveryID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
PayStatus varchar(15),
DeliveryStatus varchar(15),
DeltranID int FOREIGN KEY REFERENCES TransactionInfo(transactionID)
); 
insert into DeliveryInfo values('Done','Done',101);
insert into DeliveryInfo values('Done','Done',102);
select * from DeliveryInfo;