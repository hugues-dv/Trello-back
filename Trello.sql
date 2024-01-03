CREATE DATABASE IF NOT EXISTS Trello_db; 

-- SQLite
CREATE TABLE IF NOT EXISTS Projet (
id INT PRIMARY KEY,
Nom varchar (50),
Description varchar (50),
DateCreation DATETIME
); 

CREATE TABLE IF NOT EXISTS Liste (
    Id INTEGER PRIMARY KEY,
    Nom varchar (50),
    IdProjet INT,
    FOREIGN KEY (IdProjet) REFERENCES Projet(Id)
);

CREATE TABLE IF NOT EXISTS Carte (
    Id INT PRIMARY KEY ,
    Titre varchar (50),
    Description varchar (50),
    DateCreation DATETIME,
    IdListe INT,
    FOREIGN KEY (IdListe) REFERENCES Liste(Id)
);

CREATE TABLE IF NOT EXISTS Commentaire (
    Id INT PRIMARY KEY,
    Contenu varchar (50),
    DateCreation DATETIME,
    IdCarte INT,
    Utilisateur varchar (50),
    FOREIGN KEY (IdCarte) REFERENCES Carte(Id)
);





