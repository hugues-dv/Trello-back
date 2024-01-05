CREATE DATABASE IF NOT EXISTS Trello_db; 

-- SQLite
CREATE TABLE IF NOT EXISTS Projects (
    id INT PRIMARY KEY,
    name varchar (50),
    description varchar (50),
    createdAt DATETIME
); 

CREATE TABLE IF NOT EXISTS lists (
    id INT PRIMARY KEY,
    name varchar (50),
    idProject INT,
    FOREIGN KEY (idProject) REFERENCES Projects(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Cards (
    id INT PRIMARY KEY ,
    title varchar (50),
    description varchar (50),
    createdAt DATETIME,
    idList INT,
    FOREIGN KEY (IdList) REFERENCES lists(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Comments (
    id INT PRIMARY KEY,
    content varchar (50),
    createdAt DATETIME,
    idCard INT,
    user varchar (50),
    FOREIGN KEY (IdCard) REFERENCES Cards(Id) ON DELETE CASCADE
);





