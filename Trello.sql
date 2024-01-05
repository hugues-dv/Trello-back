CREATE DATABASE IF NOT EXISTS Trello.db; 

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
    idProject INT,
    FOREIGN KEY (idProject) REFERENCES Project(id) ON DELETE CASCADE
);


CREATE TABLE IF NOT EXISTS Card (
    id INT PRIMARY KEY ,
    title varchar (50),
    description varchar (50),
    createdAt DATETIME,
    idList INT,
    FOREIGN KEY (idList) REFERENCES List(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Comment (
    id INT PRIMARY KEY,
    content varchar (50),
    createdAt DATETIME,
    idCard INT,
    user varchar (50),
    FOREIGN KEY (idCard) REFERENCES Card(id) ON DELETE CASCADE
);





