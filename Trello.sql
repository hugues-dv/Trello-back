-- CREATE DATABASE IF NOT EXISTS Trello.db; 

-- SQLite
CREATE TABLE IF NOT EXISTS Project (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name varchar (50),
    description TEXT (500),
    createdAt DATETIME
); 

CREATE TABLE IF NOT EXISTS List (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name varchar (50),
    idProject INT,
    FOREIGN KEY (idProject) REFERENCES Project(id) ON DELETE CASCADE
);


CREATE TABLE IF NOT EXISTS Card (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    title varchar (255),
    description TEXT (500),
    createdAt DATETIME,
    idList INT,
    FOREIGN KEY (idList) REFERENCES List(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Comment (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    content TEXT (1000),
    createdAt DATETIME,
    idCard INT,
    idUser INT,
    FOREIGN KEY (idCard) REFERENCES Card(id) ON DELETE CASCADE,
    FOREIGN KEY (idUser) REFERENCES User(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS User (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    username varchar (50),
    password TEXT
);