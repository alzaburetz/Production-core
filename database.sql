CREATE DATABASE IF NOT EXISTS Production;
USE Production;

CREATE TABLE IF NOT EXISTS Manufacturer(
    id INT PRIMARY KEY,
    m_name VARCHAR(100),
    Address VARCHAR(100)
);


CREATE TABLE IF NOT EXISTS Product(
    id INT PRIMARY KEY AUTO_INCREMENT,
     name VARCHAR(100),
      description VARCHAR(5000),
     amount INT,
     price FLOAT(10,2),
    m_id INT,
    FOREIGN KEY (m_id) REFERENCES Manufacturer(id)
);

CREATE TABLE IF NOT EXISTS Cart
(
  Id int primary key auto_increment,
  
  sum float(10,2),
  items_amount int,
  user_id int
);

CREATE TABLE IF NOT EXISTS Orders (
  id int auto_increment primary key,
  items json,
  summa float(10,2)
);

INSERT IGNORE INTO Manufacturer(id,m_name,Address) VALUES(23, "KOLOTUSHKA INC", "Pushkina str., Kolotushkina"),
                                                    (3, "GAIKA", "");

INSERT IGNORE INTO Product( name,  description, amount, price,m_id) VALUES("Gaika", "Eto gaika", 420, 6.9,3); 

CREATE USER IF NOT EXISTS test@localhost;
GRANT ALL ON Production.* TO test@localhost;
FLUSH PRIVILEGES;