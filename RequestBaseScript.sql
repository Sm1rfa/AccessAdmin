create database RequestBase

create table Users (
	UserId int identity(1, 1) not null primary key,
	Email nvarchar(60) not null,
	FirstName nvarchar(40) not null,
	LastName nvarchar(40) not null,
	UserPass nvarchar(40) not null
)

create table Systems (
	SystemId int identity(1, 1) not null primary key,
	SystemName nvarchar(40) not null
)

create table Roles (
	RoleId int identity(1, 1) not null primary key,
	RoleName nvarchar(40) not null
)

create table RequestStates (
	RequestId int identity(1, 1) not null primary key,
	RequestName nvarchar(40) not null,
	RequestState int not null
)

create table UserSystems (
	UserSystemId int identity(1, 1) not null,
	UserId int not null foreign key references Users(UserId),
	SystemId int not null foreign key references Systems(SystemId)
)

create table UserRoles (
	UserRoleId int identity(1, 1) not null,
	UserId int not null foreign key references Users(UserId),
	RoleId int not null foreign key references Roles(RoleId)
)

create table SystemRoles (
	SystemRoleId int identity(1, 1) not null,
	SystemId int not null foreign key references Systems(SystemId),
	RoleId int not null foreign key references Roles(RoleId)
)


-- Migration 2508171241
alter table Systems add IsDeleted bit
alter table Roles add IsDeleted bit

-- Migration 2508171719
alter table Users add IsAdmin bit

-- Migration 2608171626
insert into Users(Email, FirstName, LastName, UserPass, IsAdmin) values('admin@admin.net', 'Jens', 'Jensen', '1234', 1)
insert into Users(Email, FirstName, LastName, UserPass, IsAdmin) values('user1@user.net', 'Peter', 'Petersen', '5678', 0)
insert into Users(Email, FirstName, LastName, UserPass, IsAdmin) values('user2@user.net', 'Lars', 'Larsen', '4321', 0)

insert into Roles(RoleName, IsDeleted) values('Read', 0)
insert into Roles(RoleName, IsDeleted) values('Read/Write', 0)
insert into Roles(RoleName, IsDeleted) values('Review', 0)
insert into Roles(RoleName, IsDeleted) values('Ping system', 0)

insert into Systems(SystemName, IsDeleted) values('Finance app', 0)
insert into Systems(SystemName, IsDeleted) values('Sales app', 0)
insert into Systems(SystemName, IsDeleted) values('Marketing app', 0)
insert into Systems(SystemName, IsDeleted) values('HR app', 0)
insert into Systems(SystemName, IsDeleted) values('Finance app 2', 1)
insert into Systems(SystemName, IsDeleted) values('Internal Development app', 0)

insert into SystemRoles(SystemId, RoleId) values(1, 1)
insert into SystemRoles(SystemId, RoleId) values(1, 2)
insert into SystemRoles(SystemId, RoleId) values(2, 3)
insert into SystemRoles(SystemId, RoleId) values(2, 4)
insert into SystemRoles(SystemId, RoleId) values(3, 1)
insert into SystemRoles(SystemId, RoleId) values(3, 2)
insert into SystemRoles(SystemId, RoleId) values(3, 3)
insert into SystemRoles(SystemId, RoleId) values(3, 4)

insert into UserRoles(UserId, RoleId) values(1, 1)
insert into UserRoles(UserId, RoleId) values(1, 2)
insert into UserRoles(UserId, RoleId) values(1, 3)
insert into UserRoles(UserId, RoleId) values(1, 4)
insert into UserRoles(UserId, RoleId) values(2, 1)
insert into UserRoles(UserId, RoleId) values(2, 4)
insert into UserRoles(UserId, RoleId) values(3, 3)
insert into UserRoles(UserId, RoleId) values(3, 4)

insert into UserSystems(UserId, SystemId) values(1, 1)
insert into UserSystems(UserId, SystemId) values(1, 2)
insert into UserSystems(UserId, SystemId) values(1, 3)
insert into UserSystems(UserId, SystemId) values(1, 4)
insert into UserSystems(UserId, SystemId) values(2, 1)
insert into UserSystems(UserId, SystemId) values(2, 4)
insert into UserSystems(UserId, SystemId) values(3, 3)
insert into UserSystems(UserId, SystemId) values(3, 4)