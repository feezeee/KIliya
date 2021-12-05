USE railway_station;

INSERT INTO `railway_station`.`access_rights`
(`access_right_name`)
VALUES
("Администратор"),
("Кассир");

INSERT INTO `railway_station`.`clients`
(`first_name`,
`last_name`,
`pass_number`,
`phone_number`)
VALUES
("Лена", "Печень", "KB1234567", "375290000000");

INSERT INTO `railway_station`.`destinations`
(`destination_name`)
VALUES
("Минск"),
("Жлобин");


INSERT INTO `railway_station`.`sit_places`
(`place_name`)
VALUES
("1"),
("2"),
("3"),
("4"),
("5"),
("6"),
("7"),
("8"),
("9");



INSERT INTO `railway_station`.`workers`
(`first_name`,
`last_name`,
`phone_number`,
`password`,
`access_right_id`)
VALUES
("Тест", "Тест", "375291234567", "1234", 1),
("Чел1", "Чел1", "37529123", "1234", 2);



INSERT INTO `railway_station`.`trains`
(`train_name`)
VALUES
("228");










