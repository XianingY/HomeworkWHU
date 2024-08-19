CREATE USER 'newUser'@'localhost' IDENTIFIED BY 'password';
GRANT SELECT ON *.* TO 'newUser'@'localhost';
FLUSH PRIVILEGES;