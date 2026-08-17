use companyDB;

create table Employee(
Emp_Id int identity(1,1) primary key,
Emp_Name varchar(100) not null,
Emp_Age int not null,
Emp_Salary decimal(18,2) not null

);


create table Department (
 Dpt_Id int identity(1,1) primary key,
 Emp_Id int not null,
 Dpt_Name nvarchar(100) not null,

 constraint FK_Department_Employee foreign key (Emp_Id)
 references Employee(Emp_Id) on delete cascade on update cascade

)