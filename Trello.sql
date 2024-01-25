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

CREATE TABLE IF NOT EXISTS User (
    username varchar (255) PRIMARY KEY,
    password TEXT
);

CREATE TABLE IF NOT EXISTS Comment (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    content TEXT (1000),
    createdAt DATETIME,
    idCard INT,
    username VARCHAR(255),
    FOREIGN KEY (idCard) REFERENCES Card(id) ON DELETE CASCADE,
    FOREIGN KEY (username) REFERENCES User(username) ON DELETE CASCADE
);


---- SQL SERVER --------
-- Drop foreign key constraints before dropping tables

-- Drop foreign key constraint in Comment table
ALTER TABLE Comment
DROP FOREIGN KEY FK__Comment__userId__68487DD7;

-- Drop foreign key constraint in List table
ALTER TABLE List
DROP FOREIGN KEY FK__List__idProject__<YourForeignKeyConstraintName>;

-- Drop foreign key constraint in Project table
ALTER TABLE Project
DROP FOREIGN KEY FK__Project__userId__<YourForeignKeyConstraintName>;

-- Drop the tables
DROP TABLE Comment;
DROP TABLE Card;
DROP TABLE List;
DROP TABLE Project;
DROP TABLE [User];

-- Création de la table User
CREATE TABLE [User] (
    id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) NOT NULL,
    passwordHash VARBINARY(MAX) NOT NULL -- Stocker le mot de passe crypté en tant que VARBINARY
);

-- Création de la table Project
CREATE TABLE Project (
    id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(50),
    description VARCHAR(50),
    createdAt DATETIME,
    userId INT, -- Ajout de la référence à l'utilisateur
    FOREIGN KEY (userId) REFERENCES [User](id) ON DELETE CASCADE
);

-- Création de la table List
CREATE TABLE List (
    id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(50),
    idProject INT,
    FOREIGN KEY (idProject) REFERENCES Project(id) ON DELETE CASCADE
);

-- Création de la table Card
CREATE TABLE Card (
    id INT PRIMARY KEY IDENTITY(1,1),
    title VARCHAR(50),
    description VARCHAR(50),
    createdAt DATETIME,
    idList INT,
    FOREIGN KEY (idList) REFERENCES List(id) ON DELETE CASCADE
);

-- Création de la table Comment
CREATE TABLE Comment (
    id INT PRIMARY KEY IDENTITY(1,1),
    content VARCHAR(50),
    createdAt DATETIME,
    idCard INT,
    userId INT, -- Ajout de la référence à l'utilisateur
    FOREIGN KEY (idCard) REFERENCES Card(id) ON DELETE CASCADE,
    FOREIGN KEY (userId) REFERENCES [User](id)
);