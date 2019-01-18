USE Production;
CREATE TABLE IF NOT EXISTS Orders (
  id int auto_increment primary key,
  items json,
  summa float(10,2)
);