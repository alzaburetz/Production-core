CREATE DATABASE IF NOT EXISTS Production;
USE Production;

CREATE TABLE IF NOT EXISTS Manufacturer(
    id INT PRIMARY KEY,
    m_name VARCHAR(100),
    m_address VARCHAR(100)
);


CREATE TABLE IF NOT EXISTS Product(
    id INT PRIMARY KEY AUTO_INCREMENT,
    p_name VARCHAR(100),
    p_desc VARCHAR(5000),
    p_amount INT,
    p_price FLOAT(10,2),
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

INSERT IGNORE INTO Manufacturer(id,m_name,m_address) VALUES(23, "KOLOTUSHKA INC", "Pushkina str., Kolotushkina"),
                                                    (3, "GAIKA", "");

INSERT IGNORE INTO Product(p_name,p_desc,p_amount,p_price,m_id) VALUES("Gaika", "Eto gaika", 420, 6.9,3); 

CREATE USER IF NOT EXISTS test@localhost;
GRANT ALL ON Production.* TO test@localhost;
FLUSH PRIVILEGES;