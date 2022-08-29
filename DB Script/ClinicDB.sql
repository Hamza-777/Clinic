create database ClinicDB

use ClinicDB

create table Users (
username varchar(10) not null primary key,
firstname varchar(20) not null,
lastname varchar(20) not null,
password varchar(25) not null
)

insert into Users 
values ('kevin123', 'Kevin', 'Smith', 'kevin@123'),
('johnplayer', 'John', 'Player', 'john@plyr')

create table Doctors (
doctor_id int identity(2342, 1) not null primary key,
firstname varchar(20) not null,
lastname varchar(20) not null,
sex varchar(10) not null,
specialization varchar(30) not null,
visiting_hours varchar(60) not null
)

insert into Doctors
values ('Manoj', 'Khanna', 'M', 'General', 'From 10:00,To 17:00'),
('Kshitij', 'Tiwari', 'M', 'Internal Medicine', 'From 10:00,To 17:00'),
('Amir', 'Pathan', 'M', 'Pediatrics', 'From 10:00,To 17:00'),
('Ritwik', 'Gangan', 'M', 'Orthopedics', 'From 10:00,To 17:00'),
('Jayesh', 'Agarwal', 'M', 'Ophthalmology', 'From 10:00,To 17:00'),
('Sweety', 'Khanna', 'F', 'General', 'From 17:00,To 22:00'),
('Geeta', 'Modi', 'F', 'Internal Medicine', 'From 17:00,To 22:00'),
('Prachi', 'Desai', 'F', 'Pediatrics', 'From 17:00,To 22:00'),
('Sana', 'Khan', 'F', 'Orthopedics', 'From 17:00,To 22:00'),
('Mumtaz', 'Memon', 'F', 'Ophthalmology', 'From 17:00,To 22:00')

create table Patients (
patient_id int identity(14243, 1) not null primary key,
firstname varchar(20) not null,
lastname varchar(20) not null,
sex varchar(10),
age int not null,
dob date not null
)

create table Appointments (
appointment_id int identity(1, 1) not null primary key,
visit_date date not null,
appointment_time varchar(10) not null,
doctor_id int not null foreign key references Doctors(doctor_id),
patient_id int not null foreign key references Patients(patient_id)
)

select * from Doctors
select * from Patients
select * from Appointments