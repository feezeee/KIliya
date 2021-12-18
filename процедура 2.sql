CREATE DEFINER=`root`@`localhost` PROCEDURE `GetWorkers`()
BEGIN
	SELECT `w`.`worker_id`, `w`.`access_right_id`, `w`.`first_name`, `w`.`last_name`, `w`.`password`, `w`.`phone_number`, `a`.`access_right_name`, `t`.`ticket_id`, `t`.`all_price`, `t`.`client_id`, `t`.`train_van_sit_id`
      FROM `workers` AS `w`
      INNER JOIN `access_rights` AS `a` ON `w`.`access_right_id` = `a`.`access_right_id`
      LEFT JOIN `tickets` AS `t` ON `w`.`worker_id` = `t`.`worker_id`
      ORDER BY `w`.`worker_id`;
END