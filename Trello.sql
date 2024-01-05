CREATE DATABASE IF NOT EXISTS Trello_db; 

-- SQLite
CREATE TABLE IF NOT EXISTS Project (
    id INT PRIMARY KEY,
    name varchar (50),
    description varchar (50),
    createdAt DATETIME
); 

CREATE TABLE IF NOT EXISTS List (
    id INT PRIMARY KEY,
    name varchar (50),
);

CREATE TABLE IF NOT EXISTS ProjectListAssociation (
    projectId INT,
    listId INT,
    PRIMARY KEY (projectId, listId),
    FOREIGN KEY (projectId) REFERENCES Project(id) ON DELETE CASCADE,
    FOREIGN KEY (listId) REFERENCES List(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Card (
    id INT PRIMARY KEY ,
    title varchar (50),
    description varchar (50),
    createdAt DATETIME,
    idList INT,
    FOREIGN KEY (IdList) REFERENCES List(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Comment (
    id INT PRIMARY KEY,
    content varchar (50),
    createdAt DATETIME,
    idCard INT,
    user varchar (50),
    FOREIGN KEY (IdCard) REFERENCES Card(Id) ON DELETE CASCADE
);





