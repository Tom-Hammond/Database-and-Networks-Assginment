CREATE DATABASE IF NOT EXISTS  users;
USE users;
  
CREATE TABLE IF NOT EXISTS `userdetails` (
  `LoginID` varchar(50) NOT NULL,
  `UserID`  varchar(50) UNIQUE,
  `Surname` longtext,
  `Forname` longtext,
  `Title` longtext,
  `Position` longtext,
  `Phone` longtext,
  `Email` longtext,
  `Location` longtext,
 PRIMARY KEY (LoginID)
 );


 
 INSERT INTO  users.userDetails (LoginID, UserID, Surname, Forname, Title, Position, Phone, Email, Location)
Values ('554233', '231546', 'Hammond', 'Tom', 'Mr', 'Student', '4407717212226', 't.j.hammond@hull.ac.uk', 'Lab'),
 ('554477', '2122550', 'Marly', 'Bob', 'Doctor', 'Staff', '4407753116726', 'bbmarly-2011@hull.ac.uk', 'Home'),
 ('514287', '2158740', 'King', 'Ash', 'King', 'Royal', '4409758196621', 'kash21@hull.ac.uk', 'Lab'),
 ('144675', '2158745', 'Long', 'Maisy', 'Mrs', 'Student', '4422312847912', 'MaisyL991@hull.ac.uk', 'RB-201'),
 ('244123', '3564321', 'Heigns', 'Harry', 'Master', 'Child', '4425478964862', 'Harry00281@hull.ac.uk', 'HR-201'),
('254862', '1452351', 'Bobby', 'Ken', 'Mr', 'Staff', '4412587459632', 'KenBobby@hull.ac.uk', 'LL-670'),
 ('542545', '6652145', 'Poppy', 'Adrian', 'Mrs', 'Student', '4465485259854', 'Popps212@hull.ac.uk', 'HB-708') ,
('458412', '1512562', 'Bishop', 'James', 'Mr', 'Student', '4422154851114', 'Bisheeop2009@winchester.ac.uk', 'GR-09'),
('568410', '0551005', 'Hammond', 'Dale', 'Mr', 'Parent', '2215800880802', 'HammondDale@hotmail.co.uk', 'AA-01'),
 ('263432', '5511511', 'Fin', 'Kelop', 'Mr', 'Doctor', '2215800880802', 'kelop234422@gmail.com', 'SD-098'),
 ('cssbct', '123456', 'Tompsett', 'Brian C', 'Eur Ing', 'Lecturer of Computer Science', '44 1482 46 5222', 'B.C.Tompsett@hull.ac.uk@gmail.com', 'RB-336');


